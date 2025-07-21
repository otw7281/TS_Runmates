using UnityEngine;

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

    private void Start()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
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
        }

        StartCoroutine(InvincibilityCoroutine());
        Debug.Log($"����! ü��: {currentHealth}, ���: {currentLives}");
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
            Debug.Log($"��� �ϳ� ����. ���� ���: {currentLives}");
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
