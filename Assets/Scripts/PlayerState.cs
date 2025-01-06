using UnityEngine;

public class PlayerState
{

    public PlayerStateMachine stateMachine { get; private set; }
    public Player player { get; private set; }
    public string stateName { get; private set; }

    public float xInput { get; private set; }
    public float yInput { get; private set; }


    public PlayerState(Player player, PlayerStateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        Debug.Log(stateName + " Enter");
        player.animator.SetBool(stateName, true);
    }

    public virtual void Update()
    {
        Debug.Log(stateName + " Update");
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && player.dashCoolDownTimer > player.dashCoolDown)
        {
            TransitionTo(player.dashState);
        }
    }

    public virtual void Exit()
    {
        Debug.Log(stateName + " Exit");
        player.animator.SetBool(stateName, false);
    }

    protected void TransitionTo(PlayerState newState)
    {
        stateMachine.TransitionTo(newState);
    }

    // Imp
    protected virtual void DashAction()
    {

    }

}
