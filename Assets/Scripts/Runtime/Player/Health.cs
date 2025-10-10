using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected Slider _slider;
    [SerializeField] protected float _healthBarUpdateDeltaTime;
    [SerializeField] protected float _healthBarUpdateRate;
    public bool Invaulnreble;
    protected int _currentHealth;
    protected DamageFlash _damageFlash;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
        _slider.maxValue = _maxHealth;
        _slider.value = _currentHealth;
        _damageFlash = GetComponent<DamageFlash>();
    }
    public virtual void TakeDamage(int damage)
    {
        if(Invaulnreble)
            return;
        
        _currentHealth -= damage;
        StopAllCoroutines();
        StartCoroutine(UpdateUICoroutine());

        if (_currentHealth <= 0)
        {
            Death();
        }
        _damageFlash.TriggerMaterialChange();

    }

    protected virtual void Death()
    {
        
    }

    protected virtual IEnumerator UpdateUICoroutine()
    {
        while (_slider.value > _currentHealth)
        {
            _slider.value -= _healthBarUpdateRate * _healthBarUpdateDeltaTime;
            yield return new WaitForSeconds(_healthBarUpdateDeltaTime);
        }
    }
    public void SetInvaulnreble(bool invaulnreble) => Invaulnreble = invaulnreble;
}
