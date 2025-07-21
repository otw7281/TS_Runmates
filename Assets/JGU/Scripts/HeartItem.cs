using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [Header("Item Setting")]
    public float lifeTime = 10f;

    private Vector3 startPosition;
    private float currentLifeTime = 0f;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        currentLifeTime += Time.deltaTime;

        // 수명이 다하면 사라짐
        if(currentLifeTime >= lifeTime)
        {
            DestroyHeart();
            return;
        }
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

        if(playerHealthUI != null)
        {
            playerHealthUI.AddLife();
            Debug.Log("하트 수집! 목숨 회복");

            DestroyHeart();
        }
    }

    private void DestroyHeart()
    {
        Destroy(gameObject);
    }

}
