using UnityEngine;

public class PlayerJumpState : PlayerCanControlState
{
    public bool isEverInTheAir { get; private set; }
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyPad.jump))
        {
            player.SetVelocityY(player.jumpForce);
        }
        if (Input.GetKeyUp(KeyPad.jump))
        {
            player.SetVelocityY(0);
        }
        LimitMaxFallSpeed();

        player.animator.SetFloat("yVelocity", player.GetVelocityY());

        // Only transition to wall slide when has xInput in the air.
        if (player.IsWallHit() && xInput == player.facingDir)
        {
            TransitionTo(player.wallSlideState);
            return;
        }

        // For air jump
        if (Input.GetKeyDown(KeyPad.jump) && player.hasDoubleJump)
        {
            // Air jump;
            player.hasDoubleJump = false;
            TransitionTo(player.jumpState);
        }

        // Detect effects only if we have really in the air.
        if (player.IsGrounded() && isEverInTheAir)
        {
            TransitionTo(player.idleState);
            return;
        }
        if (!player.IsGrounded())
        {
            isEverInTheAir = true;
        }
    }

    private void LimitMaxFallSpeed()
    {
        if (player.GetVelocityY() < player.maxFallSpeed)
        {
            player.SetVelocityY(player.maxFallSpeed);
        }
    }
}
