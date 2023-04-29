public class Profile
{
    public string Login { get; set; }
    public string Password { get; set; }

    public Profile() { }
    public Profile(string login, string password)
    {
        Login = login;
        Password = password;
    }

}
