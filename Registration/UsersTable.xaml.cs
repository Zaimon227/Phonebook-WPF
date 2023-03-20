using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Registration
{
    /// <summary>
    /// Interaction logic for UsersTable.xaml
    /// </summary>
    public partial class UsersTable : Page
    {
        int PageNumber = 1;
        int RowSize = 10;
        string SearchText = string.Empty;

        public UsersTable()
        {
            InitializeComponent();
        }

        private void UsersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            usersgridbind();
        }
        
        public void usersgridbind()
        {

            DatabaseControl sql = new DatabaseControl();
            PageNumber = 1;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetUsers @Page, @Size, @Keyword");
            UsersDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        // Method for checking if previous and next page is empty.. also enables and disables button
        public void NextPreviousPageChecker()
        {
            DatabaseControl sql = new DatabaseControl();
            int CheckNextPage = PageNumber + 1;
            sql.Param("@Page", CheckNextPage);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetUsers @Page, @Size, @Keyword");
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
            sql.query("usp_GetUsers @Page, @Size, @Keyword");
            if (0 == sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = false;
            }
            else if (0 < sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = true;
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl sql = new DatabaseControl();
            PageNumber++;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetUsers @Page, @Size, @Keyword");
            UsersDataGrid.ItemsSource = sql.dt.DefaultView;

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
            sql.query("usp_GetUsers @Page, @Size, @Keyword");
            UsersDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = tbxSearch.Text;
            usersgridbind();
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = string.Empty;
            tbxSearch.Text = "";
            usersgridbind();
        }
    }
}
