using UnityEngine;

public class PlayerCanControlState : PlayerState
{
    public PlayerCanControlState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
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
        FlipControl();
        player.SetVelocityX(player.moveSpeed * xInput);

    }

    private void FlipControl()
    {
        if (xInput < 0 && player.facingRight || xInput > 0 && !player.facingRight)
        {
            player.Flip();
        }
    }
}
