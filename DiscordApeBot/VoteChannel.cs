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
    class VoteChannel
    {
        public static async Task UpdateVote(SocketReaction emoji, bool isThumbsUp)
        {
            IMessage message = await emoji.Channel.GetMessageAsync(emoji.MessageId);

            //Removes old vote
            if(isThumbsUp) await (message as IUserMessage).RemoveReactionAsync(new Emoji(Setup.ThumbsDownEmoji), emoji.User.Value);
            else await (message as IUserMessage).RemoveReactionAsync(new Emoji(Setup.ThumbsUpEmoji), emoji.User.Value);

            IReadOnlyCollection<IUser> thumbsUpEmojiReactions = await (message as IUserMessage).GetReactionUsersAsync(new Emoji(Setup.ThumbsUpEmoji));
            IReadOnlyCollection<IUser> thumbsDownEmojiReactions = await (message as IUserMessage).GetReactionUsersAsync(new Emoji(Setup.ThumbsDownEmoji));
            List<IUser> thumbsUpReactions = thumbsUpEmojiReactions.ToList();
            List<IUser> thumbsDownReactions = thumbsDownEmojiReactions.ToList();
            List<IEmbed> embeds = message.Embeds.ToList();

            EmbedBuilder eb = embeds[0].ToEmbedBuilder();

            for (int i = 0; i < eb.Fields.Count; i++)
            {
                if (eb.Fields[i].Name == "👍 Yes") eb.Fields[i].WithValue(getReactionUserNames(thumbsUpReactions));
                else eb.Fields[i].WithValue(getReactionUserNames(thumbsDownReactions));
            }

            await (message as IUserMessage).ModifyAsync(msg =>
            {
                msg.Embed = eb.Build();
            });
        }

        static string getReactionUserNames(List<IUser> reactionUsers)
        {
            string names = "None";
            if(reactionUsers.Count > 1)
            {
                names = "";
                for (int i = 0; i < reactionUsers.Count; i++)
                {
                    if(!reactionUsers[i].IsBot) names += reactionUsers[i].Mention + "\n";
                }
            }
            return names;
        }
    }
}
