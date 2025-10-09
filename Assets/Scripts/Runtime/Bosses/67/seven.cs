using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class seven : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private Transform _knives;
    [SerializeField] private float _knivesRotateSpeed;
    [SerializeField] private Vector2 _knockBackForce;
    [SerializeField] private Vector2 _walkRange;
    [SerializeField] private float _yPos;
    [SerializeField] private float _timeWalking;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _numberOfShots = 7;
    [SerializeField] private float _timeSpentShooting;
    [SerializeField] private Bullet _knifePrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _throwStrength;
    [SerializeField] private float _rotationSpeed;

    private Health _health;
    private float _target;
    private bool _walking;
    private float _timeRemaining;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();    
        
        GameManager.Instance.OnFightBegin.AddListener(()=>
        {
            _timeRemaining = _timeWalking;
            StartCoroutine(nameof(BeginWalk));
        });
    }
    private IEnumerator BeginWalk()
    {
        _health.SetInvaulnreble(true);
        _knives.gameObject.SetActive(true);
        _walking = true;

        _target=  MathF.Abs(transform.position.x - _walkRange.x) > MathF.Abs(transform.position.x - _walkRange.y) ? _walkRange.x : _walkRange.y;
        var x = _target > transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(x,transform.localScale.y,transform.localScale.z);
        
        var target = new Vector3(_target, _yPos);
        
        _rigidbody.linearVelocity = (target - transform.position).normalized * _walkSpeed;

        while (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            yield return null;
        }
        
        _health.SetInvaulnreble(false);
        _knives.gameObject.SetActive(false);
        _walking = false;
     
        StartCoroutine(BeginShooting());
    }

    private void Update()
    {
        var rot = _knives.eulerAngles;
        rot.z += _knivesRotateSpeed * Time.deltaTime;
        _knives.eulerAngles = rot;

        if (_walking && Math.Abs(_target - transform.position.x) < 1f)
        {
            StopCoroutine(nameof(BeginWalk));
            StartCoroutine(nameof(BeginWalk));
        }
    }

    private IEnumerator BeginShooting()
    {
        print("SHOOT");
        _rigidbody.linearVelocity = Vector2.zero;
        var right = true;
        
        for (int i = 0; i < _numberOfShots; i++)
        {
            yield return new WaitForSeconds(_timeBetweenShots);
            Instantiate(_knifePrefab,transform.position,Quaternion.identity).
                Embark(Vector2.right * ((right ? 1 : -1) * _throwStrength),_damage);
            right = !right;
        }
        yield return new WaitForSeconds(_timeSpentShooting);
        _timeRemaining = _timeWalking;
        StartCoroutine(nameof(BeginWalk));
    }
}
