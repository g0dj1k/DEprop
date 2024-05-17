using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DEprop
{
    /// <summary>
    /// Логика взаимодействия для ChapterPage.xaml
    /// </summary>
    public partial class ChapterPage : Window
    {
        Chapters currentUser = new Chapters();
        public ChapterPage(Chapters user)
        {
            InitializeComponent();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                Name1.Text = user.ChapterName;
                Price.Text = user.ChapterText;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            TestsHub adminMenu = new TestsHub();
            adminMenu.Show();
            this.Close();
        }

        private void ToEmployees_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UserHub adminMenu = new UserHub();
            adminMenu.Show();
            this.Close();
        }
    }
}
