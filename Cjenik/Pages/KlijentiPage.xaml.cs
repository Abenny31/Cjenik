using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Data;

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


    }
}
