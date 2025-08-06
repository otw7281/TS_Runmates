using UnityEngine;

public class movingtrap : MonoBehaviour
{
    public float speed = 2f;              // �̵� �ӵ�
    public float detectionRange = 10f;    // ���� �Ÿ�
    public float damage = 10f;            // �浹 �� ���� (����׿�)

    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �÷��̾ �����ϸ� ���� ���
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (player == null) return;

        Vector3 directionToPlayer = player.position - transform.position;

        // Z�� �������θ� ���󰡰� ����
        directionToPlayer = new Vector3(0, 0, directionToPlayer.z);

        if (Mathf.Abs(directionToPlayer.z) < detectionRange)
        {
            // ����ȭ �� �̵�
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���⼭ ü�� �ý��ۿ� ���ظ� �� �� ����
            Debug.Log("�÷��̾�� ���ظ� �������ϴ�: " + damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
