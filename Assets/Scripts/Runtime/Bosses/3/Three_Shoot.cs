using UnityEngine;

public class Three_Shoot : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Three>().BeginShoot();
    }
}
