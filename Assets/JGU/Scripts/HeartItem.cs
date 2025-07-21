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

        // ������ ���ϸ� �����
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
            Debug.Log("��Ʈ ����! ��� ȸ��");

            DestroyHeart();
        }
    }

    private void DestroyHeart()
    {
        Destroy(gameObject);
    }

}
