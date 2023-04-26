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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}