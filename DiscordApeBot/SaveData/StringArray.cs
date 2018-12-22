using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DiscordApeBot.SaveData
{
    [System.Serializable]
    public class BinaryStringArray
    {
        public string[] StringArray;

        public BinaryStringArray(string[] newStringArray)
        {
            StringArray = new string[newStringArray.Length];
            for (int i = 0; i < StringArray.Length; i++) StringArray[i] = newStringArray[i];
        }
    }

    class StringArray
    {
        public static bool Load(string path, ref List<string> outBinaryStringArray)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryStringArray binaryStringArrayData = loadFormater.Deserialize(loadStream) as BinaryStringArray;
                loadStream.Close();

                //Copy the entries
                if(binaryStringArrayData != null)
                {
                    List<string> entries = new List<string>();
                    for (int i = 0; i < binaryStringArrayData.StringArray.Length; i++) entries.Add(binaryStringArrayData.StringArray[i]);
                    outBinaryStringArray = entries;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, List<string> Entries)
        {
            BinaryStringArray binaryStringArrayData = new BinaryStringArray(Entries.ToArray());
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryStringArrayData);
            saveStream.Close();
        }
    }
}
