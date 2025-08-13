using UnityEngine;
using TMPro;

public class RankEntry_UI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI timeText;

    public void SetEntry(int rank, string name, float time)
    {
        rankText.text = $"{rank}.";
        nameText.text = name;
        timeText.text = $"{time:F2}";

        if (rank == 1)
        {
            rankText.color = new Color32(255, 215, 0, 255);
        }
        else if (rank == 2)
        {
            rankText.color = new Color32(192, 192, 192, 255);
        }
        else if (rank == 3)
        {
            rankText.color = new Color32(255, 153, 0, 255);
        }
        else
        {
            rankText.color = Color.white;
        }
    }
}
