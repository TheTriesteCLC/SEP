using MongoDB.Driver;
using SEP.DBManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEP
{
    public partial class AddNewTable : Form
    {
        public IMongoDatabase database { get; set; }
        public AddNewTable(IMongoDatabase db)
        {
            InitializeComponent();
            database = db;
        }

        private void NewTable_Load(object sender, EventArgs e)
        {

        }

        private void tableNameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableName.Text))
            {
                e.Cancel = true;
                txtTableName.Focus();
                errProviderTablename.SetError(txtTableName, "Table name is required");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                string tableName = txtTableName.Text;
                database.CreateCollection(tableName);
                System.Windows.Forms.MessageBox.Show($"Collection '{tableName}' created!");
                this.Close();
            }
        }

    }
}
