using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private static readonly int SlimeAttack = Animator.StringToHash("Attack");
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackDelay = 0.5f;
    
    private Animator animator;
    private EnemyPatrol enemyPatrol;
    private bool isAttacking;
    private bool canAttack = true;
    private Coroutine attackCoroutine;
    private Player playerRef;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void Update()
    {
        if (playerRef == null)
            playerRef = FindObjectOfType<Player>();

        if (playerRef != null && playerRef.isDead)
        {
            StopAttack();
            enabled = false;
            Debug.Log("EnemyAttack disabled");
            return;
        }
        
        if (playerRef != null && playerRef.isDead && isAttacking)
        {
            StopAttack();
            StopAllCoroutines();
            attackCoroutine = null;
        }
        
        if (IsPlayerInRange())
        {
            if (!isAttacking) StartAttack();
            FollowPlayer();
        }
        else if (isAttacking) StopAttack();
    }

    private void StopAttack()
    {
        isAttacking = false;
        enemyPatrol.enabled = true;
        StopAllCoroutines();
        Debug.Log("Stopping attack");
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            playerPosition.position,
            movementSpeed * Time.deltaTime
        );
    }

    private void StartAttack()
    {
        isAttacking = true;
        enemyPatrol.enabled = false;
        animator.SetTrigger(SlimeAttack);
    }

    private bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, playerPosition.position) <= detectionRange;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || attackCoroutine != null || !canAttack) return;

        Player player = collision.GetComponent<Player>();
        if (player == null || player.isDead) return;

        attackCoroutine = StartCoroutine(AttackRoutine(player));
    }

    private IEnumerator AttackRoutine(Player player)
    {
        canAttack = false;

        animator.SetTrigger(SlimeAttack);
        yield return new WaitForSeconds(attackDelay);

        if (player != null && !player.isDead)
        {
            player.TakeDamage(attackDamage, transform.position);
            Debug.Log($"Player attacked: {attackDamage}");
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        attackCoroutine = null;
    }
}
