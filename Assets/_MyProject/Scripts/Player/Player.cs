using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private SpawnManager _spawnManager = null;
    private UIManager _ui = null;
    private float _maxLife = 100.0f;

    [SerializeField] private GameObject _laserPrefab = default;
    [SerializeField] private GameObject _bombPrefab = default;
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _slider = default;
    [SerializeField] private AudioSource _playerAudio = default;
    [SerializeField] private AudioClip _laser = default;
    [SerializeField] private AudioClip _hurt = default;

    [SerializeField] private float _life = 0f;
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private float _fireRate = 0f;

    [SerializeField] private int _score = 0;

    private float _canFire = -1f;
    private float _initialFireRate;

    public int _bomb = 1;

    public float _timeStart = 0f;

    private void Start()
    {
        _initialFireRate = _fireRate;
        _spawnManager= FindObjectOfType<SpawnManager>();
        _ui = FindObjectOfType<UIManager>();
        _timeStart = Time.time;
    }

    private void Update()
    {
        BombPlacement();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            Instantiate(_laserPrefab, _projectileSpawn.position, Quaternion.identity);
            _canFire = Time.time + _fireRate;
            _playerAudio.PlayOneShot(_laser, 0.05f);
        }
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        _animator.SetFloat("Speed", horizInput);

        Vector3 direction = new Vector3(horizInput, vertInput, 0.0f);
        direction.Normalize();

        _rb.velocity = (direction * Time.fixedDeltaTime * _speed);
    }

    private void BombPlacement()
    {
        if(_bomb > 0 && Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(_bombPrefab, gameObject.transform.position, Quaternion.identity);
            _bomb--;
            _ui.UpdateBomb();
        }
    }

    public void Damage(float damage)
    {
        _life -= damage;

        float percent = _life / _maxLife;
        _slider.GetComponent<Slider>().value = percent;

        _playerAudio.PlayOneShot(_hurt, 0.3f);

        if (_life <= 0) 
        {
            Destroy(gameObject);
            _spawnManager.GameOver();
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.Save();
            SceneManager.LoadScene(2);
        }
    }

    public void AddScore(int point)
    {
        _score += point;
    }

    public void BombCollect()
    {
        if(_bomb < 3) 
            _bomb++;
    }

    // Tout les parametre du joueur
    public void SetLife(float life) { this._life = life; }

    public float GetLife() { return this._life; }

    public void SetSpeed(float speed) { this._speed = speed; }

    public float GetSpeed() { return this._speed; }

    public void SetDamage(float damage) { this._damage = damage; }

    public float GetDamage() { return this._damage; }

    public void SetFireRate(float fireRate) { this._fireRate = fireRate; }

    public float GetFireRate() { return this._fireRate; }

    public void SetScore(int score) { _score = score; }

    public int GetScore() { return _score;}
}
   