using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class RewardedAds : MonoBehaviour{
  int rewardedAdIndex = 0;
  string[] adUnitIds;

  private void Start() {
    adUnitIds = AppBrodaPlacementHandler.LoadPlacement("com_AppBroda_testAdsDemo_Rewarded_1");
  }
  public void LoadRewardedAd(){
    RewardedAd rewardedAd;
    var adRequest = new AdRequest();
    string adUnitId = adUnitIds[rewardedAdIndex];
    RewardedAd.Load(adUnitId, adRequest,
      (RewardedAd ad, LoadAdError error) => {
        // if error is not null, the load request failed 
        // we will try to load the ad using next id
        if (error != null || ad == null)
        {
          Debug.LogError("Rewarded ad failed to load @"+rewardedAdIndex+"an ad " +
                          "with error : " + error);
          loadNextAd();
        }

        Debug.Log("Rewarded ad loaded with response : "
                    + ad.GetResponseInfo());

        rewardedAd = ad;
        ShowRewardedAd(rewardedAd);
    });
  }

  public void loadNextAd(){
    rewardedAdIndex++;
    if(rewardedAdIndex<adUnitIds.Length){
      LoadRewardedAd();
    }
  }

  public void ShowRewardedAd(RewardedAd rewardedAd){
    const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
    if (rewardedAd != null && rewardedAd.CanShowAd())
    {
        rewardedAd.Show((Reward reward) =>
        {
            // TODO: Reward the user.
            Debug.Log("Ad Loaded @"+rewardedAdIndex);
            Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
        });
    }
  }
}
