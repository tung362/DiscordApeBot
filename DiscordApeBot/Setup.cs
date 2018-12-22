using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Webhook;
using System.Windows.Forms;

namespace DiscordApeBot
{
    class Setup
    {
        /*Settings*/
        //Version
        public static string BotVersion = "Zura >:V Client Version: 1.0.0";

        //Core
        public static DiscordSocketClient Client = new DiscordSocketClient();

        //Webhooks
        public static DiscordWebhookClient GorillaWebhookClient;
        public static DiscordWebhookClient MonkeyWebhookClient;
        public static DiscordWebhookClient BaboonWebhookClient;

        //Login
        public static string Game = "Cosmic Break Meta";
        public static UserStatus Status = UserStatus.DoNotDisturb;

        //Time zone
        public static TimeZoneInfo CurrentTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

        //Random
        public static Random Rand = new Random();

        //Amount
        public static int TimerUpdateInterval = 60000;
        public static long ExperienceGainPerMessage = 30;
        public static double ExperienceGrowthModifier = 1.618;
        public static int KillScreenshotWorth = 1;
        public static int DailyBaseWorth = 200;
        public static int HoursTillReset = 12;
        public static int MarriagePrice = 2000;

        //House roles
        public static string HouseOfGorillaRole = "House of gorilla";
        public static string HouseOfMonkeyRole = "House of monkey";
        public static string HouseOfBaboonRole = "House of baboon";

        //Normal roles
        public static string ApeChieftainRole = "Ape chieftain";
        public static string UnknownAnimalRole = "Unknown animal";
        public static string AwakenedApeRole = "Awakened ape";
        public static string TribeMemberRole = "Tribe member";

        //Moderator roles
        public static string RecruiterRole = "Recruiter";
        public static string ChatCleanerRole = "Chat cleaner";

        //Custom emojis
        public static string GorillaEmoji = "<:HouseOfGorilla:494420117606629376>";
        public static string MonkeyEmoji = "<:HouseOfMonkey:494420115866124299>";
        public static string BaboonEmoji = "<:HouseOfBaboon:494420114720948225>";

        //Emojis
        public static string BallotCheckEmoji = "\U00002611";
        public static string GreenCheckEmoji = "\U00002705";
        public static string CautionEmoji = "\U000026A0";
        public static string ThumbsUpEmoji = "\U0001F44D";
        public static string ThumbsDownEmoji = "\U0001F44E";

        //Guilds
        public static ulong ApeGangGuildID = 493669082491518976;

        //channels
        public static ulong WelcomeChannelID = 494691777144422406;
        public static ulong HouseChannelID = 494285202282119179;
        public static ulong HouseOfGorillaChannelID = 494371697600757771;
        public static ulong HouseOfMonkeyChannelID = 494372281103941642;
        public static ulong HouseOfBaboonChannelID = 494373131507597312;
        public static ulong TimerChannelID = 494719258778140672;
        public static ulong VoteChannelID = 493689031226556420;
        public static ulong RulesChannelID = 493674738594611200;
        public static ulong KillScreenshotsChannelID = 495027148638257154;
        public static ulong NSFWChannelID = 493697207917084673;
        public static ulong SpamChannelID = 493690400016433153;
        public static ulong SkinsChannelID = 493691665710841856;
        public static ulong BuildsChannelID = 493691986784550923;
        public static ulong HuntListChannelID = 493692301256949760;

        //Saves and loads
        public static string PlayerSavesPath = Application.StartupPath + "/PlayerSaves";
        public static string InviteCodesPath = Application.StartupPath + "/InviteCodes";
        public static string HousePath = Application.StartupPath + "/ServerSaves/House";
        public static string HuntListPath = Application.StartupPath + "/ServerSaves/HuntList";
        public static string GeneralEventsPath = Application.StartupPath + "/ServerSaves/GeneralEvents";
        public static string CBInfoEventsPath = Application.StartupPath + "/ServerSaves/CBInfoEvents";
        public static string CBGenEventsPath = Application.StartupPath + "/ServerSaves/CBGenEvents";
        public static string CBSpecEventsPath = Application.StartupPath + "/ServerSaves/CBSpecEvents";
        public static string HuntListDynamicIDPath = Application.StartupPath + "/ServerSaves/DynamicIDs/HuntListDynamicID";
        public static string PointsDynamicIDPath = Application.StartupPath + "/ServerSaves/DynamicIDs/PointsDynamicID";
        public static string EventsDynamicIDPath = Application.StartupPath + "/ServerSaves/DynamicIDs/EventsDynamicID";
        public static string ServerDynamicIDPath = Application.StartupPath + "/ServerSaves/DynamicIDs/ServerDynamicID";

        //Website links
        public static string EventsTimerThumbnail = "http://mkkr.biz/wp-content/uploads/animated-gif-stopwatch-transparent-clock-gif-fast-transparent-clock-gif-limonchello.gif";
        public static string ServerTimerThumbnail = "http://24.media.tumblr.com/cbe636a2f7dd97b89bc6e94dd45061af/tumblr_mjq2beKNN61s6x4k6o1_400.gif";
        public static string HelpThumbnail = "https://media1.tenor.com/images/296bbac2f3635736295e796bd6ae1c88/tenor.gif?itemid=7792291";

        //Resources
        public static string ClapActionPath = Application.StartupPath + "/ArtAssets/Actions/Clap";
        public static string CuddleActionPath = Application.StartupPath + "/ArtAssets/Actions/Cuddle";
        public static string DanceActionPath = Application.StartupPath + "/ArtAssets/Actions/Dance";
        public static string FightActionPath = Application.StartupPath + "/ArtAssets/Actions/Fight";
        public static string GrabActionPath = Application.StartupPath + "/ArtAssets/Actions/Grab";
        public static string HateActionPath = Application.StartupPath + "/ArtAssets/Actions/Hate";
        public static string HighFiveActionPath = Application.StartupPath + "/ArtAssets/Actions/HighFive";
        public static string HoldHandsActionPath = Application.StartupPath + "/ArtAssets/Actions/HoldHands";
        public static string HugActionPath = Application.StartupPath + "/ArtAssets/Actions/Hug";
        public static string KissActionPath = Application.StartupPath + "/ArtAssets/Actions/Kiss";
        public static string MateActionPath = Application.StartupPath + "/ArtAssets/Actions/Mate";
        public static string PokeActionPath = Application.StartupPath + "/ArtAssets/Actions/Poke";
        public static string PunchActionPath = Application.StartupPath + "/ArtAssets/Actions/Punch";
        public static string SlapActionPath = Application.StartupPath + "/ArtAssets/Actions/Slap";
        public static string StareActionPath = Application.StartupPath + "/ArtAssets/Actions/Stare";

        public static string WelcomePicturePath = Application.StartupPath + "/ArtAssets/Welcome/Welcome.png";

        public static string GorillaBannerPicturePath = Application.StartupPath + "/ArtAssets/ApeHouses/Gorilla.png";
        public static string MonkeyBannerPicturePath = Application.StartupPath + "/ArtAssets/ApeHouses/Monkey.png";
        public static string BaboonBannerPicturePath = Application.StartupPath + "/ArtAssets/ApeHouses/Baboon.png";

        public static string GorillaIconPicturePath = Application.StartupPath + "/ArtAssets/ApeHouses/Emoji/HouseOfGorilla.png";
        public static string MonkeyIconPicturePath = Application.StartupPath + "/ArtAssets/ApeHouses/Emoji/HouseOfMonkey.png";
        public static string BaboonIconPicturePath = Application.StartupPath + "/ArtAssets/ApeHouses/Emoji/HouseOfBaboon.png";

        public static string EventBannerPicturePath = Application.StartupPath + "/ArtAssets/Rules/Events.png";
        public static string HouseBannerPicturePath = Application.StartupPath + "/ArtAssets/Rules/House.png";
        public static string HuntListBannerPicturePath = Application.StartupPath + "/ArtAssets/Rules/HuntList.png";
        public static string PointBannerPicturePath = Application.StartupPath + "/ArtAssets/Rules/Points.png";
        public static string RuleBannerPicturePath = Application.StartupPath + "/ArtAssets/Rules/Rules.png";
        public static string ServerBannerPicturePath = Application.StartupPath + "/ArtAssets/Rules/Server.png";

        public static string BottomLinePicturePath = Application.StartupPath + "/ArtAssets/UI/BottomLine.png";
        public static string MiddleLinePicturePath = Application.StartupPath + "/ArtAssets/UI/MiddleLine.png";
        public static string TopLinePicturePath = Application.StartupPath + "/ArtAssets/UI/TopLine.png";

        //Bot credentials
        public static string BotToken
        {
            get
            {
                StreamReader reader = File.OpenText(Application.StartupPath + "/Tokens/Token.token");
                return reader.ReadLine();
            }
        }

        public static string GorillaToken
        {
            get
            {
                StreamReader reader = File.OpenText(Application.StartupPath + "/Tokens/Gorilla.token");
                return reader.ReadLine();
            }
        }

        public static string MonkeyToken
        {
            get
            {
                StreamReader reader = File.OpenText(Application.StartupPath + "/Tokens/Monkey.token");
                return reader.ReadLine();
            }
        }

        public static string BaboonToken
        {
            get
            {
                StreamReader reader = File.OpenText(Application.StartupPath + "/Tokens/Baboon.token");
                return reader.ReadLine();
            }
        }

        public static async Task Login()
        {
            //Bot login
            string botToken = BotToken;
            await Client.LoginAsync(TokenType.Bot, botToken);
            await Client.StartAsync();
            await Client.SetGameAsync(Game);
            await Client.SetStatusAsync(Status);

            //Webhook login
            string gorillaToken = GorillaToken;
            GorillaWebhookClient = new DiscordWebhookClient(497504740075241472, gorillaToken);

            string monkeyToken = MonkeyToken;
            MonkeyWebhookClient = new DiscordWebhookClient(497506214767493120, monkeyToken);

            string baboonToken = BaboonToken;
            BaboonWebhookClient = new DiscordWebhookClient(497506409026551810, baboonToken);

            //Debug
            Console.Write("token: " + botToken + "\n");
            Console.Write("Hook: " + gorillaToken + "\n");
            Console.Write("Hook: " + monkeyToken + "\n");
            Console.Write("Hook: " + baboonToken + "\n");

            //Client.Log += (m) =>
            //{
            //    Console.Write(m.ToString() + "\n");
            //    return Task.Delay(0);
            //};

            //GorillaWebhookClient.Log += (m) =>
            //{
            //    Console.Write(m.ToString() + "\n");
            //    return Task.Delay(0);
            //};

            //MonkeyWebhookClient.Log += (m) =>
            //{
            //    Console.Write(m.ToString() + "\n");
            //    return Task.Delay(0);
            //};

            //BaboonWebhookClient.Log += (m) =>
            //{
            //    Console.Write(m.ToString() + "\n");
            //    return Task.Delay(0);
            //};
        }
    }
}
