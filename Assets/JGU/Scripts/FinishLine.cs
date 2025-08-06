using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public string playerTag = "Player";
    public TimeAttack timeAttack;

    private bool hasTriggered = false;

    private void Start()
    {
        if (timeAttack == null)
        {
            timeAttack = FindAnyObjectByType<TimeAttack>();
        }

        Collider col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !hasTriggered && timeAttack != null && !timeAttack.IsGameEnded())
        {
            TriggerGameClear();
        }
    }

    private void TriggerGameClear()
    {
        hasTriggered = true;
        Debug.Log("게임 클리어!");

        if (timeAttack != null)
        {
            timeAttack.TriggerGameClear();
        }
    }

    public void ResetTrigger()
    {
        hasTriggered = false;
    }

    public bool HasBeenTriggered()
    {
        return hasTriggered;
    }
}
