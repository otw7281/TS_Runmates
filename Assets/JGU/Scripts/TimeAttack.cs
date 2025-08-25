using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimeAttack : MonoBehaviour
{
    public static TimeAttack Instance;

    [Header("Timer Setting")]
    public float gameTime = 120f;

    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    [Header("Game Over Setting")]
    public Button goRestartButton;
    public Button goHomeButton;
    public Button goQuitButton;

    [Header("Coin Bonus")]
    public int totalCoins = 35;
    public float perfectClearBonus = 10f;

    public GameObject heartContainer;

    private float initialGameTime;
    private bool gameEnded;
    public float elapsedTime;
    private int currentCoins = 0;
    private bool isPerfectClear = false;

    [Header("Perfect Clear UI")]
    public GameObject perfectClearPanel;

    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        initialGameTime = gameTime;
        currentCoins = 0;
        isPerfectClear = false;

        // 버튼 이벤트 등록
        goRestartButton.onClick.AddListener(RestartGame);
        goHomeButton.onClick.AddListener(GoHome);
        goQuitButton.onClick.AddListener(GoQuit);

        UpdateCoinUI();
    }

    private void Update()
    {
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;
            elapsedTime += Time.deltaTime;

            int mintues = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);

            // 음수 방지
            if (gameTime < 0)
            {
                mintues = 0;
                seconds = 0;
                ShowGameOver();
            }
            timerText.text = string.Format("{0:00}:{1:00}", mintues, seconds);

            Debug.Log("Timer: " + timerText.text);

            UpdateTimerColor();
        }

    }

    private void UpdateTimerColor()
    {
        if (gameTime <= 30f)
        {
            timerText.color = Color.red;
        }
    }

    public void AddCoin()
    {
        currentCoins++;

        UpdateCoinUI();

        if (currentCoins >= totalCoins && !isPerfectClear)
        {
            isPerfectClear = true;
            ShowPerfectClearPanel();
        }
    }

    private void ShowPerfectClearPanel()
    {
        if (perfectClearPanel != null)
        {
            perfectClearPanel.SetActive(true);
        }

        StartCoroutine(HidePerfectClearPanel());
    }

    private IEnumerator HidePerfectClearPanel()
    {
        yield return new WaitForSeconds(1f);

        if (perfectClearPanel != null)
        {
            perfectClearPanel.SetActive(false);
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coin : " + currentCoins + "/" + totalCoins;

        if (isPerfectClear)
        {
            coinText.color = Color.yellow;
        }
        else
        {
            coinText.color = Color.white;
        }
    }

    public void ShowGameOver()
    {
        if (gameEnded) return;

        gameEnded = true;
        timerText.text = "00:00";

        // 게임 정지
        Time.timeScale = 0f;

        heartContainer.SetActive(false);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Debug.Log("GameOver");
    }

    public void ShowGameClear()
    {
        if (gameEnded) return;

        gameEnded = true;

        float finalTime = elapsedTime;

        if (isPerfectClear)
        {
            finalTime -= perfectClearBonus;
            finalTime = Mathf.Max(finalTime, 0.1f);
        }

        if (GameData.Instance != null)
            GameData.Instance.SaveGameResult(true, finalTime);

        Time.timeScale = 1f;

        SceneManager.LoadScene(2);

        Debug.Log("GameClear");
    }


    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void GoQuit()
    {
        Application.Quit();
    }

    public void TriggerGameClear()
    {
        ShowGameClear();
    }

    public float GetRemainingTime()
    {
        return gameTime;
    }

    public bool IsGameEnded()
    {
        return gameEnded;
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    public bool IsPerfectClear()
    {
        return isPerfectClear;
    }
}
