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
    public class HelpCommands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.Title = "📙 Help";
            string serverText =
                "__command information:__ " + Setup.Client.CurrentUser.Mention + " help <command>";
            eb.WithDescription(serverText);
            eb.WithColor(Color.Gold);

            string generalTitle = "General";
            string generalText =
                "`help`" + ", " +
                "`invite`" + ", " +
                "`clearinvites`" + ", " +
                "`clear`" + ", " +
                "`clearall`" + ", " +
                "`cleardm`" + ", " +
                "`vote`";
            eb.AddField(Tools.CreateEmbedField(generalTitle, generalText, false));

            string houseTitle = "House";
            string houseText =
                "`houseup`" + ", " +
                "`housedown`" + ", " +
                "`housereset`";
            eb.AddField(Tools.CreateEmbedField(houseTitle, houseText, false));

            string actionsTitle = "Actions";
            string actionsText =
                "`clap`" + ", " +
                "`cuddle`" + ", " +
                "`dance`" + ", " +
                "`fight`" + ", " +
                "`grab`" + ", " +
                "`hate`" + ", " +
                "`highfive`" + ", " +
                "`holdhands`" + ", " +
                "`hug`" + ", " +
                "`kiss`" + ", " +
                "`mate`" + ", " +
                "`poke`" + ", " +
                "`punch`" + ", " +
                "`slap`" + ", " +
                "`stare`";
            eb.AddField(Tools.CreateEmbedField(actionsTitle, actionsText, false));

            string accountTitle = "Account";
            string accountText =
                "`daily`" + ", " +
                "`rep`" + ", " +
                "`credits`" + ", " +
                "`addmoney`" + ", " +
                "`removemoney`" + ", " +
                "`profile`";
            eb.AddField(Tools.CreateEmbedField(accountTitle, accountText, false));

            string gambleTitle = "Gamble";
            string gambleText =
                "`flip`";
            eb.AddField(Tools.CreateEmbedField(gambleTitle, gambleText, false));

            string marriageTitle = "Marriage";
            string marriageText =
                "`buyring`" + ", " +
                "`marry`" + ", " +
                "`acceptmarriage`" + ", " +
                "`divorce`";
            eb.AddField(Tools.CreateEmbedField(marriageTitle, marriageText, false));

            string updateTitle = "Update";
            string updateText =
                "`updatewelcome`" + ", " +
                "`updatehouse`" + ", " +
                "`updaterules`" + ", " +
                "`updatehuntlist`" + ", " +
                "`updatehouseofgorilla`" + ", " +
                "`updatehouseofmonkey`" + ", " +
                "`updatehouseofbaboon`" + ", " +
                "`updatetimers`";
            eb.AddField(Tools.CreateEmbedField(updateTitle, updateText, false));


            string modifyTitle = "Modify";
            string modifyText =
                "`addtohuntlist`" + ", " +
                "`removefromhuntlist`" + ", " +
                "`clearhuntlist`";
            eb.AddField(Tools.CreateEmbedField(modifyTitle, modifyText, false));

            string TimersTitle = "Timers";
            string TimersText =
                "`addgeneralevent`" + ", " +
                "`addcbinfoevent`" + ", " +
                "`addcbgenevent`" + ", " +
                "`addcbspecevent`" + ", " +
                "`removegeneralevent`" + ", " +
                "`removecbinfoevent`" + ", " +
                "`removecbgenevent`" + ", " +
                "`removecbspecevent`" + ", " +
                "`addgeneraleventdaily`" + ", " +
                "`addcbinfoeventdaily`" + ", " +
                "`addcbgeneventdaily`" + ", " +
                "`addcbspeceventdaily`" + ", " +
                "`addgeneraleventdate`" + ", " +
                "`addcbinfoeventdate`" + ", " +
                "`addcbgeneventdate`" + ", " +
                "`addcbspeceventdate`" + ", " +
                "`addgeneraleventfake`" + ", " +
                "`addcbinfoeventfake`" + ", " +
                "`addcbgeneventfake`" + ", " +
                "`addcbspeceventfake`" + ", " +
                "`addgeneraleventfakeactive`" + ", " +
                "`addcbinfoeventfakeactive`" + ", " +
                "`addcbgeneventfakeactive`" + ", " +
                "`addcbspeceventfakeactive`" + ", " +
                "`resetgeneralevent`" + ", " +
                "`resetcbinfoevent`" + ", " +
                "`resetcbgenevent`" + ", " +
                "`resetcbspecevent`";
            eb.AddField(Tools.CreateEmbedField(TimersTitle, TimersText, false));

            //eb.WithThumbnailUrl(Setup.HelpThumbnail);
            eb.WithFooter(Setup.BotVersion);

            await Context.User.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }

        [Command("help")]
        public async Task Help(string commandName)
        {
            EmbedBuilder eb = new EmbedBuilder();
            string description = "";
            List<string> usage = new List<string>();
            List<string> examples = new List<string>();

            switch (commandName)
            {
                //General
                case "help":
                    description = "Displays list of commands and provides information about a specific command.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " help");
                    usage.Add(Setup.Client.CurrentUser.Mention + " help <command>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " help");
                    examples.Add(Setup.Client.CurrentUser.Mention + " help clear");
                    eb = Tools.CreateHelpInfo("Help", new Color(255, 255, 255), description, usage, examples);
                    break;

                case "invite":
                    description = "Generates a one time use invitation code used to invite new members.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " invite");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " invite");
                    eb = Tools.CreateHelpInfo("Invite", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "clearinvites":
                    description = "Expire and clears all generated invitation codes.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " clearinvites");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " clearinvites");
                    eb = Tools.CreateHelpInfo("ClearInvites", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "clear":
                    description = "Clears messages for a specified type.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " clear <message count>");
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " clear <user> <message count>");
                    usage.Add(Setup.Client.CurrentUser.Mention + " clear <role> <message count>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " clear 100");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " clear @Gorilla 100");
                    examples.Add(Setup.Client.CurrentUser.Mention + " clear @Tribe member 100");
                    eb = Tools.CreateHelpInfo("Clear", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "clearall":
                    description = "Clears messages for all types.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " clearall <message count>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " clearall 100");
                    eb = Tools.CreateHelpInfo("ClearAll", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "cleardm":
                    description = "Clears messages from direct messages.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " cleardm <message count>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " cleardm 100");
                    eb = Tools.CreateHelpInfo("ClearDM", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "vote":
                    description = "Creates a new vote topic in " + Context.Guild.GetTextChannel(Setup.VoteChannelID).Mention + ".";
                    usage.Add(Setup.Client.CurrentUser.Mention + " vote <topic> <thumbnail url>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " vote \"Is water wet?\" http://example.com/example.gif");
                    eb = Tools.CreateHelpInfo("Vote", new Color(255, 255, 255), description, usage, examples);
                    break;
                //House
                case "houseup":
                    description = "Adds money to the specified user.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " houseup <user> <amount>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " houseup @Gorilla 1000");
                    eb = Tools.CreateHelpInfo("HouseUp", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "housedown":
                    description = "Removes money from the specified user.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " housedown <user> <amount>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " housedown @Gorilla 1000");
                    eb = Tools.CreateHelpInfo("HouseDown", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "housereset":
                    description = "Resets house points from every house.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " housereset");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " housereset");
                    eb = Tools.CreateHelpInfo("HouseReset", new Color(255, 255, 255), description, usage, examples);
                    break;
                //Actions
                case "clap":
                    description = "Expresses the clap action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " clap <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " clap @Gorilla");
                    eb = Tools.CreateHelpInfo("Clap", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "cuddle":
                    description = "Expresses the cuddle action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " cuddle <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " cuddle @Gorilla");
                    eb = Tools.CreateHelpInfo("Cuddle", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "dance":
                    description = "Expresses the dance action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " dance <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " dance @Gorilla");
                    eb = Tools.CreateHelpInfo("Dance", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "fight":
                    description = "Expresses the fight action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " fight <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " fight @Gorilla");
                    eb = Tools.CreateHelpInfo("Fight", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "grab":
                    description = "Expresses the grab action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " grab <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " grab @Gorilla");
                    eb = Tools.CreateHelpInfo("Grab", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "hate":
                    description = "Expresses the hate action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " hate <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " hate @Gorilla");
                    eb = Tools.CreateHelpInfo("Hate", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "highfive":
                    description = "Expresses the high five action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " highfive <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " highfive @Gorilla");
                    eb = Tools.CreateHelpInfo("HighFive", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "holdhands":
                    description = "Expresses the hold hands action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " holdhands <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " holdhands @Gorilla");
                    eb = Tools.CreateHelpInfo("HoldHands", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "hug":
                    description = "Expresses the hug action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " hug <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " hug @Gorilla");
                    eb = Tools.CreateHelpInfo("Hug", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "kiss":
                    description = "Expresses the kiss action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " kiss <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " kiss @Gorilla");
                    eb = Tools.CreateHelpInfo("Kiss", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "mate":
                    description = "Expresses the mate action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " mate <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " mate @Gorilla");
                    eb = Tools.CreateHelpInfo("Mate", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "poke":
                    description = "Expresses the poke action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " poke <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " poke @Gorilla");
                    eb = Tools.CreateHelpInfo("Poke", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "punch":
                    description = "Expresses the punch action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " punch <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " punch @Gorilla");
                    eb = Tools.CreateHelpInfo("Punch", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "slap":
                    description = "Expresses the slap action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " slap <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " slap @Gorilla");
                    eb = Tools.CreateHelpInfo("Slap", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "stare":
                    description = "Expresses the stare action to someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " stare <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " stare @Gorilla");
                    eb = Tools.CreateHelpInfo("Stare", new Color(255, 255, 255), description, usage, examples);
                    break;
                //Account
                case "daily":
                    description = "Gets your daily credits. The higher your streak the more credits you earn. Resets every 12 hours.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " daily");
                    examples.Add(Setup.Client.CurrentUser.Mention + " daily");
                    eb = Tools.CreateHelpInfo("Daily", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "rep":
                    description = "Gives reputation points to someone specified. Resets every 12 hours.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " rep <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " rep @Gorilla");
                    eb = Tools.CreateHelpInfo("Rep", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "credits":
                    description = "Displays how much credits you have.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " credits");
                    examples.Add(Setup.Client.CurrentUser.Mention + " credits");
                    eb = Tools.CreateHelpInfo("Credits", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addmoney":
                    description = "Adds credits to a specified user.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addmoney <user> <amount>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addmoney @Gorilla 1000");
                    eb = Tools.CreateHelpInfo("AddMoney", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "removemoney":
                    description = "Removes credits from a specified user.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " removemoney <user> <amount>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " removemoney @Gorilla 1000");
                    eb = Tools.CreateHelpInfo("RemoveMoney", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "profile":
                    description = "Displays your profile or someone else's profile.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " profile");
                    usage.Add(Setup.Client.CurrentUser.Mention + " profile <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " profile");
                    examples.Add(Setup.Client.CurrentUser.Mention + " profile @Gorilla");
                    eb = Tools.CreateHelpInfo("Profile", new Color(255, 255, 255), description, usage, examples);
                    break;
                //Gamble
                case "flip":
                    description = "50% chance of earning or losing amount of credits risked.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " flip <amount>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " flip 1000");
                    eb = Tools.CreateHelpInfo("Flip", new Color(255, 255, 255), description, usage, examples);
                    break;
                //Marriage
                case "buyring":
                    description = "Buys a wedding ring for $" + Setup.MarriagePrice + " credits. Makes you eligible to marry someone.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " buyring");
                    examples.Add(Setup.Client.CurrentUser.Mention + " buyring");
                    eb = Tools.CreateHelpInfo("BuyRing", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "marry":
                    description = "Requests a marriage proposal to a user. Wedding ring must be bought first before using this command.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " marry <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " marry @Gorilla");
                    eb = Tools.CreateHelpInfo("Marry", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "acceptmarriage":
                    description = "Accepts a marriage proposal requested from a user.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " acceptmarriage <user>");
                    examples.Add(Setup.Client.CurrentUser.Mention + " acceptmarriage @Gorilla");
                    eb = Tools.CreateHelpInfo("AcceptMarriage", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "divorce":
                    description = "Divorces your current marriage.";
                    usage.Add(Setup.Client.CurrentUser.Mention + " divorce");
                    examples.Add(Setup.Client.CurrentUser.Mention + " divorce");
                    eb = Tools.CreateHelpInfo("Divorce", new Color(255, 255, 255), description, usage, examples);
                    break;
                //Update
                case "updatewelcome":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.WelcomeChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updatewelcome");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updatewelcome");
                    eb = Tools.CreateHelpInfo("UpdateWelcome", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "updatehouse":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.HouseChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updatehouse");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updatehouse");
                    eb = Tools.CreateHelpInfo("UpdateHouse", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "updaterules":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.RulesChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updaterules");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updaterules");
                    eb = Tools.CreateHelpInfo("UpdateRules", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "updatehuntlist":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.HuntListChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updatehuntlist");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updatehuntlist");
                    eb = Tools.CreateHelpInfo("UpdateHuntList", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "updatehouseofgorilla":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.HouseOfGorillaChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updatehouseofgorilla");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updatehouseofgorilla");
                    eb = Tools.CreateHelpInfo("UpdateHouseOfGorilla", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "updatehouseofmonkey":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.HouseOfMonkeyChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updatehouseofmonkey");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updatehouseofmonkey");
                    eb = Tools.CreateHelpInfo("UpdateHouseOfMonkey", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "updatehouseofbaboon":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.HouseOfBaboonChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updatehouseofbaboon");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updatehouseofbaboon");
                    eb = Tools.CreateHelpInfo("UpdateHouseOfBaboon", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "updatetimers":
                    description = "Updates the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " updatetimers");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " updatetimers");
                    eb = Tools.CreateHelpInfo("UpdateTimers", new Color(255, 255, 255), description, usage, examples);
                    break;
                //Modify
                case "addtohuntlist":
                    description = "Adds a target to the " + Context.Guild.GetTextChannel(Setup.HuntListChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addtohuntlist <target name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addtohuntlist gorilla");
                    eb = Tools.CreateHelpInfo("AddToHuntList", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "removefromhuntlist":
                    description = "Removes a target from the " + Context.Guild.GetTextChannel(Setup.HuntListChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " removefromhuntlist <target name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " removefromhuntlist gorilla");
                    eb = Tools.CreateHelpInfo("RemoveFromHuntList", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "clearhuntlist":
                    description = "Clears all targets from the " + Context.Guild.GetTextChannel(Setup.HuntListChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " clearhuntlist");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " clearhuntlist");
                    eb = Tools.CreateHelpInfo("ClearHuntList", new Color(255, 255, 255), description, usage, examples);
                    break;
                //Timers
                case "addgeneralevent":
                    description = "Adds a general event to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addgeneralevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addgeneralevent \"Group picture\"");
                    eb = Tools.CreateHelpInfo("AddGeneralEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbinfoevent":
                    description = "Adds a cosmic break information event to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbinfoevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbinfoevent \"Maintenance\"");
                    eb = Tools.CreateHelpInfo("AddCBInfoEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbgenevent":
                    description = "Adds a cosmic break general event to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbgenevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbgenevent \"Clan fight\"");
                    eb = Tools.CreateHelpInfo("AddCBGenEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbspecevent":
                    description = "Adds a cosmic break special event to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbspecevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbspecevent \"Cards campaign\"");
                    eb = Tools.CreateHelpInfo("AddCBSpecEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "removegeneralevent":
                    description = "removes a general event from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " removegeneralevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " removegeneralevent \"Group picture\"");
                    eb = Tools.CreateHelpInfo("RemoveGeneralEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "removecbinfoevent":
                    description = "removes a cosmic break information event from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " removecbinfoevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " removecbinfoevent \"Maintenance\"");
                    eb = Tools.CreateHelpInfo("RemoveCBInfoEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "removecbgenevent":
                    description = "removes a cosmic break general event from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " removecbgenevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " removecbgenevent \"Clan fight\"");
                    eb = Tools.CreateHelpInfo("RemoveCBGenEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "removecbspecevent":
                    description = "removes a cosmic break special event from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " removecbspecevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " removecbspecevent \"Cards campaign\"");
                    eb = Tools.CreateHelpInfo("RemoveCBSpecEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addgeneraleventdaily":
                    description = "Adds a general event daily schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventdaily <event name> <starting day> <ending day> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventdaily \"Group picture\" monday friday 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddGeneralEventDaily", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbinfoeventdaily":
                    description = "Adds a cosmic break information event daily schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventdaily <event name> <starting day> <ending day> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventdaily \"Maintenance\" monday friday 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddCBInfoEventDaily", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbgeneventdaily":
                    description = "Adds a cosmic break general event daily schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventdaily <event name> <starting day> <ending day> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventdaily \"Clan fight\" monday friday 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddCBGenEventDaily", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbspeceventdaily":
                    description = "Adds a cosmic break special event daily schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventdaily <event name> <starting day> <ending day> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventdaily \"Cards campaign\" monday friday 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddCBSpecEventDaily", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addgeneraleventdate":
                    description = "Adds a general event date schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventdate <event name> <starting date> <ending date> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventdate \"Group picture\" 10-12-2018 10-15-2018 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddGeneralEventDate", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbinfoeventdate":
                    description = "Adds a cosmic break information event date schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventdate <event name> <starting date> <ending date> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventdate \"Maintenance\" 10-12-2018 10-15-2018 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddCBInfoEventDate", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbgeneventdate":
                    description = "Adds a cosmic break general event date schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventdate <event name> <starting date> <ending date> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventdate \"Clan fight\" 10-12-2018 10-15-2018 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddCBGenEventDate", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbspeceventdate":
                    description = "Adds a cosmic break special event date schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventdate <event name> <starting date> <ending date> <duration> <start times>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventdate \"Cards campaign\" 10-12-2018 10-15-2018 01:00 07:00 17:00");
                    eb = Tools.CreateHelpInfo("AddCBSpecEventDate", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addgeneraleventfake":
                    description = "Adds a general event non-active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventfake <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventfake \"Group picture\" \"It's a myth\"");
                    eb = Tools.CreateHelpInfo("AddGeneralEventFake", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbinfoeventfake":
                    description = "Adds a cosmic break information event non-active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventfake <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventfake \"Maintenance\" \"It's a myth\"");
                    eb = Tools.CreateHelpInfo("AddCBInfoEventFake", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbgeneventfake":
                    description = "Adds a cosmic break general event non-active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventfake <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventfake \"Clan fight\" \"It's a myth\"");
                    eb = Tools.CreateHelpInfo("AddCBGenEventFake", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbspeceventfake":
                    description = "Adds a cosmic break special event non-active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventfake <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventfake \"Cards campaign\" \"It's a myth\"");
                    eb = Tools.CreateHelpInfo("AddCBSpecEventFake", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addgeneraleventfakeactive":
                    description = "Adds a general event active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventfakeactive <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addgeneraleventfakeactive \"Group picture\" \"Everyday\"");
                    eb = Tools.CreateHelpInfo("AddGeneralEventFakeActive", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbinfoeventfakeactive":
                    description = "Adds a cosmic break information event active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventfakeactive <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbinfoeventfakeactive \"Maintenance\" \"Everyday\"");
                    eb = Tools.CreateHelpInfo("AddCBInfoEventFakeActive", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbgeneventfakeactive":
                    description = "Adds a cosmic break general event active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventfakeactive <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbgeneventfakeactive \"Clan fight\" \"Everyday\"");
                    eb = Tools.CreateHelpInfo("AddCBGenEventFakeActive", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "addcbspeceventfakeactive":
                    description = "Adds a cosmic break special event active fake schedule to the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventfakeactive <event name> <event discription>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " addcbspeceventfakeactive \"Cards campaign\" \"Everyday\"");
                    eb = Tools.CreateHelpInfo("AddCBSpecEventFakeActive", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "resetgeneralevent":
                    description = "Resets a general event schedule from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " resetgeneralevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " resetgeneralevent \"Group picture\"");
                    eb = Tools.CreateHelpInfo("ResetGeneralEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "resetcbinfoevent":
                    description = "Resets a cosmic break information event schedule from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " resetcbinfoevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " resetcbinfoevent \"Maintenance\"");
                    eb = Tools.CreateHelpInfo("ResetCBInfoEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "resetcbgenevent":
                    description = "Resets a cosmic break general event schedule from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " resetcbgenevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " resetcbgenevent \"Clan fight\"");
                    eb = Tools.CreateHelpInfo("ResetCBGenEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                case "resetcbspecevent":
                    description = "Resets a cosmic break special event schedule from the " + Context.Guild.GetTextChannel(Setup.TimerChannelID).Mention + " channel.";
                    usage.Add("__Requires role:__");
                    usage.Add(Setup.Client.CurrentUser.Mention + " resetcbspecevent <event name>");
                    examples.Add("__Requires role:__");
                    examples.Add(Setup.Client.CurrentUser.Mention + " resetcbspecevent \"Cards campaign\"");
                    eb = Tools.CreateHelpInfo("ResetCBSpecEvent", new Color(255, 255, 255), description, usage, examples);
                    break;
                default:
                    eb.Title = "**🚫 unknown command!**";
                    eb.WithColor(Color.Red);
                    break;
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Message.DeleteAsync();
        }
    }
}
