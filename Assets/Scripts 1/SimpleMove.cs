using UnityEngine;
//���� ������ �������� �̵��ϰ� �ʹ�.
//�������� ���� �ٲ��.
public class SimpleMove : MonoBehaviour
{
    // string : "����" , int : ���� (�Ҽ��� x )
    // float : �Ǽ� (�Ҽ���o), Vector3 : ���� (x,y,z)
    public Vector3 dir = new Vector3(0, 0, 1);
    public float speed = 1f; // m/s

    public float jumpPower = 5f; //����(����) ��
    public bool isGrounded = false;  // boolean : true (1), false(0)
    public float gravity = -9.8f; //�߷�
    public float yVelocity = 0; // y�� ��ȭ

    CharacterController controller;    //���� ���� : ��׸�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>(); //�׸��� �����͸� ���
    }

    // Update is called once per frame
    void Update()
    {
        //���� �Է��� �������� �̵��ϰ� �ʹ�.
        float h = Input.GetAxis("Horizontal"); // a (-1) �� d (+1)�� ������
        float v = Input.GetAxis("Vertical"); // s (-1) �� w (+1)�� ������

        dir = new Vector3(h,0,v);

        //����ȭ Normalize = ������ �����ϸ鼭 ������ ���̸� 1�� ���� 
        dir.Normalize ();

        //(ĳ���� ��Ʈ�ѷ���) �ٴڿ� ����ִ°� �³�?
        if (controller.collisionFlags == CollisionFlags.Below)
        {
            isGrounded = true;
            yVelocity = 0; //�ٴڿ� ������ �Ʒ��� ���������� 0
        }

        //�ٴڿ� ����ִ°� �°�, ����Ű�� ������ �´ٸ�,
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            isGrounded = false;
        }
        //�߷��� �����ض�
        yVelocity = yVelocity +gravity * Time.deltaTime;
        dir.y = yVelocity;

        //��ġ�� ����ؼ� �ٲ۴�.
        //P = p0 + vt(����,����)(�ð�)
        //transform.position = transform.position + dir * speed * Time.deltaTime;  //���
        controller.Move(dir * speed * Time.deltaTime);
     
    }
}
