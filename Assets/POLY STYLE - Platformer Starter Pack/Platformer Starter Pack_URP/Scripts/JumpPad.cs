using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpHeight = 3f;       // 얼마나 높이 띄울지
    public float launchDuration = 0.2f; // 이동 시간

    private void OnTriggerEnter(Collider other)
    {
        // 조건: 이동 대상에 Capsule Collider가 있는지만 확인
        if (other.GetComponent<CapsuleCollider>() != null)
        {
            StartCoroutine(LaunchUpward(other.transform));
        }
    }
    private System.Collections.IEnumerator LaunchUpward(Transform target)
    {
        Vector3 startPos = target.position;
        Vector3 targetPos = startPos + new Vector3(0, jumpHeight, 0);
        float elapsed = 0f;

        while (elapsed < launchDuration)
        {
            target.position = Vector3.Lerp(startPos, targetPos, elapsed / launchDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.position = targetPos;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
 }

