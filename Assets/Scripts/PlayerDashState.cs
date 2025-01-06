using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashTimer;
    private float dashWin = .2f;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dashTimer = 0;
        player.ResetDashCoolDown();
        Debug.Log("Where did we come from to dashState:" + stateMachine.lastState.stateName);
        if (stateMachine.lastState == player.wallSlideState)
        {
            player.Flip();
            Debug.Log("Reverse dash direction from facing direction.");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        dashTimer += Time.deltaTime;
        //if (xInput != 0)
        //{
        //    player.SetVelocity(player.dashSpeed * xInput, 0);
        //}
        //else
        //{
            // If from wall slide state. Dash direction is reversed.
            player.SetVelocity(player.dashSpeed * player.facingDir, 0);

        //}
        if (dashTimer > dashWin)
        {
            if (player.isGrounded)
            {
                TransitionTo(player.idleState);
            }
            else
            {
                TransitionTo(player.airState);
            }
        }
    }
}
