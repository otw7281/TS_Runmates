using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;              // 따라갈 대상 (플레이어)
    public Vector3 offset = new Vector3(0, 3, -5); // 카메라 위치 오프셋
    public float rotateSpeed = 5f;        // 마우스 회전 감도
    public float minYAngle = -35f;
    public float maxYAngle = 60f;

    private float currentX = 0f;
    private float currentY = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // 마우스 입력으로 회전값 조절
        currentX += Input.GetAxis("Mouse X") * rotateSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotateSpeed;
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // 카메라 회전 및 위치 계산
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // 카메라 위치 설정
        transform.position = desiredPosition;

        // 캐릭터를 바라보게 설정
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
