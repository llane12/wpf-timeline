using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace TestApp
{
    public partial class App
    {
        public App()
        {
            LocalizeDictionary.Instance.Culture = new CultureInfo("en");
        }
    }
}
