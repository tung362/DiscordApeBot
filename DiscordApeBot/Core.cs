using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using DiscordApeBot.SaveData;


namespace DiscordApeBot
{
    class Core
    {
        private static CommandService Service;

        public static async Task CoreStart()
        {
            Service = new CommandService();

            await Service.AddModulesAsync(Assembly.GetEntryAssembly());

            Setup.Client.UserJoined += HandleUserJoinedAsync;
            Setup.Client.MessageReceived += HandleCommandAsync;
            Setup.Client.ReactionAdded += HandleReactionAdded;
            Setup.Client.Ready += HandleReady;
        }

        static async Task HandleReady()
        {
            await TimerChannel.PeriodicUpdate();
        }

        static async Task HandleUserJoinedAsync(SocketGuildUser user)
        {
            IRole newRole = user.Guild.Roles.FirstOrDefault(x => x.Name == Setup.UnknownAnimalRole);
            await user.AddRoleAsync(newRole);
        }

        static async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(Setup.Client, msg);

            //If the message recieved isn't a bot
            if (!context.User.IsBot && !context.IsPrivate)
            {
                bool isCommand = false;
                //Welcome channel
                if (context.Channel.Id == Setup.WelcomeChannelID)
                {
                    //Check for valid invitation code
                    if (Empty.Load(Setup.InviteCodesPath + "/" + context.Message.Content))
                    {
                        await WelcomeChannel.Approved(context);
                        FileInfo codeFile = new FileInfo(Setup.InviteCodesPath + "/" + context.Message.Content);
                        await Tools.DeleteFileAsync(codeFile);
                    }
                    await context.Message.DeleteAsync();
                }
                //Kill screenshot channel
                else if (context.Channel.Id == Setup.KillScreenshotsChannelID) await KillScreenshotChannel.RequestHouseApproval(context);
                else
                {
                    int argPos = 0;
                    if (msg.HasMentionPrefix(Setup.Client.CurrentUser, ref argPos))
                    {
                        var result = await Service.ExecuteAsync(context, argPos);

                        if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                        {

                        }

                        if(result.IsSuccess) isCommand = true;
                    }
                }

                //Levels
                if (!isCommand
                    && context.Channel.Id != Setup.WelcomeChannelID)
                {
                    await Level.GainExperience(context);
                }
            }
        }

        static async Task HandleReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction emoji)
        {
            //If the reaction recieved isn't a bot
            if (!emoji.User.Value.IsBot)
            {
                //House channel
                if (channel.Id == Setup.HouseChannelID)
                {
                    if (emoji.Emote.ToString() == Setup.GorillaEmoji) await HouseChannel.JoinGorillaHouse(emoji);
                    else if (emoji.Emote.ToString() == Setup.MonkeyEmoji) await HouseChannel.JoinMonkeyHouse(emoji);
                    else if (emoji.Emote.ToString() == Setup.BaboonEmoji) await HouseChannel.JoinBaboonHouse(emoji);
                }
                //Kill screenshot channel
                else if (channel.Id == Setup.KillScreenshotsChannelID)
                {
                    if (emoji.Emote.Name == new Emoji(Setup.BallotCheckEmoji).Name) await KillScreenshotChannel.HouseApprove(emoji, Setup.KillScreenshotWorth);
                }
                //Vote channel
                else if (channel.Id == Setup.VoteChannelID)
                {
                    if (emoji.Emote.Name == new Emoji(Setup.ThumbsUpEmoji).Name) await VoteChannel.UpdateVote(emoji, true);
                    else if(emoji.Emote.Name == new Emoji(Setup.ThumbsDownEmoji).Name) await VoteChannel.UpdateVote(emoji, false);
                }
            }
        }
    }
}
