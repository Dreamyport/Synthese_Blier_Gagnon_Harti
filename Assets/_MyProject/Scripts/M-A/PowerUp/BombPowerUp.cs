using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = FindObjectOfType<Player>();
            player.BombCollect();
            Destroy(gameObject);

            UIManager ui = FindObjectOfType<UIManager>();
            ui.UpdateBomb();
        }
    }
}
