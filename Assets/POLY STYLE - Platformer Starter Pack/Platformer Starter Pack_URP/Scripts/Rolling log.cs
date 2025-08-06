using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class RollingLog : MonoBehaviour
{
    [Header("굴러가는 힘")]
    public float rollForce = 10f;

    [Header("정지 후 리셋까지 대기 시간")]
    public float resetDelay = 3f;

    [Header("정지로 판단할 속도 기준")]
    public float stopThreshold = 0.05f;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool hasRolled = false;
    private float stopTimer = 0f;
    private bool isRolling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb.useGravity = true;
        rb.isKinematic = false;

        MeshCollider meshCol = GetComponent<MeshCollider>();
        meshCol.convex = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasRolled && collision.gameObject.CompareTag("Ground"))
        {
            hasRolled = true;
            Roll();
        }
    }

    private void FixedUpdate()
    {
        if (hasRolled)
        {
            // 속도가 stopThreshold 이하라면 멈춘 것으로 간주
            if (rb.linearVelocity.magnitude < stopThreshold)
            {
                stopTimer += Time.fixedDeltaTime;

                if (stopTimer >= resetDelay)
                {
                    ResetLog();
                }
            }
            else
            {
                // 다시 움직이면 타이머 초기화
                stopTimer = 0f;
            }
        }
    }
    private void Roll()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // X축 방향으로 힘 가하기
        rb.AddForce(Vector3.right * rollForce, ForceMode.Impulse);
    }

    private void ResetLog()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        transform.rotation = startRotation;

        hasRolled = false;
        stopTimer = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
