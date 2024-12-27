﻿using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CurrUser;
using SEP.CustomClassBuilder;
using SEP.DBManagement;
using SEP.Interfaces;
using SEP.Observers;
using SEP.Ultils;
using System.Collections;
using System.Data;
using System.Xml.Linq;

namespace SEP.Screens
{
    public partial class DetailDocument : Form
    {

        private string collectionName;
        private string documentId;
        private bool editable;

        IMongoDatabase database;
        private DocumentDetailManager documentDetailManager;
        private List<(string PropertyName, Type PropertyType)> fields;

        public DetailDocument(string collectionName, string documentId, bool editable)
        {
            InitializeComponent();
            this.collectionName = collectionName;
            this.documentId = documentId;
            this.editable = editable;

            this.database = CurrUserInfo.getUserDB();
            this.documentDetailManager = new DocumentDetailManager();
            this.fields = new List<(string PropertyName, Type PropertyType)>();

            LoadDocumentData();
            if (editable)
            {
                // Cho phép chỉnh sửa
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
        }
        private void ConfigureDataGridView()
        {
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dataGridView1.Columns.Count >= 3)
            {
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = !editable;
            dataGridView1.RowHeadersVisible = false;

            DataGridViewCellStyle valueCellStyle = new DataGridViewCellStyle();
            valueCellStyle.WrapMode = DataGridViewTriState.True;
            valueCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[2].DefaultCellStyle = valueCellStyle;

        }
        private void LoadDocumentData()
        {
            // Xóa dữ liệu cũ (nếu có)
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Thêm cột
            dataGridView1.Columns.Add("FieldName", "Field");
            dataGridView1.Columns.Add("FieldType", "Type");
            dataGridView1.Columns.Add("FieldValue", "Value");

            var collection = database.GetCollection<BsonDocument>(this.collectionName);
            var document = collection.Find(
                Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(this.documentId))).FirstOrDefault();
            // Thêm dữ liệu
            foreach (var element in document)
            {
                if (editable && element.Name == "_id")
                {
                    continue;
                }
                fields.Add((element.Name, BsonHelper.BsonTypeToSystemType(element.Value.BsonType)));
                dataGridView1.Rows.Add(element.Name, BsonHelper.BsonTypeToSystemType(element.Value.BsonType), document[element.Name].ToString());
            }

            // Thiết lập chế độ chỉnh sửa
            dataGridView1.ReadOnly = !editable;
            dataGridView1.AllowUserToAddRows = false; // Không cho phép thêm hàng
        }
        private void handleUpdateDocument()
        {
            CustomClass newDocument = new CustomClass(collectionName, this.fields);
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var fieldName = dataGridView1.Rows[i].Cells[0].Value?.ToString();
                    var fieldValue = dataGridView1.Rows[i].Cells[2].Value?.ToString();

                    newDocument.setProp(fieldName, fieldValue);
                }
            }
            catch (Exception ex)
            {
                this.label1.Visible = true;
            }

            var collection = database.GetCollection<BsonDocument>(collectionName);
            var result = collection.ReplaceOne(
                Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(this.documentId)),
                newDocument.ToBsonDocument());
            MessageBox.Show("Data has been updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.documentDetailManager.NotifyObservers();
        }
        public void registerObserver(IDocumentObserver documentObserver)
        {
            this.documentDetailManager.RegisterObserver(documentObserver);
        }
        public void unregisterObserver(IDocumentObserver documentObserver)
        {
            this.documentDetailManager.UnregisterObserver(documentObserver);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (editable)
            {
                handleUpdateDocument();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (editable)
            {
                string fieldValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                Type fieldType = this.fields[e.RowIndex].PropertyType;

                validateDataInput(fieldValue, fieldType);
            }
        }

        public bool validateDataInput(string value, Type type)
        {
            // Clear any previous error
            this.label1.Visible = false;
            this.button1.Enabled = true;

            //if (string.IsNullOrWhiteSpace(data))
            //{
            //    this.errorType.SetError(this.dataInput, "Data field cannot be empty.");
            //    return false;
            //}
            //if (selectedType == null)
            //{
            //    return false;
            //}

            try
            {
                // Validate based on the selected type
                if (type == typeof(int))
                {
                    int.Parse(value);
                }
                else if (type == typeof(long))
                {
                    long.Parse(value);
                }
                else if (type == typeof(decimal))
                {
                    decimal.Parse(value);
                }
                else if (type == typeof(double))
                {
                    double.Parse(value);
                }
                else if (type == typeof(string))
                {
                    // Strings are always valid, but you can add custom rules
                    return true;
                }
                else if (type == typeof(bool))
                {
                    try
                    {
                        if (!Constants.booleanTypeValidation.Contains(value))
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.label1.Visible = true;
                        this.button1.Enabled = false;
                        return false;
                    }
                }
                else if (type == typeof(DateTime))
                {
                    try
                    {
                        DateTime.Parse(value);
                    }
                    catch (Exception ex)
                    {
                        this.label1.Visible = true;
                        this.button1.Enabled = false;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.label1.Visible = true;
                this.button1.Enabled = false;
                return false;
            }
            this.label1.Visible = false;
            this.button1.Enabled = true;
            return true;
        }

        
    }
}
