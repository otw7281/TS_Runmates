using UnityEngine;

public class movingtrap : MonoBehaviour
{
    public float speed = 2f;              // 이동 속도
    public float detectionRange = 10f;    // 감지 거리
    public float damage = 10f;            // 충돌 시 피해 (디버그용)

    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 플레이어가 존재하면 방향 계산
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (player == null) return;

        Vector3 directionToPlayer = player.position - transform.position;

        // Z축 방향으로만 따라가게 제한
        directionToPlayer = new Vector3(0, 0, directionToPlayer.z);

        if (Mathf.Abs(directionToPlayer.z) < detectionRange)
        {
            // 정규화 후 이동
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 여기서 체력 시스템에 피해를 줄 수 있음
            Debug.Log("플레이어에게 피해를 입혔습니다: " + damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
