using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private UIManager _ui = default;
    private EnemyManager _enemyManager = default;

    [Header("Wave Management")]
    public int _wave = 1;
    private bool _stopSpawn = false;
    private float _spawnRate = 2f;

    private int _maxEnemies = 0;
    private int _enemiesSpawned = 0;

    [SerializeField] private int _maxBasics = 15;
    private int _basicsSpawned = 0;

    [SerializeField] private int _maxShields = 4;
    private int _shieldsSpawned = 0;

    [SerializeField] private int _maxProjectiles = 6;
    private int _projectilesSpawned = 0;

    private bool _validEnemy = false;

    private GameObject _spawnPrefab = default;

    [Header("Prefab Enemy")]
    [SerializeField] private GameObject _basicPrefab = default;
    [SerializeField] private GameObject _shieldPrefab = default;
    [SerializeField] private GameObject _projectilePrefab = default;
    [SerializeField] private GameObject _enemyContainer = default;
    
    [Header("Spawn Zone")]
    [SerializeField] private GameObject[] _listSpawnZone = default;

    public bool _gameOver = false;

    private void Start()
    {
        _ui = FindObjectOfType<UIManager>();
        _enemyManager = FindObjectOfType<EnemyManager>();
        _maxEnemies = _maxBasics + _maxShields + _maxProjectiles;
        StartCoroutine(SpawnEnemyRoutine());
    }
    
    // Routine des vagues
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f); // attente de 3 sec au d�but
        while (!_stopSpawn && !_gameOver)
        {
                _validEnemy = false;
                int spawnZone = Random.Range(0, 5); // Selection de la zone de spawn al�atoire
                Vector3 posSpawnZone = _listSpawnZone[spawnZone].transform.position;

                while (!_validEnemy && _enemiesSpawned < _maxEnemies)
                {
                    int enemyChoice = Random.Range(1, 4); // Selection de l'ennemi al�atoire (si valide)
                    EnemyChoice(enemyChoice);
                }
                if (_validEnemy)
                {
                    GameObject newEnemy = Instantiate(_spawnPrefab, posSpawnZone, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    _enemiesSpawned++;
                }
                if (_enemiesSpawned >= _maxEnemies && _enemyContainer.transform.childCount == 0)
                {
                    EndWave();
                }
                yield return new WaitForSeconds(_spawnRate); // attente entre les ennemies
        }
    }

    // Selection d'un ennemi valide
    private void EnemyChoice(int choice)
    {
        if (choice == 1)
        {
            if (_basicsSpawned < _maxBasics)
            {
                _spawnPrefab = _basicPrefab;
                _basicsSpawned++;
                _validEnemy = true;
            }
            else
                _validEnemy = false;
        }
        else if (choice == 2)
        {
            if (_shieldsSpawned < _maxShields)
            {
                _spawnPrefab = _shieldPrefab;
                _shieldsSpawned++;
                _validEnemy = true;
            }
            else
                _validEnemy = false;
        }
        else if (choice == 3)
        {
            if (_projectilesSpawned < _maxProjectiles)
            {
                _spawnPrefab = _projectilePrefab;
                _projectilesSpawned++;
                _validEnemy = true;
            }
            else
                _validEnemy = false;
        }
    }

    private void EndWave() // Fin de vague + augmentation difficulté
    {
        _wave++;
        _enemiesSpawned = 0;
        _basicsSpawned = 0;
        _shieldsSpawned = 0;
        _projectilesSpawned = 0;

        _maxBasics += 1; // Ajout d'un ennemi normale à chaque vague
        _enemyManager.SetBasicDamage(_enemyManager.GetBasicDamage() + (_enemyManager.GetBasicDamage() * 10/100)); // Augmentation des dégats des ennemis de 10% à chaque vagues
        _enemyManager.SetShieldDamage(_enemyManager.GetShieldDamage() + (_enemyManager.GetShieldDamage() * 10/100));
        _enemyManager.SetProjectileDamage(_enemyManager.GetProjectileDamage() + (_enemyManager.GetProjectileDamage() * 10/100));

        if(_wave % 2 == 0)
        {
            _maxShields += 1; // Ajout d'un ennemi avec bouclier à chaque 2 vagues
            _enemyManager.SetBasicLife(_enemyManager.GetBasicLife() + (_enemyManager.GetBasicLife() * 10/100)); // Augmentation de la vie des ennemis de 10% à chaque 2 vagues
            _enemyManager.SetShieldLife(_enemyManager.GetShieldLife() + (_enemyManager.GetShieldLife() * 10/100));
            _enemyManager.SetProjectileLife(_enemyManager.GetProjectileLife() + (_enemyManager.GetProjectileLife() * 10/100));
        }
        else if (_wave % 3 == 0)
        {
            _maxProjectiles += 1; // Ajout d'un ennemi qui tir à chaque 3 vagues
            _enemyManager.SetBasicSpeed(_enemyManager.GetBasicSpeed() + (_enemyManager.GetBasicSpeed() * 10/100)); // Augmentation de la vitesse des ennemis de 10% à chaque 3 vagues
            _enemyManager.SetShieldSpeed(_enemyManager.GetShieldSpeed() + (_enemyManager.GetShieldSpeed() * 10/100));
            _enemyManager.SetProjectileSpeed(_enemyManager.GetProjectileSpeed() + (_enemyManager.GetProjectileSpeed() * 10/100));
        }
        else if(_wave % 5 == 0)
        {
            if(_spawnRate > 1f)
                _spawnRate -= 0.5f; // Diminution du temps entre les ennemis à chaque 5 vagues
        }

        _maxEnemies = _maxBasics + _maxShields + _maxProjectiles; // Ajustement du max d'ennemi pour la vague
        _ui.ChangeWave();
    }

    public void GameOver()
    {
        _gameOver= true;
    }
}
