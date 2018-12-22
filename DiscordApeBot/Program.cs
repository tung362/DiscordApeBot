using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordApeBot
{
    class Program
    {

        static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private async Task Start()
        {
            await Setup.Login();
            await Core.CoreStart();
            await Task.Delay(-1);
        }
    }
}
