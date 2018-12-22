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
    public class BinaryEventArray
    {
        public BinaryEvent[] Events;

        public BinaryEventArray(BinaryEvent[] newEvents)
        {
            Events = newEvents;
        }
    }

    [System.Serializable]
    public class BinaryEvent
    {
        public string EventName;
        //-1 = no type, 0 = fake inactive, 1 = fake active, 2 = daily, 3 = date
        public int EventType;
        public BinaryEventSchedule[] Schedules;

        public BinaryEvent(string newEventName, int newEventType, BinaryEventSchedule[] newSchedules)
        {
            EventName = newEventName;
            EventType = newEventType;
            Schedules = newSchedules;
        }
    }

    [System.Serializable]
    public class BinaryEventSchedule
    {
        public string StartData;
        public string EndData;
        //Military time
        public long[] StartTimes;
        public long Duration;

        public BinaryEventSchedule(string newStartData, string newEndData, long[] newStartTimes, long newDuration)
        {
            //True = has specific time, False = no specific time
            StartData = newStartData;
            EndData = newEndData;
            StartTimes = newStartTimes;
            Duration = newDuration;
        }
    }

    class EventArray
    {
        public static bool Load(string path, ref BinaryEventArray outBinaryEventArray)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryEventArray BinaryEventArrayData = loadFormater.Deserialize(loadStream) as BinaryEventArray;
                loadStream.Close();

                if (BinaryEventArrayData != null)
                {
                    //Copy the entries
                    outBinaryEventArray = BinaryEventArrayData;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, BinaryEventArray Entry)
        {
            BinaryEventArray BinaryEventArrayData = new BinaryEventArray(Entry.Events);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, BinaryEventArrayData);
            saveStream.Close();
        }
    }
}
