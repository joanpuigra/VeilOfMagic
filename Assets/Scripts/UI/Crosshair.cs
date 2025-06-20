using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color enemyColor = Color.red;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Color playerColor = Color.green;
    [SerializeField] private LayerMask playerLayer;

    private RectTransform rectTransform;
    private Image crosshairImage;
    private Camera mainCamera;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        crosshairImage = GetComponent<Image>();
        mainCamera = Camera.main;

        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform,
            Input.mousePosition,
            null,
            out mousePos
        );

        rectTransform.localPosition = mousePos;

        UpdateCrosshairColor();
    }

    private void UpdateCrosshairColor()
    {
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitEnemy = Physics2D.Raycast(worldPos, Vector2.zero, 0f, enemyLayer);
        RaycastHit2D hitPlayer = Physics2D.Raycast(worldPos, Vector2.zero, 0f, playerLayer);

        crosshairImage.color = hitEnemy.collider != null ? enemyColor : normalColor;
        crosshairImage.color = hitPlayer.collider != null ? playerColor : normalColor;
    }
}