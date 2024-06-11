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

    public partial class AnswersAdd : Window
    {
        Questions currentUser = new Questions();
        public AnswersAdd(Questions user)
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
                Name1.Text = user.QuestionName;
                Price.SelectedItem = user.TestId;
                Answer1.Text = user.AnswerOne;
                Answer2.Text = user.AnswerTwo;
                Answer3.Text = user.AnswerThree;
                QuestionIdd.SelectedItem = user.CorrectAnswer;
                SaveBtn.Content = "Изменить вопрос";
            }
            QuestionIdd.Items.Add(1);
            QuestionIdd.Items.Add(2);
            QuestionIdd.Items.Add(3);
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var answer in db.Tests)
                {
                    Price.Items.Add(answer.TestId);
                }
            }
        }

        public void AddNew(string name, string price)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Questions position = new Questions();
                position.TestId = Convert.ToInt32(Price.SelectedItem);
                position.QuestionName = name;
                position.QuestionId = Convert.ToInt32(QuestionIdd.SelectedItem);
                position.AnswerOne = Answer1.Text;
                position.AnswerTwo = Answer2.Text;
                position.AnswerThree = Answer3.Text;
                db.Questions.Add(position);
                db.SaveChanges();
            }
        }

        public void Change(string name, string price)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Questions position = db.Questions.Where(x => x.QuestionId == currentUser.QuestionId).FirstOrDefault();
                position.TestId = Convert.ToInt32(Price.SelectedItem);
                position.QuestionName = name;
                position.CorrectAnswer = Convert.ToInt32(QuestionIdd.SelectedItem);
                position.AnswerOne = Answer1.Text;
                position.AnswerTwo = Answer2.Text;
                position.AnswerThree = Answer3.Text;
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
            if (Name1.Text == "" || Price.SelectedItem == null || QuestionIdd.SelectedItem == null || Answer1.Text == "" || Answer2.Text == "" || Answer3.Text == "")
            {
                System.Windows.MessageBox.Show("Заполните все поля");
            }
            else
            {
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    if (currentUser == null)
                    {
                        AddNew(Name1.Text, Price.Text);
                        System.Windows.MessageBox.Show("Вопрос успешно добавлен");
                    }
                    else
                    {
                        Change(Name1.Text, Price.Text);
                        System.Windows.MessageBox.Show("Вопрос успешно изменен");
                    }
                }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModeratorWindow nod = new ModeratorWindow();
            nod.MainFrame.Navigate(new ModeratorAnswersAddPage());
            nod.ToUsers.FontWeight = FontWeights.Light;
            nod.ToAnswers.FontWeight = FontWeights.Bold;
            nod.ToChapters.FontWeight = FontWeights.Light;
            nod.ToTests.FontWeight = FontWeights.Light;
            nod.Show();
        }
    }
}
