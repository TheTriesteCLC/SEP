﻿using System;
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
    public partial class DetailDocument : Form
    {
        private Dictionary<string, string> documentData;
        private bool isEditable;
        private Action<Dictionary<string, string>> onSave;
        public DetailDocument(Dictionary<string, string> data, bool editable, Action<Dictionary<string, string>> onSaveCallback)
        {
            InitializeComponent();
            documentData = data;
            isEditable = editable;
            onSave = onSaveCallback;
            LoadDocumentData();
            if (isEditable)
            {
                // Cho phép chỉnh sửa nếu cần
                dataGridView1.ReadOnly = false;
                button1.Visible = true;
            }
            else
            {
                // Không cho phép chỉnh sửa
                dataGridView1.ReadOnly = true;
                button1.Visible = false;
            }
            ConfigureDataGridView();
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

                // Gọi callback lưu dữ liệu
                if (onSave != null)
                {
                    onSave(documentData);
                }

                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
