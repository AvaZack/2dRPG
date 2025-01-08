using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    private float stateTimer;
    private float stateDuration = .1f;

    public override void Enter()
    {
        base.Enter();
        player.Flip();
        player.SetVelocityY(player.jumpForce);
        stateTimer = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocityX(player.facingDir * player.jumpForce);
        stateTimer += Time.deltaTime;
        if (stateTimer > stateDuration)
        {
            TransitionTo(player.airState);
        }
    }
}
