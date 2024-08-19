using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZeoLite : MonoBehaviour
{
    [SerializeField] Image zeoHP;
    [SerializeField] Boss boss;
    [SerializeField]Player player;
    float maxHealth = 5000f;
    float currentHealth;
    float damageFlashDuration = 0.2f;
    Color damageFlashColor = Color.red;
    Color originalColor;
    bool isFlashing = false;

    SpriteRenderer zeoSpriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        zeoSpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = zeoSpriteRenderer.color;
    }
    void Update()
    {
        ZeoHPbar();
    }
    void ZeoHPbar()
    {
        zeoHP.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            boss.BossDead();
            player.BossClear();
        }
        if (!isFlashing)
        {
            StartCoroutine(FlashSprite());
        }
    }
    IEnumerator FlashSprite()
    {
        isFlashing = true;
        zeoSpriteRenderer.color = damageFlashColor;
        yield return new WaitForSeconds(damageFlashDuration);
        zeoSpriteRenderer.color = originalColor;
        isFlashing = false;
    }
}
