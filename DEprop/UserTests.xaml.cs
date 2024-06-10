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
using static System.Net.Mime.MediaTypeNames;

namespace DEprop
{
    /// <summary>
    /// Логика взаимодействия для UserTests.xaml
    /// </summary>
    public partial class UserTests : Page
    {
        Tests test;
        Questions questions;
        Questions questic;
        Chapters chap;
        int spis = 0;
        int score = 0;
        int smt = 0;
        List<int> TagList = new List<int>();
        Users user1;
        public UserTests(Chapters chapter, Users user)
        {
            user1 = user;
            chap = chapter;
            InitializeComponent();
            using(DEPropDBEntities db = new DEPropDBEntities())
            {
                foreach(Tests test1 in db.Tests)
                {
                    if(test1.ChapterId == chapter.ChapterId)
                    {
                        test = test1; 
                        break;
                    }
                }
                foreach(Questions quest in db.Questions)
                {
                    if (quest.TestId == test.TestId)
                    {
                        TagList.Add(quest.QuestionId);
                    }
                }
                foreach (Questions quest in db.Questions)
                {
                    if (quest.QuestionId == TagList[0])
                    {
                        questions = quest;
                    }
                }
                Question.Text = questions.QuestionName;
                Answer1.Content = questions.AnswerOne;
                Answer2.Content = questions.AnswerTwo;
                Answer3.Content = questions.AnswerThree;
            }    
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            

            if(spis == TagList.Count)
            {
                MessageBox.Show($"Ваш результат " + score + " из " + TagList.Count);
                this.NavigationService.Navigate(new UserChaptersPage(chap, user1));
                using(DEPropDBEntities db = new DEPropDBEntities())
                {
                    TestEsts userPR = new TestEsts();
                    userPR.UserId = user1.UserId;
                    userPR.TestId = test.TestId;
                    userPR.TestScore = score;
                    db.TestEsts.Add(userPR);
                    db.SaveChanges();
                }
            }
            else
            {
                spis++;
                int choose = 1;
                if (rb1.IsChecked != false)
                {
                    choose = 1;
                }
                else if (rb2.IsChecked != false)
                {
                    choose = 2;
                }
                else if (rb3.IsChecked != false)
                {
                    choose = 3;
                }
                if (questions.CorrectAnswer == choose)
                {
                    score++;
                }
                if (rb1.IsChecked != false || rb2.IsChecked != false || rb3.IsChecked != false)
                {
                    if (spis < TagList.Count)
                    {
                        using (DEPropDBEntities db = new DEPropDBEntities())
                        {
                            foreach (Questions quest in db.Questions)
                            {
                                if (quest.QuestionId == TagList[spis])
                                {
                                    questions = quest;
                                    Answer1.Content = questions.AnswerOne;
                                    Answer2.Content = questions.AnswerTwo;
                                    Answer3.Content = questions.AnswerThree;
                                    Question.Text = questions.QuestionName;
                                    rb1.IsChecked = false;
                                    rb2.IsChecked = false;
                                    rb3.IsChecked = false;
                                    return;
                                }
                            }
                        }    
                    }
                    else if (spis == TagList.Count)
                    {
                        Next.Content = "Закончить";
                    }
                    
                }
                else
                {
                    MessageBox.Show("Выбирите ответ");
                }
                
            }
            
        }
    }
}
