using UnityEngine;

public class PlarForm_InV2 : MonoBehaviour
{
    public float visibleTime = 2f;    // ������ ���̴� �ð�
    public float invisibleTime = 2f;  // ������ ������� �ð�

    private Renderer platformRenderer;
    private BoxCollider boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ������Ʈ ��������
        platformRenderer = GetComponent<Renderer>();
        boxCollider = GetComponent<BoxCollider>();

        // ������� ��Ÿ���� �ݺ� ����
        StartCoroutine(PlatformCycle());
    }

    private System.Collections.IEnumerator PlatformCycle()
    {
        while (true)
        {
            // ���� �������
            platformRenderer.enabled = false;
            boxCollider.enabled = false;
            yield return new WaitForSeconds(invisibleTime);

            // ���� ��Ÿ����
            platformRenderer.enabled = true;
            boxCollider.enabled = true;
            yield return new WaitForSeconds(visibleTime);

           
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
