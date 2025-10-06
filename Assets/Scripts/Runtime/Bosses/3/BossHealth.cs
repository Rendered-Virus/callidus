using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _healthBarUpdateDeltaTime;
    [SerializeField] private float _healthBarUpdateRate;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _slider.maxValue = _maxHealth;
        _slider.value = _currentHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        StopAllCoroutines();
        StartCoroutine(UpdateUICoroutine());

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        print("haha dead");
    }

    private IEnumerator UpdateUICoroutine()
    {
        while (_slider.value > _currentHealth)
        {
            _slider.value -= _healthBarUpdateRate * _healthBarUpdateDeltaTime;
            yield return new WaitForSeconds(_healthBarUpdateDeltaTime);
        }
    }
}
