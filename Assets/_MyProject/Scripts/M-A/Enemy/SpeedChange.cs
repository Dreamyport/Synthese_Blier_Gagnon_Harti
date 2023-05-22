using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    private Enemy _enemy;
    private float _speed;

    private void Start()
    {
        _enemy = _enemyPrefab.GetComponent<Enemy>();
    }

    private void Update()
    {
        _speed = _enemy.GetCurrentSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Enemy statCollision = collision.transform.GetComponent<Enemy>();
            if(statCollision._projectileEnemy)
            {
                if(!statCollision.GetCanShoot())
                {
                    statCollision.SetCurrentSpeed(_speed);
                }
            }
            else
                statCollision.SetCurrentSpeed(_speed);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Enemy statCollision = collision.transform.GetComponent<Enemy>();
            if (statCollision._projectileEnemy)
            {
                if (!statCollision.GetCanShoot())
                {
                    statCollision.SetCurrentSpeed(_speed);
                }
            }
            else
                statCollision.SetCurrentSpeed(_speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Enemy statCollision = collision.transform.GetComponent<Enemy>();
            if (statCollision._projectileEnemy)
            {
                if (!statCollision.GetCanShoot())
                {
                    statCollision.ResetSpeed();
                }
            }
            else
                statCollision.ResetSpeed();
        }
    }


}
