using UnityEngine;

public class HeartItem : MonoBehaviour
{

    [Header("하트 애니메이션 설정")]
    public float floatAmplitude = 0.3f;
    public float floatSpeed = 1.5f;
    public float pulseScale = 0.1f;
    public float pulseSpeed = 3f;

    [Header("아이템 효과")]
    public AudioClip collectSFX;
    public GameObject collectEffect;

    private Vector3 startPosition;
    private Vector3 originalScale;
    private AudioSource audioSource;

    private GameAudioManager audioManager;
    private void Start()
    {
        startPosition = transform.position;
        originalScale = transform.localScale;

        audioManager = FindAnyObjectByType<GameAudioManager>();
    }

    private void Update()
    {

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

        if (collectSFX != null)
        {
            if (audioManager != null)
            {
                audioManager.PlaySFXAtPoint(collectSFX, transform.position);
            }
            else
            {
                float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
                AudioSource.PlayClipAtPoint(collectSFX, transform.position, sfxVolume);
            }
        }

        if (collectEffect != null)
        {
            GameObject effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        Destroy(gameObject);
    }


}
