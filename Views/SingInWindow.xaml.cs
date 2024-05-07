using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Booking.Views
{
    /// <summary>
    /// Логика взаимодействия для SingInWindow.xaml
    /// </summary>
    public partial class SingInWindow : Window
    {
        readonly SolidColorBrush MyBrush = (SolidColorBrush) Application.Current.Resources["PlaceholderGrey"];

        public SingInWindow()
        {
            InitializeComponent();
            TB_Login.Text = "Введите логин";
            TB_Login.Foreground = MyBrush;
            TB_Password.Text = "Введите пароль";
            TB_Password.Foreground = MyBrush;
        }

        private void MyGotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Login.Text == "Введите логин")
            {
                TB_Login.Text = string.Empty;
                TB_Login.Foreground = Brushes.Black;
            }
            if (TB_Password.Text == "Введите пароль")
            {
                TB_Password.Text = string.Empty;
                TB_Password.Foreground = Brushes.Black;
            }
        }

        private void MyLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_Login.Text))
            {
                TB_Login.Text = "Введите логин";
                TB_Login.Foreground = MyBrush;
            }
            if (string.IsNullOrEmpty(TB_Password.Text))
            {
                TB_Password.Text = "Введите пароль";
                TB_Password.Foreground = MyBrush;
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Unwrap_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_RollUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
