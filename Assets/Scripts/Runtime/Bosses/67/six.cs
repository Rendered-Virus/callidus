using System;
using Unity.VisualScripting;
using UnityEngine;

public class six : MonoBehaviour
{
    [SerializeField] private Vector2 _startVelocity;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private int _hitDamage;
    private Rigidbody2D _rigidbody;
    private bool _canRotate;
    private BossHealth _bossHealth;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossHealth = GetComponent<BossHealth>();
        _bossHealth.OnDeath.AddListener(()=> Destroy(gameObject));
        GameManager.Instance.OnFightBegin.AddListener(ShootSelf);
    }

    private void ShootSelf()
    {
        _rigidbody.linearVelocity = _startVelocity;
        _canRotate = true;
    }

    private void Update()
    {
        if (_canRotate)
        {
            var rot = transform.eulerAngles;
            rot.z += _rotateSpeed  * Time.deltaTime;
            transform.eulerAngles = rot;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.TryGetComponent<PlayerHealth>(out var playerHealth))
            playerHealth.TakeDamage(_hitDamage);
    }
}
