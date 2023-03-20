using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using RestSharp;

namespace Registration
{
    public partial class PhonebookTable : Page
    {
        int PageNumber = 1;
        int RowSize = 10;
        string SearchText = string.Empty;

        public PhonebookTable()
        {
            InitializeComponent();
        }

        private void PhonebookDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            phonebookgridbind();
        }

        public void phonebookgridbind()
        {
            // save connection string in properties.settings and fetch dynamically

            DatabaseControl sql = new DatabaseControl();
            PageNumber = 1;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetContacts @Page, @Size, @Keyword");
            PhonebookDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();

            //string sqlConnectionString = "Data Source=LAPTOP-S6G2PFTV\\\\MSSQLSERVER2022;Initial Catalog=phonebookCRUD;Integrated Security=True; Pooling=False"; //connection string
            //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-S6G2PFTV\\MSSQLSERVER2022;Initial Catalog=phonebookCRUD;Integrated Security=True; Pooling=False; Encrypt=False");
            //SqlCommand command = new SqlCommand("exec dbo.spGetContacts", conn);
            //command.Connection.Open();
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //int recordcount = da.Fill(dt);
            //PhonebookDataGrid.ItemsSource = dt.DefaultView;
            //command.Connection.Close();

            //Server server = new Server(new ServerConnection(conn));
            //server.ConnectionContext.ExecuteNonQuery(script);
        }

        public void NextPreviousPageChecker()
        {
            DatabaseControl sql = new DatabaseControl();
            int CheckNextPage = PageNumber + 1;
            sql.Param("@Page", CheckNextPage);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetContacts @Page, @Size, @Keyword");
            if (0 == sql.dt.Rows.Count)
            {
                NextPageButton.IsEnabled = false;
            }
            else if (0 < sql.dt.Rows.Count)
            {
                NextPageButton.IsEnabled = true;
            }
            int CheckPreviousPage = PageNumber - 1;
            sql.Param("@Page", CheckPreviousPage);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetContacts @Page, @Size, @Keyword");
            if (0 == sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = false;
            }
            else if (0 < sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = true;
            }
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            AddContactForm addcontactform = new AddContactForm();
            addcontactform.btnAdd.Visibility = Visibility.Visible;
            addcontactform.btnUpdate.Visibility = Visibility.Hidden;
            addcontactform.tbxID.Visibility = Visibility.Hidden;
            addcontactform.ShowDialog();
            if (addcontactform.DialogResult == true)
            {
                phonebookgridbind();
            }
        }

        private void EditContactButton_Click(object sender, RoutedEventArgs e)
        {
            AddContactForm addcontactform = new AddContactForm();

            addcontactform.tbxFormHeader.Text = "Edit Contact";

            addcontactform.btnAdd.Visibility = Visibility.Hidden;
            addcontactform.btnUpdate.Visibility = Visibility.Visible;
            addcontactform.tbxID.Visibility = Visibility.Hidden;

            DataRowView dataRowView = PhonebookDataGrid.SelectedItem as DataRowView;
            addcontactform.tbxID.Text = dataRowView["id"].ToString();
            addcontactform.tbxName.Text = dataRowView["name"].ToString();
            addcontactform.tbxAddress.Text = dataRowView["address"].ToString();
            addcontactform.tbxEmail.Text = dataRowView["email"].ToString();
            addcontactform.tbxContact.Text = dataRowView["contact"].ToString();

            addcontactform.ShowDialog();
            if (addcontactform.DialogResult == true)
            {
                phonebookgridbind();
            }
        }

        private void DeleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want delete this record?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseControl sql = new DatabaseControl();

                DataRowView dataRowView = PhonebookDataGrid.SelectedItem as DataRowView;
                sql.Param("@ID", int.Parse(dataRowView["id"].ToString()));

                sql.query("usp_DeleteContact @ID");
                if (sql.CheckForError(true))
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Successfully Deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    phonebookgridbind();
                }
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            //PreviousPageButton.IsEnabled = true;
            DatabaseControl sql = new DatabaseControl();
            PageNumber++;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetContacts @Page, @Size, @Keyword");
            PhonebookDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl sql = new DatabaseControl();
            PageNumber--;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetContacts @Page, @Size, @Keyword");
            PhonebookDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = tbxSearch.Text;
            phonebookgridbind();
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = string.Empty;
            tbxSearch.Text = "";
            phonebookgridbind();
        }
    }
}
