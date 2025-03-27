using UnityEngine;

public class Character : MonoBehaviour
{

    public int facingDir { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;

    [Header("MoveInfo")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    [Header("CollisionInfo")]
    [SerializeField] public Transform groundChecker;
    [SerializeField] public float groundCheckDist;
    [SerializeField] public LayerMask groundCheckLayer;
    [SerializeField] public Transform wallChecker;
    [SerializeField] public float wallCheckDist;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
    }

    protected virtual void OnDrawGizmos()
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


    public bool IsGrounded()
    {
        return Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckDist, groundCheckLayer); ;
    }

    public bool IsWallHit()
    {
        return Physics2D.Raycast(wallChecker.position, Vector2.right, wallCheckDist * facingDir, groundCheckLayer);
    }

}
