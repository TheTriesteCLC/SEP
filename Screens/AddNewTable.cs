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
using SEP.CurrUser;
using SEP.Observers;
using SEP.Interfaces;

namespace SEP
{
    public partial class AddNewTable : Form
    {
        private IMongoDatabase database;
        private DocumentDetailManager addNewTableManager;
        public AddNewTable()
        {
            InitializeComponent();
            database = CurrUserInfo.getUserDB();
            this.addNewTableManager = new DocumentDetailManager();
        }
        public void registerObserver(IDocumentObserver documentObserver)
        {
            this.addNewTableManager.RegisterObserver(documentObserver);
        }
        public void unregisterObserver(IDocumentObserver documentObserver)
        {
            this.addNewTableManager.UnregisterObserver(documentObserver);
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
                this.addNewTableManager.NotifyObservers();
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
