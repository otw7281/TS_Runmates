using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("ü�� ����")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damagePerHit = 20;

    [Header("��� ����")]
    public int maxLives = 5;
    public int currentLives = 5;

    [Header("���� �ð�")]
    public float invincibilityDuration = 1f;
    private bool isInvincible = false;

    [Header("���� ������ ����")]
    public float fallThresholdY = -10f;  // Y�� �� ������ ������ ������
    public Vector3 respawnPosition = new Vector3(2.5f, 4f, -3f);

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
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }


    private void Update()
    {
        // ���� üũ
        if (transform.position.y < fallThresholdY)
        {
            Respawn();
            TakeLife();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isInvincible)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentHealth -= damagePerHit;
        if (currentHealth <= 0)
        {
            TakeLife();
            Respawn();
            currentHealth = maxHealth;
        }

        UpdateHealthSlider();
        StartCoroutine(InvincibilityCoroutine());
        Debug.Log($"����! ü��: {currentHealth}, ���: {currentLives}");
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


    private void TakeLife()
    {
        if (currentLives > 0)
        {
            currentLives--;

            if (heartImages.Count > 0)
            {
                GameObject lastHeart = heartImages[heartImages.Count - 1];
                heartImages.RemoveAt(heartImages.Count - 1);
                Destroy(lastHeart);
            }

            if (currentLives <= 0)
            {
                GameOver();
            }
            else
            {
                Debug.Log($"��� �ϳ� ����. ���� ���: {currentLives}");
            }
        }
    }

    private void Respawn()
    {
        transform.position = respawnPosition;
        Debug.Log("�÷��̾� ��������");
    }

    private void GameOver()
    {
        Debug.Log("���� ����!");
        // ���⿡ ���� ���� ó�� �߰� ����
        Time.timeScale = 0f;
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}
