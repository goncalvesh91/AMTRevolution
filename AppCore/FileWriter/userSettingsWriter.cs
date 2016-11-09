// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using AppCore.UserControl;

namespace AppCore.FileWriter
{
	public class userSettingsWriter
	{
		private string userSettingsPath;
		AppEvent.appEvent eventHandler;
		public userSettingsWriter(string path)
		{
			eventHandler = new AppEvent.appEvent(AppSettings.AppSettings.appEventsPath);
			userSettingsPath = path;
		}
		
		public userSettings readUserSettings(string path)
		{
			try
			{
				var file = File.OpenRead(path);
				var reader = new BinaryFormatter();
				var returnVal = (userSettings)reader.Deserialize(file);
				file.Close();
				return returnVal;
			}
			catch(Exception readuserLogsEx)
			{
				eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.UserControl.userName, Environment.MachineName, "Failed to load user settings");
				return new userSettings(string.Empty,string.Empty,string.Empty);
			}
		}
		
		public bool writeUserSettings(userSettings uS, string path)
		{
			try
			{
				var file = File.OpenWrite(path);
				var writer = new BinaryFormatter();
				writer.Serialize(file, uS);
				file.Close();
				eventHandler.addAppEvent(DateTime.Now, "Notification", UserControl.UserControl.userName, Environment.MachineName, "Settings saved to local file");
				return true;
			}
			catch (Exception mkfileEx)
			{
				eventHandler.addAppEvent(DateTime.Now, "Error", UserControl.UserControl.userName, Environment.MachineName, "Failed to save user settings locally");
				return false;
			}
		}
		
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
		[Serializable]
		public struct userSettings
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
			public string userId;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
			public string userOiUser;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
			public string userOiPw;
			
			public userSettings(string _userId, string _userOiUser,string _userOiPw)
			{
				this.userId = _userId;
				this.userOiUser = _userOiUser;
				this.userOiPw = _userOiPw;
			}
		}
		
		
	}
}
