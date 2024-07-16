using DemoAppNet8.Resources;
using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace DemoAppNet8
{
    internal static class LocalizedStrings
    {
        internal static string GetString(string key)
        {
            return GetString(key, LocalizeDictionary.Instance.Culture);
        }

        internal static string GetString(string key, CultureInfo cultureInfo)
        {
            var result = LocalizeDictionary.Instance.GetLocalizedObject(nameof(DemoAppNet8), nameof(Strings), key, cultureInfo);
            return result as string;
        }
    }
}
