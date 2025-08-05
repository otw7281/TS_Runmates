using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    [Header("Timer Setting")]
    public float gameTime = 30f;

    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    [Header("Game Clear Setting")]
    public Button restartButton;
    public Button homeButton;

    [Header("Game Over Setting")]
    public Button goRestartButton;
    public Button goHomeButton;

    public GameObject heartContainer;

    private float initialGameTime;
    private bool gameEnded = false;

    private void Start()
    {
        initialGameTime = gameTime;

        // 버튼 이벤트 등록
        restartButton.onClick.AddListener(RestartGame);
        homeButton.onClick.AddListener(GoHome);
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

        // 게임 정지
        Time.timeScale = 0f;

        // 클리어 시간 계산 및 표시
        float clearTime = initialGameTime - gameTime;
        int clearMinutes = Mathf.FloorToInt(clearTime / 60);
        int clearSeconds = Mathf.FloorToInt(clearTime % 60);
        int clearMilliseconds = Mathf.FloorToInt((clearTime % 1) * 100);

        heartContainer.SetActive(false);

        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }
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
