using System.Windows;
using System.Windows.Forms;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DEprop
{
    public partial class MainWindow : Window
    {
        public static readonly string appdata = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "userLoginDir");
        string answer = "";
        int counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            var screens = Screen.AllScreens;

            // Выбираем второй монитор (индекс начинается с 0)
            var screen = screens[0];

            double screenWidth = screen.Bounds.Width;
            double screenHeight = screen.Bounds.Height;

            // Устанавливаем положение окна на выбранном мониторе
            this.Left = screen.Bounds.Left + (screenWidth - this.Width) / 2;
            this.Top = screen.Bounds.Top + (screenHeight - this.Height) / 2;
            if (File.Exists(Path.Combine(appdata, "auth1.txt")))
            {
                    var data = File.ReadAllText(Path.Combine(appdata, "auth1.txt")).Split(' ');
                    string id = data[0];
                    var user = Authorize(id, data[1]);
                    var mod = AuthorizeMod(id, data[1]);
                    if (user != null)
                    {
                        OpenUserWindow(user);
                    }
                    else if (mod != null)
                    {
                        OpenModWindow(mod);
                    }
            }
            else
            {
                System.Windows.MessageBox.Show("Ты дурак");
            }
        }
        public static Users Authorize(string Login, string Password)
        {
            using (DEPropDBEntities activityEntities = new DEPropDBEntities())
            {
                Users user = activityEntities.Users.Where(x => x.UserLog == Login && x.UserPas == Password).FirstOrDefault();
                return user;

            }
        }

        public static Mods AuthorizeMod(string Login, string Password)
        {
            using (DEPropDBEntities activityEntities = new DEPropDBEntities())
            {
                Mods mod = activityEntities.Mods.Where(x => x.ModLog == Login && x.ModPas == Password).FirstOrDefault();
                return mod;

            }
        }


        public void OpenUserWindow(Users user)
        {
            bool userFound = false;
            if (!Directory.Exists(appdata))
            {
                Directory.CreateDirectory(appdata);
            }

            File.WriteAllText(Path.Combine(Path.Combine(appdata, "auth1.txt")), user.UserLog + " " + user.UserPas);

            CurrentUser.currentUserId = user.UserId;


            UserWindow hubScreen = new UserWindow(user);
            hubScreen.Show();
            this.Close();

        }

        public void OpenModWindow(Mods user)
        {
            bool userFound = false;
            if (!Directory.Exists(appdata))
            {
                Directory.CreateDirectory(appdata);
            }

            File.WriteAllText(Path.Combine(Path.Combine(appdata, "auth1.txt")), user.ModLog + " " + user.ModPas);

            CurrentUser.currentUserId = user.ModId;

            ModeratorWindow hubScreen = new ModeratorWindow();
            hubScreen.Show();
            this.Close();
        }

        async private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                string log = Login.Text;

                var result = Authorize(log, Password.Password);
                var resultMod = AuthorizeMod(log, Password.Password);
                if (result != null)
                {
                    OpenUserWindow(result);
                }
                else if (resultMod != null)
                {
                    OpenModWindow(resultMod);
                }
                else
                {
                    System.Windows.MessageBox.Show("Неправильный email или пароль");
                }
            }
            if (counter % 3 == 0)
            {
                this.IsEnabled = false;
                await Task.Delay(10000);
                this.IsEnabled = true;
            }
        }
    }
}
