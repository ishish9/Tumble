using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    ActionMap_1 actionsWrapper2;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private TextMeshProUGUI musicToggleText;
    [SerializeField] private AudioSource music;
    public static event Action<bool> OnHighSetting;

    public bool MenuDisabled;

    private void Awake()
    {
        actionsWrapper2 = new ActionMap_1();
        actionsWrapper2.Menu.MenuButton.performed += OnMenuActivated;
    }

    private void OnEnable()
    {
        actionsWrapper2.Menu.Enable();
        Manager.OnMenuToggle += OnMenuActivatedButton;
        Manager.OnMenuDisable += MenuDisableEnable;
    }

    private void OnDisable()
    {
        actionsWrapper2.Menu.Disable();
        Manager.OnMenuToggle -= OnMenuActivatedButton;
        Manager.OnMenuDisable -= MenuDisableEnable;
    }

    private void MenuDisableEnable(bool status)
    {
        MenuDisabled = status;
    }

    public void OnMenuActivatedButton()
    {
        if (MenuDisabled == false)
        {      
        // Activates Menu //
        if (menuUI.activeSelf == false)
        {
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.MenuOpen);
            AudioManager.Instance.MasterVolumeControl(0.4f);
            menuUI.gameObject.SetActive(true);
        }


        // Deactivates Menu //        
        else
        {
            if (PlayerPrefs.GetInt("musicSetting") == 0)
            {
                AudioManager.Instance.MusicOff();
            }
            else
            {
                AudioManager.Instance.MusicOn();
            }
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.MenuOpen);
            AudioManager.Instance.MasterVolumeControl(1.0f);
            menuUI.gameObject.SetActive(false);
            settingsUI.gameObject.SetActive(false);

            }
        }
    }

    public void OnMenuActivated(InputAction.CallbackContext context)
    {
        if (MenuDisabled == false)
        {
            // Activates Menu //
            if (menuUI.activeSelf == false)
            {
                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.MenuOpen);
                AudioManager.Instance.MasterVolumeControl(0.4f);
                menuUI.gameObject.SetActive(true);
                settingsUI.gameObject.SetActive(false);
            }

            // Deactivates Menu //        
            else
            {
                if (PlayerPrefs.GetInt("musicSetting") == 0)
                {
                    AudioManager.Instance.MusicOff();
                }
                else
                {
                    AudioManager.Instance.MusicOn();
                }
                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.MenuOpen);
                AudioManager.Instance.MasterVolumeControl(1.0f);
                menuUI.gameObject.SetActive(false);
                settingsUI.gameObject.SetActive(false);

            }
        }
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

    public void QualitySettingLow()
    {
        QualitySettings.SetQualityLevel(0, true);
        PlayerPrefs.SetInt("QualitySetting", 0);
        PlayerPrefs.Save();
        OnHighSetting?.Invoke(false);
    }

    public void QualitySettingHigh()
    {
        QualitySettings.SetQualityLevel(1, true);
        PlayerPrefs.SetInt("QualitySetting", 1);
        PlayerPrefs.Save();
        OnHighSetting?.Invoke(true);
    }

    public void ExitApp()
    {
        StartCoroutine(fade());
        IEnumerator fade()
        {
            AudioManager.Instance.MusicOff();
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.GameQuit);
            fadeOut.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            Application.Quit();
        }
    }
}