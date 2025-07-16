using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float mouseSpeed = 200f;

    float mX = 0f;
    float mY = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mX = mX + mouseX * mouseSpeed * Time.deltaTime;
        mY = mY + mouseY * mouseSpeed * Time.deltaTime;

        mY = Mathf.Clamp(mY, -70, 70);      

        transform.eulerAngles = new Vector3(-mY, mX, 0);
    }
}
