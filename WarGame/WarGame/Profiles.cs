
public class Profiles
{
    public List<Profile> profiles = new List<Profile>();
    public Profile currentProfile;

    public Profiles() { }
    public Profiles(Profile[] _profiles)
    {
        profiles = _profiles.ToList();
    }

}
