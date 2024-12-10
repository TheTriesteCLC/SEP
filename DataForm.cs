using System;
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

namespace SEP
{
    public partial class DataForm : Form
    {
        private IMongoDatabase database;
        private string collectionName;

        public DataForm(IMongoDatabase db, string collectionName)
        {
            InitializeComponent();
            database = db;
            this.collectionName = collectionName;
            LoadCollectionData();
        }
        private void LoadCollectionData()
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = collection.Find(FilterDefinition<BsonDocument>.Empty).ToList();

            DataTable dataTable = new DataTable();
            foreach (var doc in documents)
            {
                if (dataTable.Columns.Count == 0)
                {
                    foreach (var element in doc.Elements)
                    {
                        dataTable.Columns.Add(element.Name);
                    }
                }

                var row = new object[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    row[i] = doc[dataTable.Columns[i].ColumnName].ToString();
                }
                dataTable.Rows.Add(row);
            }

            dataGridView1.DataSource = dataTable;
        }
    }
}
