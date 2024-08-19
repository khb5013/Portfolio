using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject firePrefab;
    public Transform fireSpawnPoint;
    public float moveSpeed = 1f;


    bool isLeft = true;
    bool isDead = false;
    private float fireCooldown = 1f;
    private float fireCooldownTimer = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead)
        {
            BossMove();
            fireCooldownTimer -= Time.deltaTime;
            if (fireCooldownTimer <= 0f)
            {
                ShootFire();
                fireCooldownTimer = fireCooldown;
            }
        }
    }

    void BossMove()
    {
        Vector3 movement = isLeft ? Vector2.right : Vector2.left;
        transform.position += movement * moveSpeed * Time.fixedDeltaTime;
        if (Physics2D.Raycast(transform.position, movement, 5f, LayerMask.GetMask("MonsterArea")))
        {
            isLeft = !isLeft;
            spriteRenderer.flipX = !isLeft;
        }
    }
    public void BossDead()
    {
        isDead = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("bossdead");
    }

    void ShootFire()
    {
        Vector2[] fireDirections = new Vector2[]
        {
        Vector2.down,
        new Vector2(-0.5f, -1).normalized,
        new Vector2(-1, -1).normalized,
        new Vector2(0.5f, -1).normalized,
        new Vector2(1, -1).normalized 
        };
        foreach (Vector2 direction in fireDirections)
        {
            GameObject fire = Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
            rb.velocity = direction * 7f;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fire.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}