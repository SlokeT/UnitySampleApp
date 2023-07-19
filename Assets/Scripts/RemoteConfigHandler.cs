using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using System.Threading.Tasks;



public class RemoteConfigHandler : MonoBehaviour
{
    public TMP_Text txt;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
        dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
          FetchDataAsync();
            AppBrodaPlacementHandler.fetchAndSavePlacements();
        } else {
          showText(
            "Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
      });
    }
    public Task FetchDataAsync() {
    showText("Fetching data...");
    System.Threading.Tasks.Task fetchTask =
    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
        TimeSpan.Zero);
    return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    //[END fetch_async]

    void FetchComplete(Task fetchTask) {
        if (fetchTask.IsCanceled) {
            showText("Fetch canceled.");
        } else if (fetchTask.IsFaulted) {
            showText("Fetch encountered an error.");
        } else if (fetchTask.IsCompleted) {
            showText("Fetch completed successfully!");
        }

        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus) {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
            .ContinueWithOnMainThread(task => {
                showText(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                    info.FetchTime));
                                    
            });

            break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
            switch (info.LastFetchFailureReason) {
                case Firebase.RemoteConfig.FetchFailureReason.Error:
                showText("Fetch failed for unknown reason");
                break;
                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                showText("Fetch throttled until " + info.ThrottledEndTime);
                break;
            }
            break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
            showText("Latest Fetch call still pending.");
            break;
            }
    }

    public void showText(string message){
        txt.text = message;
        Debug.Log(message);
    }
}
