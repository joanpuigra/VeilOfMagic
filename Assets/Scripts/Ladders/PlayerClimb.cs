using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerClimb : MonoBehaviour
{
    private static readonly int IsClimbing = Animator.StringToHash("isClimbing");
    [SerializeField] private float climbSpeed = 4f;
    public bool canClimb;

    private Rigidbody2D rb;
    private float verticalInput;
    public bool isClimbing;

    public Animator animator;
    public LadderZone ladderZone;
    public bool hasShownClimbHint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hasShownClimbHint = false;
    }

    private void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        isClimbing = canClimb && verticalInput != 0f;
        animator.SetBool(IsClimbing, isClimbing);
    }


    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, verticalInput * climbSpeed);
        }
        else if (canClimb)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.gravityScale = 1.5f;
        }
    }
}