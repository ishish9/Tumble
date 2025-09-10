using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    ActionMap_1 actionsWrapper2;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject PostPross;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private Button lowButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button highButton;
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
            if (PlayerPrefs.GetInt("musicOnOff") == 0)
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
            }

            // Deactivates Menu //        
            else
            {
                if (PlayerPrefs.GetInt("musicOnOff") == 0)
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
            }
        }
    } 

    // Highlights button of currently selected graphics settings.
    public void SelectGraphicsButton()
    {
        if (PlayerPrefs.GetInt("PlayerHasSetQualityLevel") == 1)
        {
            switch (PlayerPrefs.GetInt("QualitySetting"))
            {
                case 0:
                    lowButton.Select();
                    break;
                case 1:
                    mediumButton.Select();
                    break;
                case 2:
                    highButton.Select();
                    break;               
            }
        }
        else
        {
            mediumButton.Select();
        }
    }

    public void LowSetting()
    {
        PostPross.SetActive(false);
        QualitySettings.SetQualityLevel(0, true);
        PlayerPrefs.SetInt("PlayerHasSetQualityLevel", 1);
        PlayerPrefs.SetInt("QualitySetting", 0);
        PlayerPrefs.Save();
    }

    public void MediumSetting()
    {
        PostPross.SetActive(true);
        QualitySettings.SetQualityLevel(1, true);
        PlayerPrefs.SetInt("PlayerHasSetQualityLevel", 1);
        PlayerPrefs.SetInt("QualitySetting", 1);
        PlayerPrefs.Save();
    }

    public void HighSetting()
    {
        PostPross.SetActive(true);
        QualitySettings.SetQualityLevel(2, true);
        PlayerPrefs.SetInt("PlayerHasSetQualityLevel", 1);
        PlayerPrefs.SetInt("QualitySetting", 2);
        PlayerPrefs.Save();
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