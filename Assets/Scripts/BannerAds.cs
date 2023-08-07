using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;

public class BannerAds : MonoBehaviour{
  int bannerIndex = 0;
  string[] adUnitIds;

  public void startLoadingBannerAd(){
    bannerIndex = 0;
    adUnitIds = AppBrodaPlacementHandler.LoadPlacement("com_AppBroda_testAdsDemo_Banner_1");

    loadBannerAd();
  }

  public void loadBannerAd(){
    showText("Loading banner @index:"+bannerIndex);
    if(adUnitIds.Length == 0){
      showText("Placement empty");
      return;
    }

    string adUnitId = adUnitIds[bannerIndex];
    BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    var adRequest = new AdRequest();
    bannerView.LoadAd(adRequest);
      bannerView.OnBannerAdLoaded += () => {
          showText("Banner ad loaded @index: "+bannerIndex);
          UnlockBannerAchievement();
      };
      bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>{
      showText("Banner ad failed @index: ${bannerIndex}"
          + error);
            bannerView.Destroy();
            loadNextAd();
      };
  }

  public void loadNextAd(){
    bannerIndex++;
    if(bannerIndex<adUnitIds.Length){
      loadBannerAd();
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
  string achievementId = "CgkI352R6rEXEAIQAQ";
  public void UnlockBannerAchievement(){
    GPGSManager.adShown();
    GPGSManager.UnlockAchievement(achievementId);
  }

#endregion

}
