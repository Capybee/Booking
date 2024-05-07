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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Booking.Views
{
    /// <summary>
    /// Логика взаимодействия для TOPage.xaml
    /// </summary>
    public partial class TOPage : Page
    {
        SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];

        public TOPage()
        {
            InitializeComponent();
        }

        //Методы PlaceHolder-а
        private void FIOGotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_FIO.Text == "Введите ФИО пользователя")
            {
                TB_FIO.Text = string.Empty;
                TB_FIO.Foreground = Brushes.Black;
            }
            if (TB_Post.Text == "Введите должность")
            {
                TB_Post.Text = string.Empty;
                TB_Post.Foreground = Brushes.Black;
            }
        }

        private void LoginGotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Login.Text == "Введите логин")
            {
                TB_Login.Text = string.Empty;
                TB_Login.Foreground = Brushes.Black;
            }
        }

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Password.Text == "Введите пароль")
            {
                TB_Password.Text = string.Empty;
                TB_Password.Foreground = Brushes.Black;
            }
        }

        private void PostGotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Post.Text == "Введите должность")
            {
                TB_Post.Text = string.Empty;
                TB_Post.Foreground = Brushes.Black;
            }
        }

        private void FIOLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_FIO.Text))
            {
                TB_FIO.Text = "Введите ФИО пользователя";
                TB_FIO.Foreground = MyBrush;
            }
        }

        private void LoginLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_Login.Text))
            {
                TB_Login.Text = "Введите логин";
                TB_Login.Foreground = MyBrush;
            }
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_Password.Text))
            {
                TB_Password.Text = "Введите пароль";
                TB_Password.Foreground = MyBrush;
                return;
            }
        }

        private void PostLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_Post.Text))
            {
                TB_Post.Text = "Введите должность";
                TB_Post.Foreground = MyBrush;
                return;
            }
        }

        private void MyLostFocus()
        {
            if (string.IsNullOrEmpty(TB_FIO.Text))
            {
                TB_FIO.Text = "Введите ФИО пользователя";
                TB_FIO.Foreground = MyBrush;

            }
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
            if (string.IsNullOrEmpty(TB_Post.Text))
            {
                TB_Post.Text = "Введите должность";
                TB_Post.Foreground = MyBrush;

            }
        }


        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            MyLostFocus();
            TB_FIO.GotFocus += FIOGotFocus;
            TB_FIO.LostFocus += FIOLostFocus;
            TB_Login.GotFocus += LoginGotFocus;
            TB_Login.LostFocus += LoginLostFocus;
            TB_Password.GotFocus += PasswordGotFocus;
            TB_Password.LostFocus += PasswordLostFocus;
            TB_Post.GotFocus += PostGotFocus;
            TB_Post.LostFocus += PostLostFocus;
        }
    }
}
