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
    class Empty
    {
        public static bool Load(string path)
        {
            //Load
            if (File.Exists(path)) return true;
            else return false;
        }

        public static void Save(string path)
        {
            //Create file
            FileStream saveStream = new FileStream(path, FileMode.Create);
            saveStream.Close();
        }
    }
}
