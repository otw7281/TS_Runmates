using UnityEngine;

// 특수 아이템들 사용할 때?
public class SpawnTimer : MonoBehaviour
{
    public float spawnTime = 5f;
    public float curTime = 0f;

    public GameObject spawnObj;
    void Update()
    {
        curTime = curTime + Time.deltaTime;
        if (curTime > spawnTime)
        {
            GameObject go = Instantiate(spawnObj);
            curTime = 0;
        }
    }
}
