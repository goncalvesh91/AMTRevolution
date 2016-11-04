// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System.IO;
using System.Windows;

namespace AMTRevolution
{
    public partial class App : Application
    {
        void AMTRevolution_Startup(object sender, StartupEventArgs e)
        {
            // Initial AMTRevolution Checks
            // Check VF NW share access
            if(!Directory.Exists(@"\\vf-pt\fs\ANOC-UK\"))
            {

            }

            // Run the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
