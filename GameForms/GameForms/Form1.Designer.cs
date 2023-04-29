namespace GameForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            NicknameField = new TextBox();
            BotLevelsComboBox = new ComboBox();
            BotLevelsText = new TextBox();
            GameModeText = new TextBox();
            GameModeComboBox = new ComboBox();
            LogOutButton = new Button();
            LogInButton = new Button();
            SignUpButton = new Button();
            LoginTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            LogButton = new Button();
            SignButton = new Button();
            ProfileButton = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(309, 209);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Play";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // NicknameField
            // 
            NicknameField.Location = new Point(167, 210);
            NicknameField.Name = "NicknameField";
            NicknameField.PlaceholderText = "Enter nickname";
            NicknameField.Size = new Size(121, 23);
            NicknameField.TabIndex = 1;
            // 
            // BotLevelsComboBox
            // 
            BotLevelsComboBox.AllowDrop = true;
            BotLevelsComboBox.DisplayMember = "1";
            BotLevelsComboBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            BotLevelsComboBox.Location = new Point(167, 181);
            BotLevelsComboBox.Name = "BotLevelsComboBox";
            BotLevelsComboBox.Size = new Size(121, 23);
            BotLevelsComboBox.TabIndex = 2;
            BotLevelsComboBox.Tag = "1";
            BotLevelsComboBox.ValueMember = "1";
            BotLevelsComboBox.Visible = false;
            BotLevelsComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // BotLevelsText
            // 
            BotLevelsText.Enabled = false;
            BotLevelsText.Location = new Point(52, 181);
            BotLevelsText.Name = "BotLevelsText";
            BotLevelsText.Size = new Size(109, 23);
            BotLevelsText.TabIndex = 3;
            BotLevelsText.Text = "Chose bot`s level";
            BotLevelsText.Visible = false;
            BotLevelsText.TextChanged += textBox2_TextChanged;
            // 
            // GameModeText
            // 
            GameModeText.Enabled = false;
            GameModeText.Location = new Point(52, 152);
            GameModeText.Name = "GameModeText";
            GameModeText.Size = new Size(109, 23);
            GameModeText.TabIndex = 5;
            GameModeText.Text = "Chose gamemode";
            GameModeText.TextChanged += textBox3_TextChanged;
            // 
            // GameModeComboBox
            // 
            GameModeComboBox.AllowDrop = true;
            GameModeComboBox.DisplayMember = "1";
            GameModeComboBox.Items.AddRange(new object[] { "AIvAI", "PvAI", "PvP" });
            GameModeComboBox.Location = new Point(167, 152);
            GameModeComboBox.Name = "GameModeComboBox";
            GameModeComboBox.Size = new Size(121, 23);
            GameModeComboBox.Sorted = true;
            GameModeComboBox.TabIndex = 4;
            GameModeComboBox.Tag = "1";
            GameModeComboBox.ValueMember = "1";
            GameModeComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // LogOutButton
            // 
            LogOutButton.Location = new Point(713, 12);
            LogOutButton.Name = "LogOutButton";
            LogOutButton.Size = new Size(75, 23);
            LogOutButton.TabIndex = 6;
            LogOutButton.Text = "LogOut";
            LogOutButton.UseVisualStyleBackColor = true;
            LogOutButton.Click += LogOutButton_Click;
            // 
            // LogInButton
            // 
            LogInButton.Location = new Point(632, 12);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(75, 23);
            LogInButton.TabIndex = 7;
            LogInButton.Text = "LogIn";
            LogInButton.UseVisualStyleBackColor = true;
            LogInButton.Click += LogInButton_Click;
            // 
            // SignUpButton
            // 
            SignUpButton.Location = new Point(713, 12);
            SignUpButton.Name = "SignUpButton";
            SignUpButton.Size = new Size(75, 23);
            SignUpButton.TabIndex = 8;
            SignUpButton.Text = "SignUp";
            SignUpButton.UseVisualStyleBackColor = true;
            SignUpButton.Click += SignUpButton_Click;
            // 
            // LoginTextBox
            // 
            LoginTextBox.Location = new Point(663, 41);
            LoginTextBox.Name = "LoginTextBox";
            LoginTextBox.PlaceholderText = "Enter login";
            LoginTextBox.Size = new Size(100, 23);
            LoginTextBox.TabIndex = 9;
            LoginTextBox.TextChanged += LoginTextBox_TextChanged;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(663, 70);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PlaceholderText = "Enter password";
            PasswordTextBox.Size = new Size(100, 23);
            PasswordTextBox.TabIndex = 10;
            PasswordTextBox.TextChanged += PasswordTextBox_TextChanged;
            // 
            // LogButton
            // 
            LogButton.Location = new Point(673, 108);
            LogButton.Name = "LogButton";
            LogButton.Size = new Size(75, 23);
            LogButton.TabIndex = 11;
            LogButton.Text = "LogIn";
            LogButton.UseVisualStyleBackColor = true;
            LogButton.Click += LogButton_Click;
            // 
            // SignButton
            // 
            SignButton.Location = new Point(673, 108);
            SignButton.Name = "SignButton";
            SignButton.Size = new Size(75, 23);
            SignButton.TabIndex = 12;
            SignButton.Text = "SignUp";
            SignButton.UseVisualStyleBackColor = true;
            SignButton.Click += SignButton_Click;
            // 
            // ProfileButton
            // 
            ProfileButton.Location = new Point(207, 11);
            ProfileButton.Name = "ProfileButton";
            ProfileButton.Size = new Size(147, 23);
            ProfileButton.TabIndex = 14;
            ProfileButton.Text = "Guest";
            ProfileButton.UseVisualStyleBackColor = true;
            ProfileButton.Click += ProfileButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ProfileButton);
            Controls.Add(SignButton);
            Controls.Add(LogButton);
            Controls.Add(PasswordTextBox);
            Controls.Add(LoginTextBox);
            Controls.Add(SignUpButton);
            Controls.Add(LogInButton);
            Controls.Add(LogOutButton);
            Controls.Add(GameModeText);
            Controls.Add(GameModeComboBox);
            Controls.Add(BotLevelsText);
            Controls.Add(BotLevelsComboBox);
            Controls.Add(NicknameField);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox NicknameField;
        private ComboBox BotLevelsComboBox;
        private TextBox BotLevelsText;
        private TextBox GameModeText;
        private ComboBox GameModeComboBox;
        private Button LogOutButton;
        private Button LogInButton;
        private Button SignUpButton;
        private TextBox LoginTextBox;
        private TextBox PasswordTextBox;
        private Button LogButton;
        private Button SignButton;
        private Button ProfileButton;
    }
}