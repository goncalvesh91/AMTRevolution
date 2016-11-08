// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppCore.FileWriter
{
    public class permissionsWriter
    {
        public permissionsWriter()
        {
            // Check if permissions file exists, if not create it
            if (!File.Exists(AppSettings.AppSettings.permissionsFilePath))
            {
                generatePermissionsFile(AppSettings.AppSettings.permissionsFilePath);
            }
        }

        public List<userPermission> getPermissionsList()
        {
            var eventHandler = new AppEvent.appEvent(AppSettings.AppSettings.appEventsPath);
            try
            {
                var file = File.OpenRead(AppSettings.AppSettings.permissionsFilePath);
                var reader = new BinaryFormatter();
                var returnVal = (List<userPermission>)reader.Deserialize(file);
                file.Close();
                return returnVal;
            }
            catch(Exception getPermEx)
            {
                eventHandler.addAppEvent(DateTime.Now, "Failed to read the permissions file" + Environment.NewLine + getPermEx.Message);
                return null;
            }
        }

        private bool generatePermissionsFile(string path)
        {
            try
            {
                // Generate file with the root users
                var permFile = new List<userPermission>();
                var userPerm = new userPermission("gonalvhf", "Hugo Gonçalves", 1);
                permFile.Add(userPerm);
                userPerm = new userPermission("goncarj3", "Rui Gonçalves", 1);
                permFile.Add(userPerm);
                userPerm = new userPermission("Caramelos", "Rui Gonçalves", 1);
                permFile.Add(userPerm);
                userPerm = new userPermission("Hugo Gonçalves", "Hugo Gonçalves", 1);
                permFile.Add(userPerm);
                var file = File.OpenWrite(path);
                var writer = new BinaryFormatter();
                writer.Serialize(file, permFile);
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                var eventHandler = new AppEvent.appEvent(AppSettings.AppSettings.appEventsPath);
                eventHandler.addAppEvent(DateTime.Now, "Failed to generate permissions files");
                return false;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        [Serializable]
        public struct userPermission
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string userName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string name;
            public int permission;

            public userPermission(string _userName, string _name, int _permission)
            {
                this.userName = _userName;
                this.name = _name;
                this.permission = _permission;
            }
        }
    }
}
