using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DEprop
{

    public partial class TestsAdd : Window
    {
        Tests currentUser = new Tests();
        public TestsAdd(Tests user)
        {
            currentUser = user;
            InitializeComponent();
            var screens = Screen.AllScreens;

            // Выбираем второй монитор (индекс начинается с 0)
            var screen = screens[0];

            double screenWidth = screen.Bounds.Width;
            double screenHeight = screen.Bounds.Height;

            // Устанавливаем положение окна на выбранном мониторе
            this.Left = screen.Bounds.Left + (screenWidth - this.Width) / 2;
            this.Top = screen.Bounds.Top + (screenHeight - this.Height) / 2;
            for (int indexer = 0; indexer < 15; indexer++)
            {
                MaxScoreCB.Items.Add(indexer);
                MinScoreCB.Items.Add(indexer);
            }
            if (user != null)
            {
                MaxScoreCB.SelectedItem = user.TestMaxScore;
                MinScoreCB.SelectedItem = user.TestMinScore;
                ChapterIdCB.SelectedItem = user.ChapterId;
                SaveBtn.Content = "Изменить тест";
            }
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var answer in db.Chapters)
                {
                    ChapterIdCB.Items.Add(answer.ChapterId);
                }
            }
        }

        public void AddNew()
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Tests position = new Tests();
                position.TestMaxScore = Convert.ToInt32(MaxScoreCB.SelectedItem);
                position.TestMinScore = Convert.ToInt32(MinScoreCB.SelectedItem);
                position.ChapterId = Convert.ToInt32(ChapterIdCB.SelectedItem);
                db.Tests.Add(position);
                db.SaveChanges();
            }
        }

        public void Change()
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Tests position = db.Tests.Where(x => x.TestId == currentUser.TestId).FirstOrDefault();
                position.TestMaxScore = Convert.ToInt32(MaxScoreCB.SelectedItem);
                position.TestMinScore = Convert.ToInt32(MinScoreCB.SelectedItem);
                position.ChapterId = Convert.ToInt32(ChapterIdCB.SelectedItem);

                db.SaveChanges();

            }
        }

        private void Price_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.OemComma)
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MinScoreCB.SelectedItem == null || MaxScoreCB.SelectedItem == null || ChapterIdCB.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Заполните все поля");
            }
            else if (Convert.ToInt32(MinScoreCB.SelectedItem) > Convert.ToInt32(MaxScoreCB.SelectedItem))
            {
                System.Windows.MessageBox.Show("Минимальный счет не может быть больше максимального");
            }
            else
            {
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    if (currentUser == null)
                    {
                        AddNew();
                        System.Windows.MessageBox.Show("Позиция успешно добавлена");
                    }
                    else
                    {
                        Change();
                        System.Windows.MessageBox.Show("Позиция успешно изменена");
                    }
                }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModeratorWindow nod = new ModeratorWindow();
            nod.MainFrame.Navigate(new ModeratorTestsAddPage());
            nod.ToUsers.FontWeight = FontWeights.Light;
            nod.ToAnswers.FontWeight = FontWeights.Light;
            nod.ToChapters.FontWeight = FontWeights.Light;
            nod.ToTests.FontWeight = FontWeights.Bold;
            nod.Show();
        }
    }
}
