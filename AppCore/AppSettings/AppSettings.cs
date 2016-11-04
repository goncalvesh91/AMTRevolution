// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.AppSettings
{
    // Class with all the required app settings like folder/files paths.
    public class AppSettings
    {
        // Base network path
        public const string networkPath = "\\\\vf-pt\\fs\\ANOC-UK\\";

        // Main Menu state
        public static bool mainMenuState = false; // False = closed
    }
}
