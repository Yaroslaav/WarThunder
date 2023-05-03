using System.ComponentModel;
using System.Diagnostics;

namespace GameForms
{
    public partial class Form1 : Form
    {
        public SaveLoad saveLoad = new SaveLoad();
        public string pathToGameFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents") + "/WarThunder";
        public Profiles profiles;
        public Form1()
        {
            CreateDirectory();
            profiles = saveLoad.LoadAllProfiles();

            InitializeComponent();
            
            if(profiles.currentProfile != null)
            {
                ProfileButton.Text = profiles.currentProfile.Login;
            }
        }

        private void CreateDirectory()
        {
            Directory.CreateDirectory(pathToGameFiles);
            Directory.CreateDirectory($"{pathToGameFiles}/Saves");
        }

        private GameSettings GetAllData()
        {
            string name = NicknameField.Text == "" ? "Player" : NicknameField.Text;
            string gameMode = GameModeComboBox.Items[GameModeComboBox.SelectedIndex].ToString() == "" ? "PvAI" : GameModeComboBox.Items[GameModeComboBox.SelectedIndex].ToString();
            string botLevel = BotLevelsComboBox.Items[BotLevelsComboBox.SelectedIndex].ToString() == "" ? "0" : BotLevelsComboBox.Items[BotLevelsComboBox.SelectedIndex].ToString();

            return new GameSettings(name, gameMode, botLevel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveLoad.SaveGame(GetAllData());
            Process.Start($"{pathToGameFiles}/WarGame.exe");
            
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            //saveLoad.SaveProfiles(profiles);
                        

            //profiles.currentProfile = null;

            //saveLoad.SaveProfiles(profiles);
            base.OnClosing(e);
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
            if(profiles.currentProfile != null)
                if (foundProfile.Login == profiles.currentProfile.Login && foundProfile.Password == profiles.currentProfile.Password)
                {
                    MessageBox.Show("You are already in this account");
                    LoginTextBox.Text = string.Empty;
                    PasswordTextBox.Text = string.Empty;
                    return;
                }
            profiles.currentProfile = new Profile();
            profiles.currentProfile.SetValuesFromAnotherProfile(foundProfile);

            saveLoad.SaveProfiles(profiles);

            ProfileButton.Text = profiles.currentProfile.Login;


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

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            profiles = saveLoad.LoadAllProfiles();
            if(profiles.currentProfile != null)
            {
                MessageBox.Show($"{profiles.currentProfile.Login}\n" +
                    $"Total games: {profiles.currentProfile.GamesAmount}\n" +
                    $"Total wins: {profiles.currentProfile.WinsAmount}\n" +
                    $"Total loses: {profiles.currentProfile.LosesAmount}\n" +
                    $"Total win rounds: {profiles.currentProfile.WinRoundsAmount}\n" +
                    $"Total lose rounds: {profiles.currentProfile.LosesRoundsAmount}\n");
            }
        }
    }
}