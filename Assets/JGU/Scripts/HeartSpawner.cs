using UnityEngine;

// Ư�� �����۵� ����� ��?
public class HeartSpawner : MonoBehaviour
{
    public float spawnTime = 7f;
    public float curTime = 0f;

    [Header("Item")]
    public GameObject heartPrefab;

    [Header("���� ��ġ")]
    public Transform[] spawnPoints;


    private void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= spawnTime)
        {
            SpawnHeart();
            curTime = 0f;
        }
    }

    private void SpawnHeart()
    {
        if(heartPrefab != null && spawnPoints.Length > 0)
        {
            // ������ ���� ����Ʈ ����
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomIndex].position;

            GameObject heart = Instantiate(heartPrefab, spawnPosition, Quaternion.identity);

            Debug.Log("��Ʈ ������ ����");
        } 
    }
}
