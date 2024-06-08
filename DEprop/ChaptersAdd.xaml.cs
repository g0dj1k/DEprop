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

    public partial class ChaptersAdd : Window
    {
        Chapters currentUser = new Chapters();
        public ChaptersAdd(Chapters user)
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
            if (user != null)
            {
                ChapterNameTB.Text = user.ChapterName;
                ChapterTextTB.Text = user.ChapterText;
                ChapterPathTB.Text = user.ChapterPath;
                SaveBtn.Content = "Изменить главу";
            }
        }

        public void AddNew()
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Chapters position = new Chapters();
                position.ChapterName = ChapterNameTB.Text;
                position.ChapterText = ChapterTextTB.Text;
                position.ChapterPath = ChapterPathTB.Text;
                db.Chapters.Add(position);
                db.SaveChanges();
            }
        }

        public void Change()
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Chapters position = db.Chapters.Where(x => x.ChapterId == currentUser.ChapterId).FirstOrDefault();
                position.ChapterName = ChapterNameTB.Text;
                position.ChapterText = ChapterTextTB.Text;
                position.ChapterPath = ChapterPathTB.Text;
                db.SaveChanges();


            }
        }

        private void Price_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.OemComma)
            //{
            //    e.Handled = true;
            //}
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChapterNameTB.Text == "" || ChapterTextTB.Text == "" || ChapterPathTB.Text == "")
            {
                System.Windows.MessageBox.Show("Заполните все поля");
            }
            else
            {
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    if (currentUser == null)
                    {
                        AddNew();
                        System.Windows.MessageBox.Show("Глава успешно добавлена");
                    }
                    else
                    {
                        Change();
                        System.Windows.MessageBox.Show("Глава успешно изменена");
                    }
                }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModeratorWindow nod = new ModeratorWindow();
            nod.MainFrame.Navigate(new ModeratorChapterAddPage());
            nod.ToUsers.FontWeight = FontWeights.Light;
            nod.ToAnswers.FontWeight = FontWeights.Light;
            nod.ToChapters.FontWeight = FontWeights.Bold;
            nod.ToTests.FontWeight = FontWeights.Light;
            nod.Show();
        }
    }
}
