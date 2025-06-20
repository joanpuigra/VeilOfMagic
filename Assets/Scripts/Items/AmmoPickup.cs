using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
        
        if (player == null || !(player.ammo < player.maxAmmo)) return;
        
        player.ammo = Mathf.Min(player.ammo + ammoAmount, player.maxAmmo);
        Destroy(gameObject);
    }
}