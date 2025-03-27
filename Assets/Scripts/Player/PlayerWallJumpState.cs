using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerController player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    private float stateDuration = .1f;

    public override void Enter()
    {
        base.Enter();
        // Because wall slide sprite is flipped.
        player.Flip();
        player.SetVelocityY(player.jumpForce);
        player.hasDoubleJump = true;
        player.hasDash = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocityX(player.facingDir * player.jumpForce);
        player.SetVelocityY(player.jumpForce);
        if (stateTimer > stateDuration)
        {
            TransitionTo(player.jumpState);
        }
    }
}
