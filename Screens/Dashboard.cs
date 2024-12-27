using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using SEP.Screens;
using SEP.CurrUser;
using SEP.Interfaces;

namespace SEP
{
    public partial class Dashboard : Form, IDocumentObserver
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

            // Add rows with "#BLANK" for non-existing fields
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Dictionary<string, string> documentData = new Dictionary<string, string>();

                var selectedDocumentId = selectedRow.Cells[0].Value?.ToString() ?? "";

                DetailDocument viewForm = new DetailDocument(this.collectionName, selectedDocumentId, false);
                viewForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a document to view.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                var selectedDocumentId = selectedRow.Cells[0].Value?.ToString() ?? "";

                DetailDocument updateForm = new DetailDocument(this.collectionName, selectedDocumentId, true);
                
                updateForm.registerObserver(this);
                updateForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a document to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("Notified from subscription");
            LoadCollectionData();
        }
    }
}
