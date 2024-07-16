using DemoAppNetFramework.Properties;
using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace DemoAppNetFramework
{
    internal static class LocalizedStrings
    {
        internal static string GetString(string key)
        {
            return GetString(key, LocalizeDictionary.Instance.Culture);
        }

        internal static string GetString(string key, CultureInfo cultureInfo)
        {
            var result = LocalizeDictionary.Instance.GetLocalizedObject(nameof(DemoAppNetFramework), nameof(Strings), key, cultureInfo);
            return result as string;
        }
    }
}
