using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Basic Enemy Stat")]
    [SerializeField] private float _basicLife = 0;
    [SerializeField] private float _basicSpeed = 0;
    [SerializeField] private float _basicDamage = 0;
    [SerializeField] private int _basicPoint = 0;


    [Header("Shield Enemy Stat")]
    [SerializeField] private float _shieldLife = 0; // Vie de l'enemy
    [SerializeField] private float _shieldSpeed = 0;
    [SerializeField] private float _shieldDamage = 0;
    [SerializeField] private float _lifeShield = 0f; // Vie du bouclier
    [SerializeField] private int _shieldPoint = 0;

    [Header("Projectile Enemy Stat")]
    [SerializeField] private float _projetcileLife = 0;
    [SerializeField] private float _projetcileSpeed = 0;
    [SerializeField] private float _projetcileDamage = 0;
    [SerializeField] private float _fireRate = 0;
    [SerializeField] private int _bullet = 0;
    [SerializeField] private int _projectilePoint = 0;


    // --------------------------------------- Basic

    public void SetBasicLife(float life) { _basicLife = life; }
    public float GetBasicLife() { return _basicLife; }

    public void SetBasicSpeed(float speed) { _basicSpeed = speed; }
    public float GetBasicSpeed() { return _basicSpeed; }

    public void SetBasicDamage(float damage) { _basicDamage = damage;}
    public float GetBasicDamage() { return _basicDamage;}

    public void SetBasicPoint(int point) { _basicPoint = point; }
    public int GetBasicPoint() { return _basicPoint;}

    // --------------------------------------- Shield

    public void SetShieldLife(float life) { _shieldLife = life; }
    public float GetShieldLife() { return _shieldLife; }

    public void SetShieldSpeed(float speed) { _shieldSpeed = speed; }
    public float GetShieldSpeed() { return _shieldSpeed; }

    public void SetShieldDamage(float damage) { _shieldDamage = damage; }
    public float GetShieldDamage() { return _shieldDamage; }

    public void SetLifeShield(float shield) { _lifeShield = shield; }
    public float GetLifeShield() { return _lifeShield; }

    public void SetShieldPoint(int point) { _shieldPoint = point; }
    public int GetShieldPoint() { return _shieldPoint; }

    // --------------------------------------- Projectile

    public void SetProjectileLife(float life) { _projetcileLife = life; }
    public float GetProjectileLife() { return _projetcileLife; }

    public void SetProjectileSpeed(float speed) { _projetcileSpeed = speed; }
    public float GetProjectileSpeed() { return _projetcileSpeed; }

    public void SetProjectileDamage(float damage) { _projetcileDamage = damage; }
    public float GetProjectileDamage() { return _projetcileDamage; }

    public void setFireRate(float fireRate) { _fireRate = fireRate; }
    public float getFireRate() { return _fireRate; }

    public void setBullet(int bullet) { _bullet = bullet; }
    public int getBullet() { return _bullet; }

    public void SetProjectilePoint(int point) { _projectilePoint = point;}
    public int GetProjectilePoint() { return _projectilePoint; }
}
