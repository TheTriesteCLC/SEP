namespace SEP
{
    partial class Registration
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
            label1 = new Label();
            label2 = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            label3 = new Label();
            txtConfirm = new TextBox();
            label4 = new Label();
            checkShowPassword = new CheckBox();
            button1 = new Button();
            label5 = new Label();
            label6 = new Label();
            errProviderUsername = new ErrorProvider(components);
            errProviderPassword = new ErrorProvider(components);
            errProviderConfirm = new ErrorProvider(components);
            txtConnection = new TextBox();
            label7 = new Label();
            errProviderConnection = new ErrorProvider(components);
            comboboxDatabase = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)errProviderUsername).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errProviderPassword).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errProviderConfirm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errProviderConnection).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(116, 86, 174);
            label1.Location = new Point(280, 30);
            label1.Name = "label1";
            label1.Size = new Size(164, 37);
            label1.TabIndex = 0;
            label1.Text = "Get Started";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(280, 87);
            label2.Name = "label2";
            label2.Size = new Size(69, 17);
            label2.TabIndex = 1;
            label2.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.AccessibleName = "txtUsername";
            txtUsername.BackColor = Color.FromArgb(230, 231, 233);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(280, 107);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(216, 28);
            txtUsername.TabIndex = 2;
            txtUsername.Validating += usernameValidating;
            // 
            // txtPassword
            // 
            txtPassword.AccessibleName = "txtPassword";
            txtPassword.BackColor = Color.FromArgb(230, 231, 233);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(280, 249);
            txtPassword.Multiline = true;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(216, 28);
            txtPassword.TabIndex = 4;
            txtPassword.Validating += passwordValidating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(280, 229);
            label3.Name = "label3";
            label3.Size = new Size(66, 17);
            label3.TabIndex = 3;
            label3.Text = "Password";
            // 
            // txtConfirm
            // 
            txtConfirm.AccessibleName = "txtConfirm";
            txtConfirm.BackColor = Color.FromArgb(230, 231, 233);
            txtConfirm.BorderStyle = BorderStyle.None;
            txtConfirm.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConfirm.Location = new Point(280, 320);
            txtConfirm.Multiline = true;
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '*';
            txtConfirm.Size = new Size(216, 28);
            txtConfirm.TabIndex = 6;
            txtConfirm.Validating += confirmValidating;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.Location = new Point(280, 300);
            label4.Name = "label4";
            label4.Size = new Size(120, 17);
            label4.TabIndex = 5;
            label4.Text = "Confirm Password";
            // 
            // checkShowPassword
            // 
            checkShowPassword.AccessibleName = "checkShowPassword";
            checkShowPassword.AutoSize = true;
            checkShowPassword.Cursor = Cursors.Hand;
            checkShowPassword.FlatStyle = FlatStyle.Flat;
            checkShowPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkShowPassword.Location = new Point(280, 354);
            checkShowPassword.Name = "checkShowPassword";
            checkShowPassword.Size = new Size(119, 21);
            checkShowPassword.TabIndex = 7;
            checkShowPassword.Text = "Show password";
            checkShowPassword.UseVisualStyleBackColor = true;
            checkShowPassword.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(116, 86, 174);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(335, 396);
            button1.Name = "button1";
            button1.Size = new Size(100, 35);
            button1.TabIndex = 8;
            button1.Text = "Sign up";
            button1.UseVisualStyleBackColor = false;
            button1.Enter += signupEnter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(308, 448);
            label5.Name = "label5";
            label5.Size = new Size(164, 17);
            label5.TabIndex = 10;
            label5.Text = "Already have an account?";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Cursor = Cursors.Hand;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(116, 86, 174);
            label6.Location = new Point(354, 466);
            label6.Name = "label6";
            label6.Size = new Size(63, 25);
            label6.TabIndex = 11;
            label6.Text = "Login";
            label6.Click += labelLoginClick;
            // 
            // errProviderUsername
            // 
            errProviderUsername.ContainerControl = this;
            // 
            // errProviderPassword
            // 
            errProviderPassword.ContainerControl = this;
            // 
            // errProviderConfirm
            // 
            errProviderConfirm.ContainerControl = this;
            // 
            // txtConnection
            // 
            txtConnection.AccessibleName = "txtConnection";
            txtConnection.BackColor = Color.FromArgb(230, 231, 233);
            txtConnection.BorderStyle = BorderStyle.None;
            txtConnection.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConnection.Location = new Point(280, 190);
            txtConnection.Multiline = true;
            txtConnection.Name = "txtConnection";
            txtConnection.Size = new Size(216, 28);
            txtConnection.TabIndex = 13;
            txtConnection.Leave += txtConnection_Leave;
            txtConnection.Validating += connectionValidating;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label7.Location = new Point(280, 149);
            label7.MaximumSize = new Size(180, 0);
            label7.Name = "label7";
            label7.Size = new Size(147, 34);
            label7.TabIndex = 12;
            label7.Text = "Connection string (MongoDB supported)";
            // 
            // errProviderConnection
            // 
            errProviderConnection.ContainerControl = this;
            // 
            // comboboxDatabase
            // 
            comboboxDatabase.AccessibleName = "comboboxDatabase";
            comboboxDatabase.BackColor = Color.FromArgb(230, 231, 233);
            comboboxDatabase.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboboxDatabase.FormattingEnabled = true;
            comboboxDatabase.Location = new Point(502, 193);
            comboboxDatabase.Name = "comboboxDatabase";
            comboboxDatabase.Size = new Size(121, 25);
            comboboxDatabase.TabIndex = 14;
            comboboxDatabase.Visible = false;
            comboboxDatabase.SelectedIndexChanged += comboboxDatabase_SelectedIndexChanged;
            // 
            // Registration
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(784, 511);
            Controls.Add(comboboxDatabase);
            Controls.Add(txtConnection);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(checkShowPassword);
            Controls.Add(txtConfirm);
            Controls.Add(label4);
            Controls.Add(txtPassword);
            Controls.Add(label3);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(164, 165, 169);
            Name = "Registration";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registration";
            Load += Registration_Load;
            ((System.ComponentModel.ISupportInitialize)errProviderUsername).EndInit();
            ((System.ComponentModel.ISupportInitialize)errProviderPassword).EndInit();
            ((System.ComponentModel.ISupportInitialize)errProviderConfirm).EndInit();
            ((System.ComponentModel.ISupportInitialize)errProviderConnection).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label label3;
        private TextBox txtConfirm;
        private Label label4;
        private CheckBox checkShowPassword;
        private Button button1;
        private Label label5;
        private Label label6;
        private ErrorProvider errProviderUsername;
        private ErrorProvider errProviderPassword;
        private ErrorProvider errProviderConfirm;
        private TextBox txtConnection;
        private Label label7;
        private ErrorProvider errProviderConnection;
        private ComboBox comboboxDatabase;
    }
}