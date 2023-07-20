using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEditor;

public static class AppBrodaPlacementHandler
{
  public static void fetchAndSavePlacements(){
    string remoteConfigPrefix = Application.identifier.Replace(".", "_") + "_";
    HashSet<string> remoteConfigKeys = new HashSet<string>(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetKeysByPrefix(remoteConfigPrefix));
    foreach (string item in remoteConfigKeys){
        Debug.Log(item);
    }
    foreach (string key in remoteConfigKeys){
      string value = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
      PlayerPrefs.SetString(key,value);
      Debug.Log(ConvertToArray(value));
    }
    PlayerPrefs.Save();
  }

  public static string[] LoadPlacement(string key){
    string value = PlayerPrefs.GetString(key);
    Debug.Log("raw placement value is"+value);
    if (value == "")
    {
        return new string[] { };
    }

    string[] placement = ConvertToArray(value);
    return placement == null ? new string[] { } : placement;
  }

  public static string[] ConvertToArray(string value){
    string[] array;
    value = value.Substring(1, value.Length - 2);
    array = value.Split(',');
    for (int i = 0; i < array.Length; i++)
    {
        array[i] = array[i].Trim().Trim('\"');
    }
    return array;
  }
}
