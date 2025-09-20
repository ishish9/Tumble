using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    ActionMap_1 actionsWrapper2;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject MainMenuUI;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private TextMeshProUGUI musicToggleText;
    [SerializeField] private AudioSource music;
    public static event Action <bool> OnHighSetting;
    public static event Action OnDisableJump;
    public static event Action OnEnableJump;
    [SerializeField] private bool MainMenu;
    public bool MenuDisabled;
    private static bool musicOn = true;

    private void Awake()
    {
        Application.targetFrameRate = 120;    
        actionsWrapper2 = new ActionMap_1();
        actionsWrapper2.Menu.MenuButton.performed += OnMenuActivated;
    }
    private void Start()
    {

        if (PlayerPrefs.GetInt("QualitySetting") == 0)
        {
            QualitySettings.SetQualityLevel(0, true);
            OnHighSetting?.Invoke(false);
            PlayerPrefs.SetInt("QualitySetting", 0);
            PlayerPrefs.Save();
        }
        else
        {
            QualitySettings.SetQualityLevel(1, true);
            OnHighSetting?.Invoke(true);
            PlayerPrefs.SetInt("QualitySetting", 1);
            PlayerPrefs.Save();
        }
        if (MainMenu)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.audioClips.MusicMenu);
        }
        else
        {
        }

        if (musicOn = false)
        {
            musicToggleText.text = "Music Off";
            musicOn = false;
        }
        else
        {
            musicToggleText.text = "Music On";
            musicOn = true;
            AudioManager.Instance.PlayMusic(AudioManager.Instance.audioClips.Music);
        }
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
            OnDisableJump?.Invoke();
            }


            // Deactivates Menu //        
            else
        {
            if (musicOn = false)
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
            actionsWrapper2.Player.Jump.Enable();
            OnEnableJump?.Invoke();
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
                OnDisableJump?.Invoke();
            }

            // Deactivates Menu //        
            else
            {
                if (musicOn == false)
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
                actionsWrapper2.Player.Jump.Enable();
                OnEnableJump?.Invoke();

            }
        }
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

    public void MusicToggleText()
    {
        AudioManager.Instance.ToggleMusic();

        if (musicOn == false)
        {
            musicToggleText.text = "Music on";
            musicOn = true;
        }
        else
        {
            musicToggleText.text = "Music off";
            musicOn = false;
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