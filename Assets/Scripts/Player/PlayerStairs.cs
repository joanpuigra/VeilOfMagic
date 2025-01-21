using UnityEngine;

public class PlayerStairs : MonoBehaviour
{
    public float speed = 5f;
    public Transform topPoint;
    public Transform bottomPoint;
    private bool isClimbing = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isClimbing)
        {
            StartClimbing();
        }
        else if (Input.GetKeyDown(KeyCode.S) && isClimbing)
        {
            StopClimbing();
        }

        if (isClimbing)
        {
            Climb();
        }
    }

    void StartClimbing()
    {
        isClimbing = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = 1;
    }

    void Climb()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 targetPosition = transform.position + new Vector3(0, verticalInput * speed * Time.deltaTime, 0);
        targetPosition.y = Mathf.Clamp(targetPosition.y, bottomPoint.position.y, topPoint.position.y);
        transform.position = targetPosition;
    }
}
