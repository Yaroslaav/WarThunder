using Newtonsoft.Json;
public class SaveLoad
{
    string documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents");
    public void SaveGame(string[] data)
    {
        StreamWriter fileW = new StreamWriter($"{documentsPath}/savegame.txt");
        for (int i = 0; i < data.Length; i++)
        {
            fileW.WriteLine(data[i]);
        }

        fileW.Close();
    }
    public void SaveProfiles(Profiles profiles)
    {
        if(profiles.currentProfile != null)
        {
            //profiles.profiles[FindProfileIndex(profiles)] = profiles.currentProfile;
        }
        if(profiles.currentProfile != null) 
        { 
            int profileIndex = FindProfileIndex(profiles, profiles.currentProfile);

            profiles.profiles[profileIndex].SetValuesFromAnotherProfile(profiles.currentProfile);
        }


        StreamWriter SW = new StreamWriter($"{documentsPath}/Profiles.json");

        string serializedData = JsonConvert.SerializeObject(profiles, Formatting.Indented);
        SW.WriteLine(serializedData);

        SW.Close();
    }
    public Profiles LoadAllProfiles()
    {
        try
        {
            return JsonConvert.DeserializeObject<Profiles>(File.ReadAllText($"{documentsPath}/Profiles.json"));
        }
        catch
        {
            if(File.ReadAllText($"{documentsPath}/Profiles.json").Length == 0)
            {
                SaveProfiles(new Profiles(new Profile[0]));

            }
            else
            {
                throw new Exception("Saved file is incorect");
            }
        }
        return JsonConvert.DeserializeObject<Profiles>(File.ReadAllText($"{documentsPath}/Profiles.json"));
    }


    public int FindProfileIndex(Profiles profiles, Profile profile)
    {
        for (int i = 0; i < profiles.profiles.Count; i++)
        {
            if (profiles.profiles[i].Login == profile.Login && profiles.profiles[i].Password == profile.Password)
            {
                return i;
            }
        }
        return 0;
    }
}
