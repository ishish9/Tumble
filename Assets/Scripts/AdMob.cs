using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdMob : MonoBehaviour
{
    private InterstitialAdScript intersitialScript;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    public void Start()
    {
        intersitialScript = GetComponent<InterstitialAdScript>();
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });
    }
}