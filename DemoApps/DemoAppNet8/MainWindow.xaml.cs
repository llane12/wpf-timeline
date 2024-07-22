using Microsoft.Extensions.DependencyInjection;

namespace DemoAppNet8
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService<MainWindowViewModel>();
        }
    }
}