﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DEprop
{

    public partial class AnswersAdd : Window
    {
        Answers currentUser = new Answers();
        public AnswersAdd(Answers user)
        {
            currentUser = user;
            InitializeComponent();
            Price.Items.Add(0);
            Price.Items.Add(1);
            if (user != null)
            {
                Name1.Text = user.AnswerName;
                Price.SelectedItem = user.AnswerTF;
                SaveBtn.Content = "Изменить позицию";
            }
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var answer in db.Questions)
                {
                    QuestionIdd.Items.Add(answer.QuestionId);
                }
            }
        }

        public void AddNew(string name, string price)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Answers position = new Answers();
                position.AnswerTF = Convert.ToInt32(Price.SelectedItem);
                position.AnswerName = name;
                position.QuestionId = Convert.ToInt32(QuestionIdd.SelectedItem);
                db.Answers.Add(position);
                db.SaveChanges();
            }
        }

        public void Change(string name, string price)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Answers position = db.Answers.Where(x => x.AnswerId == currentUser.AnswerId).FirstOrDefault();
                position.AnswerTF = Convert.ToInt32(Price.SelectedItem);
                position.AnswerName = name;


                db.SaveChanges();


            }
        }

        private void Price_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.OemComma)
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name1.Text == "" || Price.Text == "" || QuestionIdd.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    if (currentUser == null)
                    {
                        AddNew(Name1.Text, Price.Text);
                        MessageBox.Show("Позиция успешно добавлена");
                    }
                    else
                    {
                        Change(Name1.Text, Price.Text);
                        MessageBox.Show("Позиция успешно изменена");
                    }
                }
            }
        }

        private void ToMenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ModMenu adminMenu = new ModMenu();
            adminMenu.Show();
            this.Close();
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
