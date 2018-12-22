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
    class HouseChannel
    {
        public static async Task JoinGorillaHouse(SocketReaction emoji)
        {
            SocketGuildUser user = emoji.User.Value as SocketGuildUser;
            IRole previousRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.AwakenedApeRole);
            IRole newRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.TribeMemberRole);
            IRole houseRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfGorillaRole);
            await user.AddRoleAsync(newRole);
            await user.AddRoleAsync(houseRole);
            await user.RemoveRoleAsync(previousRole);

            EmbedBuilder eb = new EmbedBuilder();
            await emoji.User.Value.SendFileAsync(Setup.GorillaIconPicturePath);
            eb.Title = "**" + user.Guild.Name + "**";
            string text =
                "You have been approved!" + "\n" +
                "Welcome to **House of Gorilla**." + "\n" +
                "\n" +
                "__Type in server chat for list of commands:__ " + Setup.Client.CurrentUser.Mention + " help";
            eb.WithDescription(text);
            eb.WithColor(Color.Green);
            await emoji.User.Value.SendMessageAsync("", false, eb.Build());
        }

        public static async Task JoinMonkeyHouse(SocketReaction emoji)
        {
            SocketGuildUser user = emoji.User.Value as SocketGuildUser;
            IRole previousRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.AwakenedApeRole);
            IRole newRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.TribeMemberRole);
            IRole houseRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfMonkeyRole);
            await user.AddRoleAsync(newRole);
            await user.AddRoleAsync(houseRole);
            await user.RemoveRoleAsync(previousRole);

            EmbedBuilder eb = new EmbedBuilder();
            await emoji.User.Value.SendFileAsync(Setup.MonkeyIconPicturePath);
            eb.Title = "**" + user.Guild.Name + "**";
            string text =
                "You have been approved!" + "\n" +
                "Welcome to **House of Monkey**." + "\n" +
                "\n" +
                "__Type in server chat for list of commands:__ " + Setup.Client.CurrentUser.Mention + " help";
            eb.WithDescription(text);
            eb.WithColor(Color.Green);
            await emoji.User.Value.SendMessageAsync("", false, eb.Build());
        }

        public static async Task JoinBaboonHouse(SocketReaction emoji)
        {
            SocketGuildUser user = emoji.User.Value as SocketGuildUser;
            IRole previousRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.AwakenedApeRole);
            IRole newRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.TribeMemberRole);
            IRole houseRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfBaboonRole);
            await user.AddRoleAsync(newRole);
            await user.AddRoleAsync(houseRole);
            await user.RemoveRoleAsync(previousRole);

            EmbedBuilder eb = new EmbedBuilder();
            await emoji.User.Value.SendFileAsync(Setup.BaboonIconPicturePath);
            eb.Title = "**" + user.Guild.Name + "**";
            string text =
                "You have been approved!" + "\n" +
                "Welcome to **House of Baboon**." + "\n" +
                "\n" +
                "__Type in server chat for list of commands:__ " + Setup.Client.CurrentUser.Mention + " help";
            eb.WithDescription(text);
            eb.WithColor(Color.Green);
            await emoji.User.Value.SendMessageAsync("", false, eb.Build());
        }
    }
}
