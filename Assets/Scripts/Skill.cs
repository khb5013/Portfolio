using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject bladePrefab;
    public float bladeSpeed = 10f;
    public Transform spawnPoint; 
    public int bladeDamage = 20;
    public float manaCost = 30f;
    SpriteRenderer spriteRenderer;
    HPBar hpBar;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpBar = FindObjectOfType<HPBar>();
    }
    private void Update()
    {
        if (spawnPoint != null)
        {
            if (spriteRenderer.flipX)
            {
                spawnPoint.localPosition = new Vector3(-2f, 0f, 0f);
            }
            else
            {
                spawnPoint.localPosition = new Vector3(2f, 0f, 0f);
            }
        }
    }

    public void Activate()
    {
        if (Manager.instance.playerMana >= manaCost)
        {
            Manager.instance.UseMana(manaCost);
            GameObject blade = Instantiate(bladePrefab, spawnPoint.position, transform.rotation);
            Rigidbody2D rb = blade.GetComponent<Rigidbody2D>();
            hpBar.UpdateMPBar();
            if (spriteRenderer.flipX)
            {
                rb.velocity = Vector2.left * bladeSpeed;
                blade.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rb.velocity = Vector2.right * bladeSpeed;
                blade.transform.localScale = new Vector3(1, 1, 1);
            }

            Skill bladeComponent = blade.AddComponent<Skill>();
            bladeComponent.bladeDamage = bladeDamage;

            Destroy(blade, 2f);
        }
        else
        {

            Debug.Log("마나가 부족합니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(bladeDamage);
        }
    }
}
