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
using DiscordApeBot.SaveData;

namespace DiscordApeBot
{
    class Level
    {
        public static async Task GainExperience(SocketCommandContext context)
        {
            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + context.User.Id);
            //Loaded data
            BinaryLevelNormal entry = new BinaryLevelNormal(0, 0, 0);
            //Load
            LevelNormal.Load(Setup.PlayerSavesPath + "/" + context.User.Id + "/" + "Level", ref entry);
            //Modify
            entry.CurrentExp += Setup.ExperienceGainPerMessage;
            entry.TotalExp += Setup.ExperienceGainPerMessage;
            long nextLevelExp = Tools.GetExpForLevel(entry.CurrentLevel, Setup.ExperienceGrowthModifier);
            if(entry.CurrentExp >= nextLevelExp)
            {
                entry.CurrentExp = entry.CurrentExp - nextLevelExp;
                entry.CurrentLevel += 1;

                EmbedBuilder eb = new EmbedBuilder();
                eb.Title = "🆙 Level up!";
                string text = context.User.Mention + " has leveled up to **level " + entry.CurrentLevel + "**!" ;
                eb.WithDescription(text);
                eb.WithColor(255, 255, 255);
                await context.Channel.SendMessageAsync("", false, eb.Build());
            }
            //Save
            LevelNormal.Save(Setup.PlayerSavesPath + "/" + context.User.Id + "/" + "Level", entry);
        }
    }
}
