using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtGameOver = default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    private int _score;

    void Start()
    {
        _score = PlayerPrefs.GetInt("Score");
        _txtScore.text = "Votre Score : " + _score;

        GameOverSequence();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Appuyer sur R pour recommencer une partie
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) // Appuyer sur escape pour retourner au menu
        {
            SceneManager.LoadScene(0);
        }
    }

    
    private void GameOverSequence()
    {
        _txtGameOver.gameObject.SetActive(true);
        StartCoroutine(GameOverBlinkRoutine());
    }

    IEnumerator GameOverBlinkRoutine() // Fait flasher le text game over
    {
        while (true)
        {
            _txtGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _txtGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
