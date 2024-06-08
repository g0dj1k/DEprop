using System;
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
    /// Логика взаимодействия для ModeratorChapterAddPage.xaml
    /// </summary>
    public partial class ModeratorChapterAddPage : Page
    {
        public ModeratorChapterAddPage()
        {
            InitializeComponent();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                MainPlace.ItemsSource = db.Chapters.ToList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            ChaptersAdd answers = new ChaptersAdd((sender as Button).DataContext as Chapters);
            answers.Show();
            Window parentWindow = Window.GetWindow(this);

            // Закрываем родительское окно, если оно не null
            parentWindow?.Close();
        }

        private void BtnDlt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить элемент из БД", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeletePostition((sender as Button).DataContext as Chapters);
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    MainPlace.ItemsSource = db.Chapters.ToList();
                }
            }
        }
        public void DeletePostition(Chapters chapters)
        {
            int Id = chapters.ChapterId;

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                chapters = db.Chapters.Where(p => p.ChapterId == Id).First();
                db.Chapters.Remove(chapters);
                db.SaveChanges();
            }
        }
    }
}
