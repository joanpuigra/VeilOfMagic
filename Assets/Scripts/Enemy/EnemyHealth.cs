using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 30;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private Color hitColor = Color.green;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int amount, Vector2 sourcePosition, float knockbackForce = 2f)
    {
        health -= amount;
        StartCoroutine(HitFlash());

        Vector2 knockbackDir = ((Vector2)transform.position - sourcePosition).normalized;

        if (TryGetComponent(out Rigidbody2D rb))
        {
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator HitFlash()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}