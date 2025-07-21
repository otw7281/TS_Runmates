using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthUI playerHealth = other.GetComponent<PlayerHealthUI>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
                Debug.Log("Àå¾Ö¹°¿¡ ºÎµúÈû!");
            }
        }
    }
}
