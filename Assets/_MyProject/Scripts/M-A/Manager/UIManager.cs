using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Player _player = default;
    private SpawnManager _spawnManager = default;

    [SerializeField] TMP_Text _txtTime = default;
    [SerializeField] TMP_Text _txtScore = default;
    [SerializeField] TMP_Text _txtWave = default;
    [SerializeField] TMP_Text _txtBomb = default;
    [SerializeField] private GameObject _pauseMenu = default;
    private bool _pauseTime;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _txtScore.text = _player.GetScore().ToString();
        UpdateBomb();
        ChangeWave();
    }

    private void Update()
    {
        float temps = Time.time - _player._timeStart;
        _txtTime.text = temps.ToString("f2");
        GestionPause();
    }

    private void GestionPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseTime)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
            _pauseTime = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pauseTime)
        {
            EnleverPause();
        }
    }

    public void EnleverPause()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _pauseTime = false;
    }

    public void AddScore()
    {
        _txtScore.text = _player.GetScore().ToString();
    }

    public void UpdateBomb()
    {
        _txtBomb.text = "Mines : " + _player._bomb;
    }

    public void ChangeWave()
    {
        _txtWave.text = "Vague : " + _spawnManager._wave;
    }
}
