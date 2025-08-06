using UnityEngine;

public class SpinLog : MonoBehaviour
{
    public Vector3 rotateAxis = Vector3.right; // Y축 기준 회전
    public float rotateSpeed = 360f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAxis * rotateSpeed * Time.deltaTime);
    }
}
