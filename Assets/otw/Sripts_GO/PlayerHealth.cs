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
    public Image healthImage;
    public Transform heartContainer;
    public GameObject heartImagePrefab;

    private List<GameObject> heartImages = new List<GameObject>();

    public TimeAttack timeAttack;

    private void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;

        CreateInitialHearts();
        UpdateHealthBar();
    }

    private void CreateInitialHearts()
    {
        for (int i = 0; i < maxLives; i++)
        {
            GameObject heart = Instantiate(heartImagePrefab, heartContainer);
            heartImages.Add(heart);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthImage != null)
        {
            healthImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }


    private void Update()
    {
        // ���� üũ
        if (transform.position.y < fallThresholdY && !isInvincible)
        {
            FallDeath();
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
            currentHealth = maxHealth;
        }

        UpdateHealthBar();
        StartCoroutine(InvincibilityCoroutine());
        Debug.Log($"����! ü��: {currentHealth}, ���: {currentLives}");
    }

    // �������� ��(����)
    private void FallDeath()
    {
        Debug.Log("����");

        TakeLife();

        currentHealth = maxHealth;
        UpdateHealthBar();

        Respawn();

        StartCoroutine(InvincibilityCoroutine());
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

            Debug.Log($"��� �ϳ� ����. ���� ���: {currentLives}");

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
    }

    private void Respawn()
    {
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            transform.position = respawnPosition;
            cc.enabled = true;
        }
        else
        {
            transform.position = respawnPosition;
        }

        Debug.Log($"������ �� : {transform.position}");
        Debug.Log($"������ ��ǥ : {respawnPosition}");
        Debug.Log($"������ �� : {transform.position}");
    }

    private void GameOver()
    {
        Debug.Log("���� ����!");
        // ���⿡ ���� ���� ó�� �߰� ����
        timeAttack.ShowGameOver();
        Time.timeScale = 0f;
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}
