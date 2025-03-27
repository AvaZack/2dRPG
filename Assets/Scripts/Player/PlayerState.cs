using UnityEngine;

public class PlayerState
{

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerController player { get; private set; }
    public string stateName { get; private set; }

    public float xInput { get; private set; }
    public float yInput { get; private set; }

    public bool isAnimationEnd {  get; private set; }
    public float stateTimer {  get; private set; }


    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        //Debug.Log(stateName + " Enter from " + stateMachine.lastState.stateName);
        player.animator.SetBool(stateName, true);
        isAnimationEnd = false;
        stateTimer = 0;
    }

    public virtual void Update()
    {
        //Debug.Log(stateName + " Update");
        stateTimer += Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyPad.dash) && player.dashCoolDownTimer > player.dashCoolDown)
        {
            TransitionTo(player.dashState);
        }
    }

    public virtual void Exit()
    {
        //Debug.Log(stateName + " Exit");
        //Debug.Log("state last =" + stateTimer + "s");
        player.animator.SetBool(stateName, false);
    }

    protected void TransitionTo(PlayerState newState)
    {
        stateMachine.TransitionTo(newState);
    }

    public void AnimationEnd()
    {
        isAnimationEnd = true;
    }
}
