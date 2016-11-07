// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System;
using System.Linq;

namespace AppCore.AppSettings
{
	// Class with all the required app settings like folder/files paths.
	public static class AppSettings
	{
		// Base network path
		public const string networkPath = @"\\vf-pt\fs\ANOC-UK\ANOC-UK 1st LINE\1. RAN 1st LINE\AMTRevolution";

		// Main Menu state
		public static bool mainMenuState = false; // False = closed

		// Debug mode state
		public static bool debugMode = false;
		
		// User settings path in %AppData%
		public static string userSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AMTRevolution";
		
		// User settings path in network
		public const string userSettingsNetworkPath = @"\\vf-pt\fs\ANOC-UK\ANOC-UK 1st LINE\1. RAN 1st LINE\AMTRevolution\usersettings";
		
		// App Events path in %AppData%
		public static string appEventsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AMTRevolution";

		// App Events path in network
		public const string appEventsNetworkPath = @"\\vf-pt\fs\ANOC-UK\ANOC-UK 1st LINE\1. RAN 1st LINE\AMTRevolution\usersettings";
		
		// App Events path in network
		public const string permissionsFilePath = @"\\vf-pt\fs\ANOC-UK\ANOC-UK 1st LINE\1. RAN 1st LINE\AMTRevolution\permissions\permissions.xbin";
	}
}
