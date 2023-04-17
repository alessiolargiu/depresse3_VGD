using UnityEngine;
 
public class RandomAnima : StateMachineBehaviour
{
    bool _hasRandomized;
 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_hasRandomized)
        {
            animator.Play(stateInfo.fullPathHash, layerIndex, Random.Range(-0f, 1f));
            _hasRandomized = true;
        }
    }
}
 