using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private float _pauseDuration;
    [SerializeField] private float _timeRestoreDuration;
    [SerializeField] private float _timeRestoreDeltaTime;
    
    private DamageFlash _damageFlash;
    protected override void Start()
    {
        base.Start();
        _damageFlash = GetComponent<DamageFlash>();
    }

    public override void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        StopAllCoroutines();
        StartCoroutine(UpdateUICoroutine());

        if (_currentHealth <= 0)
        {
            Death();
        }
        _damageFlash.TriggerMaterialChange();
        StartCoroutine(PauseTime());
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
}
