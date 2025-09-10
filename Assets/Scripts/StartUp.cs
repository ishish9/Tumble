using System.Collections;
using UnityEngine;
using TMPro;

public class StartUp : MonoBehaviour
{
    [SerializeField] private Menu menu;
    //[SerializeField] private TextMeshProUGUI PersonalHighG;

    void Start()
    {
        Application.targetFrameRate = 120;
        //string date = System.DateTime.UtcNow.ToLocalTime().ToString();
  

        if (PlayerPrefs.GetInt("UserSetMusicSetting") == 0)
        {
            PlayerPrefs.SetInt("musicOnOff", 1);
            PlayerPrefs.Save();
            return;
        }          
    }  
}
