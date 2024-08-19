using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private bool hasTriggered = false;
    public GameObject[] wallPlatforms;
    public float dropSpeed = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                hasTriggered = true;
                DropAllPlatforms();
            }
        }
    }

    private void DropAllPlatforms()
    {
        foreach (GameObject platform in wallPlatforms)
        {
            DropPlatform(platform);
        }
    }

    private void DropPlatform(GameObject platform)
    {
        Rigidbody2D rb = platform.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = dropSpeed;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
