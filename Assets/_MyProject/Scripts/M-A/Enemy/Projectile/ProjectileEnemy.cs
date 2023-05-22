using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    private Enemy _enemy;
    private EnemyManager _enemyManager;

    [SerializeField] private GameObject _laserPrefab = default;
    [SerializeField] private Transform _projectileSpawn = default;
    [SerializeField] private AudioSource _enemyAudio = default;
    [SerializeField] private AudioClip _laser = default;

    private float _canFire = -1.0f;
    [SerializeField] private float _fireRate = 0.1f;

    private int _maxBullet;
    private int _bulletShot = 0;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemyManager = FindObjectOfType<EnemyManager>();
        setStat();
        _bulletShot = 0;
    }

    void Update()
    {
        Fire();
    }

    private void setStat()
    {
        _maxBullet = _enemyManager.getBullet();
        _fireRate = _enemyManager.getFireRate();
    }

    private void Fire()
    {
        if (_enemy.GetCanShoot() == true && Time.time > _canFire && _bulletShot < _maxBullet)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, _projectileSpawn.position, Quaternion.identity);
            _bulletShot++;
            _enemyAudio.PlayOneShot(_laser, 0.1f);
        }
        else if(_bulletShot >= _maxBullet)
        {
            _enemy.OutOfAmmo();
        }
    }
}
