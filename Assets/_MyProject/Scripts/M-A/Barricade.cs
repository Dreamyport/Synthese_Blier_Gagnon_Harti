using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour
{
    [SerializeField] private float _lives = 100;
    [SerializeField] private GameObject _slider = default;

    public void Damage(float damage)
    {
        _lives = _lives - damage;

        _slider.GetComponent<Slider>().value = _lives / 100.0f;

        if (_lives <= 0) 
        {
            Destroy(gameObject);
        }
    }

}
