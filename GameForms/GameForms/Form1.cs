using System.Diagnostics;

namespace GameForms
{
    public partial class Form1 : Form
    {
        public SaveLoad saveLoad = new SaveLoad();
        public string pathToSavedFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents");
        public Profiles profiles;

        public Profile currentProfile;
        public Form1()
        {
            InitializeComponent();
            profiles = saveLoad.LoadAllProfiles();
        }

        private string[] GetAllData()
        {
            string[] data = new string[3];
            data[0] = NicknameField.Text == "" ? "Player" : NicknameField.Text;

            data[1] = GameModeComboBox.Items[GameModeComboBox.SelectedIndex].ToString();
            data[2] = BotLevelsComboBox.Items[BotLevelsComboBox.SelectedIndex].ToString();

            return data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveLoad.SaveGame(GetAllData());
            Process.Start("WarGame.exe");
        }


        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedGameMode = GameModeComboBox.Items[GameModeComboBox.SelectedIndex].ToString();

            switch (selectedGameMode)
            {
                case "PvP":
                    BotLevelsText.Visible = false;
                    BotLevelsComboBox.Visible = false;

                    break;
                case "PvAI":
                case "AIvAI":
                    BotLevelsText.Visible = true;
                    BotLevelsComboBox.Visible = true;
                    break;
                default:
                    GameModeComboBox.SelectedIndex = 0;
                    break;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }
        private void toolStripContainer1_ContentPanel_Load_1(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            LogButton.Visible = true;
            SignButton.Visible = false;
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            LogButton.Visible = false;
            SignButton.Visible = true;

        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {

        }

        private void LogButton_Click(object sender, EventArgs e)
        {
            Profile foundProfile = null;
            for (int i = 0; i < profiles.profiles.Count; i++)
            {
                if (profiles.profiles[i].Login == LoginTextBox.Text && profiles.profiles[i].Password == PasswordTextBox.Text)
                {
                    foundProfile = profiles.profiles[i];
                    break;
                }
            }

            if (foundProfile == null)
            {
                MessageBox.Show("Wrong password or login!");
                LoginTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;
                return;
            }

            if(foundProfile == currentProfile)
            {
                MessageBox.Show("You are already in this account");
                LoginTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;
                return;
            }

            currentProfile = foundProfile;
            CurrentPlayerNameBox.Text = currentProfile.Login;

            LoginTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignButton_Click(object sender, EventArgs e)
        {
            Profile foundProfile = null;
            for (int i = 0; i < profiles.profiles.Count; i++)
            {
                if (profiles.profiles[i].Login == LoginTextBox.Text && profiles.profiles[i].Password == PasswordTextBox.Text)
                {
                    foundProfile = profiles.profiles[i];
                    break;
                }
            }

            if (foundProfile != null)
            {
                MessageBox.Show("This accaunt already exists");
                return;
            }

            profiles.profiles.Add(new Profile(LoginTextBox.Text, PasswordTextBox.Text));
            saveLoad.SaveProfiles(profiles);
            LoginTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
        }
    }
}