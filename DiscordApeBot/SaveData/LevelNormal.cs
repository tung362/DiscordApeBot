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
    public class BinaryLevelNormal
    {
        public int CurrentLevel;
        public long CurrentExp;
        public long TotalExp;

        public BinaryLevelNormal(int newCurrentLevel, long newCurrentExp, long newTotalExp)
        {
            CurrentLevel = newCurrentLevel;
            CurrentExp = newCurrentExp;
            TotalExp = newTotalExp;
        }
    }

    class LevelNormal
    {
        public static bool Load(string path, ref BinaryLevelNormal outBinaryLevelNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryLevelNormal binaryLevelNormalData = loadFormater.Deserialize(loadStream) as BinaryLevelNormal;
                loadStream.Close();

                if (binaryLevelNormalData != null)
                {
                    //Copy the entries
                    outBinaryLevelNormal = binaryLevelNormalData;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, BinaryLevelNormal Entry)
        {
            BinaryLevelNormal binaryLevelNormalData = new BinaryLevelNormal(Entry.CurrentLevel, Entry.CurrentExp, Entry.TotalExp);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryLevelNormalData);
            saveStream.Close();
        }
    }
}
