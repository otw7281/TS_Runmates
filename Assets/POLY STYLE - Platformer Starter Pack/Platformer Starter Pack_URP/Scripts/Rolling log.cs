using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class RollingLog : MonoBehaviour
{
    [Header("�������� ��")]
    public float rollForce = 20f;

    [Header("���� ��� �ð�")]
    public float resetDelay = 3f;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool hasRolled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb.useGravity = true;
        rb.isKinematic = false;

        // MeshCollider�� Convex �� üũ!
        MeshCollider meshCol = GetComponent<MeshCollider>();
        meshCol.convex = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasRolled && collision.gameObject.CompareTag("Ground"))
        {
            hasRolled = true;
            Roll();
            Invoke(nameof(ResetLog), resetDelay);
        }
    }

    private void Roll()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // X�� �������� �� ���ϱ�
        rb.AddForce(Vector3.right * rollForce, ForceMode.Impulse);
    }

    private void ResetLog()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        transform.rotation = startRotation;

        hasRolled = false;
    }


// Update is called once per frame
void Update()
    {
        
    }
}
