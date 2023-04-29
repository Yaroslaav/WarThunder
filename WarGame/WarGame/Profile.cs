public class Profile
{
    public string Login { get; set; }
    public string Password { get; set; }
    public int WinsAmount { get; set; } = 0;
    public int LosesAmount { get; set; } = 0;
    public int GamesAmount { get; set; } = 0;
    public int WinsRoundsAmount { get; set; } = 0;
    public int LosesRoundsAmount { get; set; } = 0;

    public Profile() { }
    public Profile(string login, string password)
    {
        Login = login;
        Password = password;
    }


}
