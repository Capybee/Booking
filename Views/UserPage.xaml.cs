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
using Booking.MyClasses;

namespace Booking.Views
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
        List<DateTime> Dates = new List<DateTime>();
        List<EventDate> EventDates = new List<EventDate>();
        DateTime SelectedDate = new DateTime();
        DB_BookingEntities4 Entities = new DB_BookingEntities4();

        public UserPage()
        {
            InitializeComponent();

            DateListCreate();
            CreateEventDateCollection();

            TBL_Date.Text = Dates.First().Date.ToString("D");
            SelectedDate = Dates.First().Date;
            TBL_DateBig.Text = Dates.First().Date.ToString("D");
            TBL_DateVip.Text = Dates.First().Date.ToString("D");
            SetTimeList(1);
            SetTimeList(2);
            SetTimeList(3);

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

        //Методы
        private void DateListCreate()
        {
            DateTime FirstDate = DateTime.Today.AddDays(1);
            DateTime EndDate = FirstDate.AddMonths(1);

            for (DateTime Date = FirstDate; Date < EndDate; Date = Date.AddDays(1))
            {
                Dates.Add(Date);
            }
        }

        private void CreateEventDateCollection()
        {
            foreach (var date in Dates)
            {
                EventDates.Add(new EventDate() { Start = date });
            }
        }

        private void SetTimeList(int HallId)
        {
            foreach (var date in EventDates)
            {
                if (date.Start == SelectedDate)
                {                  
                    List<Event> Events = Entities.Event.ToList();
                    bool IsContains = false;

                    foreach (var @event in Events)
                    {
                        if (@event.Date == date.Start && @event.Hall_Id == HallId)
                        {
                            IsContains = true;
                            break;
                        }
                    }

                    if (IsContains)
                    {

                        List<Event> CheckedEvents = new List<Event>();

                        foreach (var @event in Events)
                        {
                            if (@event.Date == date.Start)
                            {
                                CheckedEvents.Add(@event);
                            }
                        }

                        List<TimeClass> TimeCollection = new List<TimeClass>();

                        List<TimeSpan> ThinnedTimeCollection = new List<TimeSpan>();
                        
                        foreach (var time in date.StartTime)
                        {
                            ThinnedTimeCollection.Add(time);
                        }

                        foreach (var CheckedEvent in CheckedEvents)
                        {
                            for (int i = 0; i < date.StartTime.Count; i++)
                            {
                                if (date.StartTime[i] == CheckedEvent.TimeStart)
                                {
                                    TimeSpan StartedTime = date.StartTime[i];
                                    TimeSpan EndTime = CheckedEvent.TimeEnd;

                                    for (TimeSpan Time = StartedTime; Time <= EndTime; Time = Time.Add(new TimeSpan(0, 30, 0)))
                                    {
                                        ThinnedTimeCollection.Remove(Time);
                                    }

                                }
                            }
                        }

                        foreach (var time in ThinnedTimeCollection)
                        {
                            TimeCollection.Add(new TimeClass() { Time = SelectedDate.Add(time).ToString("t") });
                        }

                       if (HallId == 1)
                       {
                            DG_Time.ItemsSource = null;
                            DG_Time.ItemsSource = TimeCollection;
                       }
                       else if (HallId == 2)
                       {
                            DG_TimeBig.ItemsSource = null;
                            DG_TimeBig.ItemsSource = TimeCollection;
                       }
                       else if (HallId == 3)
                       {
                            DG_TimeVip.ItemsSource = null;
                            DG_TimeVip.ItemsSource = TimeCollection;
                       }
                    }
                    else
                    {
                        if (HallId == 1)
                        {
                            DG_Time.ItemsSource = null;

                            List<TimeClass> TimeCollection = new List<TimeClass>();

                            foreach (var time in date.StartTime)
                            {
                                TimeCollection.Add(new TimeClass() { Time = SelectedDate.Add(time).ToString("t") });
                            }

                            DG_Time.ItemsSource = TimeCollection;
                        }
                        else if (HallId == 2)
                        {
                            DG_TimeBig.ItemsSource = null;

                            List<TimeClass> TimeCollection = new List<TimeClass>();

                            foreach (var time in date.StartTime)
                            {
                                TimeCollection.Add(new TimeClass() { Time = SelectedDate.Add(time).ToString("t") });
                            }

                            DG_TimeBig.ItemsSource = TimeCollection;
                        }
                        else if (HallId == 3)
                        {
                            DG_TimeVip.ItemsSource = null;

                            List<TimeClass> TimeCollection = new List<TimeClass>();

                            foreach (var time in date.StartTime)
                            {
                                TimeCollection.Add(new TimeClass() { Time = SelectedDate.Add(time).ToString("t") });
                            }

                            DG_TimeVip.ItemsSource = TimeCollection;
                        }
                    }
                }
            }
        }

        //Placeholder
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
