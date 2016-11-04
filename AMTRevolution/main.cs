// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System.IO;
using System.Windows;
using AMTRevolution.ToolBox.UserControl;

namespace AMTRevolution
{
    public partial class App : Application
    {
        void AMTRevolution_Startup(object sender, StartupEventArgs e)
        {
            // Initial AMTRevolution Checks
            // Check VF NW share access
            UserControl.InitializeUserProperties();
            if(!Directory.Exists(@"\\vf-pt\fs\ANOC-UK\"))
            {
                switch(UserControl.userName)
                {
                    case "gonalvhf": case "goncarj3": case "Caramelos": case "Hugo Gonçalves":  MainWindow mainWindow = new MainWindow();
                                                                                                mainWindow.Show();
                                                                                                break;
                    default: MessageBox.Show("Out of VF-NW", "Exiting...", MessageBoxButton.OK, MessageBoxImage.Error);break;
                }
            }
        }
    }
}
