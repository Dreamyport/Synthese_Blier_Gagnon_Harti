using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _instruction = default;

    [SerializeField] private GameObject _objective = default;
    [SerializeField] private GameObject _enemy = default;
    [SerializeField] private GameObject _controle = default;

    public void NextScene() 
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(indexScene + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowInstruction()
    {
        _instruction.SetActive(true);
    }

    public void CloseInstruction()
    {
        _instruction.SetActive(false);
    }

    public void ShowObjectives()
    {
        _objective.SetActive(true);
    }

    public void CloseObjectives()
    {
        _objective.SetActive(false);
    }

    public void ShowEnemy()
    {
        _enemy.SetActive(true);
    }

    public void CloseEnemy()
    {
        _enemy.SetActive(false);
    }

    public void ShowControle()
    {
        _controle.SetActive(true);
    }
    
    public void CloseControle()
    {
        _controle.SetActive(false);
    }
}
