using UnityEngine;

// Ư�� �����۵� ����� ��?
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
