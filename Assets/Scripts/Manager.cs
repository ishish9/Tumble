using DinoFracture;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform restartPosition;
    [SerializeField] private Transform levelDistance;
    [SerializeField] private Transform barrels;
    [SerializeField] private Transform coinsReactivate;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private TextMeshProUGUI distanceDisplay;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    public static event Action <Transform> OnSendPlayerTransform;
    public static event Action OnAdLoad;
    public static event Action OnAdShow;
    public static event Action OnMenuToggle;
    public static event Action <bool> OnMenuDisable;
    private bool distanceDisplayOn = true;
    private bool AdClosed;
    private bool AltRestart;
    private int AdCount;
    private int coins;
    private int coinsCheckpoint;
    private bool checkPoint;

    private void Awake()
    {
        GameObject CreatePlayerObject = Instantiate(playerObj, restartPosition.position, Quaternion.identity);
        Transform PlayerTransform = CreatePlayerObject.transform;
        OnSendPlayerTransform(PlayerTransform);
        Player = PlayerTransform;
     
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("musicSetting") == 1)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.audioClips.Music);
        }

        if (PlayerPrefs.GetInt("QualitySetting") == 0) 
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        else
        {
            QualitySettings.SetQualityLevel(1, true);
        }

    }

    private void Update()
    {
        //float dist = Vector3.Distance(Player.position, levelDistance.position);

        //if (distanceDisplayOn)
       // {
       //     distanceDisplay.text = dist.ToString("0.0");
        //}
    }

    void OnEnable()
    {
        ControllerMultiplayer.OnDeath += EnableRestartButton;
        CannonBall.OnGetTransform += SendPlayerTransform;
        Coin.OnCollectCoin += CoinAdd;
        Barrel.OnBarrelExplode += EnableRestartButton;
        CheckPoint.OnCheckPointTriggered += CheckPointTrigger;
        InterstitialAdScript.OnAdCheck += AdcloseToggle;
        KillPlayer.OnFall += RestartFall;
    }

    void OnDisable()
    {
        ControllerMultiplayer.OnDeath -= EnableRestartButton;
        CannonBall.OnGetTransform -= SendPlayerTransform;
        Coin.OnCollectCoin -= CoinAdd;
        Barrel.OnBarrelExplode -= EnableRestartButton;
        CheckPoint.OnCheckPointTriggered -= CheckPointTrigger;
        InterstitialAdScript.OnAdCheck -= AdcloseToggle;
        KillPlayer.OnFall -= RestartFall;

    }

    public void Restart()
    {
        // Player.position = restartPosition.position;
        // rb.MovePosition(restartPosition.position);
        AudioManager.Instance.MusicOn();
        GameObject FracturesContainer = GameObject.Find("Player(Clone) - Fracture Root");
        Destroy(FracturesContainer);
        GameObject newObject = Instantiate(playerObj, restartPosition.position, Quaternion.identity);
        Transform PlayerTransform = newObject.transform;
        OnSendPlayerTransform(PlayerTransform);
        OnMenuDisable(false);
        distanceDisplay.text = "0";
        Player = PlayerTransform;
        if (checkPoint)
        {
            coins = coinsCheckpoint;
        }
        else
        {
            coins = 0;
        }
        coinDisplay.text = "Coins: " + coins.ToString();

        for (int j = 0; j < barrels.childCount; j++)
        {
            barrels.GetChild(j).gameObject.SetActive(true);
        }

        for (int j = 0; j < coinsReactivate.childCount; j++)
        {
            coinsReactivate.GetChild(j).gameObject.SetActive(true);
        }
    }

    public void RestartMenu()
    {
        AltRestart = true;
        GameObject g = SendPlayerTransform().gameObject;
        g.GetComponent<FractureGeometry>().Fracture();
        OnMenuToggle();
    }

    public void RestartFall()
    {
        AltRestart = true;
        GameObject g = SendPlayerTransform().gameObject;
        g.GetComponent<FractureGeometry>().Fracture();
    }

    public void CoinAdd(int AddAmount)
    {
        StartCoroutine(updateCoinScore());

        IEnumerator updateCoinScore()
        {
            for (int i = 0; i < AddAmount; i++)
            {
                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.GetCoin);
                coins++;
                coinDisplay.text = "Coins: " + coins.ToString();
                yield return new WaitForSeconds(.05f);
            }
        //StopCoroutine(updateCoinScore());
        }
        //GameData.gold++;
        //SaveData();
    }

    public void CoinSubtract(int SubtractAmount)
    {
        StartCoroutine(updateCoinScore());

        IEnumerator updateCoinScore()
        {
            for (int i = 0; i < SubtractAmount; i++)
            {
                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.GetCoin);
                coins--;
                coinDisplay.text = "Coins: " + coins.ToString();
                if (i == SubtractAmount -1)
                {
                    yield return new WaitForSeconds(.05f);
                    AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.CheckPointGood);
                }
                yield return new WaitForSeconds(.09f);
                
            }
            //StopCoroutine(updateCoinScore());

        }
        
        //GameData.gold++;
        //SaveData();
    }

    public void EnableRestartButton()
    {
        if (AltRestart)
        {
            AltRestart = false;
            Restart();
            return;
        }
        OnMenuDisable(true);
        uiMenu.SetActive(false);
        AudioManager.Instance.MusicOff();
        AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.Break);
        AudioManager.Instance.MasterVolumeControl(1.0f);
        AdCount++;
        if (AdCount == 4)
        {
            OnAdLoad();
        }
        else if (AdCount == 5)
        {
            OnAdShow();
            AdCount = 0;
            StartCoroutine(waitforad());

            IEnumerator waitforad()
            {
                yield return new WaitUntil(() => AdClosed == true);
                yield return new WaitForSeconds(1f);

                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.DieJingle);
                restart.SetActive(true);
                AdClosed = false;                
            }
            return;
        }
            StartCoroutine(afterDeath());

            IEnumerator afterDeath()
            {
                yield return new WaitForSeconds(1f);

                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.DieJingle);
                restart.SetActive(true);
            AdClosed = false;
            }
        }

    private void AdcloseToggle()
    {
        AdClosed = true;
    }

    public Transform SendPlayerTransform()
    {
        return Player;
    }

    public void CheckPointTrigger(int c, Transform t)
    {
        coinsCheckpoint = coins - c;
        checkPoint = true;
        CoinSubtract(c);
        restartPosition = t;
    }

    public int GetCoinAmount()
    {
        return coins;
    }
}
