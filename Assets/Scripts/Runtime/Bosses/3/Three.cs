using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using DG.Tweening;

public class Three : MonoBehaviour
{
   [SerializeField] private Bullet _bulletPrefab;
   [SerializeField] private Transform _shootPoint;
   [SerializeField] private int _numberOfShots;
   [SerializeField] private Vector2 _teleportRadius;
   [SerializeField] private Vector2 _teleportRangeX;
   [SerializeField] private float _bulletSpeed;
   [SerializeField] private float _timeBetweenShots;
   [SerializeField] private float _idleTime;
   [SerializeField] private float _switchTime;
   public Transform target;
   private Rigidbody2D _rigidbody;
   private Animator _animator;

   private void Start()
   {
      _animator = GetComponent<Animator>();
      _rigidbody = GetComponent<Rigidbody2D>();
   }

   public void BeginShoot()
   {
      StartCoroutine(ShootCoroutine());
   }

   private IEnumerator ShootCoroutine()
   {
      _rigidbody.bodyType = RigidbodyType2D.Kinematic;
      for (int i = 0; i < _numberOfShots; i++)
      {
         var pos = target.position + new Vector3(Random.insideUnitCircle.normalized.x * _teleportRadius.x,Random.value * _teleportRadius.y + 3); 
         pos.x = Mathf.Clamp(pos.x, _teleportRangeX.x, _teleportRangeX.y);
         transform.DOMove(pos,_switchTime).SetEase(Ease.OutBack).OnComplete(() =>
         {
            var dir = (target.position - transform.position).normalized;
         
            transform.right = dir;

            Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation).Embark(dir * _bulletSpeed);
         });
         yield return new WaitForSeconds(_timeBetweenShots);
         
      }
      
      transform.position = new Vector3(Random.Range(_teleportRangeX.x, _teleportRangeX.y),Random.value * _teleportRadius.y);
      transform.right = target.position.x > transform.position.x ? Vector3.right : Vector3.left;
      _rigidbody.bodyType = RigidbodyType2D.Dynamic;
      
      yield return new  WaitForSeconds(1f);
      
      _animator.CrossFade("Idle",0);
   }

   public void EnterIdle()
   {
      Invoke(nameof(EndIdle),_idleTime);
   }

   private void EndIdle()
   {
      _animator.CrossFade("Shoot",0);
   }
}
