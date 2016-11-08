// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.IO;
using AppCore.FileWriter;

namespace AppCore.UserControl
{
    /// <summary>
    /// Class to define the user settings
    /// </summary>
    public class UserSettings
    {
        public UserSettings(string userId)
        {
            var eventHandler = new AppEvent.appEvent(AppSettings.AppSettings.appEventsPath);
            var userSettingsPath = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper();
            var userSettingsNetworkPath = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper();
            // Check if the path already exists, if not create it
            if (!Directory.Exists(AppSettings.AppSettings.userSettingsPath))
            {
                try
                {
                    Directory.CreateDirectory(AppSettings.AppSettings.userSettingsPath);
                    eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Local AMT Revolution folder created");
                    // Check if user directory exists, if not create it and download settings
                    if (!Directory.Exists(userSettingsPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(userSettingsPath);
                            eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Local user folder created");
                            if (Directory.Exists(userSettingsNetworkPath))
                            {
                                var source = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper() + "\\settings.xbin";
                                var dest = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
                                if (File.Exists(source))
                                {
                                    File.Copy(source, dest);
                                    eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "User settings downloaded from share drive");
                                    if (loadUserSettings(dest))
                                    {
                                        // Load settings here
                                        isLoaded = true;
                                        errorLevel = 0;
                                        reason = "Settings loaded from share backup";
                                        eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "User settings loaded from share backup");
                                    }
                                    else
                                    {
                                        isLoaded = false; // Settings loaded
                                        errorLevel = 5;
                                        reason = "Failed to load settings from share backup";
                                        eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to load settings from share backup");
                                    }
                                }
                                else
                                {
                                    isLoaded = false;
                                    errorLevel = 3;
                                    reason = "No settings backup on share";
                                    eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "No settings backup on share");
                                }
                            }
                            else
                            {
                                try
                                {
                                    Directory.CreateDirectory(userSettingsNetworkPath);
                                    eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "No settings backup on share");
                                    generateUserSettings(userId);
                                    eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Default settings generated");
                                }
                                catch (Exception mknetdirEx)
                                {
                                    isLoaded = false;
                                    errorLevel = 2;
                                    reason = "Failed to create user folder in share" + Environment.NewLine;
                                    reason += mknetdirEx.Message;
                                    eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to create user folder in share");
                                }
                            }
                        }
                        catch (Exception mkdirEx)
                        {
                            isLoaded = false;
                            errorLevel = 2;
                            reason = "Failed to create user folder" + Environment.NewLine;
                            reason += mkdirEx.Message;
                            eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to create local user folder");
                        }
                    }
                }
                catch (Exception mkdir)
                {
                    isLoaded = false;
                    errorLevel = 1;
                    reason = "AMTRevolution does no exist" + Environment.NewLine + "Failed to create AMTRevolution folder" + Environment.NewLine;
                    reason += mkdir.Message;
                    eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to create local AMTRevolution folder");
                }
            }
            else
            {
                // Check if user directory exists, if not create it and download settings
                if (!Directory.Exists(userSettingsPath))
                {
                    try
                    {
                        Directory.CreateDirectory(userSettingsPath);
                        eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Local user folder created");
                        if (Directory.Exists(userSettingsNetworkPath))
                        {
                            var source = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper() + "\\settings.xbin";
                            var dest = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
                            if (File.Exists(source))
                            {
                                File.Copy(source, dest);
                                eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Settings downloaded from share drive");
                                if (loadUserSettings(dest))
                                {
                                    isLoaded = true; // Settings loaded
                                    errorLevel = 0;
                                    reason = "Settings loaded from share backup";
                                    eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Settings loaded from share drive");
                                }
                                else
                                {
                                    isLoaded = false; // Settings loaded
                                    errorLevel = 5;
                                    reason = "Failed to load settings from share backup";
                                    eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to load settings from share backup");
                                }
                            }
                        }
                        else
                        {
                            isLoaded = false;
                            errorLevel = 3;
                            reason = "No settings backup on share";
                            eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "No settings backup on share");
                        }

                    }
                    catch (Exception mkdirEx)
                    {
                        isLoaded = false;
                        errorLevel = 2;
                        reason = "Failed to create local user folder" + Environment.NewLine;
                        reason += mkdirEx.Message;
                        eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to create local user folder");
                    }
                }
                else
                { // User folder exists
                    var source = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper() + "\\settings.xbin";
                    var dest = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
                    if (File.Exists(dest))
                    {
                        if (loadUserSettings(dest))
                        {
                            isLoaded = true; // Settings loaded
                            errorLevel = 0;
                            reason = "Settings loaded";
                            eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "User settings loaded");
                        }
                        else
                        {
                            isLoaded = false; // Settings loaded
                            errorLevel = 6;
                            reason = "Failed to load settings";
                            eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to load settings");
                            deleteCurSettings(userId);
                        }
                    }
                    else
                    {
                        if (File.Exists(source))
                        {
                            try
                            {
                                File.Copy(source, dest);
                                eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Settings downloaded from share");
                                if (loadUserSettings(dest))
                                {
                                    isLoaded = true; // Settings loaded
                                    errorLevel = 0;
                                    reason = "Settings loaded from share backup";
                                    eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "Settings loaded from share backup");
                                }
                                else
                                {
                                    isLoaded = false; // Settings loaded
                                    errorLevel = 5;
                                    reason = "Failed to load settings from share backup";
                                    eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to load settings from share backup");
                                }
                            }
                            catch (Exception dlEx)
                            {
                                isLoaded = false; // Settings loaded
                                errorLevel = 5;
                                reason = "Failed to load settings from share backup" + Environment.NewLine;
                                reason += dlEx.Message;
                                eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.userName, Environment.MachineName, "Failed to load settings from share backup");
                            }
                        }
                        else
                        {
                            isLoaded = false;
                            errorLevel = 3;
                            reason = "No settings backup on share";
                            eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.userName, Environment.MachineName, "No settings backup on share");
                        }
                    }
                }
            }
        }

        public bool isLoaded { get; set; }

        public int errorLevel { get; set; }

        // reasons:
        // 0 - Success
        // 1 - Failed to create AMTRevolution dir
        // 2 - Failed to create user dir
        // 3 - No backup settings in share
        // 4 - Failed to download backup settings
        // 5 - Failed to load downloaded settings
        // 6 - Failed to load settings

        public string reason { get; set; }

        public userSettingsWriter.userSettings USW { get; set; }

        private bool loadUserSettings(string path)
        {
            var usw = new userSettingsWriter(path);
            try
            {
                USW = usw.readUserSettings(path);
                return true;
            }
            catch (Exception FileLoadEx)
            {
                return false;
            }
        }

        public bool deleteCurSettings(string userId)
        {
            var uSf = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
            try
            {
                File.Delete(uSf);
                return true;
            }
            catch (Exception fDelEx)
            {
                return false;
            }
        }
        public void generateUserSettings(string userId)
        {
            var userSettingsPath = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
            var userSettingsNetworkPath = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper() + "\\settings.xbin";
            var usw = new userSettingsWriter(userSettingsPath);
            var uSf = new userSettingsWriter.userSettings(userId, "empty", "empty");
            usw.writeUserSettings(uSf, userSettingsPath);
            backupSettings(userSettingsPath, userSettingsNetworkPath);
        }

        private bool backupSettings(string source, string dest)
        {
            try
            {
                File.Copy(source, dest);
                return true;
            }
            catch (Exception fBEx)
            {
                return false;
            }
        }
    }
}
