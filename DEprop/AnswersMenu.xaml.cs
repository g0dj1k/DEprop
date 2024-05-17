using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DEprop
{
    public partial class AnswersMenu : Window
    {
        private DEPropDBEntities dbContext;
        private int orderId;
        private readonly DEPropDBEntities _userRepository;
        public ObservableCollection<Answers> Answer { get; set; }


        public AnswersMenu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillMenu();
        }

        public void FillMenu()
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                MainPlace.ItemsSource = db.Answers.ToList();
            }
        }

        

        public void ChangePosition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Не работает");
            //string idString = (sender as Button).Name.Remove(0, 3);
            //int Id = Convert.ToInt32(idString);


            //Answers user = new Answers();

            //using (DEPropDBEntities db = new DEPropDBEntities())
            //{
            //    user = db.Answers.Where(p => p.AnswerId == Id).First();
            //}

            //AnswersAdd positionAdd = new AnswersAdd(user);
            //positionAdd.Show();
            //GC.Collect();

            //this.Close();
        }

        public void DeletePosition_Click(object sender, RoutedEventArgs e)
        {
            //var user = MainPlace.SelectedCells[0];
            //if (MainPlace.SelectedItems.Count == 0) return;
            //int clientID = (int)((DataRowView)MainPlace.SelectedItems[0]).Row["AnswerId"];
            MessageBox.Show("Не работает");
            //string idString = (sender as Button).Name.Remove(0, 3);
            //int Id = Convert.ToInt32(idString);

            //DeletePostition(Id);

            //FillMenu();
        }

        public void DeletePostition(int Id)
        {
            Answers position = new Answers();

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                position = db.Answers.Where(p => p.AnswerId == Id).First();
                db.Answers.Remove(position);
                db.SaveChanges();
            }
        }

        private void ToTests_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TestsHub employees = new TestsHub();
            employees.Show();
            GC.Collect();
            this.Close();
        }

        private void PositionAdd_Click(object sender, RoutedEventArgs e)
        {
            AnswersAdd positionAdd = new AnswersAdd();
            positionAdd.Show();
            GC.Collect();
            this.Close();
        }

        private void ToAnswers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ModMenu employees = new ModMenu();
            employees.Show();
            GC.Collect();
            this.Close();
        }

        private void ToChapters_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChaptersMenu employees = new ChaptersMenu();
            employees.Show();
            GC.Collect();
            this.Close();
        }

    }
}
