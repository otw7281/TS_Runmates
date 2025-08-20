using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    public static TimeAttack Instance;

    [Header("Timer Setting")]
    public float gameTime = 30f;

    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    [Header("Game Over Setting")]
    public Button goRestartButton;
    public Button goHomeButton;

    [Header("Coin Bonus")]
    public int totalCoins = 30;
    public float coinBonusSeconds = 10f;

    public GameObject heartContainer;

    private float initialGameTime;
    private bool gameEnded;
    public float elapsedTime;
    private int currentCoins = 0;
    private bool bonusApplied = false;


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

        // 버튼 이벤트 등록
        goRestartButton.onClick.AddListener(RestartGame);
        goHomeButton.onClick.AddListener(GoHome);
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

        if (currentCoins >= totalCoins && !bonusApplied)
            ApplyCoinBonus();
    }

    private void ApplyCoinBonus()
    {
        bonusApplied = true;
        gameTime += coinBonusSeconds;
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
        if (bonusApplied)
            finalTime += coinBonusSeconds;

        if (GameData.Instance != null)
            GameData.Instance.SaveGameResult(true, elapsedTime);

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

    public bool IsBonusApplied()
    {
        return bonusApplied;
    }
}
