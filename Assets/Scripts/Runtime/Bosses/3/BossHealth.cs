using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
    [SerializeField] protected float _healthBarFadeDuration;
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnFightBegin.AddListener(()=> _slider.GetComponent<CanvasGroup>().DOFade(1,_healthBarFadeDuration));
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
