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

namespace Registration
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender == menuUsers)
            {
                DisplayFrame.Navigate(new System.Uri("UsersTable.xaml", UriKind.RelativeOrAbsolute));
            }
            else if (sender == menuPhonebook)
            {
                DisplayFrame.Navigate(new System.Uri("PhonebookTable.xaml", UriKind.RelativeOrAbsolute));
            }
            else if (sender == menuReligion)
            {
                DisplayFrame.Navigate(new System.Uri("ReligionTable.xaml", UriKind.RelativeOrAbsolute));
            }
            else if (sender == menuNationality)
            {
                DisplayFrame.Navigate(new System.Uri("NationalityTable.xaml", UriKind.RelativeOrAbsolute));
            }
            else if (sender == menuCivilStatus)
            {
                DisplayFrame.Navigate(new System.Uri("CivilStatusTable.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            LoginPage loginpage = new LoginPage();
            loginpage.ShowDialog();
            
        }
    }
}
