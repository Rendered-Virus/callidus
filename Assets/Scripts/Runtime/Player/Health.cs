using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] protected Slider _slider;
    [SerializeField] private float _healthBarUpdateDeltaTime;
    [SerializeField] private float _healthBarUpdateRate;

    protected int _currentHealth;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
        _slider.maxValue = _maxHealth;
        _slider.value = _currentHealth;
        
        
    }
    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        StopAllCoroutines();
        StartCoroutine(UpdateUICoroutine());

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        
    }

    protected IEnumerator UpdateUICoroutine()
    {
        while (_slider.value > _currentHealth)
        {
            _slider.value -= _healthBarUpdateRate * _healthBarUpdateDeltaTime;
            yield return new WaitForSeconds(_healthBarUpdateDeltaTime);
        }
    }
}
