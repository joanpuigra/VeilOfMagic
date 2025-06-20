using UnityEngine;

public class FloatSpinGlow : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 180f;

    [Header("Flotation")]
    [SerializeField] private float floatAmplitude = 0.5f;
    [SerializeField] private float floatFrequency = 1f;
    private Vector3 startPosition;

    [Header("Color Pulse")]
    [SerializeField] private Color colorA = Color.cyan;
    [SerializeField] private Color colorB = Color.white;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float minAlpha = 0.5f;
    [SerializeField] private float maxAlpha = 1f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Rotate like a coin (Z)
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // Floating movement
        float offsetY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, startPosition.y + offsetY, startPosition.z);

        // Glowing pulse effect
        float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        Color pulsedColor = Color.Lerp(colorA, colorB, t);
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
        pulsedColor.a = alpha;
        spriteRenderer.color = pulsedColor;
    }
}