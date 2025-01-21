using UnityEngine;

public class EnemyWorm : MonoBehaviour
{
    public float acidDamage = 10f;
    public float acidRange = 5f;
    public GameObject acidPrefab;
    private bool isActive;

    private void Start()
    {
        isActive = GetComponent<EnemyAttack>().isAttacking;
    }


    public void AcidAttack()
    {
        GameObject acid = Instantiate(acidPrefab, transform.position, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, acidRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                hitCollider.GetComponent<Player>().TakeDamage(acidDamage);
            }
        }
        Destroy(acid, 2f);
    }

    private void Update()
    {
        if (!isActive)
        {
            AcidAttack();
        }
    }
}
