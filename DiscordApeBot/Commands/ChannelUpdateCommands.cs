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
using DiscordApeBot.SaveData;

namespace DiscordApeBot.Commands
{
    public class ChannelUpdateCommands : ModuleBase<SocketCommandContext>
    {
        [Command("updatewelcome")]
        public async Task UpdateWelcome()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.WelcomeChannelID);

                EmbedBuilder eb = new EmbedBuilder();
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.WelcomePicturePath);
                eb.Title = "**Welcome to " + Context.Guild.Name + "!**";
                string text = 
                    "__*We're an unofficial community hub dedicated to Cosmic Break and other games.*__" + "\n" +
                    "\n" +
                    "\n" +
                    "**ℹ️ Information**" + "\n" +
                    "__**" + "Please enter your invitation code" + "**__" + "\n" +
                    "▫️ Invitation code can be obtained from your __***recruiter***__.";
                eb.WithDescription(text);
                eb.WithColor(Color.Green);
                await (channel as ISocketMessageChannel).SendMessageAsync("", false, eb.Build());
            }
            await Context.Message.DeleteAsync();
        }

        [Command("updatehouse")]
        public async Task UpdateHouse()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.HouseChannelID);

                EmbedBuilder eb = new EmbedBuilder();
                eb.Title = Setup.GorillaEmoji + Setup.MonkeyEmoji + Setup.BaboonEmoji + "Apewarts Houses";
                string text =
                    "__**" + "Press on one of the emojis to join a house." + "**__" + "\n" +
                    "▫️ Each house receive points by doing events." + "\n" +
                    "▫️ The house with the most points receive __***rewards***__ and or __***benefits***__." + "\n" +
                    "▫️ Yes we like to be edgy 😉";
                eb.WithDescription(text);
                eb.WithColor(255, 255, 255);
                await (channel as ISocketMessageChannel).SendMessageAsync("", false, eb.Build());

                var gorillaBannerMessage = await (channel as ISocketMessageChannel).SendFileAsync(Setup.GorillaBannerPicturePath);
                IEmote gorillaEmote = Emote.Parse(Setup.GorillaEmoji);
                await gorillaBannerMessage.AddReactionAsync(gorillaEmote);

                var monkeyBannerMessage = await (channel as ISocketMessageChannel).SendFileAsync(Setup.MonkeyBannerPicturePath);
                IEmote monkeyEmote = Emote.Parse(Setup.MonkeyEmoji);
                await monkeyBannerMessage.AddReactionAsync(monkeyEmote);

                var baboonBannerMessage = await (channel as ISocketMessageChannel).SendFileAsync(Setup.BaboonBannerPicturePath);
                IEmote baboonEmote = Emote.Parse(Setup.BaboonEmoji);
                await baboonBannerMessage.AddReactionAsync(baboonEmote);

                eb.Title = "⚠️ __**Press on one of the emojis to join a house**__ " + Setup.GorillaEmoji + Setup.MonkeyEmoji + Setup.BaboonEmoji;
                string text2 = "";
                eb.WithDescription(text2);
                eb.WithColor(Color.Gold);
                var infoMessage = await(channel as ISocketMessageChannel).SendMessageAsync("", false, eb.Build());
                await infoMessage.AddReactionAsync(gorillaEmote);
                await infoMessage.AddReactionAsync(monkeyEmote);
                await infoMessage.AddReactionAsync(baboonEmote);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("updaterules")]
        public async Task UpdateRules()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.RulesChannelID);
                SocketTextChannel killScreenshotsChannel = Context.Guild.GetTextChannel(Setup.KillScreenshotsChannelID);
                SocketTextChannel nsfwChannel = Context.Guild.GetTextChannel(Setup.NSFWChannelID);
                SocketTextChannel spamChannel = Context.Guild.GetTextChannel(Setup.SpamChannelID);
                SocketTextChannel skinsChannel = Context.Guild.GetTextChannel(Setup.SkinsChannelID);
                SocketTextChannel buildsChannel = Context.Guild.GetTextChannel(Setup.BuildsChannelID);
                SocketTextChannel voteChannel = Context.Guild.GetTextChannel(Setup.VoteChannelID);
                SocketTextChannel huntListChannel = Context.Guild.GetTextChannel(Setup.HuntListChannelID);
                IRole recruiterRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == Setup.RecruiterRole);

                await (channel as ISocketMessageChannel).SendFileAsync(Setup.RuleBannerPicturePath);
                string text =
                    "▫️ Do not fight on discord. If you have a problem with someone then deal with it on Cosmic Break." + "\n" +
                    "▫️ Do not hunt each other while there is a hunting session in progress." + "\n" +
                    "▫️ Hunt kill screenshots only to " + killScreenshotsChannel.Mention + "." + "\n" +
                    "▫️ Please keep NSFW content to " + nsfwChannel.Mention + "." + "\n" +
                    "▫️ Please keep bot commands to " + spamChannel.Mention + "." + "\n" +
                    "▫️ Uploads or links only for " + skinsChannel.Mention + "." + "\n" +
                    "▫️ Cosmic Break calculator links and text only for " + buildsChannel.Mention + "." + "\n" +
                    "▫️ Please vote as new topics appear in " + voteChannel.Mention + ". Your vote matters!" + "\n" +
                    "▫️ If you want to invite someone to the server, ask someone with the " + recruiterRole.Mention + " role for an invite code.";
                await (channel as ISocketMessageChannel).SendMessageAsync(text);

                await (channel as ISocketMessageChannel).SendFileAsync(Setup.HouseBannerPicturePath);
                string text2 =
                    "🔹 Work together with your house mates to earn house points." + "\n" +
                    "🔹 The house with the most points receive __***rewards***__ and or __***benefits***__." + "\n" +
                    "🔹 Attending events will earn your house points." + "\n" +
                    "🔹 Killing players listed on " + huntListChannel.Mention + " will earn your house points. (Screenshot kills to " + killScreenshotsChannel.Mention + ")." + "\n" +
                    "🔹 Doing well in arena will also earn your house points. (Screenshot scores to " + killScreenshotsChannel.Mention + ")." + "\n" +
                    "🔹 Show off your house pride!";
                await (channel as ISocketMessageChannel).SendMessageAsync(text2);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("updatehuntlist")]
        public async Task UpdateHuntList()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.HuntListChannelID);

                //Loaded data
                List<string> entries = new List<string>();
                //Load
                StringArray.Load(Setup.HuntListPath, ref entries);

                EmbedBuilder eb = new EmbedBuilder();
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.HuntListBannerPicturePath);
                string text = "";
                for (int i = 0; i < entries.Count; i++)
                {
                    text += "🔸 " + entries[i] + "\n";
                }
                if (entries.Count == 0) eb.WithDescription("No one is on the hunt list!");
                else eb.WithDescription(text);
                eb.WithColor(Color.Red);
                var huntListMessage = await (channel as ISocketMessageChannel).SendMessageAsync("", false, eb.Build());
                //Save message id so the client can always track the newly generated message incase it gets deleted
                ULongNormal.Save(Setup.HuntListDynamicIDPath, huntListMessage.Id);

                EmbedBuilder eb2 = new EmbedBuilder();
                eb2.Title = "⚠️ __**Kill and troll on sight**__🗡️";
                eb2.WithColor(Color.Gold);
                var infoMessage = await (channel as ISocketMessageChannel).SendMessageAsync("", false, eb2.Build());
                await infoMessage.AddReactionAsync(new Emoji("😄"));

                string text3 = "https://vimeo.com/284759578";
                await (channel as ISocketMessageChannel).SendMessageAsync(text3);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("updatehouseofgorilla")]
        public async Task UpdateHouseOfGorilla()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.HouseOfGorillaChannelID);

                EmbedBuilder eb = new EmbedBuilder();
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.GorillaIconPicturePath);
                eb.Title = "**Welcome to the beginning of House of Gorilla chat!**";
                string text =
                    "__*Only house mates can see and talk in this chat.*__";
                eb.WithDescription(text);
                eb.WithColor(255, 255, 255);
                await (channel as ISocketMessageChannel).SendMessageAsync("", false, eb.Build());
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.GorillaBannerPicturePath);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("updatehouseofmonkey")]
        public async Task UpdateHouseOfMonkey()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.HouseOfMonkeyChannelID);

                EmbedBuilder eb = new EmbedBuilder();
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.MonkeyIconPicturePath);
                eb.Title = "**Welcome to the beginning of House of Monkey chat!**";
                string text =
                    "__*Only house mates can see and talk in this chat.*__";
                eb.WithDescription(text);
                eb.WithColor(255, 255, 255);
                await (channel as ISocketMessageChannel).SendMessageAsync("", false, eb.Build());
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.MonkeyBannerPicturePath);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("updatehouseofbaboon")]
        public async Task UpdateHouseOfBaboon()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.HouseOfBaboonChannelID);

                EmbedBuilder eb = new EmbedBuilder();
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.BaboonIconPicturePath);
                eb.Title = "**Welcome to the beginning of House of Baboon chat!**";
                string text =
                    "__*Only house mates can see and talk in this chat.*__";
                eb.WithDescription(text);
                eb.WithColor(255, 255, 255);
                await (channel as ISocketMessageChannel).SendMessageAsync("", false, eb.Build());
                await (channel as ISocketMessageChannel).SendFileAsync(Setup.BaboonBannerPicturePath);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("updatetimers")]
        public async Task UpdateTimers()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.TimerChannelID);

                //Points
                await channel.SendFileAsync(Setup.PointBannerPicturePath, "");
                EmbedBuilder pointsEB = new EmbedBuilder();
                pointsEB.WithColor(255, 255, 255);

                string gorillaTitle = Setup.GorillaEmoji + "House of Gorilla";
                string gorillaText = "`0 Points`";
                pointsEB.AddField(Tools.CreateEmbedField(gorillaTitle, gorillaText, true));

                string monkeyTitle = Setup.MonkeyEmoji + "House of Monkey";
                string monkeyText = "`0 Points`";
                pointsEB.AddField(Tools.CreateEmbedField(monkeyTitle, monkeyText, true));

                string baboonTitle = Setup.BaboonEmoji + "House of Baboon";
                string baboonText = "`0 Points`";
                pointsEB.AddField(Tools.CreateEmbedField(baboonTitle, baboonText, true));
                var pointsMessage = await channel.SendMessageAsync("", false, pointsEB.Build());

                //Save message id so the client can always track the newly generated message incase it gets deleted
                ULongNormal.Save(Setup.PointsDynamicIDPath, pointsMessage.Id);

                //Events
                await channel.SendFileAsync(Setup.EventBannerPicturePath, "");
                EmbedBuilder EventsEB = new EmbedBuilder();
                EventsEB.WithColor(255, 248, 142);

                string waitEventTitle = "Events";
                string waitEventText = "`None`";
                EventsEB.AddField(Tools.CreateEmbedField(waitEventTitle, waitEventText, true));

                string activeEventTitle = "Active events";
                string activeEventText = "`None`";
                EventsEB.AddField(Tools.CreateEmbedField(activeEventTitle, activeEventText, true));

                EventsEB.WithThumbnailUrl(Setup.EventsTimerThumbnail);

                var eventMessage = await channel.SendMessageAsync("", false, EventsEB.Build());

                //Save message id so the client can always track the newly generated message incase it gets deleted
                ULongNormal.Save(Setup.EventsDynamicIDPath, eventMessage.Id);

                //Server
                await channel.SendFileAsync(Setup.ServerBannerPicturePath, "");
                EmbedBuilder ServerEB = new EmbedBuilder();
                ServerEB.Title = "Server information";
                string serverText = "`None`";
                ServerEB.WithDescription(serverText);
                ServerEB.WithColor(158, 97, 255);

                string generalEventsTitle = "General events";
                string generalEventsText = "`None`";
                ServerEB.AddField(Tools.CreateEmbedField(generalEventsTitle, generalEventsText, true));

                string activeGeneralEventsTitle = "Active general events";
                string activeGeneralEventsText = "`None`";
                ServerEB.AddField(Tools.CreateEmbedField(activeGeneralEventsTitle, activeGeneralEventsText, true));

                string specialEventsTitle = "Special events";
                string specialEventsText = "`None`";
                ServerEB.AddField(Tools.CreateEmbedField(specialEventsTitle, specialEventsText, true));

                string activeSpecialEventsTitle = "Active special events";
                string activeSpecialEventsText = "`None`";
                ServerEB.AddField(Tools.CreateEmbedField(activeSpecialEventsTitle, activeSpecialEventsText, true));

                ServerEB.WithThumbnailUrl(Setup.ServerTimerThumbnail);
                ServerEB.WithFooter("Timers maybe inaccurate due to no enough information, please send any information you know!");

                var serverMessage = await channel.SendMessageAsync("", false, ServerEB.Build());

                //Save message id so the client can always track the newly generated message incase it gets deleted
                ULongNormal.Save(Setup.ServerDynamicIDPath, serverMessage.Id);
            }
            await Context.Message.DeleteAsync();
        }
    }
}
