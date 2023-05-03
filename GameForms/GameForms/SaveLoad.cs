using Newtonsoft.Json;
public class SaveLoad
{
    string pathToGameFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents") + "/WarThunder";
    public void SaveGame(GameSettings gameSettings)
    {
        StreamWriter SW = new StreamWriter($"{pathToGameFiles}/Saves/GameSettings.json");

        string serializedData = JsonConvert.SerializeObject(gameSettings, Formatting.Indented);
        SW.WriteLine(serializedData);

        SW.Close();

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


        StreamWriter SW = new StreamWriter($"{pathToGameFiles}/Saves/Profiles.json");

        string serializedData = JsonConvert.SerializeObject(profiles, Formatting.Indented);
        SW.WriteLine(serializedData);

        SW.Close();
    }
    public Profiles LoadAllProfiles()
    {
        try
        {
            return JsonConvert.DeserializeObject<Profiles>(File.ReadAllText($"{pathToGameFiles}/Saves/Profiles.json"));
        }
        catch
        {
            if (!File.Exists($"{pathToGameFiles}/Saves/Profiles.json"))
            {
                SaveProfiles(new Profiles(new Profile[0]));
            }
            if(File.ReadAllText($"{pathToGameFiles}/Saves/Profiles.json").Length == 0)
            {
                SaveProfiles(new Profiles(new Profile[0]));

            }
            else
            {
                return new Profiles(new Profile[0]);
            }
        }
        return JsonConvert.DeserializeObject<Profiles>(File.ReadAllText($"{pathToGameFiles}/Saves/Profiles.json"));
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
