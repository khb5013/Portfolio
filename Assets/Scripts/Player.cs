using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] Skill skill;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isDead = false;



    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDead = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
            Jump();
            Attack();
            UpdateAnimator();
            Skill();
        }
    }
    bool IsGrounded()
    {
        RaycastHit2D isGround = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, LayerMask.GetMask("Ground"));
        return isGround.collider != null;
    }
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
        if (horizontal != 0f)
            spriteRenderer.flipX = horizontal < 0;
        animator.SetBool("run", horizontal != 0);

    }
    void Jump()
    {
        bool isGround = IsGrounded();
        if (Input.GetKeyDown(KeyCode.X) && isGround)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("attack");
            Vector3 iAttack = spriteRenderer.flipX ? Vector3.left : Vector3.right;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + iAttack * 1f, 1f, LayerMask.GetMask("Monster"));
            foreach (Collider2D enemy in hitEnemies)
            {
                Monster monster = enemy.GetComponent<Monster>();
                if (monster != null)
                {
                    int damage = Random.Range(20, 30);
                    monster.TakeDamage(damage);
                }
            }
            Collider2D[] hitzeolite = Physics2D.OverlapCircleAll(transform.position + iAttack * 1f, 1f, LayerMask.GetMask("Zeolite"));
            foreach (Collider2D zeoliteCollider in hitzeolite)
            {
                ZeoLite zeoLite = zeoliteCollider.GetComponent<ZeoLite>();
                if (zeoLite != null)
                {
                    float damage = Random.Range(40, 55);
                    zeoLite.TakeDamage(damage);
                }
            }
        }
    }
    void Skill()
    {
        if (skill != null)
        {
            if (Input.GetKeyDown(KeyCode.C) && Manager.instance.playerMana >= skill.manaCost )
            {
                animator.SetTrigger("skillAni");
                skill.Activate();
            }
        }
    }

    
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("dead");
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
    }
    public Vector2 GetPlayerPosition()
    {
        return transform.position;
    }
    void UpdateAnimator()
    {
        animator.SetBool("drop", rb.velocity.y < 1f);
        animator.SetBool("isGround", IsGrounded());
    }
    public void BossClear()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("clear");
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
    }
}


public static class Method
{
    public static void ChangeAlpha(this SpriteRenderer target, float alpha)
    {
        Color color = target.color;
        color.a = alpha;
        target.color = color;
    }
}
