using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 dir = new Vector3(0f, 0f, 1f); 
    public float speed = 1f;                        

    public float jumpPower = 3f;                    
    public bool isGrounded = false;                 
    public float gravity = -9.8f;                   
    public float yVelocity = 0f;                    


    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();      
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical"); 

        dir = new Vector3(h, 0f, v);

        dir.Normalize(); 

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        if (characterController.collisionFlags == CollisionFlags.Below)
        {
            isGrounded = true;
            yVelocity = 0f;         
        }

        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            isGrounded = false;         
        }

        yVelocity = yVelocity + gravity * Time.deltaTime;
        dir.y = yVelocity;

        characterController.Move(dir * speed * Time.deltaTime);
    }
}
