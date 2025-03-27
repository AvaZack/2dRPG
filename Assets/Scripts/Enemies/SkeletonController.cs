using System.Collections;
using UnityEngine;

public class SkeletonController : Character
{

    private Rigidbody2D rb;
    private Animator animator;
    private SkeletonState state;
    [Header("EnemyThings")]
    [SerializeField] float attackRange;
    [SerializeField] float flipDelay;
    [SerializeField] float wayDetectDisc;
    [SerializeField] float attackCoolDown;
    [SerializeField] LayerMask playerLayer;

    [Header("Health")]
    [SerializeField] public int maxHealth;

    [Header("Combat")]
    [SerializeField] public int attack;
    [SerializeField] public float attackCooldown;
    public float attackTimer;

    Transform attackTarget;
    bool isWaiting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        state = SkeletonState.Idle;
        isWaiting = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Debug.Log("skeletonstate=" + state);
        Debug.Log("wallhit=" + IsWallHit());
        Debug.Log("grounded=" + IsGrounded());
        switch (state)
        {
            case SkeletonState.Idle:
                if (IsGrounded() && !isWaiting)
                {
                    state = SkeletonState.Walk;
                    animator.SetBool("walk", true);
                }
                break;
            case SkeletonState.Walk:
                rb.linearVelocityX = moveSpeed * facingDir;
                if (IsWayOut() || IsWallHit())
                {
                    StartCoroutine(WaitAndTurnAround());
                }
                if (IsPlayerInRange())
                {
                    faceToPlayer();
                    rb.linearVelocityX = 0;
                    animator.SetBool("attack", true);
                    state = SkeletonState.Attack;
                }
                break;
            case SkeletonState.Attack:
                if (!IsPlayerInRange())
                {
                    state = SkeletonState.Walk;
                    animator.SetBool("attack", false);
                }
                break;
            default:
                break;
        }
    }

    enum SkeletonState
    {
        Idle,
        Walk,
        Attack,
        Hit,
        Death
    }

    private IEnumerator WaitAndTurnAround()
    {
        isWaiting = true;
        Flip();
        state = SkeletonState.Idle;
        animator.SetBool("walk", false);
        yield return new WaitForSeconds(flipDelay);
        isWaiting = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawLine(new Vector2(groundChecker.position.x + wayDetectDisc * facingDir, groundChecker.position.y), new Vector2(groundChecker.position.x + wayDetectDisc * facingDir, groundChecker.position.y - groundCheckDist));
    }

    private bool IsWayOut()
    {
        return !Physics2D.Raycast(new Vector2(groundChecker.position.x + wayDetectDisc * facingDir, groundChecker.position.y), Vector2.down, groundCheckDist, groundCheckLayer);
    }

    bool IsPlayerInRange()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        Debug.Log("colliders=" + collider2Ds.Length);
        foreach (Collider2D collider in collider2Ds)
        {
            bool isInRange = collider.gameObject.CompareTag("Player");
            Debug.Log("player in range=" + isInRange);
            if (isInRange) attackTarget = collider.gameObject.transform;
            else attackTarget = null;
            return isInRange;
        }
        return false;
    }

    void faceToPlayer()
    {
        if (attackTarget != null)
        {
            if (transform.position.x > attackTarget.position.x && facingRight || transform.position.x < attackTarget.position.x && !facingRight)
            {
                Flip();
            }
        }
    }

}
