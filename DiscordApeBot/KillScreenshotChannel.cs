using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Audio;
using Discord.Webhook;
using DiscordApeBot.SaveData;

namespace DiscordApeBot
{
    class KillScreenshotChannel
    {
        public static async Task RequestHouseApproval(SocketCommandContext context)
        {
            await context.Message.AddReactionAsync(new Emoji(Setup.BallotCheckEmoji));
        }

        public static async Task HouseApprove(SocketReaction emoji, int amount)
        {
            SocketGuildUser user = emoji.User.Value as SocketGuildUser;
            IRole apeChieftainRole = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.ApeChieftainRole);

            //Role and permission check
            if (user.Roles.Contains(apeChieftainRole) || user.GuildPermissions.Administrator)
            {
                IMessage message = await emoji.Channel.GetMessageAsync(emoji.MessageId);
                SocketGuildUser otherUser = message.Author as SocketGuildUser;
                IRole houseOfGorillaRole = (otherUser as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfGorillaRole);
                IRole houseOfMonkeyRole = (otherUser as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfMonkeyRole);
                IRole houseOfBaboonRole = (otherUser as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfBaboonRole);
                bool inHouseOfGorilla = false;
                bool inHouseOfMonkey = false;
                bool inHouseOfBaboon = false;

                //Loaded data
                BinaryHouseNormal entry = new BinaryHouseNormal(0, 0, 0);
                //Load
                HouseNormal.Load(Setup.HousePath, ref entry);
                //Modify
                if (otherUser.Roles.Contains(houseOfGorillaRole))
                {
                    entry.GorillaPoints += amount;
                    inHouseOfGorilla = true;
                }
                if (otherUser.Roles.Contains(houseOfMonkeyRole))
                {
                    entry.MonkeyPoints += amount;
                    inHouseOfMonkey = true;
                }
                if (otherUser.Roles.Contains(houseOfBaboonRole))
                {
                    entry.BaboonPoints += amount;
                    inHouseOfBaboon = true;
                }
                //Save
                HouseNormal.Save(Setup.HousePath, entry);

                if (inHouseOfGorilla || inHouseOfMonkey || inHouseOfBaboon)
                {
                    if (inHouseOfGorilla) await AnnounceGainLoseToHouse(emoji, message, Setup.GorillaWebhookClient, true, amount, entry.GorillaPoints);
                    if (inHouseOfMonkey) await AnnounceGainLoseToHouse(emoji, message, Setup.MonkeyWebhookClient, true, amount, entry.MonkeyPoints);
                    if (inHouseOfBaboon) await AnnounceGainLoseToHouse(emoji, message, Setup.BaboonWebhookClient, true, amount, entry.BaboonPoints);
                }
                else
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    eb.Title = "⚠️ **User is not in a house!**";
                    eb.WithColor(Color.Gold);
                    await emoji.Channel.SendMessageAsync("", false, eb.Build());
                }
                await (message as IUserMessage).RemoveAllReactionsAsync();
                await (message as IUserMessage).AddReactionAsync(new Emoji(Setup.GreenCheckEmoji));
            }
        }

        public static async Task AnnounceGainLoseToHouse(SocketReaction emoji, IMessage message, DiscordWebhookClient houseWebhook, bool gain, int amount, int totalPoints)
        {
            SocketGuildUser user = emoji.User.Value as SocketGuildUser;

            EmbedBuilder eb = new EmbedBuilder();
            if (gain)
            {
                eb.Title = "🎊House points gained!";
                string text = "**" + message.Author.Mention + " gained the house __" + amount + " points!__ Current total: __" + totalPoints + " points.__**";
                eb.WithDescription(text);
                eb.WithColor(Color.Green);
            }
            else
            {
                eb.Title = "😡House points lossed!";
                string text = "**" + message.Author.Mention + " lossed the house __" + amount + " points!__ Current total: __" + totalPoints + " points.__**";
                eb.WithDescription(text);
                eb.WithColor(Color.Red);
            }
            List<Embed> embeds = new List<Embed>();
            embeds.Add(eb.Build());
            await houseWebhook.SendMessageAsync("", false, embeds);
        }
    }
}
