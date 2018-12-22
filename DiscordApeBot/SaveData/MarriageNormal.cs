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
    public class BinaryMarriageNormal
    {
        public ulong SpouseID;
        public long DateTimeTicks;
        public long DateTimeOffsetTicks;

        public BinaryMarriageNormal(ulong newSpouseID, long newDateTimeTicks, long newDateTimeOffsetTicks)
        {
            SpouseID = newSpouseID;
            DateTimeTicks = newDateTimeTicks;
            DateTimeOffsetTicks = newDateTimeOffsetTicks;
        }
    }

    class MarriageNormal
    {
        public static bool Load(string path, ref BinaryMarriageNormal outBinaryMarriageNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryMarriageNormal binaryMarriageNormalData = loadFormater.Deserialize(loadStream) as BinaryMarriageNormal;
                loadStream.Close();

                if (binaryMarriageNormalData != null)
                {
                    //Copy the entries
                    outBinaryMarriageNormal = binaryMarriageNormalData;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, BinaryMarriageNormal Entry)
        {
            BinaryMarriageNormal binaryMarriageNormalData = new BinaryMarriageNormal(Entry.SpouseID, Entry.DateTimeTicks, Entry.DateTimeOffsetTicks);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryMarriageNormalData);
            saveStream.Close();
        }
    }
}
