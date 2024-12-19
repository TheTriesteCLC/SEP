using SEP.CurrUser;
using SEP.DBManagement.UsersCollection;
using SEP.Ultils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SEP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void handleLogin()
        {
            if (txtUsername.Text.Trim().Length > 0
                && txtPassword.Text.Trim().Length > 0)
            {
                var loginUser = new User
                {
                    username = txtUsername.Text,
                    password = txtPassword.Text
                };

                User foundUser = UsersCollection.GetUsersCollection().loginToUser(loginUser);

                if (foundUser != null)
                {
                    CurrUserInfo.login(foundUser);
                    HandleLoginSuccess();
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
            }
        }

        private void usernameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                //e.Cancel = true;
                txtUsername.Focus();
                errProviderUsername.SetError(txtUsername, "Username required");
            }
            else
            {
                //e.Cancel = false;
                errProviderUsername.SetError(txtUsername, "");
            }
        }

        private void passwordValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                //e.Cancel = true;
                txtPassword.Focus();
                errProviderPassword.SetError(txtPassword, "Password required");
            }
            else
            {
                //e.Cancel = false;
                errProviderPassword.SetError(txtPassword, "");
            }
        }

        private void checkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void loginEnter(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                handleLogin();
            }
        }

        private void labelRegister_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.ShowDialog();
            this.Close();
        }

        private void HandleLoginSuccess()
        {
            TableList tableList = new TableList();
            this.Hide();
            tableList.ShowDialog();
            this.Close();
        }
    }
}
