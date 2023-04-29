
public class Profiles
{
    public List<Profile> profiles = new List<Profile>();

    public Profiles() { }
    public Profiles(Profile[] _profiles)
    {
        profiles = _profiles.ToList();
    }

}
