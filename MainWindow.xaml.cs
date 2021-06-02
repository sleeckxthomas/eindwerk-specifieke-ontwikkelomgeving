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
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;
using System.ComponentModel;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<registratie> registraties { get; set; } = new List<registratie> { };
        private List<gemeente> gemeentes { get; set; } = new List<gemeente> { };
        private List<karper> karpers { get; set; } = new List<karper> { };
        private List<water> waters { get; set; } = new List<water> { };
        private List<vangst> vangsten { get; set; } = new List<vangst> { };

        public MainWindow()
        {
            InitializeComponent();
            using (var db = new ShopContext())
            {
                registraties = db.registraties.ToList();
                gemeentes = db.gemeentes.ToList();
                karpers = db.karpers.ToList();
                waters = db.waters.ToList();
                vangsten = db.vangsten.ToList();
            }
        }
        private void data_toevoegen(object sender, RoutedEventArgs e)
        {
            using (var db = new ShopContext())
            {
                nul_waarde();
                db.Add(new karper { type_karper = combo_karper.Text, naam_karper = tbx_naam_karper.Text, gewicht = float.Parse(tbx_gewicht.Text), lengte = float.Parse(tbx_lenge.Text) });
                db.Add(new registratie { seizoen = combo_seizoen.Text, vangst_tijd = DateTime.Parse(datum_kiezer.Text), water_temperatuur = float.Parse(tbx_tem.Text), luchtdruk = int.Parse(tbx_lucht.Text), windrichting = combo_wind.Text, diepte_rig = float.Parse(tbx_rig.Text), baiting = combo_baiting.Text });
                db.Add(new water { naam = tbx_water.Text, gemeente_id = id_gemeente()});
                db.SaveChanges();
                registratie_id();
                clear_invoer();
                MessageBox.Show("Data toegevoegd");
            }
        }
        private int id_gemeente()
        {
            using(var db = new ShopContext())
            {

                var m = db.gemeentes;
                var id = m.Where(g => g.gemeente_naam == combo_gemeente.Text);
                var id1 = id.Select(i => i.gemeente_id).First();
                return id1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new ShopContext())
            {
                var m = db.gemeentes;
                var gm = m.Select(g => g.gemeente_naam);
                foreach (var gemeente in gm)
                {
                    combo_gemeente.Items.Add(gemeente);
                }
            }
        }
        private void go_to_data_menu_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            data_menu.Visibility = Visibility.Visible;
        }

        private void afsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void nul_waarde()
        {
            float g = 0;
            if (tbx_gewicht.Text == "")
            {
                tbx_gewicht.Text = g.ToString();
            }
            if (tbx_lenge.Text == "")
            {
                tbx_lenge.Text = g.ToString();
            }
            if (datum_kiezer.Text == "")
            {
                datum_kiezer.Text = DateTime.Now.ToString();
            }
            if (tbx_tem.Text == "")
            {
                tbx_tem.Text = g.ToString();
            }
            if (tbx_lucht.Text == "")
            {
                tbx_lucht.Text = g.ToString();
            }
            if (tbx_rig.Text == "")
            {
                tbx_rig.Text = g.ToString();
            }
        }
        private void registratie_id()
        {
            using (var db = new ShopContext())
            {
                var k = db.karpers.OrderBy(x => x.karper_id).Last();
                var k_id = k.karper_id;
                var r = db.registraties.OrderBy(x => x.registratie_id).Last();
                var r_id = r.registratie_id;
                var w = db.waters.OrderBy(x => x.water_id).Last();
                var w_id = w.water_id;
                db.Add(new vangst { karper_id = k_id, registratie_id = r_id, water_id = w_id });
                db.SaveChanges();
            }
        }
        private void clear_invoer()
        {
            combo_karper.SelectedIndex = -1;
            combo_seizoen.SelectedIndex = -1;
            combo_gemeente.SelectedIndex = 1448;
            combo_seizoen.SelectedIndex = -1;
            combo_wind.SelectedIndex = -1;
            combo_baiting.SelectedIndex = -1;
            datum_kiezer.SelectedDate = DateTime.Now;
            tbx_naam_karper.Clear();
            tbx_water.Clear();
            tbx_gewicht.Clear();
            tbx_tem.Clear();
            tbx_lenge.Clear();
            tbx_lucht.Clear();
            tbx_rig.Clear();
        }
        private void terug_menu_button_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Visible;
            data_menu.Visibility = Visibility.Hidden;
            clear_invoer();
        }
        private void naar_search_menu_button_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            data_menu.Visibility = Visibility.Hidden;
            gegevens_menu.Visibility = Visibility.Visible;
            clear_invoer();
        }
        private void tbx_rig_TextChanged(object sender, TextChangedEventArgs e)
        {
            float floatrig;
            if (tbx_rig.Text != "")
            {
                try
                {
                    floatrig = float.Parse(tbx_rig.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("gelieven enkel een getal in te geven");
                    tbx_rig.Clear();
                    return;
                }
            }
        }
        private void tbx_gewicht_TextChanged(object sender, TextChangedEventArgs e)
        {
            float fltgewicht;
            if (tbx_gewicht.Text != "")
            {
                try
                {
                    fltgewicht = float.Parse(tbx_gewicht.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("gelieven enkel een getal in te geven");
                    tbx_gewicht.Clear();
                    return;
                }
            }
        }
        private void tbx_tem_TextChanged(object sender, TextChangedEventArgs e)
        {
            float flttem;
            if (tbx_tem.Text != "")
            {
                try
                {
                    flttem = float.Parse(tbx_tem.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("gelieven enkel een getal in te geven");
                    tbx_tem.Clear();
                    return;
                }

            }
        }
        private void tbx_lenge_TextChanged(object sender, TextChangedEventArgs e)
        {
            float fltlengte;
            if (tbx_lenge.Text != "")
            {
                try
                {
                    fltlengte = float.Parse(tbx_lenge.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("gelieven enkel een getal in te geven");
                    tbx_lenge.Clear();
                    return;
                }
            }
        }
        private void tbx_lucht_TextChanged(object sender, TextChangedEventArgs e)
        {
            int intluchtdruk;
            if (tbx_lucht.Text != "")
            {
                try
                {
                    intluchtdruk = int.Parse(tbx_lucht.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("gelieven enkel een getal in te geven");
                    tbx_lucht.Clear();
                    return;
                }
            }
        }

        private void go_to_search_menu_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            data_menu.Visibility = Visibility.Hidden;
            gegevens_menu.Visibility = Visibility.Visible;
        }

        private void cbx_kiezen_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new ShopContext())
            {
                string text = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
                if (text == "type karper")
                {
                    cmb_detail_keuze_selectie.Items.Clear();
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Text = "welke type wilt u opzoeken?";
                    var type_karper = db.karpers.Select(k => k.type_karper).Where(k => k != "");
                    foreach (var karpers in type_karper)
                    {
                        if(!cmb_detail_keuze_selectie.Items.Contains(karpers))
                        cmb_detail_keuze_selectie.Items.Add(karpers);
                    }

                }
                else if (text == "naam karper")
                {
                    cmb_detail_keuze_selectie.Items.Clear();

                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Text = "welke karper zoekt u?";
                    var naam_karper = db.karpers.Select(k => k.naam_karper).Where(k => k != "");
                    foreach (var naam in naam_karper)
                    {
                        if(!cmb_detail_keuze_selectie.Items.Contains(naam))
                        cmb_detail_keuze_selectie.Items.Add(naam);
                    }
                }
                else if (text == "gewicht")
                {
                    cmb_detail_keuze_selectie.Items.Clear();
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Text = "in welk gewichtsklasse zoekt u?";
                    var gewicht = db.karpers.Select(k => k.gewicht).Where(k => k != 0);
                    cmb_detail_keuze_selectie.Items.Add("0-5 kg");
                    cmb_detail_keuze_selectie.Items.Add("5-10 kg");
                    cmb_detail_keuze_selectie.Items.Add("10-15 kg");
                    cmb_detail_keuze_selectie.Items.Add("15-20 kg");
                    cmb_detail_keuze_selectie.Items.Add("20-25 kg");
                    cmb_detail_keuze_selectie.Items.Add("25+ kg");
                }
                else if (text == "seizoen")
                {
                    cmb_detail_keuze_selectie.Items.Clear();
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Text = "in welk seizoen wilt u zoeken?";
                    var seizoen = db.registraties.Select(k => k.seizoen).Where(k => k != "");
                    foreach (var weer in seizoen)
                    {
                        if(!cmb_detail_keuze_selectie.Items.Contains(weer))
                        cmb_detail_keuze_selectie.Items.Add(weer);
                    }
                }
                else if (text == "naam water")
                {
                    cmb_detail_keuze_selectie.Items.Clear();
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Text = "welk water zoekt u?";
                    var naam_water = db.waters.Select(k => k.naam).Where(k => k != "");
                    foreach (var naam in naam_water)
                    {
                        if (!cmb_detail_keuze_selectie.Items.Contains(naam))
                            cmb_detail_keuze_selectie.Items.Add(naam);
                    }
                }
                else if (text == "gemeente")
                {
                    cmb_detail_keuze_selectie.Items.Clear();
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    cmb_detail_keuze_selectie.Visibility = Visibility.Visible;
                    tbx_detail_keuze.Text = "welke gemeente zoekt u?";
                    var plaats = db.waters.Where(k => k.gemeente_id != null);
                    foreach (var karper_plaats in plaats)
                    {
                        var gemeente_naam = db.gemeentes.Where(g => g.gemeente_id == karper_plaats.gemeente_id).First();
                        if(!cmb_detail_keuze_selectie.Items.Contains(gemeente_naam.gemeente_naam))
                        cmb_detail_keuze_selectie.Items.Add(gemeente_naam.gemeente_naam);
                    }
                }
            }
        }

        private void naar_menu_Click(object sender, RoutedEventArgs e)
        {
            gegevens_menu.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }

        private void naar_data_menu_Click(object sender, RoutedEventArgs e)
        {
            gegevens_menu.Visibility = Visibility.Hidden;
            data_menu.Visibility = Visibility.Visible;
        }

        private void cmb_detail_keuze_selectie_DropDownClosed(object sender, EventArgs e)
        {
            if(cmb_detail_keuze_selectie.SelectedIndex != -1) 
            {
                using (var db = new ShopContext())
                {
                    //string text = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
                    if (cbx_kiezen_data.Text == "type karper")
                    {
                        List<data_karper> items = new List<data_karper>();
                        var karpers = db.karpers.Where(k => k.type_karper == cmb_detail_keuze_selectie.Text);
                        foreach (var k in karpers)
                        {
                            var water = db.waters.Where(w => w.water_id == k.karper_id).First();
                            var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                            var registratie = db.registraties.Where(r => r.registratie_id == k.karper_id).First();
                            items.Add(new data_karper() { type_karper = k.type_karper, naam_karper = k.naam_karper, gewicht = k.gewicht, seizoen = registratie.seizoen, gemeente_naam = gemeente.gemeente_naam, water_naam = water.naam });
                        }
                        lvw_data.ItemsSource = items;
                    }
                    else if (cbx_kiezen_data.Text == "naam karper")
                    {
                        List<data_karper> items = new List<data_karper>();
                        var karpers = db.karpers.Where(k => k.naam_karper == cmb_detail_keuze_selectie.Text);
                        foreach (var k in karpers)
                        {
                            var water = db.waters.Where(w => w.water_id == k.karper_id).First();
                            var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                            var registratie = db.registraties.Where(r => r.registratie_id == k.karper_id).First();
                            items.Add(new data_karper() { type_karper = k.type_karper, naam_karper = k.naam_karper, gewicht = k.gewicht, seizoen = registratie.seizoen, water_naam = water.naam, gemeente_naam = gemeente.gemeente_naam });
                        }
                        lvw_data.ItemsSource = items;
                    }
                    else if(cbx_kiezen_data.Text == "gewicht")
                    {
                        List<data_karper> items = new List<data_karper>();
                        if(cmb_detail_keuze_selectie.Text == "0-5 kg")
                        {
                            var gewicht = db.karpers.Where(g => g.gewicht >= 0 && g.gewicht <= 5);
                            foreach(var g in gewicht)
                            {
                                var water = db.waters.Where(w => w.water_id == g.karper_id).First();
                                var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                                var registratie = db.registraties.Where(r => r.registratie_id == g.karper_id).First();
                                items.Add(new data_karper() { type_karper = g.type_karper, naam_karper = g.naam_karper, gewicht = g.gewicht, seizoen = registratie.seizoen, gemeente_naam = gemeente.gemeente_naam, water_naam = water.naam });
                            }
                            lvw_data.ItemsSource = items;
                        }
                        else if (cmb_detail_keuze_selectie.Text == "5-10 kg")
                        {
                            var gewicht = db.karpers.Where(g => g.gewicht >= 5 && g.gewicht <= 10);
                            foreach (var g in gewicht)
                            {
                                var water = db.waters.Where(w => w.water_id == g.karper_id).First();
                                var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                                var registratie = db.registraties.Where(r => r.registratie_id == g.karper_id).First();
                                items.Add(new data_karper() { type_karper = g.type_karper, naam_karper = g.naam_karper, gewicht = g.gewicht, seizoen = registratie.seizoen, gemeente_naam = gemeente.gemeente_naam, water_naam = water.naam });
                            }
                            lvw_data.ItemsSource = items;
                        }
                        else if (cmb_detail_keuze_selectie.Text == "10-15 kg")
                        {
                            var gewicht = db.karpers.Where(g => g.gewicht >= 10 && g.gewicht <= 15);
                            foreach (var g in gewicht)
                            {
                                var water = db.waters.Where(w => w.water_id == g.karper_id).First();
                                var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                                var registratie = db.registraties.Where(r => r.registratie_id == g.karper_id).First();
                                items.Add(new data_karper() { type_karper = g.type_karper, naam_karper = g.naam_karper, gewicht = g.gewicht, seizoen = registratie.seizoen, gemeente_naam = gemeente.gemeente_naam, water_naam = water.naam });
                            }
                            lvw_data.ItemsSource = items;
                        }
                        else if (cmb_detail_keuze_selectie.Text == "15-20 kg")
                        {
                            var gewicht = db.karpers.Where(g => g.gewicht >= 15 && g.gewicht <= 20);
                            foreach (var g in gewicht)
                            {
                                var water = db.waters.Where(w => w.water_id == g.karper_id).First();
                                var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                                var registratie = db.registraties.Where(r => r.registratie_id == g.karper_id).First();
                                items.Add(new data_karper() { type_karper = g.type_karper, naam_karper = g.naam_karper, gewicht = g.gewicht, seizoen = registratie.seizoen, gemeente_naam = gemeente.gemeente_naam, water_naam = water.naam });
                            }
                            lvw_data.ItemsSource = items;
                        }
                        else if (cmb_detail_keuze_selectie.Text == "20-25 kg")
                        {
                            var gewicht = db.karpers.Where(g => g.gewicht >= 20 && g.gewicht <= 25);
                            foreach (var g in gewicht)
                            {
                                var water = db.waters.Where(w => w.water_id == g.karper_id).First();
                                var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                                var registratie = db.registraties.Where(r => r.registratie_id == g.karper_id).First();
                                items.Add(new data_karper() { type_karper = g.type_karper, naam_karper = g.naam_karper, gewicht = g.gewicht, seizoen = registratie.seizoen, gemeente_naam = gemeente.gemeente_naam, water_naam = water.naam });
                            }
                            lvw_data.ItemsSource = items;
                        }
                        else if (cmb_detail_keuze_selectie.Text == "25+ kg")
                        {
                            var gewicht = db.karpers.Where(g => g.gewicht >= 25);
                            foreach (var g in gewicht)
                            {
                                var water = db.waters.Where(w => w.water_id == g.karper_id).First();
                                var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                                var registratie = db.registraties.Where(r => r.registratie_id == g.karper_id).First();
                                items.Add(new data_karper() { type_karper = g.type_karper, naam_karper = g.naam_karper, gewicht = g.gewicht, seizoen = registratie.seizoen, gemeente_naam = gemeente.gemeente_naam, water_naam = water.naam });
                            }
                            lvw_data.ItemsSource = items;
                        }
                        else
                        {
                            lvw_data.ItemsSource = null;
                        }
                    }
                    else if (cbx_kiezen_data.Text == "seizoen")
                    {
                        List<data_karper> items = new List<data_karper>();
                        var registratie = db.registraties.Where(r => r.seizoen == cmb_detail_keuze_selectie.Text);
                        foreach (var r in registratie)
                        {
                            var water = db.waters.Where(w => w.water_id == r.registratie_id).First();
                            var gemeente = db.gemeentes.Where(g => g.gemeente_id == water.gemeente_id).First();
                            var karper = db.karpers.Where(k => k.karper_id == r.registratie_id).First();
                            
                            items.Add(new data_karper() { type_karper = karper.type_karper, naam_karper = karper.naam_karper, gewicht = karper.gewicht, seizoen = r.seizoen, water_naam = water.naam, gemeente_naam = gemeente.gemeente_naam });
                        }
                        lvw_data.ItemsSource = items;
                    }
                    else if(cbx_kiezen_data.Text == "naam water")
                    {
                        List<data_karper> items = new List<data_karper>();
                        var water = db.waters.Where(w => w.naam == cmb_detail_keuze_selectie.Text);
                        foreach (var w in water)
                        {
                            var wat = db.waters.Where(wa => wa.water_id == w.water_id).First();
                            var gemeente = db.gemeentes.Where(g => g.gemeente_id == wat.gemeente_id).First();
                            var karper = db.karpers.Where(k => k.karper_id == w.water_id).First();
                            var registratie = db.registraties.Where(r => r.registratie_id == w.water_id).First();
                            items.Add(new data_karper() {gemeente_naam = gemeente.gemeente_naam, water_naam = w.naam, type_karper = karper.type_karper, naam_karper = karper.naam_karper, gewicht = karper.gewicht, seizoen = registratie.seizoen});
                        }
                        lvw_data.ItemsSource = items;
                    }
                    else if (cbx_kiezen_data.Text == "gemeente")
                    {
                        List<data_karper> items = new List<data_karper>();
                        var gemeente = db.gemeentes.Where(g => g.gemeente_naam == cmb_detail_keuze_selectie.Text).First();
                        var water = db.waters.Where(w => w.gemeente_id == gemeente.gemeente_id);
                        foreach (var w in water)
                        {
                            var karper = db.karpers.Where(k => k.karper_id == w.water_id).First();
                            var registratie = db.registraties.Where(r => r.registratie_id == w.water_id).First();
                            items.Add(new data_karper() {water_naam = w.naam, gemeente_naam = gemeente.gemeente_naam, type_karper = karper.type_karper, naam_karper = karper.naam_karper, gewicht = karper.gewicht, seizoen = registratie.seizoen});
                        }
                        lvw_data.ItemsSource = items;

                    }
                }
            }
        }
    }
}
