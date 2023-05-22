using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioSource _music = default;

    [SerializeField] Button _buttonSound = default;
    [SerializeField] Sprite _musicOn = default;
    [SerializeField] Sprite _musicOff = default;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            _music.volume = 0.0f;
            _buttonSound.GetComponent<Image>().sprite = _musicOff;
        }
    }

    public void Mute() 
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
                _music.volume = 0.02f;
            else
                _music.volume = 0.1f;

            PlayerPrefs.SetInt("Muted", 1);
            PlayerPrefs.Save();
            _buttonSound.GetComponent<Image>().sprite = _musicOn;
        }
        else
        {
            _music.volume = 0.0f;
            PlayerPrefs.SetInt("Muted", 0);
            PlayerPrefs.Save();
            _buttonSound.GetComponent<Image>().sprite = _musicOff;
        }
    }
}
