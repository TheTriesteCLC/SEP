using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CustomClassBuilder;
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
        public AddNewDocument()
        {
            InitializeComponent();
        }

        public void addNewDocument()
        {
            //List<(string PropertyName, Type PropertyType)> columnNames
            //   = new List<(string PropertyName, Type PropertyType)>();
            //foreach (DataColumn col in dataTable.Columns)
            //{
            //    columnNames.Add((col.ColumnName, typeof(string)));
            //}

            //var newClass = new CustomClass("huhu", columnNames);

            //foreach ((string PropertyName, Type PropertyType) col in columnNames)
            //{
            //    newClass.setProp(col.PropertyName, "hihi");
            //}

            //var client = new MongoClient(Constants.connectionString);
            //var database1 = client.GetDatabase(Constants.mainDBString);
            //var collection1 = database1.GetCollection<BsonDocument>("test");

            //collection1.InsertOne(newClass.ToBsonDocument());
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
            if (tableLayoutPanel1.RowStyles.Count > 0)
            {
                tableLayoutPanel1.RowStyles.RemoveAt(tableLayoutPanel1.RowCount - 1);
            }

            tableLayoutPanel1.RowCount--;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button removeButton = new Button
            {
                Text = "X",
                BackColor = Color.FromArgb(251, 65, 65),
                ForeColor = Color.White,
                Width = 30,
                Height = 30,
                Anchor = AnchorStyles.Top,
            };

            TextBox fieldNameTextBox = new TextBox
            {
                Width = 150,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            };

            ComboBox typeComboBox = new ComboBox
            {
                Width = 100,
                Anchor = AnchorStyles.Top,
            };
            // Set ComboBox items dynamically later
             typeComboBox.Items.Add("Example Type");
             typeComboBox.Items.Add("Example Type");
             typeComboBox.Items.Add("Example Type");
             typeComboBox.Items.Add("Example Type");
             typeComboBox.Items.Add("Example Type");

            TextBox dataTextBox = new TextBox
            {
                Width = 200,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
            };

            int rowIndex = tableLayoutPanel1.RowCount;
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            tableLayoutPanel1.Controls.Add(removeButton, 0, rowIndex);
            tableLayoutPanel1.Controls.Add(fieldNameTextBox, 1, rowIndex);
            tableLayoutPanel1.Controls.Add(typeComboBox, 2, rowIndex);
            tableLayoutPanel1.Controls.Add(dataTextBox, 3, rowIndex);

            removeButton.Click += (s, e) =>
            {
                RemoveRow(rowIndex);
            };
        }
    }
}
