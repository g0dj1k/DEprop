using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace DEprop
{
    /// <summary>
    /// Логика взаимодействия для UserChaptersPage.xaml
    /// </summary>
    public partial class UserChaptersPage : Page
    {
        private Chapters chap;
        DispatcherTimer timer;
        Users user1;
        public UserChaptersPage(Chapters chapter, Users user)
        {
            user1 = user;
            chap = chapter;
            InitializeComponent();
            string UriString = chapter.ChapterPath;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
            videoElement.Source = new Uri(UriString, UriKind.RelativeOrAbsolute);
            videoElement.Volume = (double)Slider_Volume.Value;
            videoElement.Play();
            LabelName.Content = chapter.ChapterName;
            TBName.Text = chapter.ChapterText;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Slider_Sick.Value = videoElement.Position.TotalSeconds;
        }

        private void PLayBtn_Click(object sender, RoutedEventArgs e)
        {
            videoElement.Play();
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            videoElement.Pause();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            videoElement.Stop();
        }

        private void Slider_Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoElement.Volume = (double)Slider_Volume.Value;
        }

        private void Slider_Sick_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoElement.Position = TimeSpan.FromSeconds(Slider_Sick.Value);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            //string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            //videoElement.Source = new Uri(filename);

            
        }

        private void videoElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = videoElement.NaturalDuration.TimeSpan;
            Slider_Sick.Maximum = ts.TotalSeconds;
            timer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            videoElement.Stop();
        }

        private void TestBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserTests(chap, user1));
        }
    }
}
