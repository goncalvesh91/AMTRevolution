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
            var splash = new splashScreen();
            var bgWorker = new BackgroundWorker();
            // Do all the work with bgWorker

            // When all work is done close the splashscreen
            bgWorker.RunWorkerCompleted += (obj, e1) => 
            {
                splash.Close();
            };

            // Work before launching main window
            bgWorker.DoWork += (obj, e1) =>
            {
                // Check if outside VF NW
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Network check..."; }));
                UserControl.InitializeUserProperties();
                if (!Directory.Exists(AppSettings.networkPath))
                {
                    switch (UserControl.userName.ToLower())
                    {
                        case "gonalvhf":
                        case "goncarj3":
                        case "caramelos":
                        case "hugo gonçalves":
                            break;
                        default: MessageBox.Show("Out of VF-NW", "Exiting...", MessageBoxButton.OK, MessageBoxImage.Error); Environment.Exit(1); break;
                    }
                }
                // Check for updates to the GUI here
                // TODO: GUI updater
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Checking for GUI updates..."; }));
                // ...
                // ...

                // Check for updates to the appCore here
                // TODO: appCore updater
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Checking for AppCore updates..."; }));
                // ...
                // ...


                // User checks 
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Initial Checks..."; }));
                // Ask to activate debug mode
                switch (UserControl.userName.ToLower())
                {
                    case "gonalvhf":
                    case "goncarj3":
                    case "caramelos":
                    case "hugo gonçalves":
                        var res = MessageBox.Show("Activate Debug Mode?", "Debug Mode", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (res == MessageBoxResult.Yes)
                            AppSettings.debugMode = true;
                        break;
                }

                // TODO: Load user settings
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Loading user settings..."; }));
                // ...
                // ...

                // TODO: Load Databases
                splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Loading databases..."; }));
                // ...
                // ...

                // Initial loading finished
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
