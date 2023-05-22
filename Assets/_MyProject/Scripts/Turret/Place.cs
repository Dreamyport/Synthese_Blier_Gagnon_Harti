using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    private bool _canPlace = false;

    private TurretPickUp _turret;

    private void Start()
    {
        _turret = FindObjectOfType<TurretPickUp>();
    }

    private void Update()
    {
        if (_canPlace && Input.GetKeyDown(KeyCode.E))
        {
            _canPlace = false;

            _turret.DropTurret(gameObject.GetComponent<Transform>());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            _canPlace = true;
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            _canPlace = false;
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }
}
