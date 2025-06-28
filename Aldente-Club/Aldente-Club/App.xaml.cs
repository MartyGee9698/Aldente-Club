using System.Windows;

namespace Aldente_Club
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            HomeWindow home = new HomeWindow();
            home.Show();
        }
    }
}