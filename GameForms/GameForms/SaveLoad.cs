using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class SaveLoad
{
    string documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents");
    public void Save(string[] data)
    {
        StreamWriter fileW = new StreamWriter($"{documentsPath}/savegame.txt");
        for (int i = 0; i < data.Length; i++)
        {
            fileW.WriteLine(data[i]);
        }

        fileW.Close();
    }
}
