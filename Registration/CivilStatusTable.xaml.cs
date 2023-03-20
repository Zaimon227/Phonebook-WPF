using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class CivilStatusTable : Page
    {
        int PageNumber = 1;
        int RowSize = 10;
        string SearchText = string.Empty;

        public CivilStatusTable()
        {
            InitializeComponent();
        }
        private void civilstatusDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            civilstatusgridbind();
        }

        public void civilstatusgridbind()
        {
            DatabaseControl sql = new DatabaseControl();
            PageNumber = 1;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetCivilStatus @Page, @Size, @Keyword");
            CivilStatusDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        public void NextPreviousPageChecker()
        {
            DatabaseControl sql = new DatabaseControl();
            int CheckNextPage = PageNumber + 1;
            sql.Param("@Page", CheckNextPage);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetCivilStatus @Page, @Size, @Keyword");
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
            sql.query("usp_GetCivilStatus @Page, @Size, @Keyword");
            if (0 == sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = false;
            }
            else if (0 < sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = true;
            }
        }

        private void AddCivilStatusButton_Click(object sender, RoutedEventArgs e)
        {
            AddCivilStatusForm addcivilstatusform = new AddCivilStatusForm();
            addcivilstatusform.btnAdd.Visibility = Visibility.Visible;
            addcivilstatusform.btnUpdate.Visibility = Visibility.Hidden;
            addcivilstatusform.tbxID.Visibility = Visibility.Hidden;
            addcivilstatusform.ShowDialog();
            if (addcivilstatusform.DialogResult == true)
            {
                civilstatusgridbind();
            }
        }
        
        private void EditCivilStatusButton_Click(object sender, RoutedEventArgs e)
        {
            AddCivilStatusForm addcivilstatusform = new AddCivilStatusForm();

            addcivilstatusform.tbxFormHeader.Text = "Edit CivilStatus";

            addcivilstatusform.btnAdd.Visibility = Visibility.Hidden;
            addcivilstatusform.btnUpdate.Visibility = Visibility.Visible;
            addcivilstatusform.tbxID.Visibility = Visibility.Hidden;

            DataRowView dataRowView = CivilStatusDataGrid.SelectedItem as DataRowView;
            addcivilstatusform.tbxID.Text = dataRowView["civilstatusid"].ToString();
            addcivilstatusform.tbxName.Text = dataRowView["name"].ToString();
            addcivilstatusform.tbxDescription.Text = dataRowView["description"].ToString();

            addcivilstatusform.ShowDialog();
            if (addcivilstatusform.DialogResult == true)
            {
                civilstatusgridbind();
            }
        }

        private void DeleteCivilStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want delete this record?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseControl sql = new DatabaseControl();

                DataRowView dataRowView = CivilStatusDataGrid.SelectedItem as DataRowView;
                sql.Param("@CivilStatusID", int.Parse(dataRowView["civilstatusid"].ToString()));

                sql.query("usp_DeleteCivilStatus @CivilStatusID");
                if (sql.CheckForError(true))
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Successfully Deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    civilstatusgridbind();
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
            sql.query("usp_GetCivilStatus @Page, @Size, @Keyword");
            CivilStatusDataGrid.ItemsSource = sql.dt.DefaultView;

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
            sql.query("usp_GetCivilStatus @Page, @Size, @Keyword");
            CivilStatusDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = tbxSearch.Text;
            civilstatusgridbind();
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = string.Empty;
            tbxSearch.Text = "";
            civilstatusgridbind();
        }

    }
}
