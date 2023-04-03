using UnityEngine;

public class AnimationAutoDestroy : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject.transform.root.gameObject, stateInfo.length);
    }
}
