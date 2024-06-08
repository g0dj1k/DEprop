﻿using System;
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

namespace DEprop
{
    /// <summary>
    /// Логика взаимодействия для ModeratorAnswersAddPage.xaml
    /// </summary>
    public partial class ModeratorAnswersAddPage : Page
    {
        public ModeratorAnswersAddPage()
        {
            InitializeComponent();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                MainPlace.ItemsSource = db.Answers.ToList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AnswersAdd answers = new AnswersAdd((sender as Button).DataContext as Answers);
            answers.Show();
            Window parentWindow = Window.GetWindow(this);

            // Закрываем родительское окно, если оно не null
            parentWindow?.Close();

        }

        private void BtnDlt_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите удалить элемент из БД", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeletePostition((sender as Button).DataContext as Answers);
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    MainPlace.ItemsSource = db.Answers.ToList();
                }
            }
        }
        public void DeletePostition(Answers answers)
        {
            int Id = answers.AnswerId;

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                answers = db.Answers.Where(p => p.AnswerId == Id).First();
                db.Answers.Remove(answers);
                db.SaveChanges();
            }
        }
    }
}
