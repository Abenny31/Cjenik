using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Cjenik
{

    public partial class UslugePage : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=BENIC;Initial Catalog=CjenikDatabase;Integrated Security=True;");

        public UslugePage()
        {
            InitializeComponent();
            UcitajUsluge();
            ListaTipova();

        }

        private void PovratakBtn(object sender, RoutedEventArgs e)
        {
            this.Content = null;
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

                ComboTip.Items.Add(dodaj);

            }

            conn.Close();

        }

        public void UcitajUsluge()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SveUslugeNaziv";
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
            cmd.CommandText = "NovaUsluga";
            SqlParameter naziv = new SqlParameter("@Naziv", Naziv_txt.Text);
            SqlParameter fkTip = new SqlParameter("@FkTip", Int32.Parse(FKtip_txt.Text));
            cmd.Parameters.Add(naziv);
            cmd.Parameters.Add(fkTip);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajUsluge();
            MessageBox.Show("Uspješno dodana Usluga", "Spremljeno", MessageBoxButton.OK); ;
            ocisti();

        }

        public void ocisti()
        {
            ID_TXT.Clear();
            Naziv_txt.Clear();
            UcitajUsluge();

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
            cmd.CommandText = "IzbrisiUslugu";

            SqlParameter ID = new SqlParameter("@ID", ID_TXT.Text);
            cmd.Parameters.Add(ID);

            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajUsluge();
            MessageBox.Show("Uspješno izbrisana", "Izbrisano", MessageBoxButton.OK); ;
            ocisti();

        }

        private void promijeniBtn(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UrediUslugu";
            SqlParameter ID = new SqlParameter("@ID", ID_TXT.Text);
            SqlParameter naziv = new SqlParameter("@Naziv", Naziv_txt.Text);
            SqlParameter FkTip = new SqlParameter("@FkTip", FKtip_txt.Text);
            cmd.Parameters.Add(ID);
            cmd.Parameters.Add(naziv);
            cmd.Parameters.Add(FkTip);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajUsluge();
            MessageBox.Show("Uspješna izmjena", "Izmijenjeno", MessageBoxButton.OK); ;
            ocisti();
        }


        private void FilterNazivBtn(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "FilterPoNazivuUsluga";
            SqlParameter naziv = new SqlParameter("@Naziv", FilterNaziv_txt.Text);
            cmd.Parameters.Add(naziv);
            DataTable dataTable = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dataTable.Load(sdr);
            conn.Close();
            dataGrid.ItemsSource = dataTable.DefaultView;

        }

        private void TipSelectionCh(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DataRowView row = dataGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                ID_TXT.Text = row["ID"].ToString();
                Naziv_txt.Text = row["Naziv"].ToString();
                ComboTip.SelectedItem = row["Naziv1"].ToString();

            }

        }

        private void ComboSelection(object sender, SelectionChangedEventArgs e)
        {
            string text = ComboTip.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ComboSelect";
            SqlParameter naziv = new SqlParameter("@Naziv", text);
            cmd.Parameters.Add(naziv);
            DataTable dataTable = new DataTable();
            conn.Open();
            FKtip_txt.Text = Convert.ToString(cmd.ExecuteScalar());

            conn.Close();

        }
    }
}
