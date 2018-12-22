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
    public class BinaryULongNormal
    {
        public ulong ULongNormal;

        public BinaryULongNormal(ulong newULongNormal)
        {
            ULongNormal = newULongNormal;
        }
    }

    class ULongNormal
    {
        public static bool Load(string path, ref ulong outBinaryULongNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryULongNormal binaryULongNormalData = loadFormater.Deserialize(loadStream) as BinaryULongNormal;
                loadStream.Close();

                if(binaryULongNormalData != null)
                {
                    //Copy the entries
                    outBinaryULongNormal = binaryULongNormalData.ULongNormal;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, ulong Entry)
        {
            BinaryULongNormal binaryULongNormalData = new BinaryULongNormal(Entry);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryULongNormalData);
            saveStream.Close();
        }
    }
}
