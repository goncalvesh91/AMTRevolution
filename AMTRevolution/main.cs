// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Windows;
using AppCore.UserControl;
using AppCore.AppSettings;

namespace AMTRevolution
{
    public partial class App : Application
    {
        void AMTRevolution_Startup(object sender, StartupEventArgs e)
        {
            // Open SplashScreen
            var splash = new splashScreen();
            var bgWorker = new BackgroundWorker();
            // Do all work with bgWorker

            // When all work is done close the splashscreen
            bgWorker.RunWorkerCompleted += (obj, e1) => 
            {
                splash.Close();
            };

            // Work before launching main window
            bgWorker.DoWork += (obj, e1) =>
            {
                // Check for updates to the GUI here
                // TODO: GUI updater
                // ...
                // ...
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Checking for GUI updates..."; }));
                // HACK: WAIT TIME TO SEE SPLASHSCREEN
                for (long a = 0; a < 500000000; a++)
                {
                    double lol = 100000000000 / 2.123132123123;
                }
                // Check for updates to the appCore here
                // TODO: appCore updater
                // ...
                // ...
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Checking for AppCore updates..."; }));

                // HACK: WAIT TIME TO SEE SPLASHSCREEN
                for(long a = 0; a < 500000000; a++)
                {
                    double lol = 100000000000 / 2.123132123123;
                }
                // Initial AMTRevolution Checks
                // Check VF NW share access
                // Initial AMTRevolution Checks
                // Check VF NW share access
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Initial Checks..."; }));
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
                            break;
                        default: MessageBox.Show("Out of VF-NW", "Exiting...", MessageBoxButton.OK, MessageBoxImage.Error); Environment.Exit(1); break;
                    }
                }
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
            };
            splash.buildLabel.Text = "Build " + Assembly.GetExecutingAssembly().GetName().Version;
            bgWorker.RunWorkerAsync();
            splash.Closing += (obj, e1) =>
            {
                // Run the app when splash closes
                MainWindow mainWin = new MainWindow(AppSettings.debugMode);
                mainWin.Show();
            };
            splash.ShowDialog();
        }
    }
}
