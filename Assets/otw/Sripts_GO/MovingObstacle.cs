using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("������ ����")]
    public float moveDistance = 2f;         // ���Ʒ� �̵� �Ÿ�
    public float moveSpeed = 2f;            // �̵� �ӵ�
    public bool startGoingUp = true;        // ó���� ���� ���� ����

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
        // ���� ���� ��ġ�� �� �������� ������Ʈ
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
