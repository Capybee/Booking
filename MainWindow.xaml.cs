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
using Booking.DB;
using Booking.Views;

namespace Booking
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string AccessData = "";
        public object ReturnedInstance;
        private Admin AdminInstance;
        private User UserInstance;
        private TO TOInstance;

        public MainWindow()
        {
            InitializeComponent();
            Frame_Main.Navigate(new EmptyPage());
            Frame_Main.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SingInWindow SingInWindowInstance = new SingInWindow(this);
            SingInWindowInstance.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SingInWindowInstance.Topmost = true;
            if (SingInWindowInstance.ShowDialog() == true)
            {
                if (AccessData == "Администратор")
                {
                    AdminInstance = ReturnedInstance as Admin;
                    Frame_Main.Navigate(new AdminPage());
                }
                else if (AccessData == "Пользователь")
                {
                    UserInstance = ReturnedInstance as User;
                    Frame_Main.Navigate(new UserPage());
                }
                else if (AccessData == "ТО")
                {
                    TOInstance = ReturnedInstance as TO;
                    Frame_Main.Navigate(new TOPage());
                }
            }
        }

        //Кнопки состояния
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Unwrap_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Width = 1280;
            this.Height = 720;
            this.ResizeMode = ResizeMode.CanResize;
        }

        private void Btn_RollUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
