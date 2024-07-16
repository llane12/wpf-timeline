using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace DemoAppNetFramework
{
    public partial class App
    {
        public App()
        {
            LocalizeDictionary.Instance.Culture = new CultureInfo("ko");
        }
    }
}
