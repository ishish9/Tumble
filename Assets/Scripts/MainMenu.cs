using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;

    void Start()
    {
        Application.targetFrameRate = 120;
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
    }

    public void QualitySettingHigh()
    {
        QualitySettings.SetQualityLevel(1, true);
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
