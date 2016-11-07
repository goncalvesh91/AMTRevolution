// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using AppCore.AppSettings;
using AppCore.UserControl;
using AMTRevolution.GUI.MessageBox;

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
				// TODO: Check user permissions, exit here if user is not allowed!
				splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Checking user permissions..."; }));
				// ..
				// ..
				
				splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Loading user settings..."; }));
			LOADUSERSETTINGS:
				var userSettings = new UserSettings(UserControl.userName.ToLower()); // this will try to load the user settings
				
				if(!userSettings.isLoaded)
				{
					switch(userSettings.errorLevel)
					{
							case 0 : splash.Dispatcher.BeginInvoke(new Action(() => { splash.statusLabel.Text = "Settings loaded from share backup"; })); break; // No errors
							case 1 : Environment.Exit(1); break; // TODO: Add event to app log
							case 2 : Environment.Exit(1); break; // TODO: Add event to app log
							case 3 : userSettings.generateUserSettings(UserControl.userName.ToLower()); break; // TODO: Add event to app log
							case 4 : userSettings.generateUserSettings(UserControl.userName.ToLower()); break; // TODO: Add event to app log
							case 5 : userSettings.generateUserSettings(UserControl.userName.ToLower()); break; // TODO: Add event to app log
							case 6 : userSettings.generateUserSettings(UserControl.userName.ToLower()); break; // TODO: Add event to app log
							default: Environment.Exit(1); break; // TODO: Add event to app log
					}
					goto LOADUSERSETTINGS;
				}

				// Check if settings matches user
				if(!userSettings.USW.userId.Equals(UserControl.userName, StringComparison.InvariantCultureIgnoreCase))
				{ // Does not match
					// TODO: Add event to app log
					Environment.Exit(1);
				}
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
