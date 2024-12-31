using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using SEP.DBManagement.UsersCollection;
using SEP.Ultils;
using System.Data.SqlClient;

namespace SEP
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        public void handleRegister()
        {
            if (txtUsername.Text.Trim().Length > 0
                && txtPassword.Text.Trim().Length > 0
                && txtConfirm.Text == txtPassword.Text
                && !string.IsNullOrWhiteSpace(txtConnection.Text)
                && !string.IsNullOrWhiteSpace(comboboxDatabase.Text))
            {
                var newUser = new User
                {
                    username = txtUsername.Text,
                    password = txtPassword.Text,
                    connectionString = txtConnection.Text,
                    databaseName = comboboxDatabase.Text,
                };

                var result = UsersCollection.GetUsersCollection().addNewUser(newUser);

                if (result)
                {
                    MessageBox.Show("Account has been created !!!");
                }
                else
                {
                    MessageBox.Show("Username already exists !!!");
                }
            }
        }

        private void usernameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                //e.Cancel = true;
                txtUsername.Focus();
                errProviderUsername.SetError(txtUsername, "Username should not be left blank!");
            }
            else
            {
                //e.Cancel = false;
                errProviderUsername.SetError(txtUsername, "");
            }
        }

        private void signupEnter(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                handleRegister();
            }
        }

        private void passwordValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                //e.Cancel = true;
                txtPassword.Focus();
                errProviderPassword.SetError(txtPassword, "Password should not be left blank!");
            }
            else if (txtPassword.Text.Length < 5)
            {
                //e.Cancel = true;
                txtPassword.Focus();
                errProviderPassword.SetError(txtPassword, "Password must contains at least 5 characters");
            }
            else
            {
                //e.Cancel = false;
                errProviderPassword.SetError(txtPassword, "");
            }
        }

        private void confirmValidating(object sender, CancelEventArgs e)
        {
            if (txtConfirm.Text != txtPassword.Text)
            {
                //e.Cancel = true;
                txtConfirm.Focus();
                errProviderConfirm.SetError(txtConfirm, "Password is not match");
            }
            else
            {
                //e.Cancel = false;
                errProviderConfirm.SetError(txtConfirm, "");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirm.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirm.PasswordChar = '*';
            }
        }

        private void labelLoginClick(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void connectionValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConnection.Text))
            {
                //e.Cancel = true;
                txtConnection.Focus();
                errProviderConnection.SetError(txtConnection, "Connection string should not be left blank!");
            }
            else
            {
                //e.Cancel = false;
                errProviderConnection.SetError(txtConnection, "");
            }
        }

        private void comboboxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtConnection_Leave(object sender, EventArgs e)
        {
            string connectionString = txtConnection.Text;
            if (ConnectionHelper.IsMongoDBConnectionString(connectionString))
            {
                if (ConnectionHelper.IsValidMongoDBConnection(connectionString))
                {
                    comboboxDatabase.Visible = true;

                    MongoClient mongoClient = new MongoClient(connectionString);
                    IAsyncCursor<string> cursor = mongoClient.ListDatabaseNames();
                    while (cursor.MoveNext())
                    {
                        foreach (var doc in cursor.Current)
                        {
                            comboboxDatabase.Items.Add(doc);
                        }
                    }
                    comboboxDatabase.SelectedIndex = 0;
                }
                else
                {
                    comboboxDatabase.Visible = false;
                    comboboxDatabase.Items.Clear();
                }
            }
            else if (ConnectionHelper.IsSQLServerConnectionString(connectionString))
            {
                var databaseNames = new List<string>();
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    // SQL query to get all database names
                    string query = Constants.sqlQuery["getDatabaseNames"];
                    using (var command = new SqlCommand(query, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                databaseNames.Add(reader["name"].ToString());
                            }
                            foreach (var db in databaseNames)
                            {
                                comboboxDatabase.Items.Add(db);
                            }
                        }
                    }
                    comboboxDatabase.Visible = true;
                }
            }
        }
    }
}
