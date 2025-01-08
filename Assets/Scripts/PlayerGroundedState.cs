using UnityEngine;

public class PlayerGroundedState : PlayerCanControlState
{
    public bool isEverGrounded {  get; private set; }
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Clear velocity when grounded.
        player.SetVelocityX(0);
        isEverGrounded = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyPad.jump)) // Check grounded again cause..
        {
            TransitionTo(player.jumpState);
            return;
        }

        if (Input.GetKeyDown(KeyPad.attack))
        {
            TransitionTo(player.attackState);
            return;
        }

        // Detect effects only if we have really grounded.
        if (!player.IsGrounded() && isEverGrounded) 
        {
            TransitionTo(player.airState);
            return;
        }
        if (!player.IsGrounded())
        {
            isEverGrounded = true;
        }
    }
}
