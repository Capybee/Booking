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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
        public UserPage()
        {
            InitializeComponent();
            
            Tb_EventTitle.Text = "Введите название мероприятия";
            Tb_EventTitle.Foreground = MyBrush;
            TB_AdditionalEquipment.Text = "Укажите необходимое оборудование";
            TB_AdditionalEquipment.Foreground = MyBrush;

            Tb_EventTitleBig.Text = "Введите название мероприятия";
            Tb_EventTitleBig.Foreground = MyBrush;
            TB_AdditionalEquipmentBig.Text = "Укажите необходимое оборудование";
            TB_AdditionalEquipmentBig.Foreground = MyBrush;

            Tb_EventTitleVip.Text = "Введите название мероприятия";
            Tb_EventTitleVip.Foreground = MyBrush;
            TB_AdditionalEquipmentVip.Text = "Укажите необходимое оборудование";
            TB_AdditionalEquipmentVip.Foreground = MyBrush;
        }

        private void Tb_EventTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_EventTitle.Text == "Введите название мероприятия")
            {
                Tb_EventTitle.Text = string.Empty;
                Tb_EventTitle.Foreground = Brushes.Black;
            }
            if (Tb_EventTitleBig.Text == "Введите название мероприятия")
            {
                Tb_EventTitleBig.Text = string.Empty;
                Tb_EventTitleBig.Foreground = Brushes.Black;
            }
            if (Tb_EventTitleVip.Text == "Введите название мероприятия")
            {
                Tb_EventTitleVip.Text = string.Empty;
                Tb_EventTitleVip.Foreground = Brushes.Black;
            }
        }

        private void Tb_EventTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_EventTitleBig.Text))
            {
                Tb_EventTitleBig.Text = "Введите название мероприятия";
                SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
                Tb_EventTitleBig.Foreground = MyBrush;
            }
            if (string.IsNullOrEmpty(Tb_EventTitle.Text))
            {
                Tb_EventTitle.Text = "Введите название мероприятия";
                SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
                Tb_EventTitle.Foreground = MyBrush;
            }
            if (string.IsNullOrEmpty(Tb_EventTitleVip.Text))
            {
                Tb_EventTitleVip.Text = "Введите название мероприятия";
                SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
                Tb_EventTitleVip.Foreground = MyBrush;
            }
        }

        private void TB_AdditionalEquipment_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_AdditionalEquipment.Text == "Укажите необходимое оборудование")
            {
                TB_AdditionalEquipment.Text = string.Empty;
                TB_AdditionalEquipment.Foreground = Brushes.Black;
            }
            if (TB_AdditionalEquipmentBig.Text == "Укажите необходимое оборудование")
            {
                TB_AdditionalEquipmentBig.Text = string.Empty;
                TB_AdditionalEquipmentBig.Foreground = Brushes.Black;
            }
            if (TB_AdditionalEquipmentVip.Text == "Укажите необходимое оборудование")
            {
                TB_AdditionalEquipmentVip.Text = string.Empty;
                TB_AdditionalEquipmentVip.Foreground = Brushes.Black;
            }
        }

        private void TB_AdditionalEquipment_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_AdditionalEquipment.Text))
            {
                TB_AdditionalEquipment.Text = "Укажите необходимое оборудование";
                SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
                TB_AdditionalEquipment.Foreground = MyBrush;
            }
            if (string.IsNullOrEmpty(TB_AdditionalEquipmentBig.Text))
            {
                TB_AdditionalEquipmentBig.Text = "Укажите необходимое оборудование";
                SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
                TB_AdditionalEquipmentBig.Foreground = MyBrush;
            }
            if (string.IsNullOrEmpty(TB_AdditionalEquipmentVip.Text))
            {
                TB_AdditionalEquipmentVip.Text = "Укажите необходимое оборудование";
                SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
                TB_AdditionalEquipmentVip.Foreground = MyBrush;
            }
        }
    }
}
