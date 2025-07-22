using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }
}
