using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cjenik.Pages

{

    public partial class CjenikPage : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=BENIC;Initial Catalog=CjenikDatabase;Integrated Security=True;");

        public CjenikPage()
        {
            InitializeComponent();
            UcitajCjenik();
            ListaUsluga();
        }


        private void navComplete(object sender, NavigationEventArgs e)
        {
            //e.Content.ToString();
            
            string str = (string)e.ExtraData;
            KlijentID.Text = str;

        }



        private void PovratakBtn(object sender, RoutedEventArgs e)
        {
            this.Content = null;

        }

        public void UcitajCjenik()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "UcitajCjenikJednoga";
            cmd.CommandText = "UcitajCjenike";
            SqlParameter id = new SqlParameter("@ID",KlijentID.Text);
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
            SqlParameter Cijena = new SqlParameter("@Cijena", Cijena_txt);
            cmd.Parameters.Add(IDklijenta);
            cmd.Parameters.Add(IdUsluge);
            cmd.Parameters.Add(Cijena);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            conn.Close();
            UcitajCjenik();
            MessageBox.Show("Uspješno dodana Usluga", "Spremljeno", MessageBoxButton.OK); ;
            ocisti();


        }

        public void ocisti()
        {
            Cijena_txt.Clear();
            ID_TXT.Clear();
            Klijent_txt.Clear();
            UslugaID.Clear();
            comboUsluga.Items.Clear();
            comboTip.Items.Clear();
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
            SqlParameter IdKlijenta = new SqlParameter("@IDklijenta", KlijentID.Text);
            SqlParameter IdUsluge = new SqlParameter("@IdUsluge", UslugaID.Text);
            SqlParameter Cijena = new SqlParameter("@Cijena", Cijena_txt);
            cmd.Parameters.Add(ID);
            cmd.Parameters.Add(IdUsluge);
            cmd.Parameters.Add(Cijena);
            cmd.Parameters.Add(IdKlijenta);
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
        //private void ComboSelection(object sender, SelectionChangedEventArgs e)
        //{
        //    string text = comboUsluga.SelectedItem.ToString();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "ComboSelect";
        //    SqlParameter naziv = new SqlParameter("@Naziv", text);
        //    cmd.Parameters.Add(naziv);
        //    DataTable dataTable = new DataTable();
        //    conn.Open();
        //    //int result = (int)cmd.ExecuteScalar();
        //    FKtip_txt.Text = Convert.ToString(cmd.ExecuteScalar());


        //    conn.Close();

        //    //FKtip_txt.Text = result.ToString(); ;




        //}
    }
}
