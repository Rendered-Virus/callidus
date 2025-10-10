using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class One : MonoBehaviour
{
   [SerializeField] private float _teleportRangeX;
   [SerializeField] private GameObject _teleportationParticles;
   [SerializeField] private float _timeDisappeared;
   [SerializeField] private float _fadeDuration;
   [SerializeField] private Transform _target;
   [SerializeField] private float _slashRange;
   [SerializeField] private float _slashDuration;
   private bool _canDamage;
   [SerializeField] private int _damage = 10000;
   [SerializeField] private float _standDuration;
   private BossHealth _bossHealth;
   private void Start()
   {
       _bossHealth = GetComponent<BossHealth>();
       _bossHealth.OnDeath.AddListener(StopAllCoroutines);
       GameManager.Instance.OnFightBegin.AddListener(TeleportAway);
   }

   private void TeleportAway()
   {
       _bossHealth.SetInvaulnreble(true);
       Instantiate(_teleportationParticles,transform);
        transform.DOScale(0,_fadeDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            StartCoroutine(TeleportIn());
        });
   }

   private IEnumerator TeleportIn()
   {
       yield return new WaitForSeconds(_timeDisappeared);
       
       transform.position = new Vector3(GetPosX(),transform.position.y);
       
       var lookingRight = _target.position.x > transform.position.x;
       transform.rotation = Quaternion.Euler(0,lookingRight ? 0 : 180,0);
       
       transform.DOScale(1, _fadeDuration).SetEase(Ease.OutBack);
       
       yield return new WaitForSeconds(_fadeDuration);

       var slashTarget = transform.position.x + (lookingRight ? _slashRange : -_slashRange);

       _canDamage = true;
       
       transform.DOMoveX(slashTarget,_slashDuration).SetEase(Ease.OutBack);
       yield return new WaitForSeconds(_slashDuration);
       
       _canDamage = false;
       _bossHealth.SetInvaulnreble(false);
       
       yield return new WaitForSeconds(_standDuration);
       TeleportAway();
   }

   private float GetPosX()
   {
       if(_target.position.x > 10)
           return _target.position.x - _teleportRangeX;
       
       if(_target.position.x < -10)
        return _target.position.x + _teleportRangeX;
       
       return _target.position.x +  _teleportRangeX * (Random.value > 0.5f ? 1 : -1);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
       if (!_canDamage) return;
       if(other.TryGetComponent<PlayerHealth>(out var playerHealth))
           playerHealth.TakeDamage(_damage);
   }
}
