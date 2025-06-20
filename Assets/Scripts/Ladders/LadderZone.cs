using UnityEngine;

public class LadderZone : MonoBehaviour
{
    private static readonly int IsClimbing = Animator.StringToHash("isClimbing");
    public GameObject climbHintUI;
    private bool hasShownClimbHint;
    
    private void Awake()
    {
        if (climbHintUI != null)
        {
            climbHintUI.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerClimb>().canClimb = true;
            
            if (!hasShownClimbHint && climbHintUI != null)
            {
                climbHintUI.SetActive(true);
                hasShownClimbHint = true;
            }        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            climbHintUI.SetActive(true);

            PlayerClimb player = collision.GetComponent<PlayerClimb>();
            player.canClimb = false;
            player.isClimbing = false;
            player.animator.SetBool(IsClimbing, false);
            
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1.5f;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }
}