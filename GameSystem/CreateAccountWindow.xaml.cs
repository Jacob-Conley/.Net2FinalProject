using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameSystem
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {

        private const int MIN_PASSWORD_LENGTH = 6; // business rule
        private const int MIN_USERNAME_LENGTH = 8; // forced by naming rules
        private const int MAX_USERNAME_LENGTH = 16; // forced by db field length
        private UserManager _userManager = null;

        public CreateAccountWindow(UserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {

            var username = txtUsername.Text;

            if (username.Length < MIN_USERNAME_LENGTH ||
                 username.Length > MAX_USERNAME_LENGTH)
            {
                MessageBox.Show("Username must be between 8 and 16 characters. \n Please try again.");
                clearUserBox();
                return;
            }

            var password = pswPassword.Password;
            if (password != pswConfirmPassword.Password)
            {
                MessageBox.Show("Your password and retyped password doesn't match. Try again");
                clearPasswordBoxes();
                return;
            }

            var rgx = new Regex(@"[-\w]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}");
            var email = txtEmail.Text;
            if (!rgx.IsMatch(email))
            {
                MessageBox.Show("Invalid Email. Please try again");
                clearEmail();
                return;
            }

            if (password.Length < MIN_PASSWORD_LENGTH)
            {
                MessageBox.Show("Password must be at least 6 characters.");
                clearPasswordBoxes();

                return;
            }

            try
            {
                var accountResults = _userManager.CreateAccount(username, email, password );

                if (accountResults == 1)
                {
                    MessageBox.Show("Account created.");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Account was not created.");

                    clearEmail();
                    clearPasswordBoxes();
                    clearPasswordBoxes();
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }

                MessageBox.Show(message, "Account Creation failed",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                clearEmail();
                clearPasswordBoxes();
                clearPasswordBoxes();
            }

        }

        private void clearEmail()
        {
            txtEmail.Text = "";
            txtEmail.Focus();
        }

        private void clearUserBox()
        {
            txtUsername.Text = "";
            txtUsername.Focus();
        }

        private void clearPasswordBoxes()
        {
            pswPassword.Password = "";
            pswConfirmPassword.Password = "";

            pswPassword.Focus();

        }
    }
}
