using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    public GameObject rankingPanel;
    public GameObject rankentryPrefab;
    public Transform rankingContent;

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerTimeText;

    public Button HomeButton;
    public Button RestartButton;
    public Button QuitButton;

    private void Start()
    {
        string name = GameData.Instance.PlayerName;
        float time = GameData.Instance.ClearTime;

        playerNameText.text = $"{name}";
        playerTimeText.text = $"{time:F2}";
        ShowRanking();
        UpdateRankingUI();
    }
    public void ShowRanking()
    {
        List<(string, float)> rankList = GameData.Instance.GetRankingList();

        for (int i = 0; i < rankList.Count; i++)
        {
            GameObject entry = Instantiate(rankentryPrefab, rankingContent);

            RankEntry_UI entry_UI = entry.GetComponent<RankEntry_UI>();
            entry_UI.SetEntry(i + 1, rankList[i].Item1, rankList[i].Item2);

        }

    }

    public void UpdateRankingUI()
    {
        // 1) 기존 엔트리 제거
        for (int i = rankingContent.childCount - 1; i >= 0; i--)
            Destroy(rankingContent.GetChild(i).gameObject);

        // 2) GameData에서 최신 랭킹 읽어와 생성
        var rankList = GameData.Instance.GetRankingList();
        for (int i = 0; i < rankList.Count; i++)
        {
            var entry = Instantiate(rankentryPrefab, rankingContent);
            var ui = entry.GetComponent<RankEntry_UI>();
            ui.SetEntry(i + 1, rankList[i].Item1, rankList[i].Item2);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
