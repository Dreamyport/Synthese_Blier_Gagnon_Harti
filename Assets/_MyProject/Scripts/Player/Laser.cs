using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Player _player = default;
    [SerializeField] private float _speed = 14.0f;


    public void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    public void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
        if(transform.position.x > 9f)
        {
            if (this.transform.parent == null)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.Damage(_player.GetDamage());
        }
    }
}
