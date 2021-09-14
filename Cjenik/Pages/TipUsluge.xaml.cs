using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Cjenik.Pages
{

    public partial class TipUsluge : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2CM2IA0\SQLEXPRESS;Initial Catalog=CjenikDatabase;Integrated Security=True;");
        public TipUsluge()
        {
            InitializeComponent();
            UcitajTipove();
        }

        private void PovratakBtn(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }


        public void UcitajTipove()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SviTipovi";
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
            cmd.CommandText = "NoviTip";
            SqlParameter naziv = new SqlParameter("@Naziv", Naziv_txt.Text);
            cmd.Parameters.Add(naziv);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajTipove();
            MessageBox.Show("Uspješno dodan Tip Usluge", "Spremljeno", MessageBoxButton.OK); ;
            ocisti();


        }

        public void ocisti()
        {
            ID_TXT.Clear();
            Naziv_txt.Clear();
            UcitajTipove();

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
            cmd.CommandText = "IzbrisiTip";

            SqlParameter ID = new SqlParameter("@ID", ID_TXT.Text);
            cmd.Parameters.Add(ID);

            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajTipove();
            MessageBox.Show("Uspješno izbrisan", "Izbrisano", MessageBoxButton.OK); ;
            ocisti();

        }

        private void promijeniBtn(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UrediTip";
            SqlParameter ID = new SqlParameter("@ID", ID_TXT.Text);
            SqlParameter naziv = new SqlParameter("@Naziv", Naziv_txt.Text);
            cmd.Parameters.Add(ID);
            cmd.Parameters.Add(naziv);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajTipove();
            MessageBox.Show("Uspješna izmjena", "Izmijenjeno", MessageBoxButton.OK); ;
            ocisti();
        }


        private void FilterNazivBtn(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "FilterPoNazivuTip";
            SqlParameter naziv = new SqlParameter("@naziv", FilterNaziv_txt.Text);
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

            }

        }
    }
}
