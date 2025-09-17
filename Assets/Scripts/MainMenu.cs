using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject MainMenuUI;
    [SerializeField] private TextMeshProUGUI musicToggleText;
    [SerializeField] private AudioSource music;
    public static event Action<bool> OnHighSetting;

    void Start()
    {
        Application.targetFrameRate = 120;
        AudioManager.Instance.PlayMusic(AudioManager.Instance.audioClips.MusicMenu);
        QualitySettings.SetQualityLevel(0, true);
        PlayerPrefs.SetInt("musicSetting",1);
        PlayerPrefs.Save();
    }

    public void LoadLevel1()
    {
        StartCoroutine(fade());
        IEnumerator fade()
        {
            AudioManager.Instance.MusicOff();
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.GamePlay);
            fadeOut.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene("Level1");
        }
    }

    public void QualitySettingLow()
    {
        QualitySettings.SetQualityLevel(0, true);
        OnHighSetting?.Invoke(false);
    }

    public void QualitySettingHigh()
    {
        QualitySettings.SetQualityLevel(1, true);
        OnHighSetting?.Invoke(true);
    } 

    public void MusicToggleText()
    {
        AudioManager.Instance.ToggleMusic();
        if (music.mute)
        {
            musicToggleText.text = "Music off";
            PlayerPrefs.SetInt("musicSetting", 0);
            PlayerPrefs.Save();
        }
        else
        {
            musicToggleText.text = "Music on";
            PlayerPrefs.SetInt("musicSetting", 1);
            PlayerPrefs.Save();
        }
    }

    public void ExitApp()
    {
        StartCoroutine(fade());
        IEnumerator fade()
        {
            AudioManager.Instance.MusicOff();
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.GameQuit);
            MainMenuUI.SetActive(false);
            fadeOut.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            Application.Quit();
        }
    }
}
