using UnityEngine;

public class PlarForm_InV : MonoBehaviour
{
    public float visibleTime = 2f;    // 발판이 보이는 시간
    public float invisibleTime = 2f;  // 발판이 사라지는 시간

    private Renderer platformRenderer;
    private BoxCollider boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 컴포넌트 가져오기
        platformRenderer = GetComponent<Renderer>();
        boxCollider = GetComponent<BoxCollider>();

        // 사라졌다 나타나는 반복 실행
        StartCoroutine(PlatformCycle());
    }

    private System.Collections.IEnumerator PlatformCycle()
    {
        while (true)
        {
            // 발판 나타나기
            platformRenderer.enabled = true;
            boxCollider.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            // 발판 사라지기
            platformRenderer.enabled = false;
            boxCollider.enabled = false;
            yield return new WaitForSeconds(invisibleTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
