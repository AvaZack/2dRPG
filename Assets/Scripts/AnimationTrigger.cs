using UnityEngine;

public class AnimationTrigger: MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    public void AnimationEnd()
    {
        player.stateMachine.curState.AnimationEnd();
    }

}
