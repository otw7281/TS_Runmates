using UnityEngine;


//�÷��� �ٲٰ�ʹ�.
public class ColorChange : MonoBehaviour
{
    Renderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //��(gamobject)�� ������ �ִ� ������(mesh renderer)�� ������ �����
        rend = GetComponent<Renderer>();
        //�������� ������ �ִ� ��Ƽ������ �÷��� �������� �ٲ���.
        rend.material.color = Color.red;
        transform.position = new Vector3(100, 20, -50);
        
        
        //GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.color = Color.purple;
    }
}
