namespace SEP
{
    partial class AddNewTable
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
            button4 = new Button();
            button5 = new Button();
            txtTableName = new TextBox();
            label2 = new Label();
            errProviderTablename = new ErrorProvider(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            button1 = new Button();
            label4 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)errProviderTablename).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(607, 9);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(82, 22);
            button4.TabIndex = 9;
            button4.Text = "Add";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.Control;
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(10, 9);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(82, 22);
            button5.TabIndex = 10;
            button5.Text = "Back\r\n";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // txtTableName
            // 
            txtTableName.AccessibleName = "txtTableName";
            txtTableName.BackColor = Color.FromArgb(230, 231, 233);
            txtTableName.BorderStyle = BorderStyle.None;
            txtTableName.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTableName.Location = new Point(250, 62);
            txtTableName.Name = "txtTableName";
            txtTableName.Size = new Size(216, 28);
            txtTableName.TabIndex = 17;
            txtTableName.Validating += tableNameValidating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(164, 165, 169);
            label2.Location = new Point(250, 42);
            label2.Name = "label2";
            label2.Size = new Size(79, 17);
            label2.TabIndex = 18;
            label2.Text = "Table name";
            // 
            // errProviderTablename
            // 
            errProviderTablename.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.8378382F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87.16216F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 232F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(button1, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(label3, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 108);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(677, 218);
            tableLayoutPanel1.TabIndex = 20;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top;
            button1.BackColor = SystemColors.Control;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(12, 3);
            button1.Name = "button1";
            button1.Size = new Size(33, 34);
            button1.TabIndex = 13;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(539, 9);
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
            label3.Location = new Point(207, 9);
            label3.Name = "label3";
            label3.Size = new Size(86, 21);
            label3.TabIndex = 12;
            label3.Text = "Field name";
            // 
            // AddNewTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label2);
            Controls.Add(txtTableName);
            Controls.Add(button5);
            Controls.Add(button4);
            Name = "AddNewTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add New Table";
            ((System.ComponentModel.ISupportInitialize)errProviderTablename).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button4;
        private Button button5;
        private TextBox txtTableName;
        private Label label2;
        private ErrorProvider errProviderTablename;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button1;
        private Label label4;
        private Label label3;
    }
}