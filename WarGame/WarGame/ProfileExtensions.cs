
public static class ProfileExtensions
{
    public static Profile ShallowCopy(this Profile profile)
    {
        return profile.CopyValues();
    }
    public static void SetValuesFromAnotherProfile(this Profile profile, Profile anotherProfile)
    {
        profile.Login = anotherProfile.Login;
        profile.Password = anotherProfile.Password;
        profile.GamesAmount = anotherProfile.GamesAmount;
        profile.WinsAmount = anotherProfile.WinsAmount;
        profile.LosesAmount = anotherProfile.LosesAmount;
        profile.LosesRoundsAmount = anotherProfile.LosesRoundsAmount;
        profile.WinRoundsAmount = anotherProfile.WinRoundsAmount;
    }
    
}
