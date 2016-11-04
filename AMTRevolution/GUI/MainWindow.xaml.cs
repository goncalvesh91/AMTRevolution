// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using AppCore.AppSettings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AMTRevolution
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region bttActions
        private void exitBtt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minBtt_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void mainMenuBtt_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("showmainMenuPanel", mainMenuPanel);
        }

        private void settingsBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void aboutBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchSiteBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void templatesBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void netcoolParserBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ccBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void scriptsBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void outagesBtt_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch(AppSettings.mainMenuState)
            {
                case true: ShowHideMenu("hidemainMenuPanel", mainMenuPanel); AppSettings.mainMenuState = false; break;
            }
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void ShowHideMenu(string Storyboard, Grid pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            AppSettings.mainMenuState = true;
            sb.Begin(pnl);
        }
    }
}
