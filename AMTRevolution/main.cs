// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.IO;
using System.Windows;
using AppCore.UserControl;
using AppCore.AppSettings;
using System.Threading;
using System.Reflection;

namespace AMTRevolution
{
    public partial class App : Application
    {
        void AMTRevolution_Startup(object sender, StartupEventArgs e)
        {
            // Open SplashScreen
            var splash = new splashScreen();
            splash.buildLabel.Text = "Build " + Assembly.GetExecutingAssembly().GetName().Version;
            splash.Show();
            // Check for updates to the GUI here
            // TODO: GUI updater
            // ...
            // ...

            // Check for updates to the appCore here
            // TODO: appCore updater
            // ...
            // ...

            // HACK: WAIT TIME TO SEE SPLASHSCREEN
            Thread.Sleep(10000);
            // Initial AMTRevolution Checks
            // Check VF NW share access
            UserControl.InitializeUserProperties();
            if (!Directory.Exists(AppSettings.networkPath))
            {
                switch (UserControl.userName.ToLower())
                {
                    case "gonalvhf":
                    case "goncarj3":
                    case "caramelos":
                    case "hugo gonçalves":
                        AppSettings.debugMode = true;
                        MainWindow mainWindow = new MainWindow(AppSettings.debugMode);
                        splash.Close();
                        mainWindow.ShowDialog();
                        break;
                    default: MessageBox.Show("Out of VF-NW", "Exiting...", MessageBoxButton.OK, MessageBoxImage.Error); Environment.Exit(1); break;
                }
            }
            // Run the app
            // Ask to activate debug mode
            switch (UserControl.userName.ToLower())
            {
                case "gonalvhf":
                case "goncarj3":
                    var res = MessageBox.Show("Activate Debug Mode?", "Debug Mode", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                        AppSettings.debugMode = true;
                    break;
            }
            MainWindow mainWin = new MainWindow(AppSettings.debugMode);
            splash.Close();
            mainWin.ShowDialog();
        }
    }
}
