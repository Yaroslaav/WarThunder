
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class SaveLoad
{
    string documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents");
    public string[] Load()
    {
        string[] loadedData = new string[3];
        //FileStream fileStream = new FileStream($"{documentsPath}/savegame.xml");
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
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Profiles));

        using (FileStream fs = new FileStream($"{documentsPath}/Profiles.xml", FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, profiles);

        }
    }
    public Profiles LoadAllProfiles()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Profiles));

        using (FileStream fs = new FileStream($"{documentsPath}/Profiles.xml", FileMode.OpenOrCreate))
        {
            try
            {
                Profiles profiles = xmlSerializer.Deserialize(fs) as Profiles;
                return profiles;
            }
            catch
            {
                return new Profiles(new Profile[0]);
            }
        }
    }



}
