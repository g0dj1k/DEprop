using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace DEprop
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public static readonly string appdata = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "userLoginDir");
        private Users userPR;
        public UserWindow(Users user)
        {
            userPR = user;
            InitializeComponent();
            ToProfile.FontWeight = FontWeights.Bold;
            ToEsts.FontWeight = FontWeights.Light;
            ToChapters.FontWeight = FontWeights.Light;
            var screens = Screen.AllScreens;

            // Выбираем второй монитор (индекс начинается с 0)
            var screen = screens[0];

            double screenWidth = screen.Bounds.Width;
            double screenHeight = screen.Bounds.Height;

            // Устанавливаем положение окна на выбранном мониторе
            this.Left = screen.Bounds.Left + (screenWidth - this.Width) / 2;
            this.Top = screen.Bounds.Top + (screenHeight - this.Height) / 2;
            MainFrame.Navigate(new UserProfile(user));
        }

        private void ToEsts_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ToProfile.FontWeight = FontWeights.Light;
            ToEsts.FontWeight = FontWeights.Bold;
            ToChapters.FontWeight = FontWeights.Light;
            MainFrame.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            MainFrame.Navigate(new UserEsts(userPR));
        }

        private void ToProfile_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ToProfile.FontWeight = FontWeights.Bold;
            ToEsts.FontWeight = FontWeights.Light;
            ToChapters.FontWeight = FontWeights.Light;
            MainFrame.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            MainFrame.Navigate(new UserProfile(userPR));
        }

        private void ToChapters_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ToProfile.FontWeight = FontWeights.Light;
            ToEsts.FontWeight = FontWeights.Light;
            ToChapters.FontWeight = FontWeights.Bold;
            MainFrame.Navigate(new UserChapters(userPR));
        }

        private void ExtBtn_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(System.IO.Path.Combine(System.IO.Path.Combine(appdata, "auth1.txt")), " ");
            MainWindow wid = new MainWindow();
            wid.Show();
            this.Close();
        }
    }
}
