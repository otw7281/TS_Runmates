using UnityEngine;

// 특수 아이템들 사용할 때?
public class HeartSpawner : MonoBehaviour
{
    public float spawnTime = 7f;
    public float curTime = 0f;

    [Header("Item")]
    public GameObject heartPrefab;

    [Header("스폰 위치")]
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
            // 렌덤한 스폰 포인트 선택
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomIndex].position;

            GameObject heart = Instantiate(heartPrefab, spawnPosition, Quaternion.identity);

            Debug.Log("하트 아이템 스폰");
        } 
    }
}
