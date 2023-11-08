using System.Text.RegularExpressions;

public static class AppBrodaAdUnitHandler
{
    public static string[] LoadAdUnit(string key)
    {
        string adUnit = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
        return ConvertToArray(adUnit);
    }

    private static string[] ConvertToArray(string value)
    {
        string[] array = new string[] { };
        if (string.IsNullOrEmpty(value)) return array;

        string pattern = "[\\\"\\[\\]]";
        string trimmedString = Regex.Replace(value, pattern, "");
        array = trimmedString.Split(',');
        return array;
    }
}
