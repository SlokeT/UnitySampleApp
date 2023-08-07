using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class RewardedAds : MonoBehaviour{
  int rewardedAdIndex = 0;
  RewardedAd rewardedAd;
  string[] adUnitIds;

  public void startLoadingRewardedAds(){
    rewardedAdIndex = 0;
    adUnitIds = AppBrodaPlacementHandler.LoadPlacement("com_AppBroda_testAdsDemo_Rewarded_1");
    
    LoadRewardedAd();
  }

  public void LoadRewardedAd(){
    showText("Loading rewarded @index:"+rewardedAdIndex);
    if(adUnitIds.Length == 0){
      showText("Placement empty");
      return;
    }

    string adUnitId = adUnitIds[rewardedAdIndex];
    var adRequest = new AdRequest();
    RewardedAd.Load(adUnitId, adRequest,
      (RewardedAd ad, LoadAdError error) => {
        // if error is not null, the load request failed 
        // we will try to load the ad using next id
        if (error != null || ad == null) {
          showText("Rewarded ad failed @index:"+rewardedAdIndex);
          loadNextAd();
        }

        showText("Rewarded ad loaded @index:"+rewardedAdIndex);

        rewardedAd = ad;
        ShowRewardedAd();
    });
  }

  public void loadNextAd(){
    rewardedAdIndex++;
    if(rewardedAdIndex<adUnitIds.Length){
      LoadRewardedAd();
    }
  }

  public void ShowRewardedAd(){
    const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
    if (rewardedAd != null && rewardedAd.CanShowAd())
    {
        rewardedAd.Show((Reward reward) =>
        {
            // TODO: Reward the user.
            Debug.Log("Ad Loaded @"+rewardedAdIndex);
            UnlockRewardedAchievement();
            Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
        });
    }
  }

  public TMP_Text txt;
	public void showText(string message){
			txt.text = message;
			Debug.Log(message);
	}

#region Google Play Games
	GPGSManagerScript GPGSManager;
	private void Start(){
		GPGSManager = GameObject.FindObjectOfType(typeof(GPGSManagerScript)) as GPGSManagerScript;
	}
  string achievementId = "CgkI352R6rEXEAIQAw";
  public void UnlockRewardedAchievement(){
    GPGSManager.adShown();
    GPGSManager.UnlockAchievement(achievementId);
  }
#endregion

}
