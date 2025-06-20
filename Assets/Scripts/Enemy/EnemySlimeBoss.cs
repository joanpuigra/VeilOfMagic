using UnityEngine;

public class EnemySlimeBoss : MonoBehaviour
{
    public float toxicGasDamage = 1f;
    public float toxicGasRange = 2f;
    public GameObject toxicGasPrefab;
    private bool isActive;

    private void Start()
    {
        // isActive = GetComponent<EnemyAttack>().isAttacking;
    }


    private void ToxicGas()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, toxicGasRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // hitCollider.GetComponent<Player>().TakeDamage(toxicGasDamage++);
            }
        }
        Instantiate(toxicGasPrefab, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (!isActive)
        {
            ToxicGas();
        }
    }
}
