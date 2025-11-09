using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class SharedHealth : BossHealth
{
    private SharedHealthBar _sharedHealthBar;

    private void Awake()
    {
        if (_slider.TryGetComponent<SharedHealthBar>(out _sharedHealthBar))
        {
            //nothing
        }
        else
            _sharedHealthBar = _slider.AddComponent<SharedHealthBar>();
        _sharedHealthBar.MaxHealth += _maxHealth;
    }

    protected override void Start()
    {
        _damageFlash = GetComponent<DamageFlash>();
        _currentHealth = _maxHealth;
        _sharedHealthBar.CurrentHealth = _sharedHealthBar.MaxHealth;
        _slider.maxValue = _sharedHealthBar.MaxHealth;
        _slider.value = _slider.maxValue;
        
        GameManager.Instance.OnFightBegin.AddListener(()=> _slider.GetComponent<CanvasGroup>().DOFade(1,_healthBarFadeDuration));

    }

    public override void TakeDamage(int damage)
    {
        if(Invaulnreble) return;
        
        _sharedHealthBar.CurrentHealth -= damage;
        _currentHealth -= damage;
        
        _sharedHealthBar.UpdateUI(_healthBarUpdateRate, _healthBarUpdateDeltaTime, _slider);

        if (_currentHealth <= 0)
        {
            if (_sharedHealthBar.CurrentHealth <= 0)
                SuperDeath();
            
            Death();
        }
        _damageFlash.TriggerMaterialChange();

    }
    private void SuperDeath()
    {
        GameManager.Instance.OnFightWin.Invoke();
    }
}
