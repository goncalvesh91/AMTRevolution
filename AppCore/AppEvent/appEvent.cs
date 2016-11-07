// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppCore.AppEvent
{
	/// <summary>
	/// Class that stores all AppEvents
	/// </summary>
	public class appEvent
	{
		private string eventPath;
		
		public appEvent(string path)
		{
			eventPath = path;
		}
		
		public bool addAppEvent(DateTime timeStamp, string error)
		{
			// New event
			var nEvent = new appEventEntry(timeStamp,error);
			var eventFile = new List<appEventEntry>();
			try
			{
				if(File.Exists(AppSettings.AppSettings.appEventsPath))
				{
					var file = File.OpenRead(AppSettings.AppSettings.appEventsPath);
					var reader = new BinaryFormatter();
					var data = (List<appEventEntry>)reader.Deserialize(file);
					file.Close();
					data.Add(nEvent);
					file = File.OpenWrite(AppSettings.AppSettings.appEventsPath);
					var writer = new BinaryFormatter();
					writer.Serialize(file, data);
					file.Close();
					return true;
				}
				else
				{
					eventFile.Add(nEvent);
					var file = File.OpenWrite(AppSettings.AppSettings.appEventsPath);
					var writer = new BinaryFormatter();
					writer.Serialize(file, eventFile);
					file.Close();
					return true;
				}
			}
			catch(Exception newEvEx)
			{
				return false;
			}
			
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
		[Serializable]
		private struct appEventEntry
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
			public string timeStamp;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15000)]
			public string error;
			
			public appEventEntry(DateTime _timeStamp, string _error)
			{
				this.timeStamp = _timeStamp.ToLongTimeString();
				this.error = _error;
			}
		}
	}
}
