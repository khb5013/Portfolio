using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            Manager manager = Manager.instance; // Manager�� �ν��Ͻ��� ����
            HPBar bar = player.GetComponent<HPBar>();
            if (manager != null)
            {
                bar.DeadZoneDamage();
                manager.PlayerLives();
            }
        }
    }
}

