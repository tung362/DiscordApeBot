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
    public class SecurityCommands : ModuleBase<SocketCommandContext>
    {
        [Command("invite")]
        public async Task Invite()
        {
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);
            roles.Add(Setup.RecruiterRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[1]))
            {
                //Generate invite code
                string id = Tools.GenerateUniqueId();
                //Save invite code
                Empty.Save(Setup.InviteCodesPath + "/" + id);

                EmbedBuilder eb = new EmbedBuilder();
                eb.Title = "🔒 Code Generated";
                string text =
                    "Code good for one time use:" + "\n" +
                    "```" + "\n" +
                    id + "\n" +
                    "```";
                eb.WithDescription(text);
                eb.WithColor(Color.Gold);
                await Context.User.SendMessageAsync("", false, eb.Build());
            }
            await Context.Message.DeleteAsync();
        }

        [Command("clearinvites")]
        public async Task ClearInvites()
        {
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);
            roles.Add(Setup.RecruiterRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[1]))
            {
                DirectoryInfo fileInfo = new DirectoryInfo("InviteCodes");
                foreach (FileInfo fileToDelete in fileInfo.GetFiles()) await Tools.DeleteFileAsync(fileToDelete);
            }
            await Context.Message.DeleteAsync();
        }
    }
}
