using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DEprop
{

    public partial class UserHub : Window
    {
        public WrapPanel mainPLace;
        public UserHub()
        {
            InitializeComponent();
            FillMenu();
            mainPLace = MainPlace;
        }
        public static class CurrentUser
        {
            public static int Id { get; set; }
        }
        public static class CurrentChapter
        {
            public static int Id { get; set; }
        }

        private void Label_MouseUpGoToOrders(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void Label_MouseUpGoToProfile(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillMenu();
        }

        public void FillMenu()
        {
            MainPlace.Children.Clear();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (var pos in db.Chapters)
                {
                        CreatePositionBlock(pos);
                }
            }
        }

        public void CreatePositionBlock(Chapters pos)
        {
            Border border = new Border();
            StackPanel stackPanel = new StackPanel();
            DockPanel dockPanel = new DockPanel();
            Label labelId = new Label();
            Label labelName = new Label();
            Label labelPrice = new Label();
            Button buttonBuy = new Button();

            border.Margin = new Thickness(178, 25, 0, 10);
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = Brushes.DeepSkyBlue;
            border.Width = 385;
            border.Height = 170;

            stackPanel.Width = 375;
            stackPanel.Height = 150;


            labelId.Visibility = Visibility.Collapsed;
            labelId.Content = pos.ChapterId;
            labelId.Name = "Id";

            labelName.FontSize = 20;
            labelPrice.FontSize = 20;
            labelPrice.HorizontalAlignment = HorizontalAlignment.Right;

            buttonBuy.FontSize = 16;
            buttonBuy.Name = $"But{pos.ChapterId}";
            buttonBuy.Content = "Прочитать главу";
            buttonBuy.Margin = new Thickness(0, 45, 0, 0);
            buttonBuy.Width = 190;
            buttonBuy.Height = 37;
            buttonBuy.HorizontalAlignment = HorizontalAlignment.Center;
            buttonBuy.Background = Brushes.DeepSkyBlue;

            labelName.Content = pos.ChapterName;
            labelPrice.Content = pos.ChapterText;

            DockPanel.SetDock(labelName, Dock.Left);
            DockPanel.SetDock(labelPrice, Dock.Right);

            dockPanel.Children.Add(labelName);
            dockPanel.Children.Add(labelPrice);

            stackPanel.Children.Add(dockPanel);
            stackPanel.Children.Add(buttonBuy);

            border.Child = stackPanel;

            MainPlace.Children.Add(border);

            buttonBuy.Click += new RoutedEventHandler(AddToBasket);

            int idCh = pos.ChapterId;
        }

        public void AddToBasket(object sender, RoutedEventArgs e)
        {
            string idString = (sender as Button).Name.Remove(0, 3);
            int Id = Convert.ToInt32(idString);


            Chapters user = new Chapters();

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                user = db.Chapters.Where(p => p.ChapterId == Id).First();
            }

            
        }

    }
}
