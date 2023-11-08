using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;

public class BannerAds : MonoBehaviour
{
    int bannerIndex;
    string[] adUnitIds;

    public void startLoadingBannerAd()
    {
        bannerIndex = 0;
        adUnitIds = AppBrodaAdUnitHandler.LoadAdUnit("com_AppBroda_testAdsDemo_Banner_1");
        loadBannerAd();
    }

    public void loadBannerAd()
    {
        showText("Loading banner @index:" + bannerIndex);
        if (adUnitIds == null || adUnitIds.Length == 0 || bannerIndex >= adUnitIds.Length)
        {
            showText("Ad Unit empty or not loaded");
            return;
        }

        string adUnitId = adUnitIds[bannerIndex];
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        var adRequest = new AdRequest();
        bannerView.LoadAd(adRequest);
        bannerView.OnBannerAdLoaded += () =>
        {
            showText("Banner ad loaded @index:" + bannerIndex);
            // Reset bannerIndex to 0
            bannerIndex = 0;
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            showText("Banner ad failed @index:" + bannerIndex
                + error);
            bannerView.Destroy();
            loadNextAd();
        };
    }

    public void loadNextAd()
    {
        bannerIndex++;
        if (bannerIndex >= adUnitIds.Length)
        {
            bannerIndex = 0;
            return;
        }

        loadBannerAd();
    }

    public TMP_Text logText;
    public void showText(string message)
    {
        logText.text = message;
        Debug.Log(message);
    }
}
