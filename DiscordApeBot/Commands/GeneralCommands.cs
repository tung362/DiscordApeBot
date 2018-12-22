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

namespace DiscordApeBot.Commands
{
    public class GeneralCommands : ModuleBase<SocketCommandContext>
    {
        [Command("clear")]
        public async Task Clear(int messageCount)
        {
            await Context.Message.DeleteAsync();

            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
            List<IMessage> filteredMessages = messages.ToList<IMessage>();

            for (int i = 0; i < filteredMessages.Count; i++)
            {
                TimeSpan difference = DateTimeOffset.Now.Subtract(filteredMessages[i].CreatedAt);
                if (filteredMessages[i].Author.Id != Context.Client.CurrentUser.Id || difference.TotalMilliseconds >= (double)1203552000)
                {
                    filteredMessages.RemoveAt(i);
                    i--;
                }
            }
            await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
        }

        [Command("clear")]
        public async Task ClearUser(SocketUser mentionedUser, int messageCount)
        {
            await Context.Message.DeleteAsync();

            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);
            roles.Add(Setup.ChatCleanerRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[1]))
            {
                IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
                List<IMessage> filteredMessages = messages.ToList<IMessage>();

                for (int i = 0; i < filteredMessages.Count; i++)
                {
                    TimeSpan difference = DateTimeOffset.Now.Subtract(filteredMessages[i].CreatedAt);
                    if (!(filteredMessages[i].Author.Id == mentionedUser.Id) || difference.TotalMilliseconds >= (double)1203552000)
                    {
                        filteredMessages.RemoveAt(i);
                        i--;
                    }
                }
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
            }
        }

        [Command("clear")]
        public async Task ClearRole(IRole role, int messageCount)
        {
            await Context.Message.DeleteAsync();

            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);
            roles.Add(Setup.ChatCleanerRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[1]))
            {
                IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
                List<IMessage> filteredMessages = messages.ToList<IMessage>();

                for (int i = 0; i < filteredMessages.Count; i++)
                {
                    TimeSpan difference = DateTimeOffset.Now.Subtract(filteredMessages[i].CreatedAt);
                    if (!(filteredMessages[i].Author as SocketGuildUser).Roles.Contains(role) || difference.TotalMilliseconds >= (double)1203552000)
                    {
                        filteredMessages.RemoveAt(i);
                        i--;
                    }
                }
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
            }
        }

        [Command("clearall")]
        public async Task ClearAll(int messageCount)
        {
            await Context.Message.DeleteAsync();

            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);
            roles.Add(Setup.ChatCleanerRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[1]))
            {
                IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
                List<IMessage> filteredMessages = messages.ToList<IMessage>();

                for (int i = 0; i < filteredMessages.Count; i++)
                {
                    TimeSpan difference = DateTimeOffset.Now.Subtract(filteredMessages[i].CreatedAt);
                    if (difference.TotalMilliseconds >= (double)1203552000)
                    {
                        filteredMessages.RemoveAt(i);
                        i--;
                    }
                }
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
            }
        }

        [Command("cleardm")]
        public async Task ClearDM(int messageCount)
        {
            await Context.Message.DeleteAsync();

            IDMChannel dmChannel = await Context.Message.Author.GetOrCreateDMChannelAsync();
            IEnumerable<IMessage> messages = await dmChannel.GetMessagesAsync(messageCount).FlattenAsync();
            List<IMessage> filteredMessages = messages.ToList<IMessage>();

            for (int i = 0; i < filteredMessages.Count; i++)
            {
                TimeSpan difference = DateTimeOffset.Now.Subtract(filteredMessages[i].CreatedAt);
                if (filteredMessages[i].Author.Id == Context.User.Id || difference.TotalMilliseconds >= (double)1203552000)
                {
                    filteredMessages.RemoveAt(i);
                    i--;
                }
                else await filteredMessages[i].DeleteAsync();
            }
        }
    }
}
