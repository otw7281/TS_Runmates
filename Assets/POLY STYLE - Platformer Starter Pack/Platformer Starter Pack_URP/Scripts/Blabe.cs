using UnityEngine;

public class Blabe : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Y�� ���� ȸ��
    public float rotationSpeed = 360f;        // 1�ʿ� 360��

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
