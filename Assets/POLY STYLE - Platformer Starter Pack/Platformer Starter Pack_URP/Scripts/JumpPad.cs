using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 점프대에 닿았을 때
        PlayerMove player = other.GetComponent<PlayerMove>();
        if (player != null)
        {
            // 플레이어의 yVelocity를 바로 변경하여 점프시킴
            player.SetJumpVelocity(jumpPower);
        }
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

