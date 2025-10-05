using System;
using System.Collections;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    private Rigidbody2D _rigidbody;
    private float _speed;
    private float _updateTime;
    private PlayerShoot _sender;
    private float _maxTime;
    private bool _canReturn;

    public void Embark(float speed, float updateTime, Vector3 target,float maxTime, PlayerShoot sender)
    {
        _speed = speed;
        _updateTime = updateTime;
        _sender = sender;
        _maxTime = maxTime;
        
        var dir =  (target- transform.position).normalized;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.linearVelocity = dir * _speed;

        StartCoroutine(UpdateCoroutine(speed, updateTime, target));
    }

    private IEnumerator UpdateCoroutine(float speed, float updateTime, Vector3 target)
    {
        yield return new WaitForSeconds(_maxTime);
        _canReturn = true;
        
        while (enabled)
        {
            yield return new WaitForSeconds(updateTime);
            if (_sender != null)
            {
                var dir =  (_sender.transform.position + Vector3.up * 2 - transform.position).normalized;
                _rigidbody.linearVelocity = dir * speed;
            }
            else
                yield break;
        }

    }

    private void FixedUpdate()
    {
        var rot = transform.localEulerAngles;
        rot.z -= _rotateSpeed * Time.fixedDeltaTime;
        transform.localEulerAngles = rot;
        
        if (!_canReturn) return;
        
        if (TouchPlayer())
        {
            StopAllCoroutines();
            _sender.ReturnSwords();
            Destroy(gameObject);
        }
    }

    private bool TouchPlayer()
    {
        return Vector2.Distance(transform.position, _sender.transform.position) < 2.5f;
    }
}
