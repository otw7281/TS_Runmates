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

    public GameObject heartContainer;

    private float initialGameTime;
    private bool gameEnded;
    public float elapsedTime;


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
        if (gameTime <= 5f)
        {
            timerText.color = Color.red;
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
}
