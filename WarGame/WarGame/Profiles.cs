[Serializable]
public class Profiles
{
    public List<Profile> profiles = new List<Profile>();
    public Profile currentProfile { get; 
        set; }

    public Profiles() { }
    public Profiles(Profile[] _profiles)
    {
        profiles = _profiles.ToList();
    }

}
