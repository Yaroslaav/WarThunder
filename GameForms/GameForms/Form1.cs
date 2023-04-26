using System.Diagnostics;

namespace GameForms
{
    public partial class Form1 : Form
    {
        SaveLoad saveLoad = new SaveLoad();
        public Form1()
        {
            InitializeComponent();
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
            string selectedGameMode = GameModeComboBox.Items[GameModeComboBox.SelectedIndex].ToString();
            saveLoad.Save(GetAllData());
                    Process.Start("WarGame.exe");
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

    }
}