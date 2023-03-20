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
    public partial class NationalityTable : Page
    {
        int PageNumber = 1;
        int RowSize = 10;
        string SearchText = string.Empty;

        public NationalityTable()
        {
            InitializeComponent();
        }
        private void NationalityDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            nationalitygridbind();
        }

        public void nationalitygridbind()
        {
            DatabaseControl sql = new DatabaseControl();
            PageNumber = 1;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetNationalities @Page, @Size, @Keyword");
            NationalityDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();

        }

        public void NextPreviousPageChecker()
        {
            DatabaseControl sql = new DatabaseControl();
            int CheckNextPage = PageNumber + 1;
            sql.Param("@Page", CheckNextPage);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetNationalities @Page, @Size, @Keyword");
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
            sql.query("usp_GetNationalities @Page, @Size, @Keyword");
            if (0 == sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = false;
            }
            else if (0 < sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = true;
            }
        }

        private void AddNationalityButton_Click(object sender, RoutedEventArgs e)
        {
            AddNationalityForm addnationalityform = new AddNationalityForm();
            addnationalityform.btnAdd.Visibility = Visibility.Visible;
            addnationalityform.btnUpdate.Visibility = Visibility.Hidden;
            addnationalityform.tbxID.Visibility = Visibility.Hidden;
            addnationalityform.ShowDialog();
            if (addnationalityform.DialogResult == true)
            {
                nationalitygridbind();
            }
        }
        
        private void EditNationalityButton_Click(object sender, RoutedEventArgs e)
        {
            AddNationalityForm addnationalityform = new AddNationalityForm();

            addnationalityform.tbxFormHeader.Text = "Edit Nationality";

            addnationalityform.btnAdd.Visibility = Visibility.Hidden;
            addnationalityform.btnUpdate.Visibility = Visibility.Visible;
            addnationalityform.tbxID.Visibility = Visibility.Hidden;

            DataRowView dataRowView = NationalityDataGrid.SelectedItem as DataRowView;
            addnationalityform.tbxID.Text = dataRowView["nationalityid"].ToString();
            addnationalityform.tbxName.Text = dataRowView["name"].ToString();
            addnationalityform.tbxDescription.Text = dataRowView["description"].ToString();

            addnationalityform.ShowDialog();
            if (addnationalityform.DialogResult == true)
            {
                nationalitygridbind();
            }
        }

        private void DeleteNationalityButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want delete this record?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseControl sql = new DatabaseControl();

                DataRowView dataRowView = NationalityDataGrid.SelectedItem as DataRowView;
                sql.Param("@NationalityID", int.Parse(dataRowView["nationalityid"].ToString()));

                sql.query("usp_DeleteNationality @NationalityID");
                if (sql.CheckForError(true))
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Successfully Deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    nationalitygridbind();
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
            sql.Param("Keyword", SearchText);
            sql.query("usp_GetNationalities @Page, @Size, @Keyword");
            NationalityDataGrid.ItemsSource = sql.dt.DefaultView;

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
            sql.query("usp_GetNationalities @Page, @Size, @Keyword");
            NationalityDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = tbxSearch.Text;
            nationalitygridbind();
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = string.Empty;
            tbxSearch.Text = "";
            nationalitygridbind();
        }

    }
}
