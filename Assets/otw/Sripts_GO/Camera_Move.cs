using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public float mouseSpd = 2000; // ���콺 ����

    float mx = 0f; // ���콺 x���� ����
    float my = 0f; // ���콺 y���� ����
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���콺�� �����ӿ� ���� ���� �޾ƿ���
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");
        //P = p0 + vt
        mx = mx + mouse_x * mouseSpd * Time.deltaTime;
        my = my + mouse_y * mouseSpd * Time.deltaTime;
        //���� �����Ѵ� Mathf.Clamp(������ ����, min, max)
        my = Mathf.Clamp(my, -90, 90);
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
