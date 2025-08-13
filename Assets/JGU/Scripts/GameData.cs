using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public string PlayerName;
    public float ClearTime;
    public bool isGameClear;

    private List<(string, float)> rankingList = new List<(string, float)>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGameResult(bool isClear, float time)
    {
        isGameClear = isClear;
        ClearTime = time;

        if (isClear)
        {
            SaveRanking(PlayerName, ClearTime);
        }
    }

    private void SaveRanking(string name, float time)
    {
        LoadRanking();

        rankingList.Add((name, time));
        rankingList = rankingList.OrderBy(r => r.Item2).Take(5).ToList();

        for (int i = 0; i < rankingList.Count; i++)
        {
            PlayerPrefs.SetString($"RankName_{i}", rankingList[i].Item1);
            PlayerPrefs.SetFloat($"RankTime_{i}", rankingList[i].Item2);
        }

        PlayerPrefs.Save();
    }

    private void LoadRanking()
    {
        rankingList.Clear();

        for(int i = 0; i < 5; i++)
        {
            string name = PlayerPrefs.GetString($"RankName_{i}", "");
            float time = PlayerPrefs.GetFloat($"RankTime_{i}", float.MaxValue);

            if (!string.IsNullOrEmpty(name))
            {
                rankingList.Add((name, time));
            }
        }
    }

    public List<(string, float)> GetRankingList()
    {
        LoadRanking();
        return rankingList;
    }
}
