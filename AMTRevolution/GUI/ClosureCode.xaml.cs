using AppCore.Tools;
using System;
using System.Windows;
using System.Windows.Input;

namespace AMTRevolution.GUI
{
    /// <summary>
    /// Interaction logic for ClosureCode.xaml
    /// </summary>
    public partial class ClosureCode : Window
    {
        public ClosureCode(string INC, string INIT, string CC)
        {
            InitializeComponent();
            this.initCcTxtBox.Text = INIT;
            this.CcTxtBox.Text = CC;
        }

        private void exitBtt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minBtt_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

                        System.Windows.MessageBox.Show("INC number must only contain digits!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void incCcTxtBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
                    System.Windows.MessageBox.Show("INC/CRQ can only contain numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    labelCC.Text = "ENTER to complete";
                }
                else
                {
                    if (initCcTxtBox.Text.Length != 15)
                        labelCC.Text = "Insert INC/CRQ";
                    else
                        labelCC.Text = "";
                }
            }
        }

        private void initCcTxtBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        public void Show(Window owner)
        {
            this.Owner = owner;
            this.Show();
        }

        public void ShowDialog(Window owner)
        {
            this.Owner = owner;
            this.ShowDialog();
        }
    }
}
