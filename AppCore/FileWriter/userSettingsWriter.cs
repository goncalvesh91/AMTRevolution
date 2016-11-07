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
		
		public userSettingsWriter(string path)
		{
			userSettingsPath = path;
		}
		
		public userSettings readUserSettings(string path)
		{
			var file = File.OpenRead(path);
			var reader = new BinaryFormatter();
			var returnVal = (userSettings)reader.Deserialize(file);
			file.Close();
			return returnVal;
		}
		
		public bool writeUserSettings(userSettings uS, string path)
		{
			try
			{
				var file = File.OpenWrite(path);
				var writer = new BinaryFormatter();
				writer.Serialize(file, uS);
				file.Close();
				return true;
			}
			catch (Exception mkfileEx)
			{
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
