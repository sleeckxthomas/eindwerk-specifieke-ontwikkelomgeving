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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;


namespace eindwerk_ontwikkelingomgeving
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Circle> circles = new List<Circle> { };
        private List<speler> spelers { get; set; } = new List<speler> { };
        MediaPlayer mplayer = new MediaPlayer();

        private int aantal = 20;
        private int aantal_2 = 5;

        private int teller = 50;
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < aantal; i++)
            {
                var c = new Circle("images\\balcb.jpg");
                circles.Add(c);
                c.AddToCanvas(speel_veld);
            }
            for (int i = 0; i < aantal_2; i++)
            {
                var c1 = new Circle("images\\baland.jpg");
                circles.Add(c1);
                c1.AddToCanvas(speel_veld);
            }
            CompositionTarget.Rendering += Loop;
            

        }
        private void Loop(object sender, EventArgs e)
        {
            foreach (var c in circles)
            {
                c.Move();
            }
        }
        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_naam.Text == "")
            {
                MessageBox.Show("gelieven uw naam in te geven voor te starten");
            }
            else if (tbx_naam.Text != "")
            {
                dispatcherTimer.Tick += update_timer;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
                show_timer();
                speel_veld.Visibility = Visibility.Visible;
            }
        }
        private void mouse_speel_veld(object sender, MouseButtonEventArgs e)
        {
            aantal -= 1;
            if (teller >=20)
            {
                score += 3;
                tbx_score.Text = score.ToString();
            }
            else if(teller >= 10 && teller < 20)
            {

                score += 2;
                tbx_score.Text = score.ToString();
            }
            else if(teller >= 1 && teller < 10)
            {
                score += 1;
                tbx_score.Text = score.ToString();
            }
            var circle = circles.FirstOrDefault(c => c.Ellps == e.Source);
            if(circle.Ellps.Tag.ToString() == "fouten bal.")
            {
                mplayer.Open(new Uri(@"sound/incorrect sound effect.mp3", UriKind.Relative));
                mplayer.Play();
                speel_veld.Visibility = Visibility.Hidden;
                reset();
                MessageBox.Show("ge zet eraan!!");
            }
            else if(circle.Ellps.Tag.ToString() == "")
            {
                mplayer.Open(new Uri(@"sound/PING - Sound effect.mp3", UriKind.Relative));
                mplayer.Play();
                circles.Remove(circle);
                circle.remove(speel_veld);
            }

        }


        private void update_timer(object sender, EventArgs e)
        {
            teller--;
            show_timer();
            using (var db = new ShopContext())
            {
                if (teller == 0)
                {
                    mplayer.Open(new Uri(@"sound/Mission Failed.mp3", UriKind.Relative));
                    mplayer.Play();
                    dispatcherTimer.Stop();
                    speel_veld.Visibility = Visibility.Hidden;
                    nul_waarde();
                    db.Add(new speler { naam = tbx_naam.Text, score = int.Parse(tbx_score.Text) });
                    db.SaveChanges();
                    reset();
                    MessageBox.Show("spel is afgelopen.");
                }
                else if(aantal == 0)
                {
                    int eind_score = int.Parse(tbx_score.Text);
                    tbx_score.Text = (eind_score += teller).ToString();
                    mplayer.Open(new Uri(@"sound/You Win (Street Fighter) - Sound Effect.mp3", UriKind.Relative));
                    mplayer.Play();
                    dispatcherTimer.Stop();
                    speel_veld.Visibility = Visibility.Hidden;
                    nul_waarde();
                    db.Add(new speler { naam = tbx_naam.Text, score = int.Parse(tbx_score.Text) });
                    db.SaveChanges();
                    reset();
                    MessageBox.Show("spel is afgelopen.");
                }
            }
        }
        private void show_timer()
        {
            int min = teller / 60;
            int sec = teller % 60;
            tbx_timer.Text = $"{min}:{(sec < 10 ? "0" + sec.ToString() : sec.ToString())}";
        }
        private void reset()
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
        private void nul_waarde()
        {
            float g = 0;
            if(tbx_score.Text == "")
            {
                tbx_score.Text = g.ToString();
            }
        }
    }
}
