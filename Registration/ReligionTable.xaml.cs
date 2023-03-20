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
    public partial class ReligionTable : Page
    {
        int PageNumber = 1;
        int RowSize = 10;
        string SearchText = string.Empty;

        public ReligionTable()
        {
            InitializeComponent();
        }
        private void ReligionDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            religiongridbind();
        }

        public void religiongridbind()
        {
            DatabaseControl sql = new DatabaseControl();
            PageNumber = 1;
            tblkPageNumber.Text = "Page " + PageNumber;
            sql.Param("@Page", PageNumber);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetReligions @Page, @Size, @Keyword");
            ReligionDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        public void NextPreviousPageChecker()
        {
            DatabaseControl sql = new DatabaseControl();
            int CheckNextPage = PageNumber + 1;
            sql.Param("@Page", CheckNextPage);
            sql.Param("@Size", RowSize);
            sql.Param("@Keyword", SearchText);
            sql.query("usp_GetReligions @Page, @Size, @Keyword");
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
            sql.query("usp_GetReligions @Page, @Size, @Keyword");
            if (0 == sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = false;
            }
            else if (0 < sql.dt.Rows.Count)
            {
                PreviousPageButton.IsEnabled = true;
            }
        }

        private void AddReligionButton_Click(object sender, RoutedEventArgs e)
        {
            AddReligionForm addreligionform = new AddReligionForm();
            addreligionform.btnAdd.Visibility = Visibility.Visible;
            addreligionform.btnUpdate.Visibility = Visibility.Hidden;
            addreligionform.tbxID.Visibility = Visibility.Hidden;
            addreligionform.ShowDialog();
            if (addreligionform.DialogResult == true )
            {
                religiongridbind();
            }
        }

        private void EditReligionButton_Click(object sender, RoutedEventArgs e)
        {
            AddReligionForm addreligionform = new AddReligionForm();

            addreligionform.tbxFormHeader.Text = "Edit Religion";

            addreligionform.btnAdd.Visibility = Visibility.Hidden;
            addreligionform.btnUpdate.Visibility = Visibility.Visible;
            addreligionform.tbxID.Visibility = Visibility.Hidden;

            DataRowView dataRowView = ReligionDataGrid.SelectedItem as DataRowView;
            addreligionform.tbxID.Text = dataRowView["religionid"].ToString();
            addreligionform.tbxName.Text = dataRowView["name"].ToString();
            addreligionform.tbxDescription.Text = dataRowView["description"].ToString();

            addreligionform.ShowDialog();
            if (addreligionform.DialogResult == true)
            {
                religiongridbind(); 
            }
        }
        
        private void DeleteReligionButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure you want delete this record?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DatabaseControl sql = new DatabaseControl();

                DataRowView dataRowView = ReligionDataGrid.SelectedItem as DataRowView;
                sql.Param("@ReligionID", int.Parse(dataRowView["religionid"].ToString()));

                sql.query("usp_DeleteReligion @ReligionID");
                if (sql.CheckForError(true))
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Successfully Deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    religiongridbind();
                }
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
            sql.query("usp_GetReligions @Page, @Size, @Keyword");
            ReligionDataGrid.ItemsSource = sql.dt.DefaultView;

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
            sql.query("usp_GetReligions @Page, @Size, @Keyword");
            ReligionDataGrid.ItemsSource = sql.dt.DefaultView;

            NextPreviousPageChecker();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = tbxSearch.Text;
            religiongridbind();
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText = string.Empty;
            tbxSearch.Text = "";
            religiongridbind();
        }

    }
}
