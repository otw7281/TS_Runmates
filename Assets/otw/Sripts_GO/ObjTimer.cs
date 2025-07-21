using UnityEngine;

public class ObjTimer : MonoBehaviour
{
    public GameObject block;
    public float spawnTime = 2;
    public float curTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //타이머 생성
        curTime += Time.deltaTime;
        if (curTime > spawnTime)
        {

        }
    }
}
