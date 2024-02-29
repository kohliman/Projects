using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls.Primitives;

namespace A2ManmeetSinghKohli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void displaybillingdata()
        {
            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))
            {
                string query = "SELECT B.BillingID, C.FirstName AS ClientFirstName, C.LastName AS ClientLastName, " +
                        "A.FirstName AS AttorneyFirstName, A.LastName AS AttorneyLastName, " +
                        "CA.Category AS CategoryName, R.Rate AS BillingRate, B.Date, B.Hours " +
                        "FROM Billing B " +
                        "INNER JOIN Clients C ON B.ClientID = C.ClientID " +
                        "INNER JOIN Attorneys A ON B.AttorneyID = A.AttorneyID " +
                        "INNER JOIN Categories CA ON B.CategoryID = CA.CategoryID " +
                        "INNER JOIN Rates R ON B.RateID = R.RateID ";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                grdinfo.ItemsSource = dt.DefaultView;
            }


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startDate.DisplayDateStart = new DateTime(2018, 6, 1);
            startDate.DisplayDateEnd = new DateTime(2018, 6, 15);
            EndDate.DisplayDateStart = new DateTime(2018, 6, 1);
            EndDate.DisplayDateEnd = new DateTime(2018, 6, 15);
            displaybillingdata();

            using (SqlConnection conn=new SqlConnection(Data.ConnectionString))
            {
                string query = "SELECT AttorneyID, LastName FROM Attorneys";
                        SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                cmbAttorneys.ItemsSource = dt.DefaultView;
                cmbAttorneys.DisplayMemberPath = "LastName";
                cmbAttorneys.SelectedValuePath = "AttorneyID";

            }

        }

        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))
            {
                string query = "SELECT B.BillingID, C.FirstName AS ClientFirstName, C.LastName AS ClientLastName, " +
           "A.FirstName AS AttorneyFirstName, A.LastName AS AttorneyLastName, " +
           "CA.Category AS CategoryName, R.Rate AS BillingRate, B.Date, B.Hours " +
           "FROM Billing B " +
           "INNER JOIN Clients C ON B.ClientID = C.ClientID " +
           "INNER JOIN Attorneys A ON B.AttorneyID = A.AttorneyID " +
           "INNER JOIN Categories CA ON B.CategoryID = CA.CategoryID " +
           "INNER JOIN Rates R ON B.RateID = R.RateID " +
           "WHERE C.FirstName LIKE @fname OR C.LastName LIKE @lname"; 

                string searchText = "%" + txtClientbyname.Text + "%";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fname", searchText);
                cmd.Parameters.AddWithValue("@lname", searchText);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                grdinfo.ItemsSource = dt.DefaultView;

            }


        }

        private void btngetbybills_Click(object sender, RoutedEventArgs e)
        {
            displaybillingdata();
        }

        private void cmbAttorneys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAttorneys.SelectedValue != null)
            {
                int attorneyid = (int)cmbAttorneys.SelectedValue;

                using (SqlConnection conn = new SqlConnection(Data.ConnectionString))
                {
                    string query = "SELECT B.BillingID, C.FirstName AS ClientFirstName, C.LastName AS ClientLastName," +
                       "A.FirstName AS AttorneyFirstName, A.LastName AS AttorneyLastName, " +
                       "CA.Category AS CategoryName, R.Rate AS BillingRate, B.Date, B.Hours " +
                       "FROM Billing B " +
                       "INNER JOIN Clients C ON B.ClientID = C.ClientID " +
                       "INNER JOIN Attorneys A ON B.AttorneyID = A.AttorneyID " +
                       "INNER JOIN Categories CA ON B.CategoryID = CA.CategoryID " +
                       "INNER JOIN Rates R ON B.RateID = R.RateID WHERE A.AttorneyID = @at";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@at", attorneyid);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    grdinfo.ItemsSource = dt.DefaultView;
                }
            }   
        }
        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (startDate.SelectedDate.HasValue)
            {
                EndDate.DisplayDateStart = startDate.SelectedDate.Value;

                if (EndDate.SelectedDate < startDate.SelectedDate.Value)
                {
                    EndDate.SelectedDate = startDate.SelectedDate.Value;
                }
            }
        }

        private void btngetBillsBydate_Click(object sender, RoutedEventArgs e)
        {
            if (startDate.SelectedDate.HasValue && EndDate.SelectedDate.HasValue)
            {
                DateTime staDate = startDate.SelectedDate.Value;
                DateTime endDate = EndDate.SelectedDate.Value.AddDays(1);

                using (SqlConnection conn = new SqlConnection(Data.ConnectionString))
                {
                    string query = @"
                SELECT B.BillingID, C.FirstName AS ClientFirstName, C.LastName AS ClientLastName, 
                       A.FirstName AS AttorneyFirstName, A.LastName AS AttorneyLastName, 
                       CA.Category AS CategoryName, R.Rate AS BillingRate, B.Date, B.Hours 
                FROM Billing B 
                INNER JOIN Clients C ON B.ClientID = C.ClientID 
                INNER JOIN Attorneys A ON B.AttorneyID = A.AttorneyID 
                INNER JOIN Categories CA ON B.CategoryID = CA.CategoryID 
                INNER JOIN Rates R ON B.RateID = R.RateID 
                WHERE B.Date >= @startDate AND B.Date < @endDate";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@startDate", staDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    grdinfo.ItemsSource = dt.DefaultView; 
                }
            }
            else
            {
                MessageBox.Show("Please select start and end date both.");
            }


        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int bid = int.Parse(txtBillID.Text);
            using (SqlConnection conn = new SqlConnection(Data.ConnectionString))
            {
                string query = "SELECT B.BillingID, C.FirstName AS ClientFirstName, C.LastName AS ClientLastName," +
                       "A.FirstName AS AttorneyFirstName, A.LastName AS AttorneyLastName, " +
                       "CA.Category AS CategoryName, R.Rate AS BillingRate, B.Date, B.Hours " +
                       "FROM Billing B " +
                       "INNER JOIN Clients C ON B.ClientID = C.ClientID " +
                       "INNER JOIN Attorneys A ON B.AttorneyID = A.AttorneyID " +
                       "INNER JOIN Categories CA ON B.CategoryID = CA.CategoryID " +
                       "INNER JOIN Rates R ON B.RateID = R.RateID WHERE B.BillingID=@bid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bid", bid);


                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtClients1.Text = $"{reader["ClientFirstName"]} {reader["ClientLastName"]}";
                        txtAttorney.Text = $"{reader["AttorneyFirstName"]} {reader["AttorneyLastName"]}";
                        txtCategory.Text = reader["CategoryName"].ToString();
                        txtFee.Text = $"{decimal.Parse(reader["Hours"].ToString()) * decimal.Parse(reader["BillingRate"].ToString()):C}"; // Format as currency
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        private void btnClearFields_Click(object sender, RoutedEventArgs e)
        {
            txtAttorney.Text = txtBillID.Text = txtCategory.Text = txtClientbyname.Text = txtClients1.Text = txtFee.Text="";
            grdinfo.ItemsSource = "";
            cmbAttorneys.SelectedIndex = -1;
            EndDate.SelectedDate = null; 
            startDate.SelectedDate = null;


                }
    }
}
