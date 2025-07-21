using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("ü�� ����")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damagePerHit = 20;

    [Header("��� ����")]
    public int maxLives = 5;
    public int currentLives = 5;

    [Header("UI ����")]
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
        Debug.Log($"���� ü�� : {currentHealth}, ���� ���: {currentLives}");
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

            Debug.Log($"��� ����! ���� ���: {currentLives}");

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

            Debug.Log($"��� ȸ��! ���� ��� : {currentLives}");
        }
        else
        {
            Debug.Log("����� �̹� �ִ� �Դϴ�.");
        }
    }

    private void GameOver()
    {
        Debug.Log("���� ����");
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
