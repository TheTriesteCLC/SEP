namespace SEP.Screens
{
    partial class AddNewDocument
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            button4 = new Button();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            buttonAdd = new Button();
            labelCollectionName = new Label();
            button1 = new Button();
            buttonGetSchema = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            contextMenuStrip2 = new ContextMenuStrip(components);
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.01939F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86.9806061F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 345F));
            tableLayoutPanel1.Controls.Add(button4, 0, 0);
            tableLayoutPanel1.Controls.Add(label5, 3, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(label3, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 91);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(860, 347);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top;
            button4.BackColor = SystemColors.Control;
            button4.Cursor = Cursors.Hand;
            button4.FlatAppearance.BorderSize = 0;
            button4.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.ForeColor = SystemColors.ControlText;
            button4.Location = new Point(5, 3);
            button4.Name = "button4";
            button4.Size = new Size(33, 34);
            button4.TabIndex = 13;
            button4.Text = "+";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(666, 9);
            label5.Name = "label5";
            label5.Size = new Size(42, 21);
            label5.TabIndex = 14;
            label5.Text = "Data";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(403, 9);
            label4.Name = "label4";
            label4.Size = new Size(42, 21);
            label4.TabIndex = 13;
            label4.Text = "Type";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(145, 9);
            label3.Name = "label3";
            label3.Size = new Size(86, 21);
            label3.TabIndex = 12;
            label3.Text = "Field name";
            // 
            // buttonAdd
            // 
            buttonAdd.BackColor = SystemColors.Control;
            buttonAdd.Cursor = Cursors.Hand;
            buttonAdd.FlatAppearance.BorderSize = 0;
            buttonAdd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAdd.ForeColor = SystemColors.ControlText;
            buttonAdd.Location = new Point(772, 58);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(100, 22);
            buttonAdd.TabIndex = 9;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += button2_Click;
            // 
            // labelCollectionName
            // 
            labelCollectionName.AutoSize = true;
            labelCollectionName.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCollectionName.Location = new Point(12, 18);
            labelCollectionName.Name = "labelCollectionName";
            labelCollectionName.Size = new Size(155, 37);
            labelCollectionName.TabIndex = 10;
            labelCollectionName.Text = "Table name";
            // 
            // button1
            // 
            button1.BackColor = Color.LightGray;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(650, 58);
            button1.Name = "button1";
            button1.Size = new Size(116, 22);
            button1.TabIndex = 11;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // buttonGetSchema
            // 
            buttonGetSchema.AccessibleName = "buttonGetSchema";
            buttonGetSchema.BackColor = SystemColors.Control;
            buttonGetSchema.Cursor = Cursors.Hand;
            buttonGetSchema.FlatAppearance.BorderSize = 0;
            buttonGetSchema.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonGetSchema.ForeColor = SystemColors.ControlText;
            buttonGetSchema.Location = new Point(12, 58);
            buttonGetSchema.Name = "buttonGetSchema";
            buttonGetSchema.Size = new Size(116, 22);
            buttonGetSchema.TabIndex = 12;
            buttonGetSchema.Text = "Get schema";
            buttonGetSchema.UseVisualStyleBackColor = false;
            buttonGetSchema.Click += buttonGetSchema_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(61, 4);
            // 
            // AddNewDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 450);
            Controls.Add(buttonGetSchema);
            Controls.Add(button1);
            Controls.Add(labelCollectionName);
            Controls.Add(buttonAdd);
            Controls.Add(tableLayoutPanel1);
            Name = "AddNewDocument";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add New Document";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonAdd;
        private Label labelCollectionName;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button button1;
        private Button buttonGetSchema;
        private Button button4;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
    }
}