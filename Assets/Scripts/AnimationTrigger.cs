using UnityEngine;

public class AnimationTrigger: MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }
    public void AnimationEnd()
    {
        player.stateMachine.curState.AnimationEnd();
    }

}
