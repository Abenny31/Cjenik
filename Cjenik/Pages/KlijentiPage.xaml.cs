using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Cjenik.Models;
using System;
using Cjenik.Pages;

namespace Cjenik
{
    /// <summary>
    /// Interaction logic for KlijentiPage.xaml
    /// </summary>
    public partial class KlijentiPage : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=BENIC;Initial Catalog=CjenikDatabase;Integrated Security=True;");
        public KlijentiPage()
        {
            InitializeComponent();
            UcitajKlijente();


        }
        public void UcitajKlijente()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SviKlijenti";
            DataTable dataTable = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dataTable.Load(sdr);
            conn.Close();
            dataGrid.ItemsSource = dataTable.DefaultView;

        }

        private void PovratakBtn(object sender, RoutedEventArgs e)
        {
            this.Content = null;


        }

        private void SpremiBtn(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "NoviKlijent";
            Klijenti klijent = new Klijenti();
            klijent.Naziv = Naziv_txt.Text;
            klijent.OIB = Oib_txt.Text;
            klijent.Adresa = Adresa_txt.Text;
            if (Email_txt.Text == null)
            {
                klijent.Email = null;
            }
            else
            {
                klijent.Email = Email_txt.Text;
            }
            SqlParameter naziv = new SqlParameter("@Naziv", klijent.Naziv);
            SqlParameter oib = new SqlParameter("@OIB", klijent.OIB);
            SqlParameter adresa = new SqlParameter("@Adresa", klijent.Adresa);
            SqlParameter email = new SqlParameter("@Email", klijent.Email);
            cmd.Parameters.Add(naziv);
            cmd.Parameters.Add(oib);
            cmd.Parameters.Add(adresa);
            cmd.Parameters.Add(email);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajKlijente();
            MessageBox.Show("Uspješno dodan klijent", "Spremljeno", MessageBoxButton.OK); ;
            ocisti();


        }

        public void ocisti()
        {
            ID_TXT.Clear();
            Naziv_txt.Clear();
            Oib_txt.Clear();
            Adresa_txt.Clear();
            Email_txt.Clear();
            UcitajKlijente();

        }

        private void ocistiBtn(object sender, RoutedEventArgs e)
        {
            ocisti();
        }

        private void izbrisiBtn(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IzbrisiKlijenta";
            Klijenti klijent = new Klijenti();
            klijent.ID = Int32.Parse(ID_TXT.Text);

            SqlParameter ID = new SqlParameter("@ID", klijent.ID);
            cmd.Parameters.Add(ID);

            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajKlijente();
            MessageBox.Show("Uspješno izbrisan", "Izbrisano", MessageBoxButton.OK); ;
            ocisti();

        }

        private void promijeniBtn(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UrediKlijenta";
            Klijenti klijent = new Klijenti();
            klijent.ID = Int32.Parse(ID_TXT.Text);
            klijent.Naziv = Naziv_txt.Text;
            klijent.OIB = Oib_txt.Text;
            klijent.Adresa = Adresa_txt.Text;
            klijent.Email = Email_txt.Text;
            SqlParameter ID = new SqlParameter("@ID" , klijent.ID);
            SqlParameter naziv = new SqlParameter("@Naziv", klijent.Naziv);
            SqlParameter oib = new SqlParameter("@OIB", klijent.OIB);
            SqlParameter adresa = new SqlParameter("@Adresa", klijent.Adresa);
            SqlParameter email = new SqlParameter("@Email", klijent.Email);
            cmd.Parameters.Add(ID);
            cmd.Parameters.Add(naziv);
            cmd.Parameters.Add(oib);
            cmd.Parameters.Add(adresa);
            cmd.Parameters.Add(email);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajKlijente();
            MessageBox.Show("Uspješna izmjena", "Izmijenjeno", MessageBoxButton.OK); ;
            ocisti();
        }
        public string IDtxt { get; set; }
        private void IzaberiBtn(object sender, RoutedEventArgs e)
        {
            IDtxt= ID_TXT.Text;
            CjenikPage c1 = new CjenikPage();
            this.NavigationService.Navigate(c1, IDtxt);

        }

        private void FilterNazivBtn(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "FilterPoNazivu";
            SqlParameter naziv = new SqlParameter("@naziv", FilterNaziv_txt.Text);
            cmd.Parameters.Add(naziv);
            DataTable dataTable = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dataTable.Load(sdr);
            conn.Close();
            dataGrid.ItemsSource = dataTable.DefaultView;

        }

        private void dataGridSelCh(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row = dataGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                ID_TXT.Text = row["ID"].ToString();
                Naziv_txt.Text = row["Naziv"].ToString();
                Oib_txt.Text = row["OIB"].ToString();
                Adresa_txt.Text = row["Adresa"].ToString();
                Email_txt.Text = row["Email"].ToString();

            }


        }
    }
}
