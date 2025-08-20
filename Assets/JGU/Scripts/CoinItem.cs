using TMPro;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public GameObject HeartEffectPrefab;
    public AudioClip EffectSound;

    private float rotationSpeed;
    private Vector3 startPosition;
    private float floatSpeed;
    private float floatHeight;
    private float floatOffset;

    private AudioSource audioSource;

    private void Start()
    {
        rotationSpeed = Random.Range(30f, 90f);

        startPosition = transform.position;
        floatSpeed = Random.Range(1f, 2f);
        floatHeight = Random.Range(0.1f, 0.25f);
        floatOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.World);

        float yOffset = Mathf.Sin(Time.time * floatSpeed + floatOffset) * floatHeight;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
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
        if (TimeAttack.Instance != null)
            TimeAttack.Instance.AddCoin();

        if (HeartEffectPrefab != null)
        {
            GameObject effect = Instantiate(HeartEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        if (EffectSound != null)
        {
            AudioSource.PlayClipAtPoint(EffectSound, transform.position);
        }

        Destroy(gameObject);
    }
}
