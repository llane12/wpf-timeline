using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace TestApp.Resources
{
    internal static class LocalizedStrings
    {
        internal static string GetString(string key)
        {
            return GetString(key, LocalizeDictionary.Instance.Culture);
        }

        internal static string GetString(string key, CultureInfo cultureInfo)
        {
            var result = LocalizeDictionary.Instance.GetLocalizedObject("TestApp", "Strings", key, cultureInfo);
            return result as string;
        }
    }
}
