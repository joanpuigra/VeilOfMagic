using System.Collections;
using UnityEngine;

public class LadderZone : MonoBehaviour
{
    private static readonly int IsClimbing = Animator.StringToHash("isClimbing");
    public GameObject climbHintUI;
    
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
            PlayerClimb playerClimb = collision.GetComponent<PlayerClimb>();
            playerClimb.canClimb = true;
            playerClimb.ladderZone = this;
            
            if (!playerClimb.hasShownClimbHint && climbHintUI != null)
            {
                climbHintUI.SetActive(true);
                playerClimb.hasShownClimbHint = true;
                StartCoroutine(FinishTutorial());
                climbHintUI.SetActive(false);
            }        
        }
    }

    private IEnumerator FinishTutorial()
    {
        yield return new WaitForSeconds(5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerClimb playerClimb = collision.GetComponent<PlayerClimb>();
            playerClimb.canClimb = false;
            playerClimb.isClimbing = false;
            playerClimb.ladderZone = null;
            playerClimb.animator.SetBool(IsClimbing, false);
            
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1.5f;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }
}