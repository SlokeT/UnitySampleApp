using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEditor;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public static class AppBrodaPlacementHandler{
  private static Dictionary<string, string[]> placements = new Dictionary<string, string[]>();

  public static string[] LoadPlacement(string key){
    string placementValue = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
    Debug.Log("placementValue" + placementValue);
    string[] placement = ConvertToArray(placementValue);
    Debug.Log("converted to array");
    return placement == null ? new string[] { } : placement;
  }

  public static string[] ConvertToArray(string value){
    string[] array;
    string pattern = "[\\\"\\[\\]]";
    string trimmedString = Regex.Replace(value, pattern, "");
    array = trimmedString.Split(',');
    Debug.Log("Printing array :");
    for (int i = 0; i < array.Length; i++){
        Debug.Log(array[i]);
    } 
    return array;
  }
}
