using UnityEngine;

public class blade : MonoBehaviour
{
    public float speed = 2f;           // �̵� �ӵ�
    public float moveDistance = 2f;    // �պ� �Ÿ�

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
            // x�� + �������� �̵�
            currentPos.x += moveStep;

            if (currentPos.x >= startPos.x + moveDistance)
                movingForward = false;
        }
        else
        {
            // x�� - �������� �̵�
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
            Debug.Log("�÷��̾�� ���ظ� �������ϴ�!");
            // �ʿ�� ü�� ���� ���� �߰�
        }
    }
}
