using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BossIntro2 : MonoBehaviour
{
    [SerializeField] private float _startDelay;
    [SerializeField] private Transform _six, _seven;
    [SerializeField] private float _timeToDrop;
    [SerializeField] private float _YPos;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startDelay);
        
        _six.DOMoveY(_YPos, _timeToDrop).SetEase(Ease.OutBack);
        _seven.DOMoveY(_YPos, _timeToDrop).SetEase(Ease.OutBack);
        
        yield return new WaitForSeconds(_startDelay);
        
        GameManager.Instance.OnFightBegin?.Invoke();
    }
}
