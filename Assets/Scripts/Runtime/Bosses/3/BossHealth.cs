using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
    [SerializeField] private float _healthBarFadeDuration;
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnFightBegin.AddListener(()=> _slider.GetComponent<CanvasGroup>().DOFade(1,_healthBarFadeDuration));
    }
}
