using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Audio;
using Discord.Webhook;
using DiscordApeBot.SaveData;

namespace DiscordApeBot.Commands
{
    public class HouseCommands : ModuleBase<SocketCommandContext>
    {
        [Command("houseup")]
        public async Task HouseUp(SocketUser mentionedUser, int amount)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketGuildUser otherUser = mentionedUser as SocketGuildUser;
                IRole houseOfGorillaRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfGorillaRole);
                IRole houseOfMonkeyRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfMonkeyRole);
                IRole houseOfBaboonRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfBaboonRole);
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

                if(inHouseOfGorilla || inHouseOfMonkey || inHouseOfBaboon)
                {
                    if (inHouseOfGorilla) await AnnounceGainLoseToHouse(Context, Setup.GorillaWebhookClient, true, amount, entry.GorillaPoints);
                    if (inHouseOfMonkey) await AnnounceGainLoseToHouse(Context, Setup.MonkeyWebhookClient, true, amount, entry.MonkeyPoints);
                    if (inHouseOfBaboon) await AnnounceGainLoseToHouse(Context, Setup.BaboonWebhookClient, true, amount, entry.BaboonPoints);
                }
                else
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    eb.Title = "⚠️ **User is not in a house!**";
                    eb.WithColor(Color.Gold);
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
            }
            await Context.Message.DeleteAsync();
        }

        [Command("housedown")]
        public async Task HouseDown(SocketUser mentionedUser, int amount)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketGuildUser otherUser = mentionedUser as SocketGuildUser;
                IRole houseOfGorillaRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfGorillaRole);
                IRole houseOfMonkeyRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfMonkeyRole);
                IRole houseOfBaboonRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == Setup.HouseOfBaboonRole);
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
                    entry.GorillaPoints -= amount;
                    inHouseOfGorilla = true;
                }
                if (otherUser.Roles.Contains(houseOfMonkeyRole))
                {
                    entry.MonkeyPoints -= amount;
                    inHouseOfMonkey = true;
                }
                if (otherUser.Roles.Contains(houseOfBaboonRole))
                {
                    entry.BaboonPoints -= amount;
                    inHouseOfBaboon = true;
                }
                //Save
                HouseNormal.Save(Setup.HousePath, entry);

                if (inHouseOfGorilla || inHouseOfMonkey || inHouseOfBaboon)
                {
                    if (inHouseOfGorilla) await AnnounceGainLoseToHouse(Context, Setup.GorillaWebhookClient, false, amount, entry.GorillaPoints);
                    if (inHouseOfMonkey) await AnnounceGainLoseToHouse(Context, Setup.MonkeyWebhookClient, false, amount, entry.MonkeyPoints);
                    if (inHouseOfBaboon) await AnnounceGainLoseToHouse(Context, Setup.BaboonWebhookClient, false, amount, entry.BaboonPoints);
                }
                else
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    eb.Title = "⚠️ **User is not in a house!**";
                    eb.WithColor(Color.Gold);
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
            }
            await Context.Message.DeleteAsync();
        }

        [Command("housereset")]
        public async Task HouseReset()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                //Loaded data
                BinaryHouseNormal entry = new BinaryHouseNormal(0, 0, 0);
                //Load
                HouseNormal.Load(Setup.HousePath, ref entry);
                //Modify
                entry.GorillaPoints = 0;
                entry.MonkeyPoints = 0;
                entry.BaboonPoints = 0;
                //Save
                HouseNormal.Save(Setup.HousePath, entry);

                await AnnounceResetToHouse(Context, Setup.GorillaWebhookClient, entry.GorillaPoints);
                await AnnounceResetToHouse(Context, Setup.MonkeyWebhookClient, entry.MonkeyPoints);
                await AnnounceResetToHouse(Context, Setup.BaboonWebhookClient, entry.BaboonPoints);
            }
            await Context.Message.DeleteAsync();
        }

        public async Task AnnounceGainLoseToHouse(SocketCommandContext context, DiscordWebhookClient houseWebhook, bool gain, int amount, int totalPoints)
        {
            EmbedBuilder eb = new EmbedBuilder();
            if (gain)
            {
                eb.Title = "🎊House points gained!";
                string text = "**" + context.User.Mention + " gained the house __" + amount + " points!__ Current total: __" + totalPoints + " points.__**";
                eb.WithDescription(text);
                eb.WithColor(Color.Green);
            }
            else
            {
                eb.Title = "😡House points lossed!";
                string text = "**" + context.User.Mention + " lossed the house __" + amount + " points!__ Current total: __" + totalPoints + " points.__**";
                eb.WithDescription(text);
                eb.WithColor(Color.Red);
            }
            List<Embed> embeds = new List<Embed>();
            embeds.Add(eb.Build());
            await houseWebhook.SendMessageAsync("", false, embeds);
        }

        public async Task AnnounceResetToHouse(SocketCommandContext context, DiscordWebhookClient houseWebhook, int totalPoints)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.Title = "ℹ️House points has reset!";
            string text = "**let us begin anew. Current total: __" + totalPoints + " points.__**";
            eb.WithDescription(text);
            eb.WithColor(Color.Green);

            List<Embed> embeds = new List<Embed>();
            embeds.Add(eb.Build());
            await houseWebhook.SendMessageAsync("", false, embeds);
        }
    }
}
