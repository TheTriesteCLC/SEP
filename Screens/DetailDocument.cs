using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CurrUser;
using SEP.DBManagement;
using SEP.Interfaces;
using SEP.Observers;
using System.Collections;

namespace SEP.Screens
{
    public partial class DetailDocument : Form
    {
        private IMongoDatabase database;
        private string collectionName;
        private Dictionary<string, string> documentData;
        private Dictionary<string, string> originalData;
        private bool isEditable;
        public DocumentDetailManager documentDetailManager;

        public DetailDocument(string collectionName, Dictionary<string, string> data, bool editable)
        {
            InitializeComponent();
            documentData = data;
            originalData = data;
            isEditable = editable;
            documentDetailManager = new DocumentDetailManager();

            LoadDocumentData();
            if (isEditable)
            {
                // Cho phép chỉnh sửa nếu cần
                dataGridView1.ReadOnly = false;
                button1.Visible = true;
                button2.Visible = true;
            }
            else
            {
                // Không cho phép chỉnh sửa
                dataGridView1.ReadOnly = true;
                button1.Visible = false;
                button2.Visible = false;
            }
            ConfigureDataGridView();

            database = CurrUserInfo.getUserDB();
            this.collectionName = collectionName;
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dataGridView1.Columns.Count >= 2)
            {
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = !isEditable;
            dataGridView1.RowHeadersVisible = false; 

            DataGridViewCellStyle valueCellStyle = new DataGridViewCellStyle();
            valueCellStyle.WrapMode = DataGridViewTriState.True;
            valueCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].DefaultCellStyle = valueCellStyle;
            
        }


        private void LoadDocumentData()
        {
            // Xóa dữ liệu cũ (nếu có)
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Thêm cột
            dataGridView1.Columns.Add("FieldName", "Field");
            dataGridView1.Columns.Add("FieldValue", "Value");

            // Thêm dữ liệu
            foreach (var pair in documentData)
            {
                dataGridView1.Rows.Add(pair.Key, pair.Value);
            }

            // Thiết lập chế độ chỉnh sửa
            dataGridView1.ReadOnly = !isEditable;
            dataGridView1.AllowUserToAddRows = false; // Không cho phép thêm hàng
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (isEditable)
            {
                // Cập nhật documentData từ DataGridView
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var fieldName = dataGridView1.Rows[i].Cells[0].Value?.ToString();
                    var fieldValue = dataGridView1.Rows[i].Cells[1].Value?.ToString();

                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        documentData[fieldName] = fieldValue;
                    }
                }

                MessageBox.Show("Data has been updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // get updated data
                var newDoc = documentData.ToBsonDocument();
                newDoc.RemoveElement(newDoc.GetElement("_id"));

                var updateDefinition = new List<UpdateDefinition<BsonDocument>>();
                foreach (var dataField in newDoc)
                {
                    updateDefinition.Add(Builders<BsonDocument>.Update.Set(dataField.Name, dataField.Value));
                }
                var combinedUpdate = Builders<BsonDocument>.Update.Combine(updateDefinition);

                // add filter
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(originalData["_id"]));
                // and update
                var collection = database.GetCollection<BsonDocument>(collectionName);
                var result = collection.UpdateOne(filter, combinedUpdate);

                // Notify that the data has been updated
                documentDetailManager.NotifyObservers();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isEditable)
            {
                documentData = new Dictionary<string, string>(originalData);

                // Loop through all columns of the selected row
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var fieldName = dataGridView1.Rows[i].Cells[0].Value?.ToString();
                    dataGridView1.Rows[i].Cells[1].Value = documentData[fieldName];
                }

                dataGridView1.Update();
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
