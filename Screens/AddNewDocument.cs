using Microsoft.VisualBasic.FileIO;
using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CurrUser;
using SEP.CustomClassBuilder;
using SEP.DBManagement;
using SEP.Ultils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEP.Screens
{
    public partial class AddNewDocument : Form
    {
        private IMongoDatabase database;
        private string collectionName;
        private CustomClass schema;
        private List<(string PropertyName, Type PropertyType)> initFields;

        private List<FieldUI> fieldUIList;

        public AddNewDocument(string collectionName)
        {
            InitializeComponent();
            this.database = CurrUserInfo.getUserDB();
            this.collectionName = collectionName;
            labelCollectionName.Text = collectionName;
            this.initFields = GetCollectionFields();
            //this.schema = new CustomClass(collectionName, fields);

            fieldUIList = new List<FieldUI>();
        }

        private List<(string PropertyName, Type PropertyType)> GetCollectionFields()
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = collection.Find(FilterDefinition<BsonDocument>.Empty).ToList();

            HashSet<(string PropertyName, Type PropertyType)> fields
                = new HashSet<(string PropertyName, Type PropertyType)>();
            foreach (var doc in documents)
            {
                foreach (var element in doc.Elements)
                {
                    var field = (
                        element.Name,
                        BsonHelper.BsonTypeToSystemType(element.Value.BsonType
                    ));
                    if (!fields.Contains(field) && element.Name != "_id")
                    {
                        fields.Add(field);
                    }
                }
            }

            return fields.ToList();
        }
        private void RemoveRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= tableLayoutPanel1.RowCount) return;

            for (int col = 0; col < tableLayoutPanel1.ColumnCount; col++)
            {
                Control control = tableLayoutPanel1.GetControlFromPosition(col, rowIndex);
                if (control != null)
                {
                    tableLayoutPanel1.Controls.Remove(control);
                    control.Dispose();
                }
            }

            for (int i = rowIndex + 1; i < tableLayoutPanel1.RowCount; i++)
            {
                for (int col = 0; col < tableLayoutPanel1.ColumnCount; col++)
                {
                    Control control = tableLayoutPanel1.GetControlFromPosition(col, i);
                    if (control != null)
                    {
                        tableLayoutPanel1.SetRow(control, i - 1);
                    }
                }
            }
            if (tableLayoutPanel1.RowStyles.Count > rowIndex)
            {
                tableLayoutPanel1.RowStyles.RemoveAt(rowIndex);
            }
            else if (tableLayoutPanel1.RowStyles.Count > 0)
            {
                tableLayoutPanel1.RowStyles.RemoveAt(tableLayoutPanel1.RowStyles.Count - 1);
            }

            tableLayoutPanel1.RowCount--;
        }

        private void addNewField(string fieldName, Type fieldType)
        {
            FieldUI newFieldUI = new FieldUI(fieldType, fieldName);
            fieldUIList.Add(newFieldUI);

            int rowIndex = tableLayoutPanel1.RowCount;
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            tableLayoutPanel1.Controls.Add(newFieldUI.deleteBtn, 0, rowIndex);
            tableLayoutPanel1.Controls.Add(newFieldUI.nameInput, 1, rowIndex);
            tableLayoutPanel1.Controls.Add(newFieldUI.typeComboBox, 2, rowIndex);
            tableLayoutPanel1.Controls.Add(newFieldUI.dataInput, 3, rowIndex);

            newFieldUI.deleteBtn.Click += (s, e) =>
            {
                var deleteIndex = fieldUIList.FindIndex(field => field.deleteBtn == newFieldUI.deleteBtn);
                if (deleteIndex != -1)
                {
                    RemoveRow(deleteIndex + 1);
                    System.Diagnostics.Debug.WriteLine(deleteIndex);
                    fieldUIList.RemoveAt(deleteIndex);
                }
            };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addNewField("", typeof(string));
        }
        private void buttonGetSchema_Click(object sender, EventArgs e)
        {
            ClearTableLayoutPanel();
            foreach (var field in initFields)
            {
                addNewField(field.PropertyName, field.PropertyType);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            handleCreateNewDocument();
        }
        private void ClearTableLayoutPanel()
        {
            for (int i = tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                var control = tableLayoutPanel1.Controls[i];
                var position = tableLayoutPanel1.GetPositionFromControl(control);
                if (position.Row >= 1)
                {
                    tableLayoutPanel1.Controls.Remove(control);
                    control.Dispose();
                }
            }
            for (int i = tableLayoutPanel1.RowStyles.Count - 1; i >= 1; i--)
            {
                tableLayoutPanel1.RowStyles.RemoveAt(i);
            }
            tableLayoutPanel1.RowCount = 1;
            this.fieldUIList.Clear();
        }
        private void handleCreateNewDocument()
        {
            List<(string PropertyName, Type PropertyType)> fields = new List<(string PropertyName, Type PropertyType)>();
            foreach (var fieldUI in fieldUIList)
            {
                int selectedIndex = fieldUI.typeComboBox.SelectedIndex;
                fields.Add((fieldUI.nameInput.Text, Constants.supportedType[selectedIndex]));
            }
            CustomClass newDocument = new CustomClass(collectionName, fields);

            foreach (var fieldUI in fieldUIList)
            {
                newDocument.setProp(fieldUI.nameInput.Text, fieldUI.dataInput.Text);
            }


            var client = new MongoClient(Constants.connectionString);
            var database1 = client.GetDatabase(Constants.mainDBString);
            var collection1 = database1.GetCollection<BsonDocument>(collectionName);

            collection1.InsertOne(newDocument.ToBsonDocument());

            System.Windows.Forms.MessageBox.Show($"Add new document to '{collectionName}'!");
            ClearTableLayoutPanel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    internal class FieldUI
    {
        public Button deleteBtn;
        public TextBox nameInput;
        public ComboBox typeComboBox;
        public TextBox dataInput;

        public FieldUI(Type fieldType, string fieldName = "")
        {
            this.deleteBtn = new Button
            {
                Text = "X",
                BackColor = Color.FromArgb(192, 192, 192),
                ForeColor = Color.Black,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Width = 30,
                Height = 30,
                Anchor = AnchorStyles.Top,
            };

            this.nameInput = new TextBox
            {
                Width = 150,
                Text = fieldName,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            };

            this.typeComboBox = new ComboBox
            {
                Width = 200,
                Anchor = AnchorStyles.Top,
            };

            foreach (Type type in Constants.supportedType)
            {
                this.typeComboBox.Items.Add(type);
            }

            this.typeComboBox.SelectedIndex = Constants.supportedType.FindIndex(t => t == fieldType);

            this.dataInput = new TextBox
            {
                Width = 200,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            };
        }
    }
}
