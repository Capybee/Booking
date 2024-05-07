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
using Booking.Views;

namespace Booking
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SingInWindow SingInWindowInstance = new SingInWindow();
            SingInWindowInstance.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SingInWindowInstance.Topmost = true;
            SingInWindowInstance.Show();
            Frame_Main.Navigate(new UserPage());
        }

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
