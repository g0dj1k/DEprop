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
using System.Windows.Shapes;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DEprop
{
    /// <summary>
    /// Логика взаимодействия для ModeratorWindow.xaml
    /// </summary>
    public partial class ModeratorWindow : System.Windows.Window
    {
        public ModeratorWindow()
        {
            InitializeComponent();
            ToUsers.FontWeight = FontWeights.Bold;
            ToAnswers.FontWeight = FontWeights.Light;
            ToChapters.FontWeight = FontWeights.Light;
            ToTests.FontWeight = FontWeights.Light;
            var screens = Screen.AllScreens;

            // Выбираем второй монитор (индекс начинается с 0)
            var screen = screens[0];

            double screenWidth = screen.Bounds.Width;
            double screenHeight = screen.Bounds.Height;

            // Устанавливаем положение окна на выбранном мониторе
            this.Left = screen.Bounds.Left + (screenWidth - this.Width) / 2;
            this.Top = screen.Bounds.Top + (screenHeight - this.Height) / 2;
            MainFrame.Navigate(new ModeratorUsersAddPage());
        }
        private void PositionAdd_Click(object sender, RoutedEventArgs e)
        {
            var currentPage = MainFrame.Content as System.Windows.Controls.Page;
            if (currentPage != null)
            {
                if(currentPage.Title == "ModeratorUsersAddPage")
                {
                    UserAdd positionAdd = new UserAdd();
                    positionAdd.Show();
                    GC.Collect();
                    this.Close();
                }
                else if (currentPage.Title == "ModeratorAnswersAddPage")
                {
                    AnswersAdd positionAdd = new AnswersAdd(null);
                    positionAdd.Show();
                    GC.Collect();
                    this.Close();
                }
            } 
        }

        private void ToAnswers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var currentPage = MainFrame.Content as System.Windows.Controls.Page;
            if (currentPage != null)
            {
                if (currentPage.Title != "ModeratorAnswersAddPage")
                {
                    ToUsers.FontWeight = FontWeights.Light;
                    ToAnswers.FontWeight = FontWeights.Bold;
                    ToChapters.FontWeight = FontWeights.Light;
                    ToTests.FontWeight = FontWeights.Light;
                    MainFrame.Navigate(new ModeratorAnswersAddPage());
                }
                else
                {
                    System.Windows.MessageBox.Show("Вы уже на этой странице");
                }
            }
        }

        private void ToChapters_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToUsers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var currentPage = MainFrame.Content as System.Windows.Controls.Page;
            if (currentPage != null)
            {
                if (currentPage.Title != "ModeratorUsersAddPage")
                {
                    ToUsers.FontWeight = FontWeights.Bold;
                    ToAnswers.FontWeight = FontWeights.Light;
                    ToChapters.FontWeight = FontWeights.Light;
                    ToTests.FontWeight = FontWeights.Light;
                    MainFrame.Navigate(new ModeratorUsersAddPage());
                }
                else
                {
                    System.Windows.MessageBox.Show("Вы уже на этой странице");
                }
            }
        }

        private void ToTests_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
