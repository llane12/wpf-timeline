using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace DemoAppNet8
{
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            LocalizeDictionary.Instance.Culture = new CultureInfo("ko");

            InitializeComponent();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // ViewModels
            services.AddTransient<MainWindowViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
