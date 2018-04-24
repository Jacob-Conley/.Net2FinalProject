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
using DataObjects;
using LogicLayer;
using System.Text.RegularExpressions;

namespace GameSystem
{
    /// <summary>
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfile : Window
    {
        private User _user;
        private UserManager _userManager = new UserManager();
        private string _currentEmail;
        private string _currentPasswordHash;

        private const int MIN_PASSWORD_LENGTH = 6; // business rule

        public UpdateProfile(User _user)
        {
            this._user = _user;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _currentEmail = _user.Email;
            _currentPasswordHash = _user.PasswordHash;
            txtUsername.Text = _user.Username;
            txtEmail.Text = _user.Email;
            lblEditorTest.Visibility = Visibility.Hidden;
            txtTestResponse.Visibility = Visibility.Hidden;
            if (_user.GameEditor == true)
            {
                txtHeader.Text = "Edit Profile Information";
                lblGameEditor.Visibility = Visibility.Hidden;
                chkGameEditor.Visibility = Visibility.Hidden;
            }
        }

        private void chkGameEditor_Checked(object sender, RoutedEventArgs e)
        {
            chkGameEditor.Content = "No";
            lblEditorTest.Visibility = Visibility.Visible;
            txtTestResponse.Visibility = Visibility.Visible;
        }

        private void chkGameEditor_Unchecked(object sender, RoutedEventArgs e)
        {
            chkGameEditor.Content = "Yes";
            lblEditorTest.Visibility = Visibility.Hidden;
            txtTestResponse.Visibility = Visibility.Hidden;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this account?", "Delete Account?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                _userManager.DeleteAccount(_user.GameUserID);
                MessageBox.Show("Account deleted");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Account was not deleted");
                return;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (pswNewPassword.Password != pswConfirmPassword.Password)
            {
                MessageBox.Show("The two passwords do not match, please try again");
                pswNewPassword.Password = "";
                pswConfirmPassword.Password = "";
                pswNewPassword.Focus();
                return;
            }

            if (pswNewPassword.Password.Length <= MIN_PASSWORD_LENGTH && pswNewPassword.Password.Length != 0)
            {
                MessageBox.Show("The password is too short, please try again");
                pswNewPassword.Password = "";
                pswConfirmPassword.Password = "";
                pswNewPassword.Focus();
                return;
            }

            var rgx = new Regex(@"[-\w]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}");
            var email = txtEmail.Text;
            if (!rgx.IsMatch(email))
            {
                MessageBox.Show("Invalid Email. Please try again");
                txtEmail.Text = "";
                txtEmail.Focus();
                return;
            }

            var newEmail = txtEmail.Text;
            string newPassword;

            if (pswNewPassword.Password == "" && pswConfirmPassword.Password == "")
            {

                if (chkGameEditor.IsChecked == true)
                {
                    if (!txtTestResponse.Text.ToLower().Equals("bulbasaur"))
                    {
                        MessageBox.Show("Incorrect answer, please try again.");
                        txtTestResponse.Text = "";
                        txtTestResponse.Focus();
                        return;
                    }
                    _userManager.UpgradeAccountSamePassword(_user, _currentEmail, newEmail, _currentPasswordHash);
                    MessageBox.Show("Account Upgraded");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    _userManager.UpdateAccountSamePassword(_user, _currentEmail, newEmail, _currentPasswordHash);
                    MessageBox.Show("Account Updated");
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                newPassword = pswNewPassword.Password;

                if (chkGameEditor.IsChecked == true)
                {
                    if (!txtTestResponse.Text.ToLower().Equals("bulbasaur"))
                    {
                        MessageBox.Show("Incorrect answer, please try again.");
                        txtTestResponse.Text = "";
                        txtTestResponse.Focus();
                        return;
                    }
                    _userManager.UpgradeAccount(_user, _currentEmail, newEmail, _currentPasswordHash, newPassword);
                    MessageBox.Show("Account Upgraded");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    _userManager.UpdateAccount(_user, _currentEmail, newEmail, _currentPasswordHash, newPassword);
                    MessageBox.Show("Account Updated");
                    this.DialogResult = true;
                    this.Close();
                }
            }




        }
    }
}
