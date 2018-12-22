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
    public class BinaryIntNormal
    {
        public int IntNormal;

        public BinaryIntNormal(int newIntNormal)
        {
            IntNormal = newIntNormal;
        }
    }

    class IntNormal
    {
        public static bool Load(string path, ref int outBinaryIntNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryIntNormal binaryIntNormalData = loadFormater.Deserialize(loadStream) as BinaryIntNormal;
                loadStream.Close();

                if (binaryIntNormalData != null)
                {
                    //Copy the entries
                    outBinaryIntNormal = binaryIntNormalData.IntNormal;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, int Entry)
        {
            BinaryIntNormal binaryIntNormalData = new BinaryIntNormal(Entry);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryIntNormalData);
            saveStream.Close();
        }
    }
}
