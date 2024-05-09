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
using Booking.DB;

namespace Booking.Views
{
    /// <summary>
    /// Логика взаимодействия для SingInWindow.xaml
    /// </summary>
    public partial class SingInWindow : Window
    {
        readonly SolidColorBrush MyBrush = (SolidColorBrush) Application.Current.Resources["PlaceholderGrey"];

        DB_BookingEntities1 Entities = new DB_BookingEntities1();

        MainWindow _MainWindowInstance;

        public SingInWindow(MainWindow MainWindowInstance)
        {
            InitializeComponent();
            TB_Login.Text = "Введите логин";
            TB_Login.Foreground = MyBrush;
            TB_Password.Text = "Введите пароль";
            TB_Password.Foreground = MyBrush;
            _MainWindowInstance = MainWindowInstance;
        }

        //Placeholder
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
            }
        }

        //Кнопки состояния
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_RollUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Методы
        private bool IsChecked()
        {
            if (string.IsNullOrEmpty(TB_Login.Text))
            {
                MessageBox.Show("Поле логин не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TB_Login.Text.Length > 20)
            {
                MessageBox.Show("Логин не может быть больше 20 символов!", "Ошибка!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return false;
            }
            if (TB_Login.Text.Length < 8)
            {
                MessageBox.Show("Логин не может быть меньше 8 символов!", "Ошибка!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TB_Password.Text))
            {
                MessageBox.Show("Поле пароль не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TB_Password.Text.Length > 16)
            {
                MessageBox.Show("Пароль не может быть больше 16 символов!", "Ошибка!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return false;
            }
            if (TB_Password.Text.Length < 8)
            {
                MessageBox.Show("Пароль не может быть меньше 8 символов!", "Ошибка!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void Authorization()
        {
            List<Admin> Admins = new List<Admin>();
            Admins = Entities.Admin.ToList();

            List<User> Users = new List<User>();
            Users = Entities.User.ToList();

            List<TO> TOCollection = new List<TO>();
            TOCollection = Entities.TO.ToList();

            if (!IsChecked())
                return;

            string Login = TB_Login.Text;
            string Password = TB_Password.Text;

            foreach (Admin admin in Admins)
            {
                if (admin.Login == Login && admin.Password == Password)
                {
                    _MainWindowInstance.AccessData = "Администратор";
                    _MainWindowInstance.ReturnedInstance = admin;
                    this.DialogResult = true;
                    this.Close();
                    return;
                }
            }

            foreach (User user in Users)
            {
                if (user.Login == Login && user.Password == Password)
                {
                    _MainWindowInstance.AccessData = "Пользователь";
                    _MainWindowInstance.ReturnedInstance = user;
                    this.DialogResult = true;
                    this.Close();
                    return;
                }
            }

            foreach (TO to in TOCollection)
            {
                if (to.Login == Login && to.Password == Password)
                {
                    _MainWindowInstance.AccessData = "ТО";
                    _MainWindowInstance.ReturnedInstance = to;
                    this.DialogResult = true;
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Логин или пароль не верны!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //Обработка событий кнопок
        private void Btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            Authorization();
        }

        //События кликов
        private void TB_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Authorization();
        }

        private void TB_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                TB_Password.Focus();
        }
    }
}
