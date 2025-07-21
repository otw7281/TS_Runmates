using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Item : MonoBehaviour
{
    public TextMeshProUGUI coin;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
