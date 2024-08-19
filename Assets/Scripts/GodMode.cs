using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    [SerializeField] Collider2D playerCollider2D;
    SpriteRenderer spriteRenderer;
    HPBar hpBar;
    Animator animator;

    bool isInvincible = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpBar = GetComponent<HPBar>();
        animator = GetComponent<Animator>();
    }

     public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Monster") ||
             collision.gameObject.layer == LayerMask.NameToLayer("Fire")) && !isInvincible)
        {
            onDamaged();
        }
    }

    void onDamaged()
    {
        isInvincible = true;
        hpBar.SetInvincible(true);
        gameObject.layer = LayerMask.NameToLayer("Player");
        animator.SetTrigger("hit");
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fire"), true);
        StartCoroutine(OffDamagedAfterDelay(3));
    }

    void OffDamaged()
    {
        isInvincible = false;
        hpBar.SetInvincible(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
        spriteRenderer.color = new Color(1, 1, 1, 1);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fire"), false);

    }

    IEnumerator OffDamagedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OffDamaged();
    }
}

