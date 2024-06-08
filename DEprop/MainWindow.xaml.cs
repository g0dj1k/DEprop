using System.Windows;
using static DEprop.UserHub;
using System.Windows.Forms;

namespace DEprop
{
    public partial class MainWindow : Window
    {
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
        }
        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            bool userFound = false;
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var entity in db.Users)
                {
                    if (Login.Text == entity.UserLog)
                    {
                        userFound = true;
                        if (Password.Password == entity.UserPas)
                        {
                            CurrentUser.Id = entity.UserId;
                            UserHub hubScreen = new UserHub();
                            hubScreen.Show();
                            this.Close();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Введён неверный пароль");
                        }
                    }
                }
            }
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var entity in db.Mods)
                {
                    if (!userFound)
                    {
                        if (Login.Text == entity.ModLog)
                        {
                            if (Password.Password == entity.ModPas)
                            {
                                ModMenu menuScreen = new ModMenu();
                                menuScreen.Show();
                                this.Close();
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Введён неверный пароль");
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Такого логина не существует");
                        }
                    }
                }
                        
            }
        }
    }
}
