using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    
    bool isLeft = true;
    int maxHealth = 100;
    int currentHealth;
    bool isDead = false;
    float knockbackForce = 2f;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Raycast();
        }
    }

    void Raycast()
    {
        Vector3 movement = isLeft ? Vector3.left : Vector3.right;
        transform.position += movement * moveSpeed * Time.fixedDeltaTime;
        if (!Physics2D.Raycast(transform.position, Vector2.down, 0.01f, LayerMask.GetMask("MonsterArea")))
        {
            isLeft = !isLeft;
            spriteRenderer.flipX = !isLeft;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            int damage = Random.Range(20, 30);
            Manager player = Manager.instance;
            HPBar hpBar = FindObjectOfType<HPBar>();
            if (player != null)
            {
                if (hpBar != null)
                {
                    hpBar.TakeDamage(damage);
                }
            }
        }
    }
    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("dead");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        animator.SetTrigger("hit");
        Vector3 playerPosition = FindObjectOfType<Player>().GetPlayerPosition();
        Vector3 knockbackDirection = (transform.position - playerPosition).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
