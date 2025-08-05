using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
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
        // ��ư �̺�Ʈ ���
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(ToggleSettingsPanel);
        creditsButton.onClick.AddListener(ToggleCreditsPanel);
        howToPlayButton.onClick.AddListener(ToggleHowToPlayPanel);

        // �ݱ� ��ư �̺�Ʈ ���
        closeSettingsButton.onClick.AddListener(CloseSettingsPanel);
        closeHowToPlayButton.onClick.AddListener(CloseHowToPlayPanel);
        closeCreditsButton.onClick.AddListener(CloseCreditsPanel);

        // �����̴� �̺�Ʈ ���
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void ToggleCreditsPanel()
    {
        settingsPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    private void ToggleHowToPlayPanel()
    {
        settingsPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    private void StartGame()
    {
        PlaySFX();
        SceneManager.LoadScene(1);
    }

    private void LoadAudioSettings()
    {
        // PlayerPrefs���� ����� ����� ���� �ε�
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

        // ���� ����
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
    }

    private void SetSFXVolume(float value)
    {
        if (sfxSource != null) sfxSource.volume = value;

        // ���� ����
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();

        // ���� ���� �� �׽�Ʈ ���� ���
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
}
