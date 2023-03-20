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
using System.Windows.Shapes;

namespace Registration
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpPage signuppage = new SignUpPage();
            signuppage.ShowDialog();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseControl sql = new DatabaseControl();

            sql.Param("@Email", tbxEmail.Text);
            sql.Param("@Password", pbxPassword.Password.ToString());
            sql.query("usp_Login @Email, @Password");
            if (0 < sql.dt.Rows.Count)
            {
                MessageBox.Show("Successfull Login", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainwindow = new MainWindow();
                Application.Current.Windows[0].Close();
                mainwindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Email or Password", "Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
