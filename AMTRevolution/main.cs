// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using AppCore.AppEvent;
using AppCore.AppSettings;
using AppCore.UserControl;
using AppCore.PermissionControl;

namespace AMTRevolution
{
    public partial class App : Application
    {
        void AMTRevolution_Startup(object sender, StartupEventArgs e)
        {
            var splash = new splashScreen();
            var bgWorker = new BackgroundWorker();
            // Do all the initial work with bgWorker

            // When all work is done close the splashscreen
            bgWorker.RunWorkerCompleted += (obj, e1) =>
            {
                splash.Close();
            };

            // Work before launching main window
            bgWorker.DoWork += (obj, e1) =>
            {
                var eventHandler = new appEvent(AppSettings.appEventsPath);
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
                if (!AppSettings.debugMode)
                {
                    splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Loading user settings..."; }));
                LOADUSERSETTINGS:
                    var userSettings = new UserSettings(UserControl.userName.ToLower()); // this will try to load the user settings

                    if (!userSettings.isLoaded)
                    {
                        switch (userSettings.errorLevel)
                        {
                            case 0: eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, "Settings loaded from share backup"); splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Settings loaded from share backup"; })); break; // No errors
                            case 1: eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "Failed to create AMTRevolution dir"); Environment.Exit(1); break;
                            case 2: eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "Failed to create user dir"); Environment.Exit(1); break;
                            case 3: eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "No backup settings in share, using default settings"); userSettings.generateUserSettings(UserControl.userName.ToLower()); break;
                            case 4: eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "Failed to download backup settings, using default settings"); userSettings.generateUserSettings(UserControl.userName.ToLower()); break;
                            case 5: eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "Failed to load downloaded settings, using default settings"); userSettings.generateUserSettings(UserControl.userName.ToLower()); break;
                            case 6: eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "Failed to load settings, using default settings"); userSettings.generateUserSettings(UserControl.userName.ToLower()); break;
                            default: eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "Unknow error"); Environment.Exit(1); break;
                        }
                        goto LOADUSERSETTINGS;
                    }

                    // Check if settings matches user
                    if (!userSettings.USW.userId.Equals(UserControl.userName, StringComparison.InvariantCultureIgnoreCase))
                    { // Does not match
                        eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, "User settings do not match current user");
                        Environment.Exit(1);
                    }
                    // TODO: Check user permissions, exit here if user is not allowed!
                    splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Checking user permissions..."; }));
                    var permControl = new permissionControl(UserControl.userName);
                    // ..
                    // ..

                    // TODO: Load Databases
                    splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Loading databases..."; }));
                    // ...
                    // ...

                    // Initial loading finished
                }
            };
            splash.buildLabel.Text = "Build " + Assembly.GetExecutingAssembly().GetName().Version;
            bgWorker.RunWorkerAsync();
            splash.Closing += (obj, e1) =>
            {
                // Run the app when splash closes
                var mainWin = new MainWindow(AppSettings.debugMode);
                mainWin.Show();
            };
            splash.ShowDialog();
        }
    }
}
