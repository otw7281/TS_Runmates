using UnityEngine;

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

    private void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
    }

    private void Update()
    {
        // 낙사 체크
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
        }

        StartCoroutine(InvincibilityCoroutine());
        Debug.Log($"피해! 체력: {currentHealth}, 목숨: {currentLives}");
    }

    private void TakeLife()
    {
        currentLives--;
        currentHealth = maxHealth;

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Debug.Log($"목숨 하나 잃음. 남은 목숨: {currentLives}");
        }
    }

    private void Respawn()
    {
        transform.position = respawnPosition;
        Debug.Log("플레이어 리스폰됨");
    }

    private void GameOver()
    {
        Debug.Log("게임 오버!");
        // 여기에 게임 종료 처리 추가 가능
        Time.timeScale = 0f;
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}
