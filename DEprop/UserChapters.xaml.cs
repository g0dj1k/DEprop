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
    public partial class UserChapters : Page
    {
        Users user1;
        public UserChapters(Users user)
        {
            user1 = user;
            InitializeComponent();
            FillMenu();
        }

        public void FillMenu()
        {
            MainPlace.Children.Clear();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var user in db.Chapters)
                {

                    CreatePositionBlock(user);

                }
            }
        }

        public void CreatePositionBlock(Chapters user)
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
            border.Width = 500;
            border.Height = 150;

            stackPanel.Width = 450;
            stackPanel.Height = 150;

            buttonBuy.FontSize = 16;
            buttonBuy.Name = $"But{user.ChapterId}";
            buttonBuy.Content = "Перейти к главе";
            buttonBuy.Margin = new Thickness(0, 45, 0, 0);
            buttonBuy.Width = 190;
            buttonBuy.Height = 37;
            buttonBuy.HorizontalAlignment = HorizontalAlignment.Center;
            buttonBuy.Background = Brushes.DeepSkyBlue;

            image.Width = image.Height = 100;

            labelId.Visibility = Visibility.Collapsed;
            labelId.Content = user.ChapterId;
            labelId.Name = "Id";

            labelName.FontSize = 20;
            labelSurname.FontSize = 20;
            labelSurname.HorizontalAlignment = HorizontalAlignment.Right;

            labelName.Content = user.ChapterName;

            DockPanel.SetDock(labelName, Dock.Left);

            dockPanel.Children.Add(labelName);

            stackPanel.Children.Add(dockPanel);
            stackPanel.Children.Add(buttonBuy);

            border.Child = stackPanel;

            MainPlace.Children.Add(border);

            buttonBuy.Click += new RoutedEventHandler(ChangePosition);
        }

        public void ChangePosition(object sender, RoutedEventArgs e)
        {
            string idString = (sender as Button).Name.Remove(0, 3);
            int Id = Convert.ToInt32(idString);


            Chapters user = new Chapters();

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                user = db.Chapters.Where(p => p.ChapterId == Id).First();
            }

            GC.Collect();

            this.NavigationService.Navigate(new UserChaptersPage(user, user1));
        }
    }
}
