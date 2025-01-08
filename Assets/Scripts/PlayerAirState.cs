using UnityEngine;

public class PlayerAirState : PlayerCanControlState
{

    public bool isEverInTheAir {  get; private set; }
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isEverInTheAir = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.animator.SetFloat("yVelocity", player.GetVelocityY());

        // Only transition to wall slide when has xInput in the air.
        if (player.IsWallHit() && xInput == player.facingDir)
        {
            TransitionTo(player.wallSlideState);
            return;
        }

        // For air jump
        if (Input.GetKeyDown(KeyPad.jump)) 
        {
            // Air jump;
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
}
