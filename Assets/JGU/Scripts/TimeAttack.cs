using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeAttack : MonoBehaviour
{
    [Header("Timer Setting")]
    public float gameTime = 30f;

    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    private bool gameEnded = false;

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
        gameEnded = true;
        timerText.text = "00:00";

        // 게임 정지
        Time.timeScale = 0f;

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Debug.Log("GameOver");
    }
}
