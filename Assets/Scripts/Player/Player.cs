using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100f;

    public float ammo = 30f;

    private void Update()
    {
        CheckAmmo();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        // Destroy player if health is less than or equal to 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CheckAmmo()
    {
        // Check if player has ammo
        if (ammo <= 0)
        {
            if (ammo <= 0) return;
        }
        ammo -= 1f;
    }
}
