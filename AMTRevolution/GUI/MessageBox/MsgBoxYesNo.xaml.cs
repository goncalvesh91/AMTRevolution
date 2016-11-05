// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

using System.Windows;

namespace AMTRevolution.GUI.MessageBox
{
    public partial class MsgBoxYesNo : Window
    {
        public bool result;

        public MsgBoxYesNo(string msg, string title)
        {
            InitializeComponent();
            message.Text = msg;
            this.Title = title;
        }

        private void Ybutton_Click(object sender, RoutedEventArgs e)
        {
            result = true;
            this.Close();
        }

        private void Nbutton_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            this.Close();
        }
    }
}
