using UnityEngine;

public class Player : Character
{

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; } 
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    #endregion
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    #region CoolDowns
    public float dashCoolDown { get; private set; } = .5f;
    public float dashCoolDownTimer { get; private set; } = 0;
    #endregion

    [Header("PlayerMoveInfo")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float defaultGravity;
    [SerializeField] public float maxJumpTime;
    [SerializeField] public float maxAirJumpTime;

    private void Awake()
    {
        // Init components first.
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Air");
        jumpState = new PlayerJumpState(this, stateMachine, "Air");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Air");

        stateMachine.Init(idleState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        CancelFloat();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        stateMachine.curState.Update();
        Timer();
    }

    public Rigidbody2D GetRb()
    {
        return rb;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetVelocity(float vx, float vy)
    {
        SetVelocityX(vx);
        SetVelocityY(vy);
    }

    public void SetVelocityX(float vx)
    {
        rb.linearVelocityX = vx;
    } 

    public void SetVelocityY(float vy)
    {
        rb.linearVelocityY = vy;
    }

    public float GetVelocityX()
    {
        return rb.linearVelocityX;
    }

    public float GetVelocityY()
    {
        return rb.linearVelocityY;
    }

    private void Timer()
    {
        dashCoolDownTimer += Time.deltaTime;
    }

    public void ResetDashCoolDown()
    {
        dashCoolDownTimer = 0;
    }

    public void Float() {
        Debug.Log("Clear gravity scale.");
        rb.gravityScale = 0;
    }

    public void CancelFloat()
    {
        Debug.Log("Set gravity scale to =" + defaultGravity);
        rb.gravityScale = defaultGravity;
    }

}
