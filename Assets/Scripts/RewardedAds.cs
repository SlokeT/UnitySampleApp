using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class RewardedAds : MonoBehaviour
{
    int rewardedAdIndex;
    RewardedAd rewardedAd;
    string[] adUnitIds;

    public void startLoadingRewardedAds()
    {
        rewardedAdIndex = 0;
        adUnitIds = AppBrodaAdUnitHandler.LoadAdUnit("com_AppBroda_testAdsDemo_Rewarded_1");
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        showText("Loading rewarded @index:" + rewardedAdIndex);
        if (adUnitIds == null || adUnitIds.Length == 0 || rewardedAdIndex >= adUnitIds.Length)
        {
            showText("Ad Unit empty or not loaded");
            return;
        }

        string adUnitId = adUnitIds[rewardedAdIndex];
        var adRequest = new AdRequest();
        RewardedAd.Load(adUnitId, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              if (error != null || ad == null)
              {
                  showText("Rewarded ad failed @index:" + rewardedAdIndex);
                  loadNextAd();
              }
              else
              {
                  showText("Rewarded ad loaded @index:" + rewardedAdIndex);
                  // Reset rewardedAdIndex to 0
                  rewardedAdIndex = 0;
                  rewardedAd = ad;
                  ShowRewardedAd();
              }
          });
    }

    public void loadNextAd()
    {
        rewardedAdIndex++;
        if (rewardedAdIndex >= adUnitIds.Length)
        {
            rewardedAdIndex = 0;
            return;
        }

        LoadRewardedAd();
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log("Ad Loaded @" + rewardedAdIndex);
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    public TMP_Text logText;
    public void showText(string message)
    {
        logText.text = message;
        Debug.Log(message);
    }
}
