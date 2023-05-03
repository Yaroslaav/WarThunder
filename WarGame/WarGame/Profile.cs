[Serializable]
public class Profile
{
    public string Login { get; set; }
    public string Password { get; set; }
    public int WinsAmount { get; set; }
    public int LosesAmount { get; set; }
    public int GamesAmount { get; set; }
    public int WinRoundsAmount { get; set; }
    public int LosesRoundsAmount { get; set; }

    public Profile() { }
    public Profile(string login, string password)
    {
        Login = login;
        Password = password;
    }
    public Profile CopyValues()
    {
        return (Profile)this.MemberwiseClone();
    }


}
