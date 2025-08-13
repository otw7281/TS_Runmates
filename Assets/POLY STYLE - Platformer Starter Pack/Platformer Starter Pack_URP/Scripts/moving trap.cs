using UnityEngine;

public class SideAttackObstacle : MonoBehaviour
{
    [Header("움직임 설정")]
    public float attackDistance = 2f;
    public float attackSpeed = 5f;
    public float returnSpeed = 2f;

    [Header("감지 범위 설정")]
    public Vector3 detectionOffset = new Vector3(0f, 5f, 0f); // 기준 위치에서 앞
    public Vector3 detectionSize = new Vector3(4f, 2f, 2f);   // 감지 박스 크기

    private Vector3 startPosition;
    private Vector3 attackPosition;
    private bool isAttacking = false;
    private bool isReturning = false;

    private void Start()
    {
        startPosition = transform.position;
        attackPosition = startPosition + Vector3.forward * attackDistance;
    }

    private void Update()
    {
        // 플레이어 감지 (공격 중/복귀 중 아닐 때만)
        if (!isAttacking && !isReturning)
        {
            Vector3 center = transform.position + detectionOffset;
            Collider[] hits = Physics.OverlapBox(center, detectionSize * 0.5f, Quaternion.identity);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    PlayerHealth player = hit.GetComponent<PlayerHealth>();
                    if (player != null)
                        

                    isAttacking = true;
                    break;
                }
            }
        }

        // 이동 처리
        if (isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackPosition, attackSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, attackPosition) < 0.01f)
            {
                isAttacking = false;
                isReturning = true;
            }
        }
        else if (isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                isReturning = false;
            }
        }
    }


}
