using UnityEngine;

public class Blabe : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Y축 기준 회전
    public float rotationSpeed = 360f;        // 1초에 360도

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
