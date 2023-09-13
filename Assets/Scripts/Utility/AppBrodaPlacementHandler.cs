using System.Text.RegularExpressions;

public static class AppBrodaPlacementHandler
{
    public static string[] LoadPlacement(string key)
    {
        string placementValue = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
        return ConvertToArray(placementValue);
    }

    public static string[] ConvertToArray(string value)
    {
        string[] array = new string[] { };
        if (string.IsNullOrEmpty(value)) return array;

        string pattern = "[\\\"\\[\\]]";
        string trimmedString = Regex.Replace(value, pattern, "");
        array = trimmedString.Split(',');
        return array;
    }
}
