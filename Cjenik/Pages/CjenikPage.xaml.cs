using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Cjenik.Pages

{

    public partial class CjenikPage : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2CM2IA0\SQLEXPRESS;Initial Catalog=CjenikDatabase;Integrated Security=True;");

        public CjenikPage(string id)
        {
            InitializeComponent();
            ListaUsluga();
            ListaTipova();
            KlijentID.Text = id;
            UcitajCjenik();
        }


        private void PovratakBtn(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        public void UcitajCjenik()
        {
            String str;
            if (KlijentID.Text != "")
            {
                str = "UcitajCjenikJednoga";
            }
            else
            {
                str = "UcitajCjenike";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "UcitajCjenikJednoga";
            cmd.CommandText = str;
            SqlParameter id = new SqlParameter("@ID", KlijentID.Text);
            cmd.Parameters.Add(id);
            DataTable dataTable = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dataTable.Load(sdr);
            conn.Close();
            dataGrid.ItemsSource = dataTable.DefaultView;

        }


        private void SpremiBtn(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "NoviCjenik";
            SqlParameter IDklijenta = new SqlParameter("@IDklijenta", KlijentID.Text);
            SqlParameter IdUsluge = new SqlParameter("@IdUsluge", Int32.Parse(UslugaID.Text));
            SqlParameter Cijena = new SqlParameter("@Cijena", Cijena_txt.Text);
            cmd.Parameters.Add(IDklijenta);
            cmd.Parameters.Add(IdUsluge);
            cmd.Parameters.Add(Cijena);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajCjenik();
            MessageBox.Show("Uspješno dodan", "Spremljeno", MessageBoxButton.OK); ;
            ocisti();

        }

        public void ocisti()
        {
            Cijena_txt.Clear();
            ID_TXT.Clear();
            Klijent_txt.Clear();
            UslugaID.Clear();
            comboUsluga.SelectedItem = null;
            comboTip.SelectedItem = null;
            UcitajCjenik();

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
            cmd.CommandText = "IzbrisiCjenik";

            SqlParameter ID = new SqlParameter("@ID", ID_TXT.Text);
            cmd.Parameters.Add(ID);

            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajCjenik();
            MessageBox.Show("Uspješno izbrisan", "Izbrisano", MessageBoxButton.OK); ;
            ocisti();

        }

        private void promijeniBtn(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UrediCjenik";
            SqlParameter ID = new SqlParameter("@ID", ID_TXT.Text);
            SqlParameter IdUsluge = new SqlParameter("@IdUsluge", UslugaID.Text);
            SqlParameter Cijena = new SqlParameter("@Cijena", Cijena_txt.Text);
            cmd.Parameters.Add(ID);
            cmd.Parameters.Add(IdUsluge);
            cmd.Parameters.Add(Cijena);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajCjenik();
            MessageBox.Show("Uspješna izmjena", "Izmijenjeno", MessageBoxButton.OK); ;
            ocisti();
        }


        private void TipSelectionCh(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row = dataGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                ID_TXT.Text = row["ID"].ToString();
                comboTip.SelectedItem = row["Naziv"].ToString();
                comboUsluga.SelectedItem = row["Usluga"].ToString();
                Klijent_txt.Text = row["Klijent"].ToString();
                Cijena_txt.Text = row["Cijena"].ToString();

            }

        }
        public void ListaUsluga()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SveUslugeLista";
            DataTable dataTable = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dataTable.Load(sdr);
            foreach (DataRow dr in dataTable.Rows)
            {
                string dodaj = dr["Naziv"].ToString();

                comboUsluga.Items.Add(dodaj);

            }

            conn.Close();


        }
        public void ListaTipova()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SviTipovi";
            DataTable dataTable = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dataTable.Load(sdr);
            foreach (DataRow dr in dataTable.Rows)
            {
                string dodaj = dr["Naziv"].ToString();

                comboTip.Items.Add(dodaj);

            }

            conn.Close();

        }
        private void ComboSelection(object sender, SelectionChangedEventArgs e)
        {
            if (comboTip.SelectedItem != null)
            {
                string text = comboTip.SelectedItem.ToString();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ComboSelect";
                SqlParameter naziv = new SqlParameter("@Naziv", text);
                cmd.Parameters.Add(naziv);
                DataTable dataTable = new DataTable();
                conn.Open();
                FkTip.Text = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
            }
        }

        private void UslugaChg(object sender, SelectionChangedEventArgs e)
        {
            if (comboUsluga.SelectedItem != null)
            {
                string text = comboUsluga.SelectedItem.ToString();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ComboUslugaSelect";
                SqlParameter naziv = new SqlParameter("@Naziv", text);
                cmd.Parameters.Add(naziv);
                DataTable dataTable = new DataTable();
                conn.Open();
                UslugaID.Text = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();
            }
        }
    }
}
