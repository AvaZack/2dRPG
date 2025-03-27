using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerController player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGrounded()) 
        {
            if (xInput == 0)
                TransitionTo(player.idleState);
            else 
                TransitionTo(player.moveState);
            return;
        }

        if (!player.IsWallHit())
        {
            TransitionTo(player.jumpState);
            return;
        }

        player.SetVelocityY(player.GetVelocityY() * 0.95f);

        if (Input.GetKeyDown(KeyPad.jump))
        {
            TransitionTo(player.wallJumpState);
        }
    }
}
