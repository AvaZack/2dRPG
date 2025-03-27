using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int combo { get; private set; } = 0;
    public int maxCombo { get; private set; } = 2;
    private float lastAttackTime;
    private float comboWindow = 1f;
    public PlayerAttackState(PlayerController player, PlayerStateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
        if (Time.time - lastAttackTime > comboWindow || combo > maxCombo) 
        {
            Debug.Log("Out of combo window or reach MaxCombo.");
            combo = 0;
        }
        player.animator.SetInteger("Combo", combo);
    }

    public override void Exit()
    {
        base.Exit();
        combo++;
        lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("combo=" +  combo);
        if (isAnimationEnd)
        {
            TransitionTo(player.idleState);
        }
    }

}
