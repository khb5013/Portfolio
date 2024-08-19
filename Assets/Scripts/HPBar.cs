using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Image mpBar;
    [SerializeField] Text livesText;

    Manager manager;
    bool isInvincible = false;

    void Start()
    {
        manager = Manager.instance;
        UpdateHPBar();
        UpdateMPBar();
        UpdateLivesText();
    }

    public void UpdateHPBar()
    {
        if (hpBar != null && manager != null)
        {
            hpBar.fillAmount = manager.playerHealth / 100f;
        }
    }
    public void UpdateMPBar()
    {
        if (mpBar != null && manager != null)
        {
            float currentMana = manager.playerMana;
            float maxMana = 100f; // 최대 마나 값
            mpBar.fillAmount = currentMana / maxMana;
        }
    }

    void UpdateLivesText()
    {
        if (livesText != null && manager != null)
        {
            livesText.text = "X " + manager.GetPlayerLives().ToString();
        }
    }

    public void TakeDamage(float damage)
    {
        if (manager != null)
        {
            manager.ApplyDamage(damage);
            UpdateHPBar();
        }
    }
    public void TakeMana(float amount)
    {
        if (manager != null)
        {
            manager.UseMana(amount);
            UpdateMPBar();
        }
    }
    public void DeadZoneDamage()
    {
        if (manager != null)
        {
            manager.playerHealth = 0;
            UpdateHPBar();
        }
    }
    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }
}
