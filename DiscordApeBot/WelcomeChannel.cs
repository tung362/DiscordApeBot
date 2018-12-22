using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Audio;

namespace DiscordApeBot
{
    class WelcomeChannel
    {
        public static async Task Approved(SocketCommandContext context)
        {
            SocketGuildUser user = context.User as SocketGuildUser;
            IRole previousRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.UnknownAnimalRole);
            IRole newRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.AwakenedApeRole);
            await user.AddRoleAsync(newRole);
            await user.RemoveRoleAsync(previousRole);
        }
    }
}
