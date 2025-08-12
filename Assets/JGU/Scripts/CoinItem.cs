using TMPro;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    [Header("코인 애니메이션 설정")]
    public float rotationSpeed = 180f;
    public float floatAmplitude = 0.5f;
    public float floatSpeed = 2f;

    [Header("아이템 효과")]
    public AudioClip collectSFX;
    public ParticleSystem collectEffect;

    public TextMeshProUGUI coinText;
    public static int coinCount = 0;

    private Vector3 startPosition;
    private float bobOffset;
    private AudioSource audioSource;

    private void Start()
    {
        startPosition = transform.position;

        bobOffset = Random.Range(0f, Mathf.PI * 2f);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null )
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed + bobOffset) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem(other.gameObject);
        }
    }

    private void CollectItem(GameObject player)
    {
        coinCount++;

        if (coinText != null)
        {
            coinText.text = "Coin: " + coinCount;
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
