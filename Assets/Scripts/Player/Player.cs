using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    public float health = 100f;
    public float ammo = 30f;

    [Header("Weapon")]
    [SerializeField] private GameObject _pistolPrefabRight;
    [SerializeField] private GameObject _pistolPrefabLeft;
    private GameObject _weaponActive;

    private void Update()
    {
        CheckAmmo();
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

    public void TakeDamage(float damage)
    {
        health -= damage;

        // Destroy player if health is less than or equal to 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
