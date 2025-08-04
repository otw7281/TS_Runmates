using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;              // ���� ��� (�÷��̾�)
    public Vector3 offset = new Vector3(0, 3, -5); // ī�޶� ��ġ ������
    public float rotateSpeed = 5f;        // ���콺 ȸ�� ����
    public float minYAngle = -35f;
    public float maxYAngle = 60f;

    private float currentX = 0f;
    private float currentY = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // ���콺 �Է����� ȸ���� ����
        currentX += Input.GetAxis("Mouse X") * rotateSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotateSpeed;
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // ī�޶� ȸ�� �� ��ġ ���
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // ī�޶� ��ġ ����
        transform.position = desiredPosition;

        // ĳ���͸� �ٶ󺸰� ����
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
