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
    /// Логика взаимодействия для UserEsts.xaml
    /// </summary>
    public partial class UserEsts : Page
    {
        class Estss
        {
            public string QueName { get; set; }
            public int TestEstss { get; set; }
        }
        public UserEsts(Users user)
        {
            List<Estss> estList = new List<Estss>
            {

            };
            InitializeComponent();
            using (DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach (TestEsts est in db.TestEsts)
                {
                    int es = 0;
                    string qun = "";
                    if (user.UserId == est.UserId)
                    {
                        es = est.TestScore;
                        foreach (Tests quest in db.Tests)
                        {
                            if (est.TestId == quest.TestId)
                            {
                                foreach (Chapters quest1 in db.Chapters)
                                {
                                    if (quest.ChapterId == quest1.ChapterId)
                                    {
                                        qun = quest1.ChapterName;
                                    }
                                }
                            }
                        }
                        estList.Add(new Estss { QueName = qun, TestEstss = es });
                    }
                }
            }
            MainPlace.ItemsSource = estList;
        }
    }
}
