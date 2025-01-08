using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashWindow = .2f;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ResetDashCoolDown();
        if (stateMachine.lastState == player.wallSlideState)
        {
            player.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.CancelFloat();
    }

    public override void Update()
    {
        base.Update();

        player.Float();
        player.SetVelocity(player.dashSpeed * player.facingDir, 0);

        // End of dash.
        if (stateTimer > dashWindow)
        {
            if (player.IsGrounded())
            {
                TransitionTo(player.idleState);
            }
            else if (player.IsWallHit()) 
            { 
                TransitionTo(player.wallSlideState);
            }
            else
            {
                TransitionTo(player.airState);
            }
        }
    }
}
