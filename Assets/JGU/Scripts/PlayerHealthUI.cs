using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("체력 설정")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damagePerHit = 20;

    [Header("목숨 설정")]
    public int maxLives = 5;
    public int currentLives = 5;

    [Header("UI 연결")]
    public Slider healthSlider;
    public Transform heartContainer;
    public GameObject heartImagePrefab;

    private List<GameObject> heartImages = new List<GameObject>();

    private void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;

        CreateInitialHearts();
        UpdateHealthSlider();
    }

    private void CreateInitialHearts()
    {
        for (int i = 0; i < maxLives; i++)
        {
            GameObject heart = Instantiate(heartImagePrefab, heartContainer);
            heartImages.Add(heart);
        }
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth/maxHealth;
        }
    }
    
    public void TakeDamage()
    {
        currentHealth -= damagePerHit;

        if (currentHealth <= 0)
        {
            LoseLife();
            currentHealth = maxHealth;
        }

        UpdateHealthSlider();
        Debug.Log($"현재 체력 : {currentHealth}, 현재 목숨: {currentLives}");
    }

    private void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;

            if(heartImages.Count > 0)
            {
                GameObject lastHeart = heartImages[heartImages.Count - 1];
                heartImages.RemoveAt(heartImages.Count - 1);
                Destroy(lastHeart);
            }

            Debug.Log($"목숨 잃음! 남은 목숨: {currentLives}");

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;

            GameObject newHeart = Instantiate(heartImagePrefab, heartContainer);
            heartImages.Add(newHeart);

            Debug.Log($"목숨 회복! 현재 목숨 : {currentLives}");
        }
        else
        {
            Debug.Log("목숨이 이미 최대 입니다.");
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버");
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage();
        }
    }
}
