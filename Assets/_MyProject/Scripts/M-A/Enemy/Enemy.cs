using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Player _player = default;
    private SpawnManager _spawnManager = default;
    private UIManager _ui = default;

    private bool _canShoot = false;

    private Rigidbody2D _rb;
    private EnemyManager _enemyManager = default;

    public float _life = 0f;
    private float _speed = 3.0f;
    private float _damage = 0f;
    public float _shield = 0f;
    private float _currentSpeed = 0f;
    [SerializeField] public bool _basicEnemy = false;
    [SerializeField] public bool _shieldEnemy = false;
    [SerializeField] public bool _projectileEnemy = false;
    [SerializeField] public GameObject _shieldPrefab = default;
    [SerializeField] private GameObject _slider = default;
    [SerializeField] private GameObject _shieldSlider = default;
    [SerializeField] private AudioSource _enemyAudio = default;
    [SerializeField] private AudioClip _death = default;
    private float _spawnTime = 0f;

    public int _point = 0;

    [SerializeField] private GameObject _bombPowerUpPrefab = default;

    // ----------------------------------------------------------- Fonction Start/Update/Collision
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyManager = FindObjectOfType<EnemyManager>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _player = FindObjectOfType<Player>();
        _ui = FindObjectOfType<UIManager>();
        SetStatEnemy();
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EndGame") 
        {
            PlayerPrefs.SetInt("Score", _player.GetScore());
            _spawnManager.GameOver();
            PlayerPrefs.Save();
            SceneManager.LoadScene(2);
        }

        if (collision.transform.tag == "Player" || collision.transform.tag == "Turret" || collision.transform.tag == "Barricade")
        {
            _enemyAudio.PlayOneShot(_death, 0.2f);
            Destroy(gameObject, 0.1f);
            if (collision.transform.tag == "Player")
            {
                Player player = collision.transform.GetComponent<Player>();
                player.Damage(_damage);
            }
            else if (collision.transform.tag == "Turret")
            {
                Turret turret = collision.transform.GetComponent<Turret>();
                turret.Damage(_damage);
            }
            else if (collision.transform.tag == "Barricade")
            {
                Barricade barricade = collision.transform.GetComponent<Barricade>();
                barricade.Damage(_damage);
            }
        }
    }

    // ----------------------------------------------------------- Fonction privé

    private void EnemyMovement()
    {
        _rb.velocity = Vector3.left * Time.fixedDeltaTime * _currentSpeed;
    }

    // ----------------------------------------------------------- Fonction Public
    public void SetStatEnemy()
    {
        SetLife();
        SetSpeed();
        SetDamage();
        SetPoint();
        _currentSpeed = _speed;
    }

    public void SetSpeed()
    {
        if(_basicEnemy)
        {
            _speed = _enemyManager.GetBasicSpeed();
        }
        else if(_shieldEnemy)
        {
            _speed = _enemyManager.GetShieldSpeed();
        }
        else if(_projectileEnemy)
        {
            _speed = _enemyManager.GetProjectileSpeed();
        }
    }

    public void SetLife()
    {
        if (_basicEnemy)
        {
            _life = _enemyManager.GetBasicLife();
            _shield = 0f;
        }
        else if (_shieldEnemy)
        {
            _life = _enemyManager.GetShieldLife();
            _shield = _enemyManager.GetLifeShield();
        }
        else if (_projectileEnemy)
        {
            _life = _enemyManager.GetProjectileLife();
            _shield = 0f;
        }
    }

    public void SetDamage()
    {
        if (_basicEnemy)
        {
            _damage= _enemyManager.GetBasicDamage();
        }
        else if (_shieldEnemy)
        {
            _damage= _enemyManager.GetShieldDamage();
        }
        else if (_projectileEnemy)
        {
            _damage= _enemyManager.GetProjectileDamage();
        }
    }

    public void SetPoint()
    {
        if(_basicEnemy)
        {
            _point = _enemyManager.GetBasicPoint();
        }
        else if(_shieldEnemy)
        {
            _point = _enemyManager.GetShieldPoint();
        }
        else if(_projectileEnemy)
        {
            _point =_enemyManager.GetProjectilePoint();
        }
    }

    public void Damage(float damage)
    {
        if(_shield > 0f)
        {
            _shield -= (damage / 2.0f);

            float percent = (_shield / _enemyManager.GetLifeShield());

            _shieldSlider.GetComponent<Slider>().value = percent;

            if (_shield <= 0.0f) 
            {
                Destroy(_shieldPrefab);
            }
        }
        else
        {
            _life -= damage;

            float percent = _life;

            if (_basicEnemy)
            {
                percent = _life / _enemyManager.GetBasicLife();
            }
            else if (_shieldEnemy)
            {
                percent = _life / _enemyManager.GetShieldLife();
            }
            else if (_projectileEnemy)
            {
                percent = _life / _enemyManager.GetProjectileLife();
            }

            _slider.GetComponent<Slider>().value = percent;
        }

        if (_life <= 0 ) // mort de ennemi + changement score + ui
        {
            _enemyAudio.PlayOneShot(_death, 0.2f);
            Destroy(gameObject, 0.1f);
            _player.AddScore(_point);
            _ui.AddScore();
            DropBomb();
        }
    }

    public void DropBomb()
    {
        int chance = Random.Range(1, 10);
        if (chance == 5)
            Instantiate(_bombPowerUpPrefab, gameObject.transform.position, Quaternion.identity);
    }

    public void InShootingRange()
    {
        if(_projectileEnemy)
        {
            _currentSpeed = 0.0f;
            _canShoot = true;
        }
    }

    public void OutOfAmmo()
    {
        _currentSpeed = _speed;
    }

    public float GetLifeTime()
    {
        return _spawnTime;
    }

    public float GetCurrentSpeed()
    {  
        return _currentSpeed; 
    }

    public bool GetCanShoot() 
    {
        return _canShoot;
    }

    public void SetCurrentSpeed(float newSpeed)
    {
        _currentSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        _currentSpeed = _speed;
    }
}
