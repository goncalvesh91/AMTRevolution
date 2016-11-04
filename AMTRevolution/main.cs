// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System.Windows;

namespace AMTRevolution
{
    public partial class App : Application
    {
        void AMTRevolution_Startup(object sender, StartupEventArgs e)
        {
            // Initial AMTRevolution Checks


            // Run the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
