using System.Globalization;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace DemoAppNet6
{
    public partial class App : Application
    {
        public App()
        {
            LocalizeDictionary.Instance.Culture = new CultureInfo("ko");
        }
    }
}
