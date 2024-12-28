using Microsoft.VisualBasic.FileIO;
using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CurrUser;
using SEP.CustomClassBuilder;
using SEP.DBManagement;
using SEP.Interfaces;
using SEP.Observers;
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
        private IDatabase database;
        private string collectionName;
        private List<(string PropertyName, Type PropertyType)> initFields;

        private List<FieldUI> fieldUIList;
        private DocumentDetailManager addNewDocumentManager;

        public AddNewDocument(string collectionName)
        {
            InitializeComponent();
            this.database = CurrUserInfo.getUserDB();
            this.collectionName = collectionName;
            labelCollectionName.Text = collectionName;
            this.initFields = new List<(string PropertyName, Type PropertyType)>();

            this.fieldUIList = new List<FieldUI>();
            this.addNewDocumentManager = new DocumentDetailManager();
        }
        public void registerObserver(IDocumentObserver documentObserver)
        {
            this.addNewDocumentManager.RegisterObserver(documentObserver);
        }
        public void unregisterObserver(IDocumentObserver documentObserver)
        {
            this.addNewDocumentManager.UnregisterObserver(documentObserver);
        }

        private async Task<List<(string PropertyName, Type PropertyType)>> GetCollectionFields()
        {
            dbSchema schema = await this.database.GetCollectionSchema(this.collectionName);
            return schema.toSchemaList();
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
                    fieldUIList.RemoveAt(deleteIndex);
                }
            };

            newFieldUI.dataInput.Validating += (s, e) =>
            {
                bool isValidated = newFieldUI.validateDataInput();
                if (!isValidated)
                {
                    this.buttonAdd.Enabled = false;
                } else
                {
                    this.buttonAdd.Enabled = true;
                }
            };

            newFieldUI.typeComboBox.SelectedIndexChanged += (s, e) =>
            {
                bool isValidated = newFieldUI.validateDataInput();
                if (!isValidated)
                {
                    this.buttonAdd.Enabled = false;
                }
                else
                {
                    this.buttonAdd.Enabled = true;
                }
            };

            newFieldUI.nameInput.Validating += (s, e) =>
            {
                if (newFieldUI.nameInput.Text == "")
                {
                    this.buttonAdd.Enabled = false;
                    newFieldUI.errorName.SetError(newFieldUI.nameInput, "Field name must not be blank");
                }
                else
                {
                    this.buttonAdd.Enabled = true;
                    newFieldUI.errorName.SetError(newFieldUI.nameInput, "");
                }
            };
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
        private async Task<dbResponse> handleCreateNewDocument()
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

            return await database.AddNewDocument(collectionName, newDocument);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            addNewField("", typeof(string));
        }
        private async void buttonGetSchema_Click(object sender, EventArgs e)
        {
            ClearTableLayoutPanel();
            this.initFields = await GetCollectionFields();

            foreach (var field in initFields)
            {
                addNewField(field.PropertyName, field.PropertyType);
            }
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            dbResponse result = await handleCreateNewDocument();
            System.Windows.Forms.MessageBox.Show(result.message);

            this.addNewDocumentManager.NotifyObservers();
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
        public ErrorProvider errorType;
        public ErrorProvider errorName;

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
                Width = 150,
                Anchor = AnchorStyles.Top,
            };

            this.errorType = new ErrorProvider();
            this.errorName = new ErrorProvider();
            

            foreach (Type type in Constants.supportedType)
            {
                this.typeComboBox.Items.Add(type);
            }

            this.typeComboBox.SelectedIndex = Constants.supportedType.FindIndex(t => t == fieldType);

            this.dataInput = new TextBox
            {
                Width = 325,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
            };
        }
        public bool validateDataInput()
        {
            Type selectedType = Constants.supportedType[this.typeComboBox.SelectedIndex];
            string data = this.dataInput.Text;

            // Clear any previous error
            this.errorType.SetError(this.dataInput, "");

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
                if (selectedType == typeof(int))
                {
                    int.Parse(data);
                }
                else if (selectedType == typeof(long))
                {
                    long.Parse(data);
                }
                else if (selectedType == typeof(decimal))
                {
                    decimal.Parse(data);
                }
                else if (selectedType == typeof(double))
                {
                    double.Parse(data);
                }
                else if (selectedType == typeof(string))
                {
                    // Strings are always valid, but you can add custom rules
                    return true;
                }else if (selectedType == typeof(bool))
                {
                    try
                    {
                        if (!Constants.booleanTypeValidation.Contains(data))
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        errorType.SetError(dataInput, $"Value must be 'true'/'false'");
                        return false;
                    }
                }else if(selectedType == typeof(DateTime))
                {
                    try
                    {
                        DateTime.Parse(data);
                    }catch(Exception ex)
                    {
                        errorType.SetError(dataInput, $"Date format must be MM/DD/YYYY");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                errorType.SetError(dataInput, $"Invalid {typeComboBox.SelectedItem?.ToString()} value");
                return false;
            }
            errorType.SetError(dataInput, "");
            return true;
        }
    }
}
