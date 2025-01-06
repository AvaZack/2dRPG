using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
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
        if (player.isGrounded) 
        {
            if (xInput == 0)
                TransitionTo(player.idleState);
            else 
                TransitionTo(player.moveState);
            return;
        }

        if (!player.isWallHit)
        {
            TransitionTo(player.airState);
            return;
        }

        player.SetVelocityY(player.GetVelocityY() * 0.95f);

        if (Input.GetKey(KeyCode.Space))
        {
            player.SetVelocity(player.facingDir * player.moveSpeed * -1, player.jumpForce);
            Debug.Log(player.GetVelocityX());
            Debug.Log(player.GetVelocityY());
        }
    }
}
