// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System.IO;
using System.Windows;
using AppCore.UserControl;
using AppCore.AppSettings;

namespace AMTRevolution
{
    public partial class App : Application
    {
        void AMTRevolution_Startup(object sender, StartupEventArgs e)
        {
            // Initial AMTRevolution Checks
            // Check VF NW share access
            UserControl.InitializeUserProperties();
            if(!Directory.Exists(AppSettings.networkPath))
            {
                switch(UserControl.userName)
                {
                    case "gonalvhf": case "goncarj3": case "Caramelos": case "Hugo Gonçalves":  MainWindow mainWindow = new MainWindow();
                                                                                                mainWindow.Show();
                                                                                                break;
                    default: MessageBox.Show("Out of VF-NW", "Exiting...", MessageBoxButton.OK, MessageBoxImage.Error);break;
                }
            }

            // Check for updates to the GUI here
            // TODO: GUI updater
            // ...
            // ...

            // Check for updates to the appCore here
            // TODO: appCore updater
            // ...
            // ...

            // Run the app
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
        }
    }
}
