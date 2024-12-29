using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using SEP.ClientDatabase;
using SEP.CurrUser;
using SEP.Interfaces;

namespace SEP
{
    public partial class TableList : Form, IDocumentObserver
    {
        private IDatabase database;

        //public TableList()
        //{
        //    InitializeComponent();
        //    database = CurrUserInfo.getUserDB();
        //    LoadCollections();
        //}
        public TableList()
        {
            InitializeComponent();
            this.database = new ClientSqlServer($"data source=localhost;initial catalog=SEP;user id=sa;password=sqlserver;", "SEP");
            LoadCollections();
        }
        private async void LoadCollections()
        {
            List<dbCollection> collections = await database.GetAllCollections();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Table Name");

            foreach (var collection in collections)
            {
                dataTable.Rows.Add(collection.collectionName);
            }

            dataGridView1.DataSource = dataTable;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dataGridView1.RowTemplate.Height = 30;
        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string tableName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                Dashboard dataForm = new Dashboard(tableName);
                dataForm.ShowDialog(); // Hiển thị form mới
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(this.database is ClientSQL)
            {
                AddNewTable newTable = new AddNewTable(true);
                newTable.registerObserver(this);
                newTable.ShowDialog();
            }else
            {
                AddNewTable newTable = new AddNewTable(false);
                newTable.registerObserver(this);
                newTable.ShowDialog();
            }
        }
        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("Notified from subscription");
            LoadCollections();
        }
    }
}
