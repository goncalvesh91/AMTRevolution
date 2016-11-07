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
			var userSettingsPath = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper();
			var userSettingsNetworkPath = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper();
			// Check if the path already exists, if not create it
			if(!Directory.Exists(AppSettings.AppSettings.userSettingsPath))
			{
				try
				{
					Directory.CreateDirectory(AppSettings.AppSettings.userSettingsPath);
					// Check if user directory exists, if not create it and download settings
					if(!Directory.Exists(userSettingsPath))
					{
						try
						{
							Directory.CreateDirectory(userSettingsPath);
							if(Directory.Exists(userSettingsNetworkPath))
							{
								var source = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper() + "\\settings.xbin";
								var dest = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
								if(File.Exists(source))
								{
									File.Copy(source,dest);
									if(loadUserSettings(dest))
									{
										// Load settings here
										isLoaded = true;
										errorLevel = 0;
										reason = "Settings loaded from share backup";
									}
									else
									{
										isLoaded = false; // Settings loaded
										errorLevel = 5;
										reason = "Failed to load settings from share backup";
									}
								}
								else
								{
									isLoaded = false;
									errorLevel = 3;
									reason = "No settings backup on share";
								}
							}
						}
						catch(Exception mkdirEx)
						{
							isLoaded = false;
							errorLevel = 2;
							reason = "Failed to create user folder" + Environment.NewLine;
							reason += mkdirEx.Message;
						}
					}
				}
				catch(Exception mkdir)
				{
					isLoaded = false;
					errorLevel = 1;
					reason = "AMTRevolution does no exist" + Environment.NewLine + "Failed to create AMTRevolution folder" + Environment.NewLine;
					reason += mkdir.Message;
				}
			}
			else
			{
				// Check if user directory exists, if not create it and download settings
				if(!Directory.Exists(userSettingsPath))
				{
					try
					{
						Directory.CreateDirectory(userSettingsPath);
						if(Directory.Exists(userSettingsNetworkPath))
						{
							var source = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper() + "\\settings.xbin";
							var dest = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
							if(File.Exists(source))
							{
								File.Copy(source,dest);
								if(loadUserSettings(dest))
								{
									isLoaded = true; // Settings loaded
									errorLevel = 0;
									reason = "Settings loaded from share backup";
								}
								else
								{
									isLoaded = false; // Settings loaded
									errorLevel = 5;
									reason = "Failed to load settings from share backup";
								}
							}
							else
							{
								isLoaded = false;
								errorLevel = 3;
								reason = "No settings backup on share";
							}
						}
					}
					catch(Exception mkdirEx)
					{
						isLoaded = false;
						errorLevel = 2;
						reason = "Failed to create user folder" + Environment.NewLine;
						reason += mkdirEx.Message;
					}
				}
				else
				{ // User folder exists
					var source = AppSettings.AppSettings.userSettingsNetworkPath + "\\" + userId.ToUpper() + "\\settings.xbin";
					var dest = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
					if(File.Exists(dest))
					{
						if(loadUserSettings(dest))
						{
							isLoaded = true; // Settings loaded
							errorLevel = 0;
							reason = "Settings loaded";
						}
						else
						{
							isLoaded = false; // Settings loaded
							errorLevel = 6;
							reason = "Failed to load settings";
							deleteCurSettings(userId);
						}
					}
					else
					{
						if(File.Exists(source))
						{
							try
							{
								File.Copy(source,dest);
								if(loadUserSettings(dest))
								{
									isLoaded = true; // Settings loaded
									errorLevel = 0;
									reason = "Settings loaded from share backup";
								}
								else
								{
									isLoaded = false; // Settings loaded
									errorLevel = 5;
									reason = "Failed to load settings from share backup";
								}
							}
							catch(Exception dlEx)
							{
								isLoaded = false; // Settings loaded
								errorLevel = 5;
								reason = "Failed to load settings from share backup" + Environment.NewLine;
								reason += dlEx.Message;
							}
						}
						else
						{
							isLoaded = false;
							errorLevel = 3;
							reason = "No settings backup on share";
						}
					}
				}
			}
		}
		
		public bool isLoaded {get;set;}
		
		public int errorLevel {get;set;}
		
		// reasons:
		// 0 - Success
		// 1 - Failed to create AMTRevolution dir
		// 2 - Failed to create user dir
		// 3 - No backup settings in share
		// 4 - Failed to download backup settings
		// 5 - Failed to load downloaded settings
		// 6 - Failed to load settings
		
		public string reason {get;set;}
		
		public userSettingsWriter.userSettings USW {get;set;}
		
		private bool loadUserSettings(string path)
		{
			var usw = new userSettingsWriter(path);
			try
			{
				USW = usw.readUserSettings(path);
				return true;
			}
			catch(Exception FileLoadEx)
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
			catch(Exception fDelEx)
			{
				return false;
			}
		}
		public void generateUserSettings(string userId)
		{
			var userSettingsPath = AppSettings.AppSettings.userSettingsPath + "\\" + userId.ToUpper() + "\\settings.xbin";
			var usw = new userSettingsWriter(userSettingsPath);
			var uSf = new userSettingsWriter.userSettings(userId,"empty","empty");
			usw.writeUserSettings(uSf,userSettingsPath);
		}		
	}
}
