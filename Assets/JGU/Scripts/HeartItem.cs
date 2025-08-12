using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [Header("Item Setting")]
    public float lifeTime = 10f;

    [Header("하트 애니메이션 설정")]
    public float floatAmplitude = 0.3f;
    public float floatSpeed = 1.5f;
    public float pulseScale = 0.1f;
    public float pulseSpeed = 3f;

    [Header("아이템 효과")]
    public AudioClip collectSFX;
    public ParticleSystem collectEffect;

    private Vector3 startPosition;
    private float currentLifeTime = 0f;
    private Vector3 originalScale;
    private AudioSource audioSource;

    private void Start()
    {
        startPosition = transform.position;
        originalScale = transform.localScale;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        currentLifeTime += Time.deltaTime;

        // 수명이 다하면 사라짐
        if(currentLifeTime >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseScale;
        transform.localScale = originalScale * pulse;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectHeart(other.gameObject);
        }
    }

    private void CollectHeart(GameObject player)
    {
        PlayerHealthUI playerHealthUI = player.GetComponent<PlayerHealthUI>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.AddLife();
            Debug.Log("하트 수집! 목숨 회복");
        }

        if (collectSFX != null && audioSource != null)
            AudioSource.PlayClipAtPoint(collectSFX, transform.position);

        if (collectEffect != null)
        {
            ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, 2f);
        }

        Destroy(gameObject);
    }


}
