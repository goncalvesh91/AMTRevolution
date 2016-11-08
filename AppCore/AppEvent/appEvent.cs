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

        public List<appEventEntry> getEvents()
        {
            var file = File.OpenRead(eventPath);
            var reader = new BinaryFormatter();
            var returnVal = (List<appEventEntry>)reader.Deserialize(file);
            file.Close();
            return returnVal;
        }

        public bool routineEventBackupToShare(string userId)
        {
            if (Directory.Exists(AppSettings.AppSettings.appEventsNetworkPath))
            {
                try
                {
                    File.Copy(AppSettings.AppSettings.appEventsPath, AppSettings.AppSettings.appEventsNetworkPath);
                    return true;
                }
                catch (Exception evBckEx)
                {
                    var eventHandler = new appEvent(AppSettings.AppSettings.appEventsPath);
                    eventHandler.addAppEvent(DateTime.Now, "Error", userId, "Failed to backup events to share drive");
                    return false;
                }
            }
            return false;
        }

        public bool addAppEvent(DateTime timeStamp, string eventType, string userName, string error)
        {
            // New event
            var nEvent = new appEventEntry(timeStamp, eventType, userName, error);
            var eventFile = new List<appEventEntry>();
            try
            {
                if (File.Exists(AppSettings.AppSettings.appEventsPath))
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
            catch (Exception newEvEx)
            {
                return false;
            }

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public struct appEventEntry
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string timeStamp;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string entryType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string userName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15000)]
            public string error;

            public appEventEntry(DateTime _timeStamp, string _entryType, string _userName, string _error)
            {
                this.timeStamp = _timeStamp.ToString();
                this.entryType = _entryType;
                this.userName = _userName;
                this.error = _error;
            }
        }
    }
}
