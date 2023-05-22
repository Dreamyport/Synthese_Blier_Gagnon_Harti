using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AudioSource _bombAudio = default;
    [SerializeField] private AudioClip _bomb = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Player player = FindObjectOfType<Player>();
            UIManager ui = FindObjectOfType<UIManager>();

            _bombAudio.PlayOneShot(_bomb, 0.2f);

            player.AddScore(enemy._point);
            ui.AddScore();
            Destroy(collision.gameObject);
            enemy.DropBomb();
            Destroy(gameObject, 0.2f);
        }

    }
}
