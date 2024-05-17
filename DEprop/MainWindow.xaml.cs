using System.Windows;
using static DEprop.UserHub;

namespace DEprop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                            MessageBox.Show("Введён неверный пароль");
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
                                MessageBox.Show("Введён неверный пароль");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Такого логина не существует");
                        }
                    }
                }
                        
            }
        }
    }
}
