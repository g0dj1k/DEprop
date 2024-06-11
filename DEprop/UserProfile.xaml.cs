using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
using System.Xml.Linq;

namespace DEprop
{
    /// <summary>
    /// Логика взаимодействия для UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Page
    {
        public System.Drawing.Image userImageToConvert;
        public string fileName;
        private Users currentUserProfile;
        public UserProfile(Users user)
        {
            currentUserProfile = user;
            InitializeComponent();
            if (user != null)
            {
                fileName = @"C:\Users\sosis\source\repos\DEprop\Pictures\" + user.UserId + ".png";

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");
                string filePath = System.IO.Path.Combine(directory, user.UserId + ".png");

                bi.StreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.StreamSource.Dispose();
                PositionImage.Source = bi;
                UserLogTB.Text = user.UserLog;
                UserPasTB.Text = user.UserPas;
                UserNameTB.Text = user.UserName;
                UserSurnameTB.Text = user.UserSurname;
            }
        }

        private void ImgBtn_Click(object sender, RoutedEventArgs e)
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

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(UserLogTB.Text != "" &&  UserPasTB.Text != "")
            {
                using(DEPropDBEntities db = new DEPropDBEntities())
                {
                    Change(UserLogTB.Text, UserPasTB.Text);
                }
                MessageBox.Show("Вы изменили данные");
            }
            else
            {
                MessageBox.Show("Введите все данные");
            }
        }

        public void Change(string name, string price)
        {

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Users position = db.Users.Where(x => x.UserId == currentUserProfile.UserId).FirstOrDefault();
                position.UserLog = name;
                position.UserPas = price;
                position.UserName = UserNameTB.Text;
                position.UserSurname = UserSurnameTB.Text;

                if (userImageToConvert != null)
                {
                    string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");
                    string filePath = System.IO.Path.Combine(directory, position.UserId + ".png");
                    ImageConverter converter = new ImageConverter();
                    var ImageConvert = converter.ConvertTo(userImageToConvert, typeof(byte[]));
                    position.UserPicture = (byte[])ImageConvert;
                    userImageToConvert.Dispose();
                    File.Delete(filePath);
                    Bitmap bmp = new Bitmap(fileName);
                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                db.SaveChanges();


            }
        }
    }
}
