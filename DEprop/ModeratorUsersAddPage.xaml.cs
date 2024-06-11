using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DEprop
{
    public partial class ModeratorUsersAddPage : Page
    {
        public ModeratorUsersAddPage()
        {
            InitializeComponent();
            FillMenu();
        }

        public void FillMenu()
        {
            MainPlace.Children.Clear();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var user in db.Users)
                {

                    CreatePositionBlock(user);

                }
            }
        }

        public void CreatePositionBlock(Users user)
        {
            Border border = new Border();
            StackPanel stackPanel = new StackPanel();
            DockPanel dockPanel = new DockPanel();
            Image image = new Image();
            Label labelId = new Label();
            Label labelName = new Label();
            Label labelSurname = new Label();
            Button buttonBuy = new Button();
            Button buttonDelete = new Button();

            border.Margin = new Thickness(178, 25, 0, 10);
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = Brushes.DeepSkyBlue;
            border.Width = 200;
            border.Height = 320;

            stackPanel.Width = 180;
            stackPanel.Height = 320;


            image.Width = image.Height = 100;

            labelId.Visibility = Visibility.Collapsed;
            labelId.Content = user.UserId;
            labelId.Name = "Id";

            labelName.FontSize = 20;
            labelSurname.FontSize = 20;
            labelSurname.HorizontalAlignment = HorizontalAlignment.Right;

            buttonBuy.FontSize = 16;
            buttonBuy.Name = $"But{user.UserId}";
            buttonBuy.Content = "Изменить позицию";
            buttonBuy.Margin = new Thickness(0, 45, 0, 0);
            buttonBuy.Width = 190;
            buttonBuy.Height = 37;
            buttonBuy.HorizontalAlignment = HorizontalAlignment.Center;
            buttonBuy.Background = Brushes.DeepSkyBlue;

            buttonDelete.FontSize = 16;
            buttonDelete.Name = $"But{user.UserId}";
            buttonDelete.Content = "Удалить позицию";
            buttonDelete.Margin = new Thickness(0, 45, 0, 0);
            buttonDelete.Width = 190;
            buttonDelete.Height = 37;
            buttonDelete.HorizontalAlignment = HorizontalAlignment.Center;
            buttonDelete.Background = Brushes.DeepSkyBlue;


            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");
            string filePath = System.IO.Path.Combine(directory, user.UserId + ".png");
            bi.StreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            bi.StreamSource.Dispose();
            image.Source = bi;

            labelName.Content = user.UserLog;
            labelSurname.Content = user.UserPas;

            DockPanel.SetDock(labelName, Dock.Left);
            DockPanel.SetDock(labelSurname, Dock.Right);

            dockPanel.Children.Add(labelName);
            dockPanel.Children.Add(labelSurname);

            stackPanel.Children.Add(image);
            stackPanel.Children.Add(dockPanel);
            stackPanel.Children.Add(buttonBuy);
            stackPanel.Children.Add(buttonDelete);

            border.Child = stackPanel;

            MainPlace.Children.Add(border);

            buttonBuy.Click += new RoutedEventHandler(ChangePosition);
            buttonDelete.Click += new RoutedEventHandler(DeletePosition_Click);
        }

        public void ChangePosition(object sender, RoutedEventArgs e)
        {
            string idString = (sender as Button).Name.Remove(0, 3);
            int Id = Convert.ToInt32(idString);


            Users user = new Users();

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                user = db.Users.Where(p => p.UserId == Id).First();
            }

            UserAdd positionAdd = new UserAdd(user);
            positionAdd.Show();
            GC.Collect();
            Window parentWindow = Window.GetWindow(this);

            // Закрываем родительское окно, если оно не null
            parentWindow?.Close();

        }

        public void DeletePosition_Click(object sender, RoutedEventArgs e)
        {
            string idString = (sender as Button).Name.Remove(0, 3);
            int Id = Convert.ToInt32(idString);
            if (MessageBox.Show("Вы точно хотите удалить элемент из БД", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeletePostition(Id);
            }
            FillMenu();
        }

        public void DeletePostition(int Id)
        {
            Users position = new Users();

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                position = db.Users.Where(p => p.UserId == Id).First();
                db.Users.Remove(position);
                string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures");
                string filePath = System.IO.Path.Combine(directory, position.UserId + ".png");
                File.Delete(filePath);
                db.SaveChanges();
            }
        }
    }
}
