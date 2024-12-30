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
using SEP.ClientDatabase;
using SEP.Components;

namespace SEP
{
    public partial class AddNewTable : Form
    {
        private IDatabase database;
        private dbSchema schema;

        private List<FieldUI> fieldUIList;
        private DocumentDetailManager addNewTableManager;
        public AddNewTable(bool schemaRequired)
        {
            InitializeComponent();
            database = CurrUserInfo.getUserDB();
            this.fieldUIList = new List<FieldUI>();
            this.schema = new dbSchema("", new List<dbSchemaField>());
            this.addNewTableManager = new DocumentDetailManager();
            if (schemaRequired)
            {
                this.tableLayoutPanel1.Visible = true;
            }
            else
            {
                this.tableLayoutPanel1.Visible = false;
            }
            this.button4.Enabled = false;
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

            newFieldUI.deleteBtn.Click += (s, e) =>
            {
                var deleteIndex = fieldUIList.FindIndex(field => field.deleteBtn == newFieldUI.deleteBtn);
                if (deleteIndex != -1)
                {
                    RemoveRow(deleteIndex + 1);
                    fieldUIList.RemoveAt(deleteIndex);
                }
            };

            newFieldUI.nameInput.Validating += (s, e) =>
            {
                if (newFieldUI.nameInput.Text == "")
                {
                    this.button4.Enabled = false;
                    newFieldUI.errorName.SetError(newFieldUI.nameInput, "Field name must not be blank");
                }
                else
                {
                    this.button4.Enabled = true;
                    newFieldUI.errorName.SetError(newFieldUI.nameInput, "");
                }
            };
        }
        private async Task<dbResponse> handleAddNewTable(string newCollectionName)
        {
            List<dbSchemaField> schemaFields = new List<dbSchemaField>();
            foreach (FieldUI fieldUI in fieldUIList)
            {
                var newField = new dbSchemaField(
                    fieldUI.nameInput.Text,
                    Constants.supportedType[fieldUI.typeComboBox.SelectedIndex]);
                schemaFields.Add(newField);
            }
            return await this.database.CreateNewCollection(newCollectionName, new dbSchema(newCollectionName, schemaFields));
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                string tableName = txtTableName.Text;
                dbResponse result = await handleAddNewTable(tableName);
                System.Windows.Forms.MessageBox.Show(result.message);
                this.addNewTableManager.NotifyObservers();
                this.Close();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addNewField("", typeof(string));
        }
    }
}
