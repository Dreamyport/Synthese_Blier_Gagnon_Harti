using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject _turretLaserPrefab = default;
    [SerializeField] private GameObject _slider = default;
    [SerializeField] private AudioSource _turretAudio = default;
    [SerializeField] private AudioClip _laser = default;

    [SerializeField] private float _life = 0f;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private float _fireRate = 0f;
    private float _canFire = 0f;
    private float _maxLife = 100.0f;

    void Update()
    {
        ShotTurret();
    }

    private void ShotTurret()
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_turretLaserPrefab, transform.position + new Vector3(0.5f, 0.03f, 0f), Quaternion.identity);
            _turretAudio.PlayOneShot(_laser, 0.1f);
        }
    }

    public void Damage(float damage)
    {
        _life -= damage;

        float percent = _life / _maxLife;
        _slider.GetComponent<Slider>().value = percent;

        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetLife(float life) { this._life = life; }

    public float GetLife() { return this._life; }

    public void SetDamage(float damage) { this._damage = damage; }

    public float GetDamage() { return this._damage; }

    public void SetFireRate(float fireRate) { this._fireRate = fireRate; }

    public float GetFireRate() { return this._fireRate; }
}
