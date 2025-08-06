using UnityEngine;

public class blade : MonoBehaviour
{
    public float speed = 2f;           // 이동 속도
    public float moveDistance = 2f;    // 왕복 거리

    private Vector3 startPos;
    private bool movingForward = true;

    public Vector3 rotationAxis = Vector3.left; // 
    public float rotationSpeed = 360f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveStep = speed * Time.deltaTime;
        Vector3 currentPos = transform.position;

        if (movingForward)
        {
            // x축 + 방향으로 이동
            currentPos.x += moveStep;

            if (currentPos.x >= startPos.x + moveDistance)
                movingForward = false;
        }
        else
        {
            // x축 - 방향으로 이동
            currentPos.x -= moveStep;

            if (currentPos.x <= startPos.x - moveDistance)
                movingForward = true;
        }

        transform.position = currentPos;

        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어에게 피해를 입혔습니다!");
            // 필요시 체력 감소 로직 추가
        }
    }
}
