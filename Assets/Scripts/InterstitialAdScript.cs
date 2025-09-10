using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

    public class InterstitialAdScript : MonoBehaviour
{
    // UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-8112362826692397/7516463055";
    // UNITY_IPHONE
  //private string _adUnitId = "ca-app-pub-8112362826692397/6505242446";
    public static event Action OnAdCheck;

    private void OnEnable()
    {
        Manager.OnAdLoad += LoadInterstitialAd;
        Manager.OnAdShow += ShowAd;
    }

    private void OnDisable()
    {
        Manager.OnAdLoad -= LoadInterstitialAd;
        Manager.OnAdShow -= ShowAd;
    }




    private InterstitialAd interstitialAd;

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();


        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                ListenToAdEvents(interstitialAd);
            });
    }

    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
            // 768x1024(Clone)


        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    void ListenToAdEvents(InterstitialAd interstitialAd)
    {
        // [START ad_events]
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            // Raised when the ad is estimated to have earned money.
        };
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            // Raised when an impression is recorded for an ad.
        };
        interstitialAd.OnAdClicked += () =>
        {
            // Raised when a click is recorded for an ad.
        };
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            // Raised when the ad opened full screen content.
        };
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            OnAdCheck();
            // Raised when the ad closed full screen content.
        };
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // Raised when the ad failed to open full screen content.
        };
        // [END ad_events]]
    }
}

