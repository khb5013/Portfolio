using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private Player player;
    private HPBar hpBar;
    public static Manager instance;

    public float playerHealth = 100f; // 플레이어 체력
    public float playerMana = 100f;
    public int playerLives = 3; // 플레이어 라이프

    private float manaRegenRate = 4f;

    void Start()
    {
        Initialize();
    }
    void Update()
    {
        ManaPlus();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            if (player != null)
            {
                player.Die();
            }
            PlayerLives();
        }
    }
    public void UseMana(float mpCost)
    {
        if (playerMana >= mpCost)
        {
            playerMana -= mpCost;
        }
        hpBar.UpdateMPBar();
    }
    public void ManaPlus()
    {
        if (playerMana < 100f)
        {
            playerMana += manaRegenRate * Time.deltaTime;
            if (playerMana > 100f)
            {
                playerMana = 100f;
            }
            hpBar.UpdateMPBar();
        }
    }
    void Initialize()
    {
        player = FindObjectOfType<Player>();
        hpBar = FindObjectOfType<HPBar>();
    }

    public void PlayerLives()
    {
        playerLives--;
        if (playerLives < 0)
        {
            Invoke("GameOver", 2f);
        }
        else
        {
            Invoke("ReloadScene", 4f);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerHealth = 100f;
        playerMana = 100f;
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void RestartGame()
    {
        playerLives = 3;
        playerHealth = 100f;
        playerMana = 100f;
        SceneManager.LoadScene("FirstMap");
    }
    public int GetPlayerLives()
    {
        return playerLives;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
    }
}
