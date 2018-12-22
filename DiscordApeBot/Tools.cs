using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Audio;

namespace DiscordApeBot
{
    class Tools
    {
        public static long GetExpForLevel(int level, double growthModifier)
        {
            return (long)((level * 50) * (level * growthModifier));
        }

        public static Task DeleteFileAsync(FileInfo fileToDelete)
        {
            return Task.Factory.StartNew(() => fileToDelete.Delete());
        }

        public static string GenerateUniqueId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static string GetMention(SocketCommandContext context, ulong userID)
        {
            SocketGuildUser user = context.Guild.GetUser(userID);
            string mention = "<User left server>";
            if (user != null) mention = user.Mention;
            return mention;
        }

        public static string GetNickname(SocketCommandContext context, ulong userID)
        {
            SocketGuildUser user = context.Guild.GetUser(userID);
            string name = "<User left server>";
            if (user != null)
            {
                name = user.Nickname;
                if (name == null) name = user.Username;
            }
            return name;
        }

        public static EmbedFieldBuilder CreateEmbedField(string title, string discription, bool inline)
        {
            EmbedFieldBuilder ebf = new EmbedFieldBuilder();
            ebf.WithName(title);
            ebf.WithValue(discription);
            ebf.WithIsInline(inline);
            return ebf;
        }

        public static EmbedBuilder CreateHelpInfo(string title, Color color, string description, List<string> usage, List<string> examples)
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.Title = title;
            eb.WithColor(color);

            eb.AddField(CreateEmbedField("Description", description, false));

            string usageText = "";
            for (int i = 0; i < usage.Count; i++) usageText += usage[i] + "\n";
            eb.AddField(CreateEmbedField("Usage", usageText, false));

            string examplesText = "";
            for (int i = 0; i < examples.Count; i++) examplesText += examples[i] + "\n";
            eb.AddField(CreateEmbedField("Examples", examplesText, false));
            return eb;
        }

        public static async Task<bool> RoleCheck(SocketCommandContext context, string roleName, bool sendAccessDeniedMessage)
        {
            SocketGuildUser user = context.User as SocketGuildUser;
            IRole role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == roleName);

            if (user.Roles.Contains(role) || user.GuildPermissions.Administrator) return true;
            else
            {
                if(sendAccessDeniedMessage)
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    eb.Title = "🚫 Access Denied";
                    string text =
                        user.Mention + " " + "Role Required:" + "\n" +
                        "```" + "\n" +
                        role.Name + "\n" +
                        "```";
                    eb.WithDescription(text);
                    eb.WithColor(Color.Red);
                    await context.Channel.SendMessageAsync("", false, eb.Build());
                }
                return false;
            }
        }

        public static async Task<bool> RolesCheck(SocketCommandContext context, List<string> roleNames, bool sendAccessDeniedMessage, string defaultRoleNameRequired)
        {
            SocketGuildUser user = context.User as SocketGuildUser;

            bool hasRole = false;
            for (int i = 0; i < roleNames.Count; i++)
            {
                IRole role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == roleNames[i]);
                if (user.Roles.Contains(role))
                {
                    hasRole = true;
                    break;
                }
            }

            if (hasRole || user.GuildPermissions.Administrator) return true;
            else
            {
                if (sendAccessDeniedMessage)
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    eb.Title = "🚫 Access Denied";
                    string text =
                        user.Mention + " " + "Role Required:" + "\n" +
                        "```" + "\n" +
                        defaultRoleNameRequired + "\n" +
                        "```";
                    eb.WithDescription(text);
                    eb.WithColor(Color.Red);
                    await context.Channel.SendMessageAsync("", false, eb.Build());
                }
                return false;
            }
        }
    }
}
