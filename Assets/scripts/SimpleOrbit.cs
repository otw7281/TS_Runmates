using UnityEngine;

public class SimpleOrbit : MonoBehaviour
{
    //메인카메라
    public Camera mainCamera;

    //회전축
    public Vector3 pivotAxis = Vector3.up;

    //회전속도
    public float speed = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if(mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //회전축을 기준으로 회전
        transform.Rotate(pivotAxis, speed * Time.deltaTime);
        //카메라가 나를 바라보도록 설정
        mainCamera.transform.LookAt(transform.position);
    }
}
