using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력 설정")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damagePerHit = 20;

    [Header("목숨 설정")]
    public int maxLives = 5;
    public int currentLives = 5;

    [Header("무적 시간")]
    public float invincibilityDuration = 1f;
    private bool isInvincible = false;

    [Header("낙사 리스폰 설정")]
    public float fallThresholdY = -10f;  // Y가 이 값보다 낮으면 리스폰
    public Vector3 respawnPosition = new Vector3(2.5f, 4f, -3f);

    [Header("UI 연결")]
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
        // 낙사 체크
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
        Debug.Log($"피해! 체력: {currentHealth}, 목숨: {currentLives}");
    }

    // 떨어졌을 때(낙사)
    private void FallDeath()
    {
        Debug.Log("낙사");

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

            Debug.Log($"목숨 회복! 현재 목숨 : {currentLives}");
        }
        else
        {
            Debug.Log("목숨이 이미 최대 입니다.");
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

            Debug.Log($"목숨 하나 잃음. 남은 목숨: {currentLives}");

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

        Debug.Log($"리스폰 전 : {transform.position}");
        Debug.Log($"리스폰 목표 : {respawnPosition}");
        Debug.Log($"리스폰 후 : {transform.position}");
    }

    private void GameOver()
    {
        Debug.Log("게임 오버!");
        // 여기에 게임 종료 처리 추가 가능
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
