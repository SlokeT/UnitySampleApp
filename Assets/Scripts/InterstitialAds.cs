using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class InterstitialAds : MonoBehaviour{
  // Start is called before the first frame update
  int interstitialAdIndex = 0;
  string[] adUnitIds;
  private void Awake() {
    interstitialAdIndex = 0;
    adUnitIds = AppBrodaPlacementHandler.LoadPlacement("com_AppBroda_testAdsDemo_Interstitial_1");
  }
  public void LoadInterstitialAd(){
    InterstitialAd interstitialAd;
    Debug.Log("current index "+interstitialAdIndex);
    for (int i = 0; i < adUnitIds.Length; i++)
    {
        //adUnitIds[i] = adUnitIds[i].Trim().Trim('\"');
        Debug.Log("array"+i+": "+adUnitIds[i]);
    }

    string adUnitId = adUnitIds[interstitialAdIndex];
    // create our request used to load the ad.
    var adRequest = new AdRequest();
    // send the request to load the ad.

    InterstitialAd.Load(adUnitId, adRequest,
      (InterstitialAd ad, LoadAdError error) =>
      {
          // if error is not null, the load request failed.
          if (error != null || ad == null)
          {
              Debug.LogError("interstitial ad failed to load an ad " +
                            "with error : " + error);
              loadNextAd();
          }

          Debug.Log("Interstitial ad loaded with response : "
                    + ad.GetResponseInfo());

          interstitialAd = ad;
          //RegisterEventHandlers(ad);
          ShowAd(ad);
      });
  }

  public void loadNextAd(){
    interstitialAdIndex++;
    if(interstitialAdIndex<adUnitIds.Length-1){
        LoadInterstitialAd();
    }
  }

  public void ShowAd(InterstitialAd interstitialAd){
    if (interstitialAd != null && interstitialAd.CanShowAd())
    {
        Debug.Log("Showing interstitial ad.");
        interstitialAd.Show();
    }
    else
    {
        Debug.LogError("Interstitial ad is not ready yet.");
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
}
