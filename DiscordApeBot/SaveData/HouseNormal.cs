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
    public class BinaryHouseNormal
    {
        public int GorillaPoints;
        public int MonkeyPoints;
        public int BaboonPoints;

        public BinaryHouseNormal(int newGorillaPoints, int newMonkeyPoints, int newBaboonPoints)
        {
            GorillaPoints = newGorillaPoints;
            MonkeyPoints = newMonkeyPoints;
            BaboonPoints = newBaboonPoints;
        }
    }

    class HouseNormal
    {
        public static bool Load(string path, ref BinaryHouseNormal outBinaryHouseNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryHouseNormal binaryHouseNormalData = loadFormater.Deserialize(loadStream) as BinaryHouseNormal;
                loadStream.Close();

                if(binaryHouseNormalData != null)
                {
                    //Copy the entries
                    outBinaryHouseNormal = binaryHouseNormalData;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, BinaryHouseNormal Entry)
        {
            BinaryHouseNormal binaryHouseNormalData = new BinaryHouseNormal(Entry.GorillaPoints, Entry.MonkeyPoints, Entry.BaboonPoints);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryHouseNormalData);
            saveStream.Close();
        }
    }
}
