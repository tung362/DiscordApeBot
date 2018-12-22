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
    public class BinaryStringNormal
    {
        public string StringNormal;

        public BinaryStringNormal(string newStringNormal)
        {
            StringNormal = newStringNormal;
        }
    }

    class StringNormal
    {
        public static bool Load(string path, ref string outBinaryStringNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryStringNormal binaryStringNormalData = loadFormater.Deserialize(loadStream) as BinaryStringNormal;
                loadStream.Close();

                if(binaryStringNormalData != null)
                {
                    //Copy the entries
                    outBinaryStringNormal = binaryStringNormalData.StringNormal;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, string Entry)
        {
            BinaryStringNormal binaryStringNormalData = new BinaryStringNormal(Entry);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryStringNormalData);
            saveStream.Close();
        }
    }
}
