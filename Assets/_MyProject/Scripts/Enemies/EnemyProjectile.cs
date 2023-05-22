using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    EnemyManager _enemyManager = default;

    [SerializeField] private float _speed = 14.0f;
    [SerializeField] private float _damage = 1f;

    private void Start()
    {
        _enemyManager = FindObjectOfType<EnemyManager>();
        _damage = _enemyManager.GetProjectileDamage() * 0.5f;
    }
    private void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (transform.position.x < -9f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Turret" || collision.transform.tag == "Barricade")
        {
            Destroy(gameObject);
            if (collision.transform.tag == "Player")
            {
                Player player = collision.transform.GetComponent<Player>();
                player.Damage(_damage);
            }
            else if (collision.transform.tag == "Turret")
            {

            }
            else if (collision.transform.tag == "Barricade")
            {
                Barricade barricade = collision.transform.GetComponent<Barricade>();
                barricade.Damage(_damage);
            }
        }
    }
}
