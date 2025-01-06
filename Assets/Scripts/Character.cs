using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isGrounded { get; private set; }
    public bool isWallHit { get; private set; }

    public int facingDir { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;

    [Header("MoveInfo")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    [Header("CollisionInfo")]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckDist;
    [SerializeField] private LayerMask groundCheckLayer;
    [SerializeField] private Transform wallChecker;
    [SerializeField] private float wallCheckDist;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        CollisionCheck();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundChecker.position, new Vector3(groundChecker.position.x, groundChecker.position.y - groundCheckDist));
        Gizmos.DrawLine(wallChecker.position, new Vector3(wallChecker.position.x + wallCheckDist, wallChecker.position.y));
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDir *= -1;
        facingRight = !facingRight;
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckDist, groundCheckLayer);
        isWallHit = Physics2D.Raycast(wallChecker.position, Vector2.right, wallCheckDist * facingDir, groundCheckLayer);
    }
}
