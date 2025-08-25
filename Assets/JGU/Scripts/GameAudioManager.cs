using UnityEngine;
using UnityEngine.UI;

public class GameAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource bgmSource;

    [Header("UI Sliders")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button settingsButton;
    public GameObject settingsPanel;
    public Button closeSettingsButton;

    private float sfxVolume;

    private void Start()
    {
        AudioSettings();
        InitializeSliders();
        InitializeButtons();
    }

    private void AudioSettings()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);

        if (bgmSource != null)
            bgmSource.volume = bgmVolume;
    }

    private void InitializeSliders()
    {
        if (bgmSlider != null)
        {
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    private void InitializeButtons()
    {
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettingsPanel);

        if (closeSettingsButton != null)
            closeSettingsButton.onClick.AddListener(CloseSettingsPanel);
    }

    public void OpenSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void CloseSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);

            if (TimeAttack.Instance != null && !TimeAttack.Instance.IsGameEnded())
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
            bgmSource.volume = volume;

        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;

        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void PlaySFXAtPoint(AudioClip clip, Vector3 position)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, position, sfxVolume);
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
