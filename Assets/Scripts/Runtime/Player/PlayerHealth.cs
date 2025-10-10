using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private float _pauseDuration;
    [SerializeField] private float _timeRestoreDuration;
    [SerializeField] private float _timeRestoreDeltaTime;

    [SerializeField] private float _invincibilityDuration;
    
    private bool _invincibility;

    protected override void Start()
    {
        base.Start();
        _damageFlash = GetComponent<DamageFlash>();
    }

    public override void TakeDamage(int damage)
    {
        if(_invincibility) return;
                    
        _currentHealth -= damage;
        StopAllCoroutines();
        StartCoroutine(UpdateUICoroutine());

        if (_currentHealth <= 0)
        {
            Death();
        }
        _damageFlash.TriggerMaterialChange();
        
        _invincibility = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerInv");
        Invoke(nameof(DisableInvincibility), _invincibilityDuration);
        
        StartCoroutine(PauseTime());
    }

    private void DisableInvincibility()
    {
        _invincibility = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    
    private IEnumerator PauseTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(_pauseDuration);
        
        while (Time.timeScale < 1)
        {
            Time.timeScale += _timeRestoreDeltaTime / _timeRestoreDuration;
            yield return new WaitForSecondsRealtime(_timeRestoreDeltaTime);
        }
        Time.timeScale = 1;
    }

    protected override void Death()
    {
        GameManager.Instance.PlayerDeath();
    }
}
