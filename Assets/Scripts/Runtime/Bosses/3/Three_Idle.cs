using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Three_Idle : StateMachineBehaviour
{
    [SerializeField] private float _maxSquishScaleY;
    [SerializeField] private float _timePeriod;
    private Tween _squishTween;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!GameManager.Instance.FightBegin)
        {
            GameManager.Instance.OnFightBegin.AddListener(() =>
                animator.GetComponent<Three>().EnterIdle());
        }
        else 
            animator.GetComponent<Three>().EnterIdle();
        _squishTween = animator.transform.DOScale(_maxSquishScaleY, _timePeriod).SetLoops(-1, LoopType.Yoyo);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _squishTween.Kill();
    }
}
