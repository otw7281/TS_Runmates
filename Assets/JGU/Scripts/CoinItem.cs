using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class CoinItem : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public static int coinCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinCount++;

            if(coinText != null)
            {
                coinText.text = "Coin: " + coinCount;
            }

            Destroy(gameObject);
        }
    }
}
