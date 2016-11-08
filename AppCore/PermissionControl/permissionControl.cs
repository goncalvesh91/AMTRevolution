// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System.Collections.Generic;

namespace AppCore.PermissionControl
{
    public class permissionControl
    {
        // Global permissions
        private List<FileWriter.permissionsWriter.userPermission> globalPermissions { get; set; }

        // Init class, here it is called the permissionsWriter class which reads/creates the permissions.xbin
        public permissionControl()
        {
            var permWriter = new FileWriter.permissionsWriter();
            var globalPerms = permWriter.getPermissionsList();
            globalPermissions = globalPerms;
        }

        public int getUserPermission(string user)
        {
            foreach(FileWriter.permissionsWriter.userPermission userPerm in globalPermissions )
            {
                if (userPerm.userName.Equals(user, System.StringComparison.InvariantCultureIgnoreCase))
                    return userPerm.permission;
            }
            return 0; // If no permission is found return no permission
        }
    }
}
