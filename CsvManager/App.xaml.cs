using System.Windows;
using CsvManager.Services;

namespace CsvManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        public App()
        {
            var boot = new AppBootStrap();
            boot.Setup();
        }
    }
}
