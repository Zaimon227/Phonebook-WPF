using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Window
    {
        public string[] gender { get; set; }

        public SignUpPage()
        {
            InitializeComponent();

            gender = new string[] { "Male", "Female" };
            DataContext = this;

            DatabaseControl sql = new DatabaseControl();
            sql.query("usp_GetReligions");
            cbxReligion.ItemsSource = sql.dt.DefaultView;
            cbxReligion.DisplayMemberPath = "religionname";
            cbxReligion.SelectedValuePath = "religionid";
            cbxReligion.SelectedIndex = 0;

            sql.query("usp_GetNationalities");
            cbxNationality.ItemsSource = sql.dt.DefaultView;
            cbxNationality.DisplayMemberPath = "nationalityname";
            cbxNationality.SelectedValuePath = "nationalityid";
            cbxNationality.SelectedIndex = 0;

            sql.query("usp_GetCivilStatus");
            cbxCivilStatus.ItemsSource = sql.dt.DefaultView;
            cbxCivilStatus.DisplayMemberPath = "civilstatusname";
            cbxCivilStatus.SelectedValuePath = "civilstatusid";
            cbxCivilStatus.SelectedIndex = 0;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = dpBirthdate.SelectedDate;
            if (selectedDate.HasValue)
            {
                string formattedDate = selectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (tbxFirstName.Text == "" || tbxMiddleName.Text == "" || tbxLastName.Text == "" || cbxGender.Text == "" || tbxEmail.Text == "" || pbxConfirmPassword.Password.ToString() == "" )
            {
                MessageBox.Show("Answer all input fields", "Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (pbxPassword.Password.ToString() ==  pbxConfirmPassword.Password.ToString())
                    {
                        DatabaseControl sql = new DatabaseControl();

                        sql.Param("@Firstname", tbxFirstName.Text);
                        sql.Param("@Middlename", tbxMiddleName.Text);
                        sql.Param("@Lastname", tbxLastName.Text);
                        sql.Param("@Gender", cbxGender.Text);
                        sql.Param("@Birthdate", dpBirthdate.SelectedDate);
                        sql.Param("@ReligionID", cbxReligion.SelectedValue);
                        sql.Param("@NationalityID", cbxNationality.SelectedValue);
                        sql.Param("@CivilStatusID", cbxCivilStatus.SelectedValue);
                        sql.Param("@Email", tbxEmail.Text);
                        sql.Param("@Password", pbxPassword.Password.ToString());
                        sql.Param("@Created_by", "admin");
                        sql.query("usp_SignUp @Firstname, @Middlename, @Lastname, @Gender, @Birthdate, @ReligionID, @NationalityID, @CivilStatusID, @Email, @Password, @Created_by");
                        if (sql.CheckForError(true))
                        {
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Successfully Signed Up!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Confirm Password does not match", "Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
