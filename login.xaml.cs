using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BC = BCrypt.Net.BCrypt;

namespace project
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        private List<gebruiker> gebruikers { get; set; } = new List<gebruiker> { };
        public login()
        {
            InitializeComponent();
        }

        private void inlog_button_Click(object sender, RoutedEventArgs e)
        {
            begin_login.Visibility = Visibility.Hidden;
            login_menu.Visibility = Visibility.Visible;
        }

        private void afsluit_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new ShopContext())
            {
                var gebruiker = db.gebruikers.FirstOrDefault(gebruiker => gebruiker.naam == tbx_gebruiker.Text);

                if(gebruiker == null)
                {
                    MessageBox.Show("gebruikersnaam bestaat niet of is verkeerd.");
                }
                else
                {
                    /*
                    string hash = BC.HashPassword("Mijn_logboek!");
                    gebruiker.paswoord = hash;
                    db.SaveChanges();
                    */
                    if (BC.Verify(pwb_wachtwoord.Password, gebruiker.paswoord))
                    {
                        MainWindow MainWindow = new MainWindow();
                        MainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("wachtwoord bestaat niet of is verkeerd.");
                    }
                }
            }
        }
    }
}
