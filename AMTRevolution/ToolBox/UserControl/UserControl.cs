// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace AMTRevolution.ToolBox.UserControl
{
    class UserControl
    {
        public static bool hasOICredentials
        {
            get;
            private set;
        }
        public static string userName
        {
            get;
            private set;
        }
        public static string[] fullName
        {
            get;
            private set;
        }
        public static string department
        {
            get;
            private set;
        }

        public static void InitializeUserProperties()
        {
            userName = GetUserDetails("Username");
            fullName = GetUserDetails("Name").Split(' ');
            for (int c = 0; c < fullName.Length; c++)
                fullName[c] = fullName[c].Replace(",", string.Empty);
            department = GetUserDetails("Department").Contains("2nd Line RAN") ? "2nd Line RAN Support" : "1st Line RAN Support";
            //UserFolder.ResolveUserFolder();
            //SettingsFile.ResolveSettingsFile();
            //hasOICredentials = !string.IsNullOrEmpty(SettingsFile.OIUsername);
            //UserFolder.Initialize();
        }

        public static string GetUserDetails(string detail)
        {
            UserPrincipal current = UserPrincipal.Current;
            if (detail != null)
            {
                switch (detail)
                {
                    case "Name":
                        if (current.SamAccountName.Contains("Caramelos"))
                            return "Gonçalves, Rui";
                        if (current.SamAccountName.Contains("Hugo Gonçalves"))
                            return "Gonçalves, Hugo";
                        return current.DisplayName;
                    case "Username":
                        return current.SamAccountName;
                    case "Department":
                        if (current.SamAccountName.Contains("Caramelos"))
                            return "1st Line RAN";
                        else
                        {
                            DirectoryEntry underlyingObject = current.GetUnderlyingObject() as DirectoryEntry;
                            if (underlyingObject.Properties.Contains("department"))
                                return underlyingObject.Properties["department"].Value.ToString();
                        }
                        break;
                    case "NetworkDomain":
                        return System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                }
            }
            return string.Empty;
        }
    }
}
