using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using SEP.Screens;
using SEP.CurrUser;
using SEP.Interfaces;
using SEP.ClientDatabase;

namespace SEP
{
    public partial class Dashboard : Form, IDocumentObserver
    {
        private IDatabase database;
        private string collectionName;
        private string chosenDocumentId;

        //public Dashboard(string collectionName)
        //{
        //    InitializeComponent();
        //    database = CurrUserInfo.getUserDB();

        //    this.collectionName = collectionName;
        //    LoadCollectionData();

        //}
        public Dashboard(string collectionName)
        {
            InitializeComponent();
            this.database = new ClientSqlServer($"data source=localhost;initial catalog=SEP;user id=sa;password=sqlserver;", "SEP");
            this.collectionName = collectionName;
            LoadCollectionData();

        }
        private async void LoadCollectionData()
        {
            List<dbDocument> documents = await this.database.GetCollection(collectionName);

            DataTable dataTable = new DataTable();

            // Dynamically add columns based on all document fields
            foreach (var doc in documents)
            {
                foreach (var field in doc.fields)
                {
                    if (!dataTable.Columns.Contains(field.fieldName))
                    {
                        dataTable.Columns.Add(field.fieldName);
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
                    dbDocumentField foundField = doc.getFieldByName(columnName);
                    if (foundField != null)
                    {
                        row[i] = foundField.value.ToString();
                    }
                    else
                    {
                        row[i] = "#BLANK";
                    }
                }
                dataTable.Rows.Add(row);
            }
            dataGridView1.DataSource = dataTable;
        }
        private async Task<dbResponse> handleDeleteDocument(string id)
        {
            return await this.database.DeleteDocumentByID(collectionName, id);
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

            addNewDocument.registerObserver(this);
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
            this.chosenDocumentId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
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


        private async void deleteBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                dbResponse result = await this.database.DeleteDocumentByID(collectionName, this.chosenDocumentId);
                MessageBox.Show(result.message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCollectionData();
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