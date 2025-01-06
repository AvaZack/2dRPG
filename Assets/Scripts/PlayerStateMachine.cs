using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState curState { get; private set; }
    public PlayerState lastState { get; private set; }
    public bool isInited = false;

    public void Init(PlayerState state)
    {
        if (!isInited) 
        {
            this.curState = state;
            this.curState.Enter();
        } else
        {
            Debug.Log("already inited");
            return;
        }

    }

    public void TransitionTo(PlayerState newState)
    {
        this.curState.Exit();
        this.lastState = curState;
        this.curState = newState;
        this.curState.Enter();
    }
}
