using Microsoft.Win32;
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
        public AnswersAdd(Answers user = null)
        {
            currentUser = user;
            InitializeComponent();
            if (user != null)
            {
                Name1.Text = user.AnswerName;
                Price.Text = Convert.ToString(user.AnswerTF);
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
                int maxValue = 0;
                foreach (var user in db.Answers)
                {
                        if (user.AnswerId > maxValue)
                        {
                            maxValue = user.AnswerId;
                        }
                }
                int idnn = maxValue + 1;
                Answers position = new Answers();
                position.AnswerId = idnn;
                position.AnswerTF = Convert.ToInt32(name);
                position.AnswerName = price;
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
                position.AnswerTF = Convert.ToInt32(name);
                position.AnswerName = price;


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
                        //Position position = new Position();
                        //position.Name = Name.Text;
                        //position.Price = Convert.ToDecimal(Price.Text);

                        AddNew(Name1.Text, Price.Text);
                        //position.Image = (byte[])ImageConvert;
                        //if ((bool)IsHidden.IsChecked)
                        //{
                        //    position.IsHidden = true;
                        //}
                        //else
                        //{
                        //    position.IsHidden = false;
                        //}
                        //db.Position.Add(position);
                        //db.SaveChanges();
                        //Bitmap bmp = new Bitmap(fileName);
                        //bmp.Save(@"C:\Answers\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        MessageBox.Show("Позиция успешно добавлена");
                    }
                    else
                    {
                        //ImageConverter converter = new ImageConverter();
                        //object ImageConvert;

                        //Position position = db.Position.Where(x => x.PositionID == currentPos.PositionID).FirstOrDefault();
                        //position.Name = Name.Text;
                        //position.Price = Convert.ToDecimal(Price.Text);

                        //if ((bool)IsHidden.IsChecked)
                        //{
                        //    position.IsHidden = true;
                        //}
                        //else
                        //{
                        //    position.IsHidden = false;
                        //}
                        //db.SaveChanges();

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
    }
}
