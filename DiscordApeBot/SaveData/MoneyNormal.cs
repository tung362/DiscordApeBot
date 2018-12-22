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
    public class BinaryMoneyNormal
    {
        public int Money;
        public int CurrentBonusDay;
        public long DateTimeTicks;
        public long DateTimeOffsetTicks;

        public BinaryMoneyNormal(int newMoney, int newCurrentBonusDay, long newDateTimeTicks, long newDateTimeOffsetTicks)
        {
            Money = newMoney;
            CurrentBonusDay = newCurrentBonusDay;
            DateTimeTicks = newDateTimeTicks;
            DateTimeOffsetTicks = newDateTimeOffsetTicks;
        }
    }

    class MoneyNormal
    {
        public static bool Load(string path, ref BinaryMoneyNormal outBinaryMoneyNormal)
        {
            //Load
            if (File.Exists(path))
            {
                BinaryFormatter loadFormater = new BinaryFormatter();
                //Open file
                FileStream loadStream = new FileStream(path, FileMode.Open);

                //Obtain the saved data
                BinaryMoneyNormal binaryMoneyNormalData = loadFormater.Deserialize(loadStream) as BinaryMoneyNormal;
                loadStream.Close();

                if (binaryMoneyNormalData != null)
                {
                    //Copy the entries
                    outBinaryMoneyNormal = binaryMoneyNormalData;
                    return true;
                }
            }
            //If loaded wrong file or file does not exist
            return false;
        }

        public static void Save(string path, BinaryMoneyNormal Entry)
        {
            BinaryMoneyNormal binaryMoneyNormalData = new BinaryMoneyNormal(Entry.Money, Entry.CurrentBonusDay, Entry.DateTimeTicks, Entry.DateTimeOffsetTicks);
            BinaryFormatter saveFormater = new BinaryFormatter();
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            //Write to file
            saveFormater.Serialize(saveStream, binaryMoneyNormalData);
            saveStream.Close();
        }
    }
}
