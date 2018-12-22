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
    public class ChannelModifyCommands : ModuleBase<SocketCommandContext>
    {
        [Command("addtohuntlist")]
        public async Task AddToHuntList(string targetName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                //Loaded data
                List<string> entries = new List<string>();
                //Load
                StringArray.Load(Setup.HuntListPath, ref entries);
                //Modify
                entries.Add(targetName);
                //Save
                StringArray.Save(Setup.HuntListPath, entries);

                await ModifyHuntList(Context, entries);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("removefromhuntlist")]
        public async Task RemoveFromHuntList(string targetName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                //Loaded data
                List<string> entries = new List<string>();
                //Load
                StringArray.Load(Setup.HuntListPath, ref entries);
                //Modify
                for(int i = entries.Count - 1; i > -1; i--)
                {
                    if(entries[i] == targetName)
                    {
                        entries.RemoveAt(i);
                        break;
                    }
                }
                //Save
                StringArray.Save(Setup.HuntListPath, entries);

                await ModifyHuntList(Context, entries);
            }
            await Context.Message.DeleteAsync();
        }

        [Command("clearhuntlist")]
        public async Task ClearHuntList()
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                //Loaded data
                List<string> entries = new List<string>();
                //Save
                StringArray.Save(Setup.HuntListPath, entries);

                await ModifyHuntList(Context, entries);
            }
            await Context.Message.DeleteAsync();
        }

        public async Task ModifyHuntList(SocketCommandContext context, List<string> entries)
        {
            //Load dynamic channel id
            ulong loadedChannelID = 0;
            if(ULongNormal.Load(Setup.HuntListDynamicIDPath, ref loadedChannelID))
            {
                SocketTextChannel channel = context.Guild.GetTextChannel(Setup.HuntListChannelID);
                IMessage message = await channel.GetMessageAsync(loadedChannelID);

                EmbedBuilder eb = new EmbedBuilder();
                string text = "";
                for (int i = 0; i < entries.Count; i++)
                {
                    text += "🔸 " + entries[i] + "\n";
                }
                if (entries.Count == 0) eb.WithDescription("No one is on the hunt list!");
                else eb.WithDescription(text);
                eb.WithColor(Color.Red);

                await (message as IUserMessage).ModifyAsync(msg =>
                {
                    msg.Embed = eb.Build();
                });
            }
        }
    }
}
