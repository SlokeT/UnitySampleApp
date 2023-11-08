using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class InterstitialAds : MonoBehaviour
{
    int interstitialAdIndex;
    string[] adUnitIds;
    InterstitialAd interstitialAd;

    public void startLoadingInterstitialAd()
    {
        interstitialAdIndex = 0;
        adUnitIds = AppBrodaAdUnitHandler.LoadAdUnit("com_AppBroda_testAdsDemo_Interstitial_1");
        LoadInterstitialAd();
    }
    public void LoadInterstitialAd()
    {
        showText("Loading interstitial @index:" + interstitialAdIndex);
        if (adUnitIds == null || adUnitIds.Length == 0 || interstitialAdIndex >= adUnitIds.Length)
        {
            showText("Ad Unit empty or not loaded");
            return;
        }

        string adUnitId = adUnitIds[interstitialAdIndex];
        var adRequest = new AdRequest();
        InterstitialAd.Load(adUnitId, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  showText("interstitial ad failed @index:" + interstitialAdIndex);
                  loadNextAd();
              }
              else
              {
                  showText("Interstitial ad loaded @index:" + interstitialAdIndex);
                  // Reset interstitialAdIndex to 0
                  interstitialAdIndex = 0;
                  interstitialAd = ad;
                  //RegisterEventHandlers(ad);
                  ShowAd();
              }
          });
    }

    public void loadNextAd()
    {
        interstitialAdIndex++;
        if (interstitialAdIndex >= adUnitIds.Length)
        {
            interstitialAdIndex = 0;
            return;
        }

        LoadInterstitialAd();
    }

    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            showText("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            showText("Interstitial ad is not ready yet.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                                    "with error : " + error);
        };
    }

    public TMP_Text logText;
    public void showText(string message)
    {
        logText.text = message;
        Debug.Log(message);
    }
}
