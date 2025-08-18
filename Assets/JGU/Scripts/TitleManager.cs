using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject howToPlayPanel;

    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button creditsButton;
    public Button howToPlayButton;

    [Header("Name")]
    public TMP_InputField nameInputField;
    public TextMeshProUGUI warningText;

    public GameObject warningPanel;
    public Button closeWarningPanel;

    [Header("Settings Panel")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button closeSettingsButton;

    [Header("How To Play Panel")]
    public Button closeHowToPlayButton;

    [Header("Credits Panel")]
    public Button closeCreditsButton;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private void Start()
    {
        InitializeUI();
        LoadAudioSettings();
    }

    private void InitializeUI()
    {
        // ¹öÆ° ÀÌº¥Æ® µî·Ï
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(ToggleSettingsPanel);
        creditsButton.onClick.AddListener(ToggleCreditsPanel);
        howToPlayButton.onClick.AddListener(ToggleHowToPlayPanel);

        // ´Ý±â ¹öÆ° ÀÌº¥Æ® µî·Ï
        closeSettingsButton.onClick.AddListener(CloseSettingsPanel);
        closeHowToPlayButton.onClick.AddListener(CloseHowToPlayPanel);
        closeCreditsButton.onClick.AddListener(CloseCreditsPanel);
        closeWarningPanel.onClick.AddListener(CloseWarningPanel);

        // ½½¶óÀÌ´õ ÀÌº¥Æ® µî·Ï
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        warningPanel.SetActive(false);
    }

    private void ToggleCreditsPanel()
    {
        settingsPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(true);
        warningPanel.SetActive(false);
    }

    private void ToggleHowToPlayPanel()
    {
        settingsPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        creditsPanel.SetActive(false);
        warningPanel.SetActive(false);
    }

    private void StartGame()
    {
        string input = nameInputField.text;
        if (string.IsNullOrWhiteSpace(input))
        {
            warningText.text = "ÀÌ¸§À» ÀÔ·ÂÇÏ¼¼¿ä.";
            warningPanel.SetActive(true);
            return;
        }

        string pattern = @"^[a-zA-Z°¡-ÆR]{1,8}$";
        if (!System.Text.RegularExpressions.Regex.IsMatch(input, pattern))
        {
            warningText.text = "ÀÌ¸§Àº ÇÑ±Û ¶Ç´Â ¿µ¾î¸¸, \n1-8ÀÚ±îÁö °¡´ÉÇÕ´Ï´Ù!";
            warningPanel.SetActive(true);
            return;
        }

        if (GameData.Instance != null)
            GameData.Instance.PlayerName = input;

        PlaySFX();
        SceneManager.LoadScene(1);
    }

    private void LoadAudioSettings()
    {
        // PlayerPrefs¿¡¼­ ÀúÀåµÈ ¿Àµð¿À ¼³Á¤ ·Îµå
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.7f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);

        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        if (bgmSource != null) bgmSource.volume = bgmVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;

    }

    private void SetBGMVolume(float value)
    {
        if (bgmSource != null) bgmSource.volume = value;

        // ¼³Á¤ ÀúÀå
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
    }

    private void SetSFXVolume(float value)
    {
        if (sfxSource != null) sfxSource.volume = value;

        // ¼³Á¤ ÀúÀå
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();

        // º¼·ý º¯°æ ½Ã Å×½ºÆ® »ç¿îµå Àç»ý
        PlaySFX();
    }

    private void PlaySFX()
    {
        if (sfxSource != null && sfxSource.clip != null)
        {
            sfxSource.Play();
        }
    }

    private void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    private void CloseHowToPlayPanel()
    {
        howToPlayPanel.SetActive(false);
    }

    private void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }

    private void CloseWarningPanel()
    {
        warningPanel.SetActive(false);
    }
}
