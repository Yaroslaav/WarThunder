using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

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
