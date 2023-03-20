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
using System.Windows.Shapes;

namespace Registration
{
    /// <summary>
    /// Interaction logic for AddReligionForm.xaml
    /// </summary>
    public partial class AddReligionForm : Window
    {
        public AddReligionForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl sql = new DatabaseControl();

            sql.Param("@Name", tbxName.Text);
            sql.Param("@Description", tbxDescription.Text);
            sql.Param("@Created_by", "admin");
            sql.query("usp_InsertReligion @Name, @Description, @Created_by");
            if (sql.CheckForError(true))
            {
                return;
            }
            else
            {
                MessageBox.Show("Successfully Added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainwindow = new MainWindow();
                //ReligionTable religiontable = new ReligionTable();
                //mainwindow.DisplayFrame.Navigate(new System.Uri("ReligionTable.xaml", UriKind.RelativeOrAbsolute));
                //religiontable.religiongridbind();
                //var newReligionTable = new ReligionTable();
                //mainwindow.DisplayFrame.NavigationService.Navigate(newReligionTable);
                this.DialogResult = true;
                //Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl sql = new DatabaseControl();

            sql.Param("@ReligionID", tbxID.Text);
            sql.Param("@Name", tbxName.Text);
            sql.Param("@Description", tbxDescription.Text);
            sql.Param("@Updated_by", "admin");
            sql.query("usp_UpdateReligion @ReligionID, @Name, @Description, @Updated_by");
            if (sql.CheckForError(true))
            {
                return;
            }
            else
            {
                MessageBox.Show("Successfully Updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                //Close();
            }
        }
    }
}
