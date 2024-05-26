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
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;

namespace Booking.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        readonly SolidColorBrush MyBrush = (SolidColorBrush)Application.Current.Resources["PlaceholderGrey"];
        private Admin ThisAdmin;
        private List<Admin> Admins;
        private List<User> Users;
        private List<TO> TOCollection;
        private DB_BookingEntities4 Entities = new DB_BookingEntities4();
        private List<UsersCollection> UsersCollectionInstance = new List<UsersCollection>();
        private bool IsUserCreating = false;
        private List<Request> Requests = new List<Request>();
        private List<Event> Events = new List<Event>();
        private List<ModifiedRequest> ModifiedRequests = new List<ModifiedRequest>();
        private List<ModifiedEvent> ModifiedEvents = new List<ModifiedEvent>();
        private DataGrid DG_AdditionalMaterials;

        public AdminPage(Admin AdminInstance)
        {
            InitializeComponent();
            ThisAdmin = AdminInstance;
            Admins = Entities.Admin.ToList();
            Users = Entities.User.ToList();
            TOCollection = Entities.TO.ToList();
            ConvertToUsersCollection();
            DG_Users.ItemsSource = UsersCollectionInstance;
            CreateRequestsList();
            DG_Requests.ItemsSource = ModifiedRequests;
            Events = Entities.Event.ToList();
            CreateEventsList();
            DG_Events.ItemsSource = ModifiedEvents;
        }

        //Методы
        private void EventBinding()
        {
            TB_FIO.GotFocus += FIOGotFocus;
            TB_FIO.LostFocus += FIOLostFocus;
            TB_Login.GotFocus += LoginGotFocus;
            TB_Login.LostFocus += LoginLostFocus;
            TB_Password.GotFocus += PasswordGotFocus;
            TB_Password.LostFocus += PasswordLostFocus;
            TB_Post.GotFocus += PostGotFocus;
            TB_Post.LostFocus += PostLostFocus;
        }

        private void ConvertToUsersCollection()
        {
            UsersCollectionInstance.Clear();
            foreach (var user in Users)
            {
                UsersCollectionInstance.Add(new UsersCollection(user.Id, user.FIO, "Пользователь", user.Login, user.Password, user.Post));
            }
            foreach (var admin in Admins)
            {
                if (admin.Id != ThisAdmin.Id) 
                    UsersCollectionInstance.Add(new UsersCollection(admin.Id, admin.FIO, "Администратор", admin.Login, admin.Password, ""));
            }
            foreach (var to in TOCollection)
            {
                UsersCollectionInstance.Add(new UsersCollection(to.Id, to.FIO, "ТО", to.Login, to.Password, ""));
            }
        }

        /// <summary>
        /// Выполняет валидацию введённых пользователем данных. Допустимые типы проверяемых строк: ФИО, Логин, Пароль, Должность
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Type"></param>
        /// <returns>
        /// -1 - валидация успешна
        /// 0 - введена пустая строка
        /// 1 - введенная строка превышает допустимую длину
        /// 2 - введенная строка меньше минимальной допустимой длины
        /// 3 - введенная строка содержит один из символов запрещенных типом введенной строки: 1234567890/\*\-+!@#$%^&()_=\{\}:;
        /// 4 - введен неверный тип проверяемых данных 
        /// </returns>
        private int Validate(string Text, string Type)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return 0;
            }

            if (Type == "ФИО")
            {
                if (Text.Length > 23)
                    return 1;
                if (Text.Length < 6)
                    return 2;
                if (Regex.IsMatch(Text, "[1234567890/\\*\\-+!@#$%^&()_=\\{\\}:;]"))
                    return 3;
                return -1;
            }

            if (Type == "Логин")
            {
                if (Text.Length > 20)
                    return 1;
                if (Text.Length < 8)
                    return 2;
                return -1;
            }

            if (Type == "Пароль")
            {
                if (Text.Length > 16)
                    return 1;
                if (Text.Length < 8)
                    return 2;
                return -1;
            }

            if (Type == "Должность")
            {
                if (Text.Length > 45)
                    return 1;
                if (Text.Length < 15)
                    return 2;
                if (Regex.IsMatch(Text, "[1234567890/\\*\\-+!@#$%^&()_=\\{\\}:;]"))
                    return 3;
                return -1;
            }

            return 4;
        }

        private void InputsClear()
        {
            TB_FIO.Clear();
            TB_Login.Clear();
            TB_Password.Clear();
            TB_Post.Clear();
            RB_Admin.IsChecked = false;
            RB_TO.IsChecked = false;
            RB_User.IsChecked = false;
        }

        private bool UserInputDataCheck()
        {
            if (Validate(TB_FIO.Text, "ФИО") == 0)
            {
                MessageBox.Show("Поле ФИО не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 1)
            {
                MessageBox.Show("ФИО не может превышать 23 символа", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 2)
            {
                MessageBox.Show("ФИО не может быть меньше 6 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 3)
            {
                MessageBox.Show("ФИО не может Содержать: 1234567890/\\*\\-+!@#$%^&()_=\\{\\}:;", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Validate(TB_Login.Text, "Логин") == 0)
            {
                MessageBox.Show("Поле Логин не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Login.Text, "Логин") == 1)
            {
                MessageBox.Show("Логин не может превышать 20 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Login.Text, "Логин") == 2)
            {
                MessageBox.Show("Логин не может быть меньше 8 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Validate(TB_Password.Text, "Пароль") == 0)
            {
                MessageBox.Show("Поле Пароль не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Password.Text, "Пароль") == 1)
            {
                MessageBox.Show("Пароль не может превышать 16 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Password.Text, "Пароль") == 2)
            {
                MessageBox.Show("Пароль не может быть меньше 8 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Validate(TB_Post.Text, "Должность") == 0)
            {
                MessageBox.Show("Поле Должность не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Post.Text, "Должность") == 1)
            {
                MessageBox.Show("Должность не может превышать 45 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Post.Text, "Должность") == 2)
            {
                MessageBox.Show("Должность не может быть меньше 15 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Post.Text, "Должность") == 3)
            {
                MessageBox.Show("Должность не может Содержать: 1234567890/\\*\\-+!@#$%^&()_=\\{\\}:;", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool AdminInputDataCheck()
        {
            if (Validate(TB_FIO.Text, "ФИО") == 0)
            {
                MessageBox.Show("Поле ФИО не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 1)
            {
                MessageBox.Show("ФИО не может превышать 23 символа", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 2)
            {
                MessageBox.Show("ФИО не может быть меньше 6 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 3)
            {
                MessageBox.Show("ФИО не может Содержать: 1234567890/\\*\\-+!@#$%^&()_=\\{\\}:;", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Validate(TB_Login.Text, "Логин") == 0)
            {
                MessageBox.Show("Поле Логин не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Login.Text, "Логин") == 1)
            {
                MessageBox.Show("Логин не может превышать 20 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Login.Text, "Логин") == 2)
            {
                MessageBox.Show("Логин не может быть меньше 8 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Validate(TB_Password.Text, "Пароль") == 0)
            {
                MessageBox.Show("Поле Пароль не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Password.Text, "Пароль") == 1)
            {
                MessageBox.Show("Пароль не может превышать 16 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Password.Text, "Пароль") == 2)
            {
                MessageBox.Show("Пароль не может быть меньше 8 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool TOInputDataCheck()
        {
            if (Validate(TB_FIO.Text, "ФИО") == 0)
            {
                MessageBox.Show("Поле ФИО не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 1)
            {
                MessageBox.Show("ФИО не может превышать 23 символа", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 2)
            {
                MessageBox.Show("ФИО не может быть меньше 6 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_FIO.Text, "ФИО") == 3)
            {
                MessageBox.Show("ФИО не может Содержать: 1234567890/\\*\\-+!@#$%^&()_=\\{\\}:;", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Validate(TB_Login.Text, "Логин") == 0)
            {
                MessageBox.Show("Поле Логин не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Login.Text, "Логин") == 1)
            {
                MessageBox.Show("Логин не может превышать 20 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Login.Text, "Логин") == 2)
            {
                MessageBox.Show("Логин не может быть меньше 8 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Validate(TB_Password.Text, "Пароль") == 0)
            {
                MessageBox.Show("Поле Пароль не может быть пустым!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Password.Text, "Пароль") == 1)
            {
                MessageBox.Show("Пароль не может превышать 16 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Validate(TB_Password.Text, "Пароль") == 2)
            {
                MessageBox.Show("Пароль не может быть меньше 8 символов", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void CreateRequestsList()
        {
            Requests.Clear();

            List<Request> TempRequests = Entities.Request.ToList();

            foreach (Request request in TempRequests) 
            {
                if (!request.Approved) 
                {
                    List<User> TempUsersList = Entities.User.ToList();
                    List<Hall> TempHallList = Entities.Hall.ToList();
                    string UserFIO = "";
                    string HallTitle = "";

                    foreach (User user in TempUsersList) 
                    {
                        if (request.User_Id == user.Id) 
                        {
                            UserFIO = user.FIO;
                        }
                    }

                    foreach (var hall in TempHallList)
                    {
                        if (request.Hall_Id == hall.Id)
                        {
                            HallTitle = hall.Name;
                        }
                    }

                    string FileNames = "";

                    var AdditionalMaterialsSplit = request.AndditionalMaterial.Split(';');

                    for (int i = 0; i < AdditionalMaterialsSplit.Length; i++)
                    {
                        string FilePath = AdditionalMaterialsSplit[i];
                        var FilePathSplit = AdditionalMaterialsSplit[i].Split('\\');
                        FileNames += FilePathSplit[FilePathSplit.Length - 1] + "\n";
                    }

                    ModifiedRequests.Add(new ModifiedRequest()
                    {
                        Id = request.Id,
                        Title = request.Title,
                        Date = request.Date.ToString("D"),
                        StartTime = request.StartTime.ToString(),
                        EndTime = request.EndTime.ToString(),
                        Equipment = request.Equipment,
                        AdditionalMaterials = FileNames,
                        Hall = HallTitle,
                        SendingUser = UserFIO
                    });
                }
            }
        }

        private void CreateEventsList()
        {
            ModifiedEvents.Clear();

            foreach (var MyEvent in Events)
            {
                string SendingUser = "";
                string HallTitle = "";

                List<User> TempUsersList = Entities.User.ToList();
                List<Hall> TempHallList = Entities.Hall.ToList();

                foreach (User user in TempUsersList)
                {
                    if (MyEvent.User_Id == user.Id)
                    {
                        SendingUser = user.FIO;
                    }
                }

                foreach (var hall in TempHallList)
                {
                    if (MyEvent.Hall_Id == hall.Id)
                    {
                        HallTitle = hall.Name;
                    }
                }

                List<AdditionalFile> AdditionalFilesCollection = new List<AdditionalFile>();

                var AdditionalMaterialsSplit = MyEvent.AdditionalMaterials.Split(';');

                for (int i = 0; i < AdditionalMaterialsSplit.Length; i++)
                {
                    string FilePath = AdditionalMaterialsSplit[i];
                    var FilePathSplit = AdditionalMaterialsSplit[i].Split('\\');
                    AdditionalFilesCollection.Add(new AdditionalFile() 
                    {
                        FilePath = FilePath,
                        FileName = FilePathSplit[FilePathSplit.Length - 1]
                    });
                }



                ModifiedEvents.Add(new ModifiedEvent() 
                {
                    Id = MyEvent.Id,
                    Title = MyEvent.Title,
                    Date = MyEvent.Date.ToString("D"),
                    StartTime = MyEvent.TimeStart.ToString(),
                    EndTime = MyEvent.TimeEnd.ToString(),
                    Equipment = MyEvent.Equipment,
                    AdditionalMaterials = AdditionalFilesCollection,
                    Hall = HallTitle,
                    SendingUser = SendingUser,
                });
            }
        }



        //Placeholder
        private void FIOGotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_FIO.Text == "Введите ФИО пользователя (Фамилия И.О.)")
            {
                TB_FIO.Text = string.Empty;
                TB_FIO.Foreground = Brushes.Black;
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
                TB_FIO.Text = "Введите ФИО пользователя (Фамилия И.О.)";
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
                TB_FIO.Text = "Введите ФИО пользователя (Фамилия И.О.)";
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

        //Обработка нажатий
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (IsUserCreating)
            {
                if (RB_User.IsChecked == true)
                {
                    if (UserInputDataCheck())
                    {
                        if (MessageBox.Show("Вы уверены, что хотите добавить нового пользователя?", "Добавить нового пользователя?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            User NewUser = new User(TB_FIO.Text, TB_Password.Text, TB_Login.Text, TB_Post.Text);
                            Entities.User.Add(NewUser);
                            Entities.SaveChanges();
                            InputsClear();
                            Admins = Entities.Admin.ToList();
                            Users = Entities.User.ToList();
                            TOCollection = Entities.TO.ToList();
                            ConvertToUsersCollection();
                            DG_Users.ItemsSource = null;
                            DG_Users.ItemsSource = UsersCollectionInstance;
                            MessageBox.Show("Пользователь успешно добавлен!", "Успех!", MessageBoxButton.OK);
                        }
                    }
                }
                else if (RB_Admin.IsChecked == true) 
                {
                    
                    if (AdminInputDataCheck())
                    {
                        if (MessageBox.Show("Вы уверены, что хотите добавить нового администратора?", "Добавить нового администратора?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            Admin NewAdmin = new Admin(TB_FIO.Text, TB_Login.Text, TB_Password.Text);
                            Entities.Admin.Add(NewAdmin);
                            Entities.SaveChanges();
                            InputsClear();
                            Admins = Entities.Admin.ToList();
                            Users = Entities.User.ToList();
                            TOCollection = Entities.TO.ToList();
                            ConvertToUsersCollection();
                            DG_Users.ItemsSource = null;
                            DG_Users.ItemsSource = UsersCollectionInstance;
                            MessageBox.Show("Пользователь успешно добавлен!", "Успех!", MessageBoxButton.OK);
                        }
                    }
                }
                else if (RB_TO.IsChecked == true)
                {                    
                    if (TOInputDataCheck())
                    {
                        if (MessageBox.Show("Вы уверены, что хотите добавить нового работника ТО?", "Добавить нового работника ТО?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            TO NewTO = new TO(TB_FIO.Text, TB_Login.Text, TB_Password.Text);
                            Entities.TO.Add(NewTO);
                            Entities.SaveChanges();
                            InputsClear();
                            Admins = Entities.Admin.ToList();
                            Users = Entities.User.ToList();
                            TOCollection = Entities.TO.ToList();
                            ConvertToUsersCollection();
                            DG_Users.ItemsSource = null;
                            DG_Users.ItemsSource = UsersCollectionInstance;
                            MessageBox.Show("Пользователь успешно добавлен!", "Успех!", MessageBoxButton.OK);
                        }
                    }
                }

                IsUserCreating = false;
            }
            else
            {
                InputsClear();
                MyLostFocus();
                EventBinding();
                IsUserCreating = true;
            }


        }

        private void Btn_Change_Click(object sender, RoutedEventArgs e)
        {
            UsersCollection SelectedUser = DG_Users.SelectedItem as UsersCollection;

            if (RB_Admin.IsChecked == true)
            {
                if (AdminInputDataCheck())
                {
                    if (MessageBox.Show("Вы уверены, что хотите изменить данные пользователя? Это действие нельзя будет отменить в дальнейшем!", "Изменение данных пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Entities.Admin.Find(SelectedUser.Id).FIO = TB_FIO.Text;
                            Entities.Admin.Find(SelectedUser.Id).Login = TB_Login.Text;
                            Entities.Admin.Find(SelectedUser.Id).Password = TB_Password.Text;
                            Entities.SaveChanges();
                            MessageBox.Show("Данные успешно изменены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            InputsClear();
                            ConvertToUsersCollection();
                            DG_Users.ItemsSource = null;
                            DG_Users.ItemsSource = UsersCollectionInstance;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("При изменении данных пользователя произошла ошибка! \n" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            throw;
                        }
                    }
                }
            }

            if (RB_User.IsChecked == true)
            {
                if (UserInputDataCheck())
                {
                    if (MessageBox.Show("Вы уверены, что хотите изменить данные пользователя? Это действие нельзя будет отменить в дальнейшем!", "Изменение данных пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Entities.User.Find(SelectedUser.Id).FIO = TB_FIO.Text;
                            Entities.User.Find(SelectedUser.Id).Login = TB_Login.Text;
                            Entities.User.Find(SelectedUser.Id).Password = TB_Password.Text;
                            Entities.User.Find(SelectedUser.Id).Post = TB_Post.Text;
                            Entities.SaveChanges();
                            MessageBox.Show("Данные успешно изменены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            InputsClear();
                            ConvertToUsersCollection();
                            DG_Users.ItemsSource = null;
                            DG_Users.ItemsSource = UsersCollectionInstance;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("При изменении данных пользователя произошла ошибка! \n" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            throw;
                        }
                    }
                }
            }

            if (RB_TO.IsChecked == true)
            {
                if (TOInputDataCheck())
                {
                    if (MessageBox.Show("Вы уверены, что хотите изменить данные пользователя? Это действие нельзя будет отменить в дальнейшем!", "Изменение данных пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Entities.TO.Find(SelectedUser.Id).FIO = TB_FIO.Text;
                            Entities.TO.Find(SelectedUser.Id).Login = TB_Login.Text;
                            Entities.TO.Find(SelectedUser.Id).Password = TB_Password.Text;
                            Entities.SaveChanges();
                            MessageBox.Show("Данные успешно изменены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            InputsClear();
                            ConvertToUsersCollection();
                            DG_Users.ItemsSource = null;
                            DG_Users.ItemsSource = UsersCollectionInstance;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("При изменении данных пользователя произошла ошибка! \n" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            throw;
                        }
                    }
                }
            }
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            UsersCollection SelectedObject = DG_Users.SelectedItem as UsersCollection;

            if (MessageBox.Show("Вы уверены, что хотите удалить пользователя? Это действие невозможно отменить!", "Удаление пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (SelectedObject.Role == "Пользователь")
                {
                    User SelectedUser = Entities.User.Find(SelectedObject.Id);

                    Entities.User.Remove(SelectedUser);
                    Entities.SaveChanges();

                    InputsClear();
                    ConvertToUsersCollection();
                    DG_Users.ItemsSource = null;
                    DG_Users.ItemsSource = UsersCollectionInstance;

                    MessageBox.Show("Пользователь успешно удалён!", "Успех!", MessageBoxButton.OK);
                }
                else if(SelectedObject.Role == "Администратор")
                {
                    Admin SelectedAdmin = Entities.Admin.Find(SelectedObject.Id);

                    Entities.Admin.Remove(SelectedAdmin);
                    Entities.SaveChanges();

                    InputsClear();
                    ConvertToUsersCollection();
                    DG_Users.ItemsSource = null;
                    DG_Users.ItemsSource = UsersCollectionInstance;

                    MessageBox.Show("Пользователь успешно удалён!", "Успех!", MessageBoxButton.OK);
                }
                else if (SelectedObject.Role == "ТО")
                {
                    TO SelectedTO = Entities.TO.Find(SelectedObject.Id);

                    Entities.TO.Remove(SelectedTO);
                    Entities.SaveChanges();

                    InputsClear();
                    ConvertToUsersCollection();
                    DG_Users.ItemsSource = null;
                    DG_Users.ItemsSource = UsersCollectionInstance;

                    MessageBox.Show("Пользователь успешно удалён!", "Успех!", MessageBoxButton.OK);
                }
            }
        }

        private void Btn_Approve_Click(object sender, RoutedEventArgs e)
        {
            ModifiedRequest SelectedModifiedRequest = DG_Requests.SelectedItem as ModifiedRequest;

            Request SelectedRequest = new Request();

            List<Request> TempRequests = Entities.Request.ToList();

            foreach (var request in TempRequests)
            {
                if (request.Id == SelectedModifiedRequest.Id)
                    SelectedRequest = request;
            }

            foreach (var request in Entities.Request)
            {
                if (request == SelectedRequest)
                {
                    request.Approved = true;
                }
            }

            Event NewEvent = new Event()
            {
                Title = SelectedRequest.Title,
                TimeStart = SelectedRequest.StartTime,
                TimeEnd = SelectedRequest.EndTime,
                AdditionalMaterials = SelectedRequest.AndditionalMaterial,
                Equipment = SelectedRequest.Equipment,
                Hall_Id = SelectedRequest.Hall_Id,
                User_Id = SelectedRequest.User_Id,
                Date = SelectedRequest.Date
            };

            Entities.Event.Add(NewEvent);

            Entities.SaveChanges();
            MessageBox.Show("Заявка одобрена!", "Успех!", MessageBoxButton.OK);

            CreateRequestsList();

            DG_Requests.ItemsSource = null;
            DG_Requests.ItemsSource = Requests;
        }

        private void Btn_Reject_Click(object sender, RoutedEventArgs e)
        {
            Request SelectedRequest = DG_Requests.SelectedItem as Request;

            bool Coincidence = false;

            foreach (var request in Entities.Request)
            {
                if (request == SelectedRequest)
                {
                    Coincidence = true;
                }
            }

            if(Coincidence)
            {
                Entities.Request.Remove(SelectedRequest);
                Entities.SaveChanges();

                MessageBox.Show("Заявка успешно отклонена!", "Успех!", MessageBoxButton.OK);

                DG_Requests.ItemsSource = null;
                DG_Requests.ItemsSource = Requests;
            }
        }

        private void Btn_Download_Click(object sender, RoutedEventArgs e)
        {
            AdditionalFile SelectedFile = DG_AdditionalMaterials.SelectedItem as AdditionalFile;

            string FilePath = SelectedFile.FilePath;

            SaveFileDialog SaveFileDialogInstance = new SaveFileDialog();
            SaveFileDialogInstance.FileName = System.IO.Path.GetFileName(FilePath);
            SaveFileDialogInstance.Filter = "Документы (*.doc; *.docx; *.pdf)|*.doc;*.docx; *.pdf | Презентации (*.pptx)| *.pptx | Изображения (*.png; *.jpeg; *.jpg)| *.png; *.jpeg; *.jpg | Аудио (*.mp3) | *.mp3 | Видео (*.mp4) | *.mp4";
            
            if (SaveFileDialogInstance.ShowDialog() == true)
            {
                string DirectoryPath = SaveFileDialogInstance.FileName;

                try
                {
                    File.Copy(FilePath, DirectoryPath, true);

                    MessageBox.Show("Файл успешно сохранен!", "Успех!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла непредвиденная ошибка: \n" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
            }
        }

        //Обработка событий
        private void DG_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsersCollection SelectedUser = DG_Users.SelectedItem as UsersCollection;

            if (SelectedUser != null)
            {
                if (SelectedUser.Role == "Пользователь")
                {
                    RB_User.IsChecked = true;
                    TB_FIO.Text = SelectedUser.FIO;
                    TB_Login.Text = SelectedUser.Login;
                    TB_Password.Text = SelectedUser.Password;
                    TB_Post.Text = SelectedUser.Post;
                }
                else if (SelectedUser.Role == "Администратор")
                {
                    RB_Admin.IsChecked = true;
                    TB_FIO.Text = SelectedUser.FIO;
                    TB_Login.Text = SelectedUser.Login;
                    TB_Password.Text = SelectedUser.Password;
                    TB_Post.Text = SelectedUser.Post;
                }
                else if (SelectedUser.Role == "ТО")
                {
                    RB_TO.IsChecked = true;
                    TB_FIO.Text = SelectedUser.FIO;
                    TB_Login.Text = SelectedUser.Login;
                    TB_Password.Text = SelectedUser.Password;
                    TB_Post.Text = SelectedUser.Post;
                }
            }
        }

        private void DG_AdditionalMaterials_Loaded(object sender, RoutedEventArgs e)
        {
            DG_AdditionalMaterials = sender as DataGrid;
        }
    }
}
