using UnityEngine;

public class PlayerGroundedState : PlayerCanControlState
{ 
    public bool isEverGrounded {  get; private set; }
    public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Clear velocity when grounded.
        player.SetVelocityX(0);
        isEverGrounded = false;
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

        if (Input.GetKeyDown(KeyPad.jump) || !player.IsGrounded() && isEverGrounded) // Check grounded again cause..
        {
            TransitionTo(player.jumpState);
            return;
        }

        if (Input.GetKeyDown(KeyPad.attack))
        {
            TransitionTo(player.attackState);
            return;
        }

        if (!player.IsGrounded())
        {
            isEverGrounded = true;
        }
    }
}
