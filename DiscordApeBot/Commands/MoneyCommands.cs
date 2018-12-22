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
    public class MoneyCommands : ModuleBase<SocketCommandContext>
    {
        [Command("daily")]
        public async Task Daily()
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            //Loaded data
            BinaryMoneyNormal entry = new BinaryMoneyNormal(0, 0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
            //Load
            bool fileExists = MoneyNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Money", ref entry);

            DateTimeOffset lastLoadedTimestamp = new DateTimeOffset(entry.DateTimeTicks, new TimeSpan(entry.DateTimeOffsetTicks));
            DateTimeOffset nextUpdateTimestamp = lastLoadedTimestamp.AddHours(Setup.HoursTillReset);
            TimeSpan nextUpdateTime = nextUpdateTimestamp.Subtract(Context.Message.Timestamp);
            //Check if time threshold has passed to execute again
            if(nextUpdateTime.TotalHours <= 0 || !fileExists)
            {
                //Modify
                entry.CurrentBonusDay += 1;
                if (entry.CurrentBonusDay > 5) entry.CurrentBonusDay = 1;
                entry.Money += Setup.DailyBaseWorth * entry.CurrentBonusDay;
                entry.DateTimeTicks = Context.Message.Timestamp.Ticks;
                entry.DateTimeOffsetTicks = Context.Message.Timestamp.Offset.Ticks;
                //Save
                MoneyNormal.Save(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Money", entry);

                string[] streakSymbols = new string[5];
                streakSymbols[0] = "🇨 ";
                streakSymbols[1] = "🇭 ";
                streakSymbols[2] = "🇮 ";
                streakSymbols[3] = "🇲 ";
                streakSymbols[4] = "🇵 ";
                string streak = "";
                for (int i = 0; i < entry.CurrentBonusDay; i++) streak += streakSymbols[i];

                eb.Title = "**💰 Daily! " + Tools.GetNickname(Context, Context.User.Id) + "**";
                string text =
                    "You got **$" + Setup.DailyBaseWorth * entry.CurrentBonusDay + "** daily credits!" + "\n" +
                    "**Streak: " + streak + "**";
                eb.WithDescription(text);
                eb.WithColor(Color.Gold);
            }
            else
            {
                eb.Title = "**🚫 " + Tools.GetNickname(Context, Context.User.Id) + " please wait " + (nextUpdateTime.Hours) + " hours, " + (nextUpdateTime.Minutes) + " minutes, and " + (nextUpdateTime.Seconds) + " seconds for daily! 💰**";
                eb.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("rep")]
        public async Task Rep(SocketUser mentionedUser)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            //Loaded data
            BinaryDateTimeOffsetNormal timestampEntry = new BinaryDateTimeOffsetNormal(Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
            //Load
            bool fileExists = DateTimeOffsetNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "ReputationTimestamp", ref timestampEntry);

            DateTimeOffset lastLoadedTimestamp = new DateTimeOffset(timestampEntry.DateTimeTicks, new TimeSpan(timestampEntry.DateTimeOffsetTicks));
            DateTimeOffset nextUpdateTimestamp = lastLoadedTimestamp.AddHours(Setup.HoursTillReset);
            TimeSpan nextUpdateTime = nextUpdateTimestamp.Subtract(Context.Message.Timestamp);
            //Check if time threshold has passed to execute again
            if (nextUpdateTime.TotalHours <= 0 || !fileExists)
            {
                if (Context.User.Id != mentionedUser.Id)
                {
                    Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + mentionedUser.Id);
                    //Loaded data
                    int entry = 0;
                    //Load
                    IntNormal.Load(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Reputation", ref entry);
                    //Modify
                    entry += 1;
                    timestampEntry.DateTimeTicks = Context.Message.Timestamp.Ticks;
                    timestampEntry.DateTimeOffsetTicks = Context.Message.Timestamp.Offset.Ticks;
                    //Save
                    IntNormal.Save(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Reputation", entry);
                    DateTimeOffsetNormal.Save(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "ReputationTimestamp", timestampEntry);

                    string text = "**👍 " + Context.User.Mention + " has given " + mentionedUser.Mention + " a reputation point!**";
                    eb.WithDescription(text);
                    eb.WithColor(Color.Purple);
                }
                else
                {
                    eb.Title = "**⚠️ " + Tools.GetNickname(Context, Context.User.Id) + " you can't give yourself reputation!**";
                    eb.WithColor(Color.Orange);
                }
            }
            else
            {
                eb.Title = "**🚫 " + Tools.GetNickname(Context, Context.User.Id) + " please wait " + (nextUpdateTime.Hours) + " hours, " + (nextUpdateTime.Minutes) + " minutes, and " + (nextUpdateTime.Seconds) + " seconds to rep again! 👍**";
                eb.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("flip")]
        public async Task Flip(int amount)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            //Loaded data
            BinaryMoneyNormal entry = new BinaryMoneyNormal(0, 0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
            //Load
            MoneyNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Money", ref entry);

            int chance = Setup.Rand.Next(0, 2);

            //Check if theres enough money
            if(entry.Money >= amount)
            {
                //Win
                if (chance == 0)
                {
                    //Modify
                    entry.Money += amount;
                    eb.Title = "**💸 Flip!**";
                    string text = Context.User.Mention + " you won the flip! You have earned **$" + amount + "** credits!";
                    eb.WithDescription(text);
                    eb.WithColor(Color.Green);
                }
                //Lose
                else
                {
                    entry.Money -= amount;
                    eb.Title = "**💸 Flip!**";
                    string text = Context.User.Mention + " you lose the flip! You have lossed **$" + amount + "** credits!";
                    eb.WithDescription(text);
                    eb.WithColor(Color.Orange);
                }
                //Save
                MoneyNormal.Save(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Money", entry);
            }
            else
            {
                eb.Title = "**🚫 " + Tools.GetNickname(Context, Context.User.Id) + " you don't have enough credits to flip that amount!**";
                eb.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("credits")]
        public async Task Credits()
        {
            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            //Loaded data
            BinaryMoneyNormal entry = new BinaryMoneyNormal(0, 0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
            //Load
            MoneyNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Money", ref entry);

            EmbedBuilder eb = new EmbedBuilder();
            eb.Title = "**💰 Bank**";
            string text = Tools.GetNickname(Context, Context.User.Id) + " you got **$" + entry.Money + "** credits in the bank!";
            eb.WithDescription(text);
            eb.WithColor(Color.Gold);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("addmoney")]
        public async Task AddMoney(SocketUser mentionedUser, int amount)
        {
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                //Create player folder if it doesn't exist
                Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + mentionedUser.Id);
                //Loaded data
                BinaryMoneyNormal entry = new BinaryMoneyNormal(0, 0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
                //Load
                MoneyNormal.Load(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Money", ref entry);
                //Modify
                entry.Money += amount;
                //Save
                MoneyNormal.Save(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Money", entry);

                EmbedBuilder eb = new EmbedBuilder();
                string text = mentionedUser.Mention + " you have earned **$" + amount + "** credits!";
                eb.WithDescription(text);
                eb.WithColor(Color.Green);

                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            await Context.Message.DeleteAsync();
        }

        [Command("removemoney")]
        public async Task RemoveMoney(SocketUser mentionedUser, int amount)
        {
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0]))
            {
                //Create player folder if it doesn't exist
                Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + mentionedUser.Id);
                //Loaded data
                BinaryMoneyNormal entry = new BinaryMoneyNormal(0, 0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
                //Load
                MoneyNormal.Load(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Money", ref entry);
                //Modify
                entry.Money -= amount;
                //Save
                MoneyNormal.Save(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Money", entry);

                EmbedBuilder eb = new EmbedBuilder();
                string text = mentionedUser.Mention + " you have loss **$" + amount + "** credits!";
                eb.WithDescription(text);
                eb.WithColor(Color.Orange);

                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            await Context.Message.DeleteAsync();
        }

        [Command("profile")]
        public async Task Profile()
        {
            await DisplayProfile(Context.User.Id);
            await Context.Message.DeleteAsync();
        }

        [Command("profile")]
        public async Task ProfileUser(SocketUser mentionedUser)
        {
            await DisplayProfile(mentionedUser.Id);
            await Context.Message.DeleteAsync();
        }

        public async Task DisplayProfile(ulong userID)
        {
            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + userID);
            //Loaded data
            BinaryLevelNormal levelEntry = new BinaryLevelNormal(0, 0, 0);
            BinaryMoneyNormal moneyEntry = new BinaryMoneyNormal(0, 0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
            int repEntry = 0;
            BinaryMarriageNormal marriageEntry = new BinaryMarriageNormal(0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
            //Load
            LevelNormal.Load(Setup.PlayerSavesPath + "/" + userID + "/" + "Level", ref levelEntry);
            MoneyNormal.Load(Setup.PlayerSavesPath + "/" + userID + "/" + "Money", ref moneyEntry);
            IntNormal.Load(Setup.PlayerSavesPath + "/" + userID + "/" + "Reputation", ref repEntry);
            MarriageNormal.Load(Setup.PlayerSavesPath + "/" + userID + "/" + "Spouse", ref marriageEntry);

            EmbedBuilder eb = new EmbedBuilder();
            eb.Title = Tools.GetNickname(Context, userID) + "'s Profile";
            eb.WithThumbnailUrl(Context.User.GetAvatarUrl());
            eb.WithColor(194, 14, 213);

            string creditsTitle = "Status";
            string creditsText =
                "💰Credits: $" + moneyEntry.Money + "\n" +
                "✨Reputation: " + repEntry + "\n";
            eb.AddField(Tools.CreateEmbedField(creditsTitle, creditsText, true));

            string levelFieldTitle = "Level";
            string levelFieldText =
                "🆙Level: " + levelEntry.CurrentLevel + " (" + levelEntry.CurrentExp + "/" + Tools.GetExpForLevel(levelEntry.CurrentLevel, Setup.ExperienceGrowthModifier) + ")" + "\n" +
                "🛡️Total exp: " + levelEntry.TotalExp;
            eb.AddField(Tools.CreateEmbedField(levelFieldTitle, levelFieldText, true));

            string marriageTitle = "Married to";
            string marriageText = "None";
            if (marriageEntry.SpouseID != 0)
            {
                string dateOfMarriage = new DateTimeOffset(marriageEntry.DateTimeTicks, new TimeSpan(marriageEntry.DateTimeOffsetTicks)).Date.ToShortDateString();
                marriageText = "💕" + Tools.GetMention(Context, marriageEntry.SpouseID) + " (" + dateOfMarriage + ")";
            }
            eb.AddField(Tools.CreateEmbedField(marriageTitle, marriageText, false));

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
    }
}
