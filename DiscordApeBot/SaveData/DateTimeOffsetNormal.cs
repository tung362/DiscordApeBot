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
    public class BinaryDateTimeOffsetNormal
    {
        public long DateTimeTicks;
        public long DateTimeOffsetTicks;

        public BinaryDateTimeOffsetNormal(long newDateTimeTicks, long newDateTimeOffsetTicks)
        {
            DateTimeTicks = newDateTimeTicks;
            DateTimeOffsetTicks = newDateTimeOffsetTicks;
        }
    }

    class DateTimeOffsetNormal
    {
        public static bool Load(string path, ref BinaryDateTimeOffsetNormal outBinaryDateTimeOffsetNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryDateTimeOffsetNormal binaryDateTimeOffsetNormalData = loadFormater.Deserialize(loadStream) as BinaryDateTimeOffsetNormal;
                loadStream.Close();

                if (binaryDateTimeOffsetNormalData != null)
                {
                    //Copy the entries
                    outBinaryDateTimeOffsetNormal = binaryDateTimeOffsetNormalData;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, BinaryDateTimeOffsetNormal Entry)
        {
            BinaryDateTimeOffsetNormal binaryDateTimeOffsetNormalData = new BinaryDateTimeOffsetNormal(Entry.DateTimeTicks, Entry.DateTimeOffsetTicks);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryDateTimeOffsetNormalData);
            saveStream.Close();
        }
    }
}
