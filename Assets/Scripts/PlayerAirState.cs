using UnityEngine;

public class PlayerAirState : PlayerCanControlState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
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
        player.animator.SetFloat("yVelocity", player.GetVelocityY());
        if (player.isGrounded)
        {
            TransitionTo(player.idleState);
            return;
        }

        // Only transition to wall slide when has xInput
        if (player.isWallHit && xInput == player.facingDir)
        {
            TransitionTo(player.wallSlideState);
            return;
        }
    }
}
