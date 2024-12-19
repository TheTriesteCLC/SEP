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
            ((System.ComponentModel.ISupportInitialize)errProviderTablename).BeginInit();
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
            // 
            // txtTableName
            // 
            txtTableName.AccessibleName = "txtTableName";
            txtTableName.BackColor = Color.FromArgb(230, 231, 233);
            txtTableName.BorderStyle = BorderStyle.None;
            txtTableName.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTableName.Location = new Point(250, 74);
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
            label2.Location = new Point(250, 54);
            label2.Name = "label2";
            label2.Size = new Size(79, 17);
            label2.TabIndex = 18;
            label2.Text = "Table name";
            // 
            // errProviderTablename
            // 
            errProviderTablename.ContainerControl = this;
            // 
            // AddNewTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(label2);
            Controls.Add(txtTableName);
            Controls.Add(button5);
            Controls.Add(button4);
            Name = "AddNewTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NewTable";
            ((System.ComponentModel.ISupportInitialize)errProviderTablename).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button4;
        private Button button5;
        private TextBox txtTableName;
        private Label label2;
        private ErrorProvider errProviderTablename;
    }
}