using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Booking.DB;
using Booking.MyClasses;
using Microsoft.Win32;
using System.IO;

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
        DateTime SelectedDateBig = new DateTime();
        DateTime SelectedDateVip = new DateTime();
        DB_BookingEntities4 Entities = new DB_BookingEntities4();
        List<AdditionalFile> AdditionalFiles = new List<AdditionalFile>();
        List<AdditionalFile> AdditionalFilesBig = new List<AdditionalFile>();
        List<AdditionalFile> AdditionalFilesVip = new List<AdditionalFile>();
        List<string> EventEndTime = new List<string>();
        List<string> EventEndTimeBig = new List<string>();
        List<string> EventEndTimeVip = new List<string>();

        public UserPage()
        {
            InitializeComponent();

            DateListCreate();
            CreateEventDateCollection();

            TBL_Date.Text = Dates.First().Date.ToString("D");
            SelectedDate = Dates.First().Date;
            SelectedDateBig = Dates.First().Date;
            SelectedDateVip = Dates.First().Date;
            TBL_DateBig.Text = Dates.First().Date.ToString("D");
            TBL_DateVip.Text = Dates.First().Date.ToString("D");
            SetTimeList(1, SelectedDate);
            SetTimeList(2, SelectedDateVip);
            SetTimeList(3, SelectedDateBig);

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

        private void SetTimeList(int HallId, DateTime _SelectedDate)
        {
            foreach (var date in EventDates)
            {
                if (date.Start == _SelectedDate)
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
                            TimeCollection.Add(new TimeClass() { Time = _SelectedDate.Add(time).ToString("t") });
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
                                TimeCollection.Add(new TimeClass() { Time = _SelectedDate.Add(time).ToString("t") });
                            }

                            DG_Time.ItemsSource = TimeCollection;
                        }
                        else if (HallId == 2)
                        {
                            DG_TimeBig.ItemsSource = null;

                            List<TimeClass> TimeCollection = new List<TimeClass>();

                            foreach (var time in date.StartTime)
                            {
                                TimeCollection.Add(new TimeClass() { Time = _SelectedDate.Add(time).ToString("t") });
                            }

                            DG_TimeBig.ItemsSource = TimeCollection;
                        }
                        else if (HallId == 3)
                        {
                            DG_TimeVip.ItemsSource = null;

                            List<TimeClass> TimeCollection = new List<TimeClass>();

                            foreach (var time in date.StartTime)
                            {
                                TimeCollection.Add(new TimeClass() { Time = _SelectedDate.Add(time).ToString("t") });
                            }

                            DG_TimeVip.ItemsSource = TimeCollection;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет входную строку на соответствие параметрам.
        /// </summary>
        /// <param name="Text">Проверяемая строка</param>
        /// <param name="MaxLength">Максимальная длина строки</param>
        /// <param name="MinLength">Минимальная длина строки</param>
        /// <param name="ForbiddenSymbols">Запрещенные к использованию символы</param>
        /// <returns>
        /// -1 - валидация успешна
        /// 0 - введена пустая строка
        /// 1 - длина строки превысила допустимую
        /// 2 - длина строки меньше допустимой
        /// 3 - строка содержит запрещённый к использованию символ
        /// </returns>
        private int Validate(string Text, int MaxLength, int MinLength, string ForbiddenSymbols)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return 0;
            }
            if (Text.Length > MaxLength) 
            {
                return 1;
            }
            if (Text.Length < MinLength)
            {
                return 2;
            }
            foreach (char s in ForbiddenSymbols) 
            {
                if (Text.Contains(s))
                {
                    return 3;
                }
            }
            return -1;
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

        //Обработка кликов
        private void Btn_PrevDate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != Dates.First())
            {
                for (int i = 0; i < Dates.Count; i++)
                {
                    if (Dates[i] == SelectedDate)
                    {
                        SelectedDate = Dates[i - 1];
                        TBL_Date.Text = SelectedDate.ToString("D");
                        SetTimeList(1, SelectedDate);
                        return;
                    }
                }
            }
            else
            {
                SelectedDate = Dates.Last();
                TBL_Date.Text = SelectedDate.ToString("D");
                SetTimeList(1, SelectedDate);
            }
        }

        private void Btn_NextDate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != Dates.Last())
            {
                for (int i = 0; i < Dates.Count; i++)
                {
                    if (Dates[i] == SelectedDate)
                    {
                        SelectedDate = Dates[i + 1];
                        TBL_Date.Text = SelectedDate.ToString("D");
                        SetTimeList(1, SelectedDate);
                        return;
                    }
                }
            }
            else
            {
                SelectedDate = Dates.First();
                TBL_Date.Text = SelectedDate.ToString("D");
                SetTimeList(1, SelectedDate);
            }
        }

        private void Btn_PrevDateBig_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDateBig != Dates.First())
            {
                for (int i = 0; i < Dates.Count; i++)
                {
                    if (Dates[i] == SelectedDateBig)
                    {
                        SelectedDateBig = Dates[i - 1];
                        TBL_DateBig.Text = SelectedDateBig.ToString("D");
                        SetTimeList(2, SelectedDateBig);
                        return;
                    }
                }
            }
            else
            {
                SelectedDateBig = Dates.Last();
                TBL_DateBig.Text = SelectedDateBig.ToString("D");
                SetTimeList(2, SelectedDateBig);
            }
        }

        private void Btn_NextDateBig_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDateBig != Dates.Last())
            {
                for (int i = 0; i < Dates.Count; i++)
                {
                    if (Dates[i] == SelectedDateBig)
                    {
                        SelectedDateBig = Dates[i + 1];
                        TBL_DateBig.Text = SelectedDateBig.ToString("D");
                        SetTimeList(2, SelectedDateBig);
                        return;
                    }
                }
            }
            else
            {
                SelectedDateBig = Dates.First();
                TBL_DateBig.Text = SelectedDateBig.ToString("D");
                SetTimeList(2, SelectedDateBig);
            }
        }

        private void Btn_PrevDateVip_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDateVip != Dates.First())
            {
                for (int i = 0; i < Dates.Count; i++)
                {
                    if (Dates[i] == SelectedDateVip)
                    {
                        SelectedDateVip = Dates[i - 1];
                        TBL_DateVip.Text = SelectedDateVip.ToString("D");
                        SetTimeList(1, SelectedDateVip);
                        return;
                    }
                }
            }
            else
            {
                SelectedDateVip = Dates.Last();
                TBL_DateVip.Text = SelectedDateVip.ToString("D");
                SetTimeList(1, SelectedDateVip);
            }
        }

        private void Btn_NextDateVip_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDateVip != Dates.Last())
            {
                for (int i = 0; i < Dates.Count; i++)
                {
                    if (Dates[i] == SelectedDateVip)
                    {
                        SelectedDateVip = Dates[i + 1];
                        TBL_DateVip.Text = SelectedDateVip.ToString("D");
                        SetTimeList(3, SelectedDateVip);
                        return;
                    }
                }
            }
            else
            {
                SelectedDateVip = Dates.First();
                TBL_DateVip.Text = SelectedDateVip.ToString("D");
                SetTimeList(3, SelectedDateVip);
            }
        }

        private void Btn_AddFile_Click(object sender, RoutedEventArgs e)
        {
            string ApplicationName = "Booking";

            string DocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string ApplicationFolderPath = Path.Combine(DocumentPath, ApplicationName);

            if(!Directory.Exists(ApplicationFolderPath))
            {
                Directory.CreateDirectory(ApplicationFolderPath);
            }

            OpenFileDialog OpenFileDialogInstance = new OpenFileDialog();
            OpenFileDialogInstance.Filter = "Документы (*.doc; *.docx; *.pdf)|*.doc;*.docx; *.pdf | Презентации (*.pptx)| *.pptx | Изображения (*.png; *.jpeg; *.jpg)| *.png; *.jpeg; *.jpg | Аудио (*.mp3) | *.mp3 | Видео (*.mp4) | *.mp4";
            if (OpenFileDialogInstance.ShowDialog() == true)
            {
                string FilePath = OpenFileDialogInstance.FileName;
                string FileName = Path.GetFileName(FilePath);
                
                string TargetPath = Path.Combine(ApplicationFolderPath, FileName);

                File.Copy(FilePath, TargetPath, true);

                AdditionalFiles.Add(new AdditionalFile() { FileName = FileName, FilePath = TargetPath } );
            }

            DG_AdditionalFiles.ItemsSource = null;
            DG_AdditionalFiles.ItemsSource = AdditionalFiles;
        }

        private void Btn_DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы уверены что хотите удалить файл?", "Удаление файла", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AdditionalFile SelectedFile = DG_AdditionalFiles.SelectedItem as AdditionalFile;
                DG_AdditionalFiles.ItemsSource = null;
                AdditionalFiles.Remove(SelectedFile);
                DG_AdditionalFiles.ItemsSource = AdditionalFiles;

                FileInfo FileInfoInstance = new FileInfo(SelectedFile.FilePath);
                FileInfoInstance.Delete();
            }
        }

        private void Btn_DeleteFileBig_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить файл?", "Удаление файла", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AdditionalFile SelectedFile = DG_AdditionalFiles.SelectedItem as AdditionalFile;
                DG_AdditionalFilesBig.ItemsSource = null;
                AdditionalFilesBig.Remove(SelectedFile);
                DG_AdditionalFilesBig.ItemsSource = AdditionalFilesBig;

                FileInfo FileInfoInstance = new FileInfo(SelectedFile.FilePath);
                FileInfoInstance.Delete();
            }
        }

        private void Btn_AddFileBig_Click(object sender, RoutedEventArgs e)
        {
            string ApplicationName = "Booking";

            string DocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string ApplicationFolderPath = Path.Combine(DocumentPath, ApplicationName);

            if (!Directory.Exists(ApplicationFolderPath))
            {
                Directory.CreateDirectory(ApplicationFolderPath);
            }

            OpenFileDialog OpenFileDialogInstance = new OpenFileDialog();
            OpenFileDialogInstance.Filter = "Документы (*.doc; *.docx; *.pdf)|*.doc;*.docx; *.pdf | Презентации (*.pptx)| *.pptx | Изображения (*.png; *.jpeg; *.jpg)| *.png; *.jpeg; *.jpg | Аудио (*.mp3) | *.mp3 | Видео (*.mp4) | *.mp4";
            if (OpenFileDialogInstance.ShowDialog() == true)
            {
                string FilePath = OpenFileDialogInstance.FileName;
                string FileName = Path.GetFileName(FilePath);

                string TargetPath = Path.Combine(ApplicationFolderPath, FileName);

                File.Copy(FilePath, TargetPath, true);

                AdditionalFilesBig.Add(new AdditionalFile() { FileName = FileName, FilePath = TargetPath });
            }

            DG_AdditionalFilesBig.ItemsSource = null;
            DG_AdditionalFilesBig.ItemsSource = AdditionalFilesBig;
        }

        private void Btn_AddFileVip_Click(object sender, RoutedEventArgs e)
        {
            string ApplicationName = "Booking";

            string DocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string ApplicationFolderPath = Path.Combine(DocumentPath, ApplicationName);

            if (!Directory.Exists(ApplicationFolderPath))
            {
                Directory.CreateDirectory(ApplicationFolderPath);
            }

            OpenFileDialog OpenFileDialogInstance = new OpenFileDialog();
            OpenFileDialogInstance.Filter = "Документы (*.doc; *.docx; *.pdf)|*.doc;*.docx; *.pdf | Презентации (*.pptx)| *.pptx | Изображения (*.png; *.jpeg; *.jpg)| *.png; *.jpeg; *.jpg | Аудио (*.mp3) | *.mp3 | Видео (*.mp4) | *.mp4";
            if (OpenFileDialogInstance.ShowDialog() == true)
            {
                string FilePath = OpenFileDialogInstance.FileName;
                string FileName = Path.GetFileName(FilePath);

                string TargetPath = Path.Combine(ApplicationFolderPath, FileName);

                File.Copy(FilePath, TargetPath, true);

                AdditionalFilesVip.Add(new AdditionalFile() { FileName = FileName, FilePath = TargetPath });
            }

            DG_AdditionalFilesVip.ItemsSource = null;
            DG_AdditionalFilesVip.ItemsSource = AdditionalFilesVip;
        }

        private void Btn_DeleteFileVip_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить файл?", "Удаление файла", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AdditionalFile SelectedFile = DG_AdditionalFiles.SelectedItem as AdditionalFile;
                DG_AdditionalFilesVip.ItemsSource = null;
                AdditionalFilesVip.Remove(SelectedFile);
                DG_AdditionalFilesVip.ItemsSource = AdditionalFilesVip;

                FileInfo FileInfoInstance = new FileInfo(SelectedFile.FilePath);
                FileInfoInstance.Delete();
            }
        }

        //Обработка событий
        private void DG_Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventEndTime.Clear();
            CB_EndTime.ItemsSource = null;

            var SelectedItem = DG_Time.SelectedItem as TimeClass;
            TB_StartTime.Text = SelectedItem.Time;

            string TimeStartStr = TB_StartTime.Text;

            var TimeStartSplit = TimeStartStr.Split(':');

            TimeSpan TimeStart = new TimeSpan(Convert.ToInt32(TimeStartSplit[0]), Convert.ToInt32(TimeStartSplit[1]), 0);

            TimeSpan TimeEnd = TimeStart.Add(new TimeSpan(6, 0, 0));

            for (TimeSpan time = TimeStart; time <= TimeEnd; time = time.Add(new TimeSpan(0,30,0)))
            {
                EventEndTime.Add(time.ToString());
            }

            CB_EndTime.ItemsSource = EventEndTime;
        }

        private void DG_TimeBig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventEndTimeBig.Clear();
            CB_EndTimeBig.ItemsSource = null;

            var SelectedItem = DG_TimeBig.SelectedItem as TimeClass;
            TB_StartTimeBig.Text = SelectedItem.Time;

            string TimeStartStr = TB_StartTimeBig.Text;

            var TimeStartSplit = TimeStartStr.Split(':');

            TimeSpan TimeStart = new TimeSpan(Convert.ToInt32(TimeStartSplit[0]), Convert.ToInt32(TimeStartSplit[1]), 0);

            TimeSpan TimeEnd = TimeStart.Add(new TimeSpan(6, 0, 0));

            for (TimeSpan time = TimeStart; time <= TimeEnd; time = time.Add(new TimeSpan(0, 30, 0)))
            {
                EventEndTimeBig.Add(time.ToString());
            }

            CB_EndTimeBig.ItemsSource = EventEndTimeBig;
        }

        private void DG_TimeVip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventEndTimeVip.Clear();
            CB_EndTimeVip.ItemsSource = null;

            var SelectedItem = DG_TimeVip.SelectedItem as TimeClass;
            TB_StartTimeVip.Text = SelectedItem.Time;

            string TimeStartStr = TB_StartTimeVip.Text;

            var TimeStartSplit = TimeStartStr.Split(':');

            TimeSpan TimeStart = new TimeSpan(Convert.ToInt32(TimeStartSplit[0]), Convert.ToInt32(TimeStartSplit[1]), 0);

            TimeSpan TimeEnd = TimeStart.Add(new TimeSpan(6, 0, 0));

            for (TimeSpan time = TimeStart; time <= TimeEnd; time = time.Add(new TimeSpan(0, 30, 0)))
            {
                EventEndTimeVip.Add(time.ToString());
            }

            CB_EndTimeVip.ItemsSource = EventEndTimeVip;
        }
    }
}
