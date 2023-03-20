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
using System.Windows.Shapes;

namespace Registration
{
    /// <summary>
    /// Interaction logic for AddContactForm.xaml
    /// </summary>
    public partial class AddContactForm : Window
    {
        public AddContactForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl sql = new DatabaseControl();

            sql.Param("@Name", tbxName.Text);
            sql.Param("@Address", tbxAddress.Text);
            sql.Param("@Email", tbxEmail.Text);
            sql.Param("@Contact", tbxContact.Text);
            sql.query("usp_InsertContact @Name, @Address, @Email, @Contact");
            if (sql.CheckForError(true))
            {
                return;
            }
            else
            {
                MessageBox.Show("Successfully Added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                //Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl sql = new DatabaseControl();

            sql.Param("@ID", tbxID.Text);
            sql.Param("@Name", tbxName.Text);
            sql.Param("@Address", tbxAddress.Text);
            sql.Param("@Email", tbxEmail.Text);
            sql.Param("@Contact", tbxContact.Text/*.ToString()*/);
            sql.query("dbo.spUpdateContact @ID, @Name, @Address, @Email, @Contact");
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
