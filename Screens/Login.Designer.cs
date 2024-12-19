namespace SEP
{
    partial class Login
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
            labelRegister = new Label();
            label5 = new Label();
            btnLogin = new Button();
            checkShowPassword = new CheckBox();
            txtPassword = new TextBox();
            label3 = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            label1 = new Label();
            errProviderUsername = new ErrorProvider(components);
            errProviderPassword = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errProviderUsername).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errProviderPassword).BeginInit();
            SuspendLayout();
            // 
            // labelRegister
            // 
            labelRegister.AutoSize = true;
            labelRegister.Cursor = Cursors.Hand;
            labelRegister.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelRegister.ForeColor = Color.FromArgb(116, 86, 174);
            labelRegister.Location = new Point(337, 449);
            labelRegister.Name = "labelRegister";
            labelRegister.Size = new Size(84, 25);
            labelRegister.TabIndex = 22;
            labelRegister.Text = "Register";
            labelRegister.Click += labelRegister_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(164, 165, 169);
            label5.Location = new Point(308, 430);
            label5.Name = "label5";
            label5.Size = new Size(148, 17);
            label5.TabIndex = 21;
            label5.Text = "Dont have an account?";
            // 
            // btnLogin
            // 
            btnLogin.AccessibleName = "btnLogin";
            btnLogin.BackColor = Color.FromArgb(116, 86, 174);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(280, 320);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(100, 35);
            btnLogin.TabIndex = 20;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Enter += loginEnter;
            // 
            // checkShowPassword
            // 
            checkShowPassword.AccessibleName = "checkShowPassword";
            checkShowPassword.AutoSize = true;
            checkShowPassword.Cursor = Cursors.Hand;
            checkShowPassword.FlatStyle = FlatStyle.Flat;
            checkShowPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkShowPassword.ForeColor = Color.FromArgb(164, 165, 169);
            checkShowPassword.Location = new Point(280, 278);
            checkShowPassword.Name = "checkShowPassword";
            checkShowPassword.Size = new Size(119, 21);
            checkShowPassword.TabIndex = 19;
            checkShowPassword.Text = "Show password";
            checkShowPassword.UseVisualStyleBackColor = true;
            checkShowPassword.CheckedChanged += checkShowPassword_CheckedChanged;
            // 
            // txtPassword
            // 
            txtPassword.AccessibleName = "txtPassword";
            txtPassword.BackColor = Color.FromArgb(230, 231, 233);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 15.75F);
            txtPassword.Location = new Point(280, 234);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(216, 28);
            txtPassword.TabIndex = 16;
            txtPassword.Validating += passwordValidating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(164, 165, 169);
            label3.Location = new Point(280, 214);
            label3.Name = "label3";
            label3.Size = new Size(66, 17);
            label3.TabIndex = 15;
            label3.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.AccessibleName = "txtUsername";
            txtUsername.BackColor = Color.FromArgb(230, 231, 233);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 15.75F);
            txtUsername.Location = new Point(280, 166);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(216, 28);
            txtUsername.TabIndex = 14;
            txtUsername.Validating += usernameValidating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(164, 165, 169);
            label2.Location = new Point(280, 146);
            label2.Name = "label2";
            label2.Size = new Size(69, 17);
            label2.TabIndex = 13;
            label2.Text = "Username";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(116, 86, 174);
            label1.Location = new Point(280, 89);
            label1.Name = "label1";
            label1.Size = new Size(136, 37);
            label1.TabIndex = 12;
            label1.Text = "Welcome";
            // 
            // errProviderUsername
            // 
            errProviderUsername.ContainerControl = this;
            // 
            // errProviderPassword
            // 
            errProviderPassword.ContainerControl = this;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(784, 511);
            Controls.Add(labelRegister);
            Controls.Add(label5);
            Controls.Add(btnLogin);
            Controls.Add(checkShowPassword);
            Controls.Add(txtPassword);
            Controls.Add(label3);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)errProviderUsername).EndInit();
            ((System.ComponentModel.ISupportInitialize)errProviderPassword).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelRegister;
        private Label label5;
        private Button btnLogin;
        private CheckBox checkShowPassword;
        private TextBox txtPassword;
        private Label label3;
        private TextBox txtUsername;
        private Label label2;
        private Label label1;
        private ErrorProvider errProviderUsername;
        private ErrorProvider errProviderPassword;
    }
}