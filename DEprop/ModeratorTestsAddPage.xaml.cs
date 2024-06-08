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
    /// Логика взаимодействия для ModeratorTestsAddPage.xaml
    /// </summary>
    public partial class ModeratorTestsAddPage : Page
    {
        public ModeratorTestsAddPage()
        {
            InitializeComponent();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                MainPlace.ItemsSource = db.Tests.ToList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            TestsAdd answers = new TestsAdd((sender as Button).DataContext as Tests);
            answers.Show();
            Window parentWindow = Window.GetWindow(this);

            // Закрываем родительское окно, если оно не null
            parentWindow?.Close();
        }

        private void BtnDlt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить элемент из БД", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeletePostition((sender as Button).DataContext as Tests);
                using (DEPropDBEntities db = new DEPropDBEntities())
                {
                    MainPlace.ItemsSource = db.Tests.ToList();
                }
            }
        }
        public void DeletePostition(Tests test)
        {
            int Id = test.TestId;

            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                test = db.Tests.Where(p => p.TestId == Id).First();
                db.Tests.Remove(test);
                db.SaveChanges();
            }
        }
    }
}
