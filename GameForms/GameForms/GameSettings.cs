[Serializable]
public class GameSettings
{
    public string PlayerName { get; set; }
    public string GameMode { get; set; }
    public string BotDifficulty { get; set; }

    public GameSettings() { }

    public GameSettings(string PlayerName, string GameMode, string BotDifficulty)
    {
        this.PlayerName = PlayerName;
        this.GameMode = GameMode;
        this.BotDifficulty = BotDifficulty;
    }
}
