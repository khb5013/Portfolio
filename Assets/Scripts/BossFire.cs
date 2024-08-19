using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour
{

    private void Start()
    {
        Invoke("time" ,3f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            int damage = Random.Range(10, 20);
            Manager player = Manager.instance;
            HPBar hpBar = FindObjectOfType<HPBar>();
            if (player != null)
            {
                if (hpBar != null)
                {
                    hpBar.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }


     void time()
    {
        Destroy(gameObject);
    }
}
