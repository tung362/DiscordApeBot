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
    public class VoteCommands : ModuleBase<SocketCommandContext>
    {
        [Command("vote")]
        public async Task Vote(string topic, string thumbnailUrl)
        {
            SocketTextChannel channel = Context.Guild.GetTextChannel(Setup.VoteChannelID);
            EmbedBuilder eb = new EmbedBuilder();
            eb.Title = "🗳️ Vote";
            string text =
                "`" + topic + "`";
            eb.WithDescription(text);
            eb.WithColor(Color.Teal);

            string yesTitle = "👍 Yes";
            string yesText = "None";
            eb.AddField(Tools.CreateEmbedField(yesTitle, yesText, true));

            string noTitle = "👎 No";
            string noText = "None";
            eb.AddField(Tools.CreateEmbedField(noTitle, noText, true));

            eb.WithThumbnailUrl(thumbnailUrl);
            eb.WithFooter("**Updates whenever someone votes.**");

            var voteMessage = await channel.SendMessageAsync("", false, eb.Build());
            await voteMessage.AddReactionAsync(new Emoji(Setup.ThumbsUpEmoji));
            await voteMessage.AddReactionAsync(new Emoji(Setup.ThumbsDownEmoji));
            await Context.Message.DeleteAsync();
        }
    }
}
