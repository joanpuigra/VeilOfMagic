using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    [Header("Parameters")] 
    public float maxHealth = 100f;
    public int maxAmmo = 30;
    
    public float health = 100f;
    public int ammo = 30;
    
    public bool HasAmmo => ammo > 0;

    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float damageFlashTime = 0.2f;

    [Header("Weapon")]
    [SerializeField] private GameObject _pistolPrefabRight;
    [SerializeField] private GameObject _pistolPrefabLeft;
    private float moveValue;
    private Vector2 _movementInput;
    private PlayerMovement _playerMovement;
    private Animator animator;
    private float move;
    private PlayerShoot _playerShoot;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Color originalColor;
    
    public bool isDead { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalColor = spriteRenderer.color;
    }
    
    private void Start()
    {
        _pistolPrefabLeft.SetActive(false);
        _pistolPrefabRight.SetActive(false);
        animator = GetComponent<Animator>();
        _playerShoot = GetComponent<PlayerShoot>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        move = _playerMovement.moveValue;
        
        switch (move)
        {
            case > 0f:
                _pistolPrefabRight.SetActive(true);
                _pistolPrefabLeft.SetActive(false);
                break;
            case < 0f:
                _pistolPrefabRight.SetActive(false);
                _pistolPrefabLeft.SetActive(true);
                break;
            default:
                _pistolPrefabLeft.SetActive(false);
                _pistolPrefabRight.SetActive(false);
                break;
        }
    }
    
    public void UseAmmo()
    {
        if (ammo > 0)
            ammo--;
    }
    
    public void TakeDamage(int damage, Vector2 sourcePosition)
    {
        if (isDead) return;
        
        health -= damage;
        if (health <= 0f && !isDead)
        {
            isDead = true;
            _playerMovement.isDead = isDead;
            Debug.Log("Player has died.");
            OnDeath();
        }
        
        Vector2 knockbackDir = (rb.position - sourcePosition).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        
        StartCoroutine(DamageFeedback());
    }
    
    private IEnumerator DamageFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageFlashTime);
        spriteRenderer.color = originalColor;
    }

    private void OnDeath()
    {
        Debug.Log("Player has died.");
        
        _pistolPrefabLeft.SetActive(false);
        _pistolPrefabRight.SetActive(false);
        
        _playerShoot.enabled = false;

        animator.SetTrigger(Death);
        animator.SetBool(IsDead, true);
        animator.SetFloat(Speed, 1f);
    }
    
    public void Respawn()
    {
        isDead = false;
        health = maxHealth;

        transform.position = CheckpointManager.LastCheckpointPosition;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;

        _pistolPrefabLeft.SetActive(true);
        _pistolPrefabRight.SetActive(true);
        Debug.Log("Player respawned");
    }
}
