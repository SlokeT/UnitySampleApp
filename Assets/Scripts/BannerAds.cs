using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour{
  public TMP_Text txt;
  int bannerIndex = 0;
  string[] adUnitIds;

  private void Start(){
    bannerIndex = 0;
    adUnitIds = AppBrodaPlacementHandler.LoadPlacement("com_AppBroda_testAdsDemo_Banner_1");
  }

  public void loadBannerAd(){
    string adUnitId = adUnitIds[bannerIndex];
    BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    var adRequest = new AdRequest();
    bannerView.LoadAd(adRequest);
      bannerView.OnBannerAdLoaded += () => {
          showText("Banner view loaded @index"+bannerIndex);
      };
      bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>{
      showText("Banner view failed to load an ad with error : "
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

  public void showText(string message){
      txt.text = message;
      Debug.Log(message);
  }

}
