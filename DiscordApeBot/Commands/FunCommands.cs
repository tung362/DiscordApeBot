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
    public class FunCommands : ModuleBase<SocketCommandContext>
    {
        [Command("buyring")]
        public async Task BuyRing()
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id + "/Inventory");

            if (!Empty.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/Inventory/WeddingRing"))
            {
                //Loaded data
                BinaryMoneyNormal moneyEntry = new BinaryMoneyNormal(0, 0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
                //Load
                MoneyNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Money", ref moneyEntry);

                if (moneyEntry.Money >= Setup.MarriagePrice)
                {
                    //Modify
                    moneyEntry.Money -= Setup.MarriagePrice;
                    //Save
                    MoneyNormal.Save(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Money", moneyEntry);
                    Empty.Save(Setup.PlayerSavesPath + "/" + Context.User.Id + "/Inventory/WeddingRing");

                    eb.Title = "**🛍️ wedding ring bought!**";
                    string text =
                        Context.User.Mention + " you have bought a __**wedding ring**__  for **$" + Setup.MarriagePrice + "** credits!" + "\n" +
                        "You are now eligible to marry someone!";
                    eb.WithDescription(text);
                    eb.WithColor(Color.Blue);
                }
                else
                {
                    eb.Title = "**🚫 " + Tools.GetNickname(Context, Context.User.Id) + " you don't have enough credits to buy wedding ring! Cost: $" + Setup.MarriagePrice + " credits**";
                    eb.WithColor(Color.Red);
                }
            }
            else
            {
                eb.Title = "**🚫 " + Tools.GetNickname(Context, Context.User.Id) + " you already own a wedding ring!**";
                eb.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("marry")]
        public async Task Marry(SocketUser mentionedUser)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id + "/Inventory");

            if (Empty.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/Inventory/WeddingRing"))
            {
                //Create player folder if it doesn't exist
                Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + mentionedUser.Id);
                //Loaded data
                BinaryMarriageNormal userMarriageEntry = new BinaryMarriageNormal(0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
                BinaryMarriageNormal otherUserMarriageEntry = new BinaryMarriageNormal(0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
                //Load
                bool userMarried = MarriageNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Spouse", ref userMarriageEntry);
                bool otherUserMarried = MarriageNormal.Load(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Spouse", ref otherUserMarriageEntry);

                if(!userMarried && !otherUserMarried)
                {
                    //Create player folder if it doesn't exist
                    Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/MarriageProposals");
                    Empty.Save(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/MarriageProposals" + "/" + Context.User.Id);

                    eb.Title = "**📝 marriage proposed!**";
                    string text = mentionedUser.Mention + " you have been proposed to by " + Context.User.Mention + " do you accept?";
                    string footerText = Tools.GetNickname(Context, Context.User.Id) + " your partner needs to accept the proposal.";
                    eb.WithDescription(text);
                    eb.WithFooter(footerText);
                    eb.WithColor(Color.Green);
                }
                else
                {
                    if(userMarried)
                    {
                        string text = "**⚠️ " + Context.User.Mention + " you are already married to " + Tools.GetMention(Context, userMarriageEntry.SpouseID) + "**";
                        eb.WithDescription(text);
                        eb.WithColor(Color.Orange);
                    }
                    else
                    {
                        string text = "**⚠️ " + mentionedUser.Mention + " is already married to " + Tools.GetMention(Context, otherUserMarriageEntry.SpouseID) + "**";
                        eb.WithDescription(text);
                        eb.WithColor(Color.Orange);
                    }
                }
            }
            else
            {
                eb.Title = "**🚫 " + Tools.GetNickname(Context, Context.User.Id) + " you don't have a wedding ring please buy one before marrying! Cost: " + Setup.MarriagePrice + " credits**";
                eb.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("acceptmarriage")]
        public async Task AcceptMarriage(SocketUser mentionedUser)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id + "/MarriageProposals");

            if (Empty.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/MarriageProposals" + "/" + mentionedUser.Id))
            {
                //Create player folder if it doesn't exist
                Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + mentionedUser.Id);
                //Loaded data
                BinaryMarriageNormal userMarriageEntry = new BinaryMarriageNormal(0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
                BinaryMarriageNormal otherUserMarriageEntry = new BinaryMarriageNormal(0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
                //Load
                MarriageNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Spouse", ref userMarriageEntry);
                MarriageNormal.Load(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Spouse", ref otherUserMarriageEntry);
                //Modify
                userMarriageEntry.SpouseID = mentionedUser.Id;
                userMarriageEntry.DateTimeTicks = Context.Message.Timestamp.Ticks;
                userMarriageEntry.DateTimeOffsetTicks = Context.Message.Timestamp.Offset.Ticks;
                otherUserMarriageEntry.SpouseID = Context.User.Id;
                otherUserMarriageEntry.DateTimeTicks = Context.Message.Timestamp.Ticks;
                otherUserMarriageEntry.DateTimeOffsetTicks = Context.Message.Timestamp.Offset.Ticks;
                //Save
                MarriageNormal.Save(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Spouse", userMarriageEntry);
                MarriageNormal.Save(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/" + "Spouse", otherUserMarriageEntry);

                if(Directory.Exists(Setup.PlayerSavesPath + "/" + Context.User.Id + "/MarriageProposals")) Directory.Delete(Setup.PlayerSavesPath + "/" + Context.User.Id + "/MarriageProposals", true);
                if (Directory.Exists(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/MarriageProposals")) Directory.Delete(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/MarriageProposals", true);

                FileInfo itemFile = new FileInfo(Setup.PlayerSavesPath + "/" + mentionedUser.Id + "/Inventory/WeddingRing");
                await Tools.DeleteFileAsync(itemFile);

                eb.Title = "**🎉 Congratulations! " + Tools.GetNickname(Context, Context.User.Id) + " and " + Tools.GetNickname(Context, mentionedUser.Id) + " are now married!**";
                string text = Context.User.Mention + " x " + mentionedUser.Mention + " (" + Context.Message.Timestamp.Date.ToShortDateString() + ")";
                eb.WithDescription(text);
                eb.WithColor(255, 192, 203);
            }
            else
            {
                eb.Title = "**🚫 " + Tools.GetNickname(Context, Context.User.Id) + " you have not been proposed to by " + Tools.GetNickname(Context, mentionedUser.Id) + "**";
                eb.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("divorce")]
        public async Task Divorce()
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Create player folder if it doesn't exist
            Directory.CreateDirectory(Setup.PlayerSavesPath + "/" + Context.User.Id);
            //Loaded data
            BinaryMarriageNormal userMarriageEntry = new BinaryMarriageNormal(0, Context.Message.Timestamp.Ticks, Context.Message.Timestamp.Offset.Ticks);
            //Load
            bool userMarried = MarriageNormal.Load(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Spouse", ref userMarriageEntry);

            if (userMarried)
            {
                //Modify
                FileInfo userFile = new FileInfo(Setup.PlayerSavesPath + "/" + Context.User.Id + "/" + "Spouse");
                await Tools.DeleteFileAsync(userFile);

                FileInfo otherUserFile = new FileInfo(Setup.PlayerSavesPath + "/" + userMarriageEntry.SpouseID + "/" + "Spouse");
                await Tools.DeleteFileAsync(otherUserFile);


                eb.Title = "**😔 " + Tools.GetNickname(Context, Context.User.Id) + " and " + Tools.GetNickname(Context, userMarriageEntry.SpouseID) + " are now divorced!**";
                string text = "Rip the ship of " + Context.User.Mention + " x " + Tools.GetMention(Context, userMarriageEntry.SpouseID);
                eb.WithDescription(text);
                eb.WithColor(Color.DarkBlue);
            }
            else
            {
                string text = "**⚠️ You are not married to have a divorce!**";
                eb.WithDescription(text);
                eb.WithColor(Color.Orange);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }
    }
}
