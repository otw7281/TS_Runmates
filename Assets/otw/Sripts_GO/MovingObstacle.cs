using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("움직임 설정")]
    public float moveDistance = 2f;         // 위아래 이동 거리
    public float moveSpeed = 2f;            // 이동 속도
    public bool startGoingUp = true;        // 처음에 위로 갈지 여부

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool goingUp;

    private void Start()
    {
        startPosition = transform.position;
        goingUp = startGoingUp;
        SetTargetPosition();
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            goingUp = !goingUp;
            SetTargetPosition();
        }
    }

    private void SetTargetPosition()
    {
        float direction = goingUp ? 1f : -1f;
        targetPosition = startPosition + new Vector3(0f, moveDistance * direction, 0f);
        // 다음 기준 위치를 새 기준으로 업데이트
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }
}
