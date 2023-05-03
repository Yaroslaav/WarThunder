using Newtonsoft.Json;
public class SaveLoad
{
    string documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents");
    public string[] Load()
    {
        string[] loadedData = new string[3];
        try
        {
            StreamReader fileR = new StreamReader($"{documentsPath}/savegame.txt");

            loadedData[0] = fileR.ReadLine();
            loadedData[1] = fileR.ReadLine();
            loadedData[2] = fileR.ReadLine();
            fileR.Close();
        }
        catch 
        {
            SaveDefaultValues();
        }

        return loadedData;

    }
    public void SaveDefaultValues()
    {
        StreamWriter fileW = new StreamWriter($"{documentsPath}/savegame.txt");
        fileW.WriteLine("Player");
        fileW.WriteLine("PvP");
        fileW.WriteLine("0");

        fileW.Close();
    }

    public void SaveProfiles(Profiles profiles)
    {
        if (profiles.currentProfile != null)
        {
            //profiles.profiles[FindProfileIndex(profiles)] = profiles.currentProfile;
        }

        int profileIndex = FindProfileIndex(profiles, profiles.currentProfile);

        profiles.profiles[profileIndex].SetValuesFromAnotherProfile(profiles.currentProfile);

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
            if (File.ReadAllText($"{documentsPath}/Profiles.json").Length == 0)
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
