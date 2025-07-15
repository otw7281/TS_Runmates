using UnityEngine;

public class CamOrbit : MonoBehaviour
{
   [Header("타겟 설정")]
    public Transform target; // 카메라가 돌 중심 오브젝트
    
    [Header("회전 설정")]
    public float rotationSpeed = 30f; // 회전 속도 (도/초)
    public Vector3 rotationAxis = Vector3.up; // 회전 축 (기본값: Y축)
    
    [Header("카메라 설정")]
    public bool lookAtTarget = true; // 항상 타겟을 바라볼지 여부
    public float initialDistance = 10f; // 초기 거리 설정
    
    private Vector3 offset; // 타겟으로부터의 오프셋
    
    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("타겟이 설정되지 않았습니다. 씬에서 타겟을 지정해주세요.");
            return;
        }
        
        // 초기 오프셋 계산 (타겟으로부터 카메라까지의 벡터)
        offset = transform.position - target.position;
        
        // 만약 카메라가 타겟과 같은 위치에 있다면 기본 오프셋 설정
        if (offset.magnitude < 0.1f)
        {
            offset = new Vector3(0, 0, -initialDistance);
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // 타겟을 중심으로 회전
        RotateAroundTarget();
        
        // 타겟을 바라보도록 설정
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
    
    void RotateAroundTarget()
    {
        // 타겟 위치로 이동
        transform.position = target.position + offset;
        
        // 타겟을 중심으로 회전
        transform.RotateAround(target.position, rotationAxis, rotationSpeed * Time.deltaTime);
        
        // 회전된 후의 새로운 오프셋 계산
        offset = transform.position - target.position;
    }
    
    // 다양한 회전 축을 쉽게 설정할 수 있는 헬퍼 함수들
    [ContextMenu("Y축 회전 (수평)")]
    public void SetHorizontalRotation()
    {
        rotationAxis = Vector3.up;
    }
    
    [ContextMenu("X축 회전 (수직)")]
    public void SetVerticalRotation()
    {
        rotationAxis = Vector3.right;
    }
    
    [ContextMenu("Z축 회전")]
    public void SetZAxisRotation()
    {
        rotationAxis = Vector3.forward;
    }
    
    [ContextMenu("대각선 회전")]
    public void SetDiagonalRotation()
    {
        rotationAxis = new Vector3(1, 1, 0).normalized;
    }
    
    // 런타임에서 회전 방향을 바꾸는 함수
    public void ReverseRotation()
    {
        rotationSpeed = -rotationSpeed;
    }
    
    // 회전 속도를 설정하는 함수
    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }
    
    // 인스펙터에서 확인할 수 있도록 기즈모 그리기
    void OnDrawGizmosSelected()
    {
        if (target == null) return;
        
        // 타겟 위치 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, 0.5f);
        
        // 회전 축 표시
        Gizmos.color = Color.green;
        Gizmos.DrawLine(target.position, target.position + rotationAxis * 3f);
        
        // 카메라와 타겟 연결선
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(target.position, transform.position);
        
        // 회전 반경 표시
        float radius = Vector3.Distance(transform.position, target.position);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(target.position, radius);
    }
}
