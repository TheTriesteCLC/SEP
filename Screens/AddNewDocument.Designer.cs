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
            button2 = new Button();
            label2 = new Label();
            button1 = new Button();
            button3 = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66667F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 209F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 330F));
            tableLayoutPanel1.Controls.Add(button4, 0, 0);
            tableLayoutPanel1.Controls.Add(label5, 3, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(label3, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 91);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(776, 347);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top;
            button4.BackColor = Color.FromArgb(116, 86, 174);
            button4.Cursor = Cursors.Hand;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.ForeColor = Color.White;
            button4.Location = new Point(22, 3);
            button4.Name = "button4";
            button4.Size = new Size(33, 35);
            button4.TabIndex = 13;
            button4.Text = "+";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(595, 13);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 14;
            label5.Text = "Data";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(325, 13);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 13;
            label4.Text = "Type";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(124, 13);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 12;
            label3.Text = "Field name";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(116, 86, 174);
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(688, 12);
            button2.Name = "button2";
            button2.Size = new Size(100, 35);
            button2.TabIndex = 9;
            button2.Text = "Add";
            button2.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 22);
            label2.Name = "label2";
            label2.Size = new Size(108, 25);
            label2.TabIndex = 10;
            label2.Text = "Table name";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(116, 86, 174);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(555, 12);
            button1.Name = "button1";
            button1.Size = new Size(116, 35);
            button1.TabIndex = 11;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(116, 86, 174);
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Location = new Point(12, 50);
            button3.Name = "button3";
            button3.Size = new Size(116, 35);
            button3.TabIndex = 12;
            button3.Text = "Get schema";
            button3.UseVisualStyleBackColor = false;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // AddNewDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(tableLayoutPanel1);
            Name = "AddNewDocument";
            Text = "AddNewDocument";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button button2;
        private Label label2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button button1;
        private Button button3;
        private Button button4;
        private ContextMenuStrip contextMenuStrip1;
    }
}