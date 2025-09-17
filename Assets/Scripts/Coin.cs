using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    public static event Action <int> OnCollectCoin;
    [SerializeField] private GameObject sparkle;
    [SerializeField] private int customCoinAmount;
    [SerializeField] private bool customAdd;
    private bool highQualitySpin = false;

    void Update()
    {
        if (highQualitySpin)
        {
            transform.Rotate(Vector3.up * 200 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(sparkle, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.GetCoin);
            if (customAdd)
            {
                OnCollectCoin(customCoinAmount);
            }
            else
            {
                OnCollectCoin(1);
            }
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        MainMenu.OnHighSetting += SetQualitySpin;
    }

    private void OnDisable()
    {
        MainMenu.OnHighSetting -= SetQualitySpin;

    }
    private void SetQualitySpin(bool b)
    {
        highQualitySpin = b;
    }
}
