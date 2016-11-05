// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using AppCore.AppSettings;
using AppCore.Tools;
using AMTRevolution.GUI.MessageBox;
using AMTRevolution.GUI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AMTRevolution
{
    public partial class MainWindow : Window
    {
        public MainWindow(bool debugMode)
        {
            InitializeComponent();
            if (!debugMode)
            {
                debugModeBtt.Visibility = Visibility.Collapsed;
            }
        }

        #region bttActions
        private void exitBtt_Click(object sender, RoutedEventArgs e)
        {
            var msgBox = new MsgBoxYesNo("Are you sure?", "Exit");
            msgBox.ShowDialog(this);
            if (msgBox.result)
                this.Close();
        }

        private void minBtt_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
            ShowHideMenu("showCCPanel", closureCodePanel);
            ccBtt.IsEnabled = false;
        }

        private void scriptsBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void outagesBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void detachCcBtt_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("hideCCPanel", closureCodePanel);
            ccBtt.IsEnabled = true;
            var ccBox = new ClosureCode(incCcTxtBox.Text,initCcTxtBox.Text,CcTxtBox.Text);
            ccBox.Show(this);
        }

        private void minCcBtt_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("hideCCPanel", closureCodePanel);
            ccBtt.IsEnabled = true;
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (AppSettings.mainMenuState)
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

        private void incCcTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (incCcTxtBox.Text.Length > 0)
                {
                    string CompINC_CRQ = closureCode.CompleteINC_CRQ_TAS(incCcTxtBox.Text, "INC");
                    if (CompINC_CRQ != "error")
                    {
                        incCcTxtBox.Text = CompINC_CRQ;
                    }
                    else
                    {
                        MessageBox.Show("INC number must only contain digits!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void incCcTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CcTxtBox.Text = "";
            if (incCcTxtBox.Text.Length == 15 & initCcTxtBox.Text.Length == 3)
            {
                labelCC.Text = "";
                incCcTxtBox.TextChanged -= incCcTxtBox_TextChanged;
                initCcTxtBox.TextChanged -= initCcTxtBox_TextChanged;
                incCcTxtBox.Text = incCcTxtBox.Text.ToUpper();
                initCcTxtBox.Text = initCcTxtBox.Text.ToUpper();
                incCcTxtBox.TextChanged += incCcTxtBox_TextChanged;
                initCcTxtBox.TextChanged += initCcTxtBox_TextChanged;
                if (Tools.IsAllDigits(incCcTxtBox.Text.Substring(3, incCcTxtBox.Text.Length - 3)))
                {
                    int[] rng = new int[12];
                    for (int c = 0; c <= 11; c++)
                    {
                        rng[c] = Convert.ToInt32(incCcTxtBox.Text.Substring(c + 3, 1));
                    }
                    int SumFw = rng[0] * 2 + rng[1] * 3 + rng[2] * 4 + rng[3] * 5 + rng[4] * 6 + rng[5] * 7 + rng[6] * 8 + rng[7] * 9 + rng[8] * 10 + rng[9] * 11 + rng[10] * 12 + rng[11] * 13;
                    int SumBw = rng[11] * 2 + rng[10] * 3 + rng[9] * 4 + rng[8] * 5 + rng[7] * 6 + rng[6] * 7 + rng[5] * 8 + rng[4] * 9 + rng[3] * 10 + rng[2] * 11 + rng[1] * 12 + rng[0] * 13;
                    string hx = (SumFw * SumBw).ToString("X");
                    if (hx.Length < 5)
                    {
                        for (int c = 1; c <= 5 - hx.Length; c++)
                        {
                            CcTxtBox.Text += "0";
                        }
                    }
                    CcTxtBox.Text += hx + " " + initCcTxtBox.Text;
                }
                else
                {
                    MessageBox.Show("INC/CRQ can only contain numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    initCcTxtBox.TextChanged -= initCcTxtBox_TextChanged;
                    initCcTxtBox.Text = "";
                    initCcTxtBox.TextChanged += initCcTxtBox_TextChanged;
                }
            }
            else
            {
                labelCC.Text = "";
                if (incCcTxtBox.Text.Length > 0 && incCcTxtBox.Text.Length < 15)
                {
                    labelCC.Text = "Press ENTER key to complete INC number";
                }
                else
                {
                    if (initCcTxtBox.Text.Length != 15)
                        labelCC.Text = "Insert INC/CRQ number";
                    else
                        labelCC.Text = "";
                }
            }
        }

        private void initCcTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initCcTxtBox.Text.Length == 3 & incCcTxtBox.Text.Length == 15)
            {
                initLabel.Text = "";
                incCcTxtBox_TextChanged(sender, e);
            }
            else
            {
                CcTxtBox.Text = "";
                if (initCcTxtBox.Text.Length < 3)
                {
                    initLabel.Text = "Insert your initials";
                }
                else
                {
                    initCcTxtBox.TextChanged -= initCcTxtBox_TextChanged;
                    initCcTxtBox.Text = initCcTxtBox.Text.ToUpper();
                    initCcTxtBox.TextChanged += initCcTxtBox_TextChanged;
                }
            }
        }
    }
}
