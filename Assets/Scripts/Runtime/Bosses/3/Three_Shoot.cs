using UnityEngine;

public class Three_Shoot : StateMachineBehaviour
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Three>().BeginShoot();
    }
}
