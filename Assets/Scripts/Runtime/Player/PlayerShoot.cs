using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private SwordProjectile _projectilePrefab;
    [SerializeField] private float _projectileShootSpeed;
    [SerializeField] private float _projectileUpdateTime;
    [SerializeField] private float _maxProjectileTime;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _swords;
    
    private PlayerMovement _playerMovement;
    private bool _emptyHand;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!CanAttack()) return;
        
        if (Input.GetMouseButton(0))
        {
            _emptyHand = true;
            _swords.SetActive(false);
            Shoot();
        }
    }

    private void Shoot()
    {
        var rot = _playerMovement.IsFacingRight() ? 0 : 180; 
        var pro = Instantiate(_projectilePrefab,_shootPoint.position,_shootPoint.rotation * Quaternion.Euler(0, rot, 0));
        var target = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        pro.Embark(_projectileShootSpeed, _projectileUpdateTime,target, _maxProjectileTime,this);
    }
    private bool CanAttack()
    {
        if (!_playerMovement.enabled) return false;
        if (_emptyHand) return false;
        
        return true;
    }

    public void ReturnSwords()
    {
        _emptyHand = false;
        _swords.SetActive(true);
    }
}
