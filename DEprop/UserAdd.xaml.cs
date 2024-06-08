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

    public partial class UserAdd : Window
    {
        Users currentUser = new Users();
        public System.Drawing.Image userImageToConvert;
        public string fileName;
        public UserAdd(Users user = null)
        {
            currentUser = user;
            InitializeComponent();
            if (user != null)
            {
                fileName = @"C:\Users\sosis\source\repos\DEprop\Pictures\" + user.UserId + ".png";

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new FileStream(@"C:\Users\sosis\source\repos\DEprop\Pictures\" + user.UserId + ".png", FileMode.Open, FileAccess.Read);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.StreamSource.Dispose();
                PositionImage.Source = bi;
                Name1.Text = user.UserName;
                Price.Text = user.UserSurname;
                SaveBtn.Content = "Изменить позицию";
            }
        }


        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog pictureDialog = new OpenFileDialog();
            pictureDialog.Title = "Выбрать изображение";
            pictureDialog.Filter = "Image Files(*.JPG;*.PNG;*.GIF)|*.JPG;*PNG;*.GIF";

            if (pictureDialog.ShowDialog() == true)
            {
                fileName = pictureDialog.FileName;
                BitmapImage positionPicture = new BitmapImage();
                positionPicture.BeginInit();
                positionPicture.StreamSource = new FileStream(pictureDialog.FileName, FileMode.Open, FileAccess.Read);
                positionPicture.CacheOption = BitmapCacheOption.OnLoad;
                positionPicture.EndInit();
                positionPicture.StreamSource.Dispose();

                userImageToConvert = System.Drawing.Image.FromFile(pictureDialog.FileName);
                PositionImage.Source = positionPicture;
            }
        }

        public void AddNew(string name, string price)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                ImageConverter converter = new ImageConverter();
                var ImageConvert = converter.ConvertTo(userImageToConvert, typeof(byte[]));
                //int idn = 0;
                //foreach (var user in db.Users)
                //{
                //    idn++;
                //}
                //int maxValue = 0;
                //foreach (var user in db.Users)
                //{
                //    for (int i = 1; i < idn; i++)
                //    {
                //        if (user.UserId > maxValue)
                //        {
                //            maxValue = user.UserId;
                //        }
                //    }
                //}
                //int idnn = maxValue+1;
                Users position = new Users();
                //position.UserId = idnn;
                position.UserLog = name;
                position.UserPas = price;
                position.UserPicture = (byte[])ImageConvert;
                //MessageBox.Show(Convert.ToString(idnn));
                db.Users.Add(position);
                db.SaveChanges();

                Bitmap bmp = new Bitmap(fileName);
                bmp.Save(@"C:\Users\sosis\source\repos\DEprop\Pictures\" + position.UserId + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public void Change(string name, string price)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Users position = db.Users.Where(x => x.UserId == currentUser.UserId).FirstOrDefault();
                position.UserName = name;
                position.UserSurname = price;

                if (userImageToConvert != null)
                {
                    ImageConverter converter = new ImageConverter();
                    var ImageConvert = converter.ConvertTo(userImageToConvert, typeof(byte[]));
                    position.UserPicture = (byte[])ImageConvert;
                    userImageToConvert.Dispose();
                    File.Delete(@"C:\Users\sosis\source\repos\DEprop\Pictures\" + position.UserId + ".png");
                    Bitmap bmp = new Bitmap(fileName);
                    bmp.Save(@"C:\Users\sosis\source\repos\DEprop\Pictures\" + position.UserId + ".png", System.Drawing.Imaging.ImageFormat.Png);
                }

                db.SaveChanges();


            }
        }

        //private void Price_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.OemComma)
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name1.Text == "" || Price.Text == "" || PositionImage.Source == null)
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
                        //bmp.Save(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        MessageBox.Show("Позиция успешно добавлена");
                    }
                    else
                    {
                        //ImageConverter converter = new ImageConverter();
                        //object ImageConvert;

                        //Position position = db.Position.Where(x => x.PositionID == currentPos.PositionID).FirstOrDefault();
                        //position.Name = Name.Text;
                        //position.Price = Convert.ToDecimal(Price.Text);
                        if (userImageToConvert != null)
                        {
                            //ImageConvert = converter.ConvertTo(positionImageToConvert, typeof(byte[]));
                            //position.Image = (byte[])ImageConvert;
                            //positionImageToConvert.Dispose();
                            //File.Delete(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png");
                            //Bitmap bmp = new Bitmap(fileName);
                            //bmp.Save(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        }

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
