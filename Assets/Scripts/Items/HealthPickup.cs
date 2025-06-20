using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healthAmount = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
       
        if (player == null || !(player.health < player.maxHealth)) return;
        
        player.health = Mathf.Min(player.health + healthAmount, player.maxHealth);
        Destroy(gameObject);
    }
}