using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPickUp : MonoBehaviour
{
    private bool _inInventory = false;
    private bool _playerInZone = false;

    private ValidPlacement _validPlacementScript;

    [SerializeField] private Transform _playerInventory;
    [SerializeField] private AudioSource _turretAudio = default;
    [SerializeField] private AudioClip _placementSound = default;


    private void Start()
    {
        _validPlacementScript = FindObjectOfType<ValidPlacement>();
    }

    private void Update()
    {
        if (_playerInZone && Input.GetKeyDown(KeyCode.E) && !_inInventory)
        {
            _turretAudio.PlayOneShot(_placementSound, 0.2f);
            _inInventory = true;
        }

        if (_inInventory)
        {
            gameObject.transform.position = _playerInventory.position;
            gameObject.transform.rotation = _playerInventory.rotation;

            _validPlacementScript.Activation(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_inInventory)
            _playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_inInventory)
            _playerInZone = false;
    }

    public void DropTurret(Transform position)
    {
        _inInventory = false;

        _turretAudio.PlayOneShot(_placementSound, 0.2f);

        gameObject.transform.position = position.position;
        gameObject.transform.rotation = position.rotation;

        _validPlacementScript.Activation(false);
    }
}
