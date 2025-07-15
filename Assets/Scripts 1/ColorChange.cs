using UnityEngine;


//컬러를 바꾸고싶다.
public class ColorChange : MonoBehaviour
{
    Renderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //내(gamobject)가 가지고 있는 렌더러(mesh renderer)를 변수에 담아줘
        rend = GetComponent<Renderer>();
        //렌더러가 가지고 있는 머티리얼의 컬러를 빨강으로 바꿔줘.
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
