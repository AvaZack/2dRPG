using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public float jumpTime { get; private set; } = 0;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(player.jumpForce);
        jumpTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        jumpTime += Time.deltaTime;
        if (Input.GetKey(KeyPad.jump))
        {
            player.SetVelocityY(player.jumpForce);
        }
        if (Input.GetKeyUp(KeyPad.jump) || jumpTime > player.maxJumpTime)
        {
            TransitionTo(player.airState);
        }


    }
}
