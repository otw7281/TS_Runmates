using UnityEngine;

public class FuncOperation : MonoBehaviour
{
    //���� Ÿ�� (int) ������ (number1) = �ʱⰪ(0);
    public int number1 = 3; //public ����
    private int number2 = 2; //private �����
    int result = 0;

    //��ȯ��(void) �Լ��̸� (Add) () {}
    int Add()  //�����Լ� 
    {
        result = number1 + number2;
        return result;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print(Add());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
