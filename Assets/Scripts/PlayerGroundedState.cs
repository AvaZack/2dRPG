using UnityEngine;

public class PlayerGroundedState : PlayerCanControlState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Clear velocity when grounded.
        player.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded) // Check grounded again cause..
        {
            player.SetVelocityY(player.jumpForce);
            TransitionTo(player.airState);
        }

        if (!player.isGrounded) 
        { 
            TransitionTo(player.airState);
        }
    }
}
