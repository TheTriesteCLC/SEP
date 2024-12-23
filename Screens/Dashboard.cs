using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CustomClassBuilder;
using SEP.Screens;
using SEP.CurrUser;

namespace SEP
{
    public partial class Dashboard : Form
    {
        private IMongoDatabase database;
        private string collectionName;
        private FilterDefinition<BsonDocument> chosenDocument;

        public Dashboard(string collectionName)
        {
            InitializeComponent();
            database = CurrUserInfo.getUserDB();
            this.collectionName = collectionName;
            LoadCollectionData();
        }
        private void LoadCollectionData()
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = collection.Find(FilterDefinition<BsonDocument>.Empty).ToList();

            DataTable dataTable = new DataTable();

            // Dynamically add columns based on all document fields
            foreach (var doc in documents)
            {
                foreach (var element in doc.Elements)
                {
                    if (!dataTable.Columns.Contains(element.Name))
                    {
                        dataTable.Columns.Add(element.Name);
                    }
                }
            }

            // Add rows with "BLANK" for non-existing fields
            foreach (var doc in documents)
            {
                var row = new object[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    row[i] = doc.Contains(columnName) ? doc[columnName].ToString() : "#BLANK";
                }
                dataTable.Rows.Add(row);
            }

            dataGridView1.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddNewDocument addNewDocument = new AddNewDocument(collectionName);
            addNewDocument.ShowDialog();
            //this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            deleteBtn.Enabled = true;
            var documentId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            this.chosenDocument = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(documentId));
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                var result = this.database.GetCollection<BsonDocument>(collectionName).DeleteOne(this.chosenDocument);
                if (result.DeletedCount > 0)
                {
                    MessageBox.Show("Document deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No document found with the specified ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
