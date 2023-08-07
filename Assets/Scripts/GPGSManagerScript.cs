using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
public class GPGSManagerScript : MonoBehaviour {
    [SerializeField] TMP_Text debugtxt;
    int adCounter = 0;

    void Start(){
        Initialize();
    }
    void Initialize(){
        PlayGamesPlatform.Activate();
        debugtxt.text = "Play Games Initialized";
        SingInUserWithPlayGames();
    }

    void SingInUserWithPlayGames(){
        PlayGamesPlatform.Instance.Authenticate(SuccessCallback);
    }

    internal void SuccessCallback(SignInStatus success){
        if(success == SignInStatus.Success){        
            debugtxt.text = "Signed in player using play games successfully";
        } else {
            debugtxt.text = "Signed in player using play games successfully";
        }
    }

    public void postScoreToLeaderBoard(int score){
        Social.ReportScore(score,"CgkI352R6rEXEAIQBg",(bool success)=>{
            if (success){
                debugtxt.text = "Succesfully added score to leaderboard";
            } else {
                debugtxt.text = "Score not added to leaderboard";
            }
        });
    }

    public void ShowLeaderboard(){
        Social.ShowLeaderboardUI();
    }

    public void UnlockAchievement(string achievementId){
        Social.ReportProgress(achievementId,100.0f,(bool success)=> { 
        });
    }

    public void IncrementAchievement(string achievementId){    
        PlayGamesPlatform.Instance.IncrementAchievement(
            achievementId, 1, (bool success) => {
            if (success){
                debugtxt.text = "Incremented";
            } else {
                debugtxt.text = "failed to increment";
            }   
        });
    }

    public void ShowAchievementUI(){
        UnlockAchievement("CgkI352R6rEXEAIQBA");
        Social.ShowAchievementsUI();
    }

    public void adShown(){
        adCounter++;
        IncrementAchievement("CgkI352R6rEXEAIQBQ");
    }

    void OnApplicationQuit(){
        postScoreToLeaderBoard(adCounter);
    }
}

