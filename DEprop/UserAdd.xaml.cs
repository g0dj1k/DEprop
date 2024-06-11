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

    public partial class UserAdd : Window
    {
        Users currentUser = new Users();
        public System.Drawing.Image userImageToConvert;
        public string fileName;
        public UserAdd(Users user = null)
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

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");
                string filePath = System.IO.Path.Combine(directory, user.UserId + ".png");
                bi.StreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.StreamSource.Dispose();
                PositionImage.Source = bi;
                Name1.Text = user.UserLog;
                Price.Text = user.UserPas;
                SaveBtn.Content = "Изменить позицию";
            }
        }


        private void SelectImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog pictureDialog = new Microsoft.Win32.OpenFileDialog();
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
                string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");
                string filePath = System.IO.Path.Combine(directory, position.UserId + ".png");
                bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public void Change(string name, string price)
        {
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Users position = db.Users.Where(x => x.UserId == currentUser.UserId).FirstOrDefault();
                position.UserLog = name;
                position.UserPas = price;

                if (userImageToConvert != null)
                {
                    ImageConverter converter = new ImageConverter();
                    var ImageConvert = converter.ConvertTo(userImageToConvert, typeof(byte[]));
                    position.UserPicture = (byte[])ImageConvert;
                    userImageToConvert.Dispose();
                    string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");
                    string filePath = System.IO.Path.Combine(directory, position.UserId + ".png");
                    File.Delete(filePath);
                    Bitmap bmp = new Bitmap(fileName);
                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
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
                System.Windows.MessageBox.Show("Заполните все поля");
            }
            else
            {
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    if (currentUser == null)
                    {
                        AddNew(Name1.Text, Price.Text);
                        System.Windows.MessageBox.Show("Пользователь успешно добавлен");
                    }
                    else
                    {
                        Change(Name1.Text, Price.Text);
                        System.Windows.MessageBox.Show("Пользователь успешно изменен");
                    }
                }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModeratorWindow nod = new ModeratorWindow();
            nod.MainFrame.Navigate(new ModeratorUsersAddPage());
            nod.Show();
        }
    }
}
