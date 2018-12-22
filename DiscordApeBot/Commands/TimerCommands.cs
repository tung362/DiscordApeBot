using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Audio;
using DiscordApeBot.SaveData;

namespace DiscordApeBot.Commands
{
    public class TimerCommands : ModuleBase<SocketCommandContext>
    {
        [Command("addgeneralevent")]
        public async Task AddGeneralEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEvent(eventName, "general", Setup.GeneralEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbinfoevent")]
        public async Task AddCBInfoEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEvent(eventName, "cosmic break", Setup.CBInfoEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbgenevent")]
        public async Task AddCBGenEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEvent(eventName, "cosmic break", Setup.CBGenEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbspecevent")]
        public async Task AddCBSpecEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEvent(eventName, "cosmic break", Setup.CBSpecEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("removegeneralevent")]
        public async Task RemoveGeneralEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await RemoveEvent(eventName, "general", Setup.GeneralEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("removecbinfoevent")]
        public async Task RemoveCBInfoEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await RemoveEvent(eventName, "cosmic break", Setup.CBInfoEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("removecbgenevent")]
        public async Task RemoveCBGenEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await RemoveEvent(eventName, "cosmic break", Setup.CBGenEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("removecbspecevent")]
        public async Task RemoveCBSpecEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await RemoveEvent(eventName, "cosmic break", Setup.CBSpecEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("addgeneraleventdaily")]
        public async Task AddGeneralEventDaily(string eventName, string startingDay, string endingDay, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDaily(eventName, Setup.GeneralEventsPath, startingDay, endingDay, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbinfoeventdaily")]
        public async Task AddCBInfoEventDaily(string eventName, string startingDay, string endingDay, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDaily(eventName, Setup.CBInfoEventsPath, startingDay, endingDay, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbgeneventdaily")]
        public async Task AddCBGenEventDaily(string eventName, string startingDay, string endingDay, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDaily(eventName, Setup.CBGenEventsPath, startingDay, endingDay, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbspeceventdaily")]
        public async Task AddCBSpecEventDaily(string eventName, string startingDay, string endingDay, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDaily(eventName, Setup.CBSpecEventsPath, startingDay, endingDay, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addgeneraleventdate")]
        public async Task AddGeneralEventDate(string eventName, string startingDate, string endingDate, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDate(eventName, Setup.GeneralEventsPath, startingDate, endingDate, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbinfoeventdate")]
        public async Task AddCBInfoEventDate(string eventName, string startingDate, string endingDate, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDate(eventName, Setup.CBInfoEventsPath, startingDate, endingDate, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbgeneventdate")]
        public async Task AddCBGenEventDate(string eventName, string startingDate, string endingDate, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDate(eventName, Setup.CBGenEventsPath, startingDate, endingDate, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbspeceventdate")]
        public async Task AddCBSpecEventDate(string eventName, string startingDate, string endingDate, string duration, params String[] startTimes)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventDate(eventName, Setup.CBSpecEventsPath, startingDate, endingDate, duration, startTimes);
            await Context.Message.DeleteAsync();
        }

        [Command("addgeneraleventfake")]
        public async Task AddGeneralEventFake(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.GeneralEventsPath, discription, false);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbinfoeventfake")]
        public async Task AddCBInfoEventFake(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.CBInfoEventsPath, discription, false);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbgeneventfake")]
        public async Task AddCBGenEventFake(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.CBGenEventsPath, discription, false);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbspeceventfake")]
        public async Task AddCBSpecEventFake(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.CBSpecEventsPath, discription, false);
            await Context.Message.DeleteAsync();
        }

        [Command("addgeneraleventfakeactive")]
        public async Task AddGeneralEventFakeActive(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.GeneralEventsPath, discription, true);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbinfoeventfakeactive")]
        public async Task AddCBInfoEventFakeActive(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.CBInfoEventsPath, discription, true);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbgeneventfakeactive")]
        public async Task AddCBGenEventFakeActive(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.CBGenEventsPath, discription, true);
            await Context.Message.DeleteAsync();
        }

        [Command("addcbspeceventfakeactive")]
        public async Task AddCBSpecEventFakeActive(string eventName, string discription)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await AddEventFake(eventName, Setup.CBSpecEventsPath, discription, true);
            await Context.Message.DeleteAsync();
        }

        [Command("resetgeneralevent")]
        public async Task ResetGeneralEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await ResetEvent(eventName, Setup.GeneralEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("resetcbinfoevent")]
        public async Task ResetCBInfoEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await ResetEvent(eventName, Setup.CBInfoEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("resetcbgenevent")]
        public async Task ResetCBGenEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await ResetEvent(eventName, Setup.CBGenEventsPath);
            await Context.Message.DeleteAsync();
        }

        [Command("resetcbspecevent")]
        public async Task ResetCBSpecEvent(string eventName)
        {
            //Roles with access
            List<string> roles = new List<string>();
            roles.Add(Setup.ApeChieftainRole);

            if (await Tools.RolesCheck(Context, roles, true, roles[0])) await ResetEvent(eventName, Setup.CBSpecEventsPath);
            await Context.Message.DeleteAsync();
        }

        public async Task AddEvent(string eventName, string eventListName, string path)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Loaded data
            BinaryEventArray entry = new BinaryEventArray(new BinaryEvent[0]);
            //Load
            EventArray.Load(path, ref entry);

            bool eventExists = false;
            for (int i = 0; i < entry.Events.Length; i++)
            {
                if (entry.Events[i].EventName == eventName)
                {
                    eventExists = true;
                    break;
                }
            }

            if (!eventExists)
            {
                //Modify
                List<BinaryEvent> eventEntries = entry.Events.ToList();
                BinaryEvent eventEntry = new BinaryEvent(eventName, -1, new BinaryEventSchedule[0]);
                eventEntries.Add(eventEntry);
                entry.Events = eventEntries.ToArray();
                //Save
                EventArray.Save(path, entry);

                eb.Title = "**🗓️ " + eventName + " has been added to the " + eventListName + " events list!**";
                eb.WithColor(Color.Green);
            }
            else
            {
                eb.Title = "**🚫 The event " + eventName + " already exists!**";
                eb.WithColor(Color.Red);
            }

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        public async Task RemoveEvent(string eventName, string eventListName, string path)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Loaded data
            BinaryEventArray entry = new BinaryEventArray(new BinaryEvent[0]);
            //Load
            EventArray.Load(path, ref entry);

            bool eventExists = false;
            for (int i = 0; i < entry.Events.Length; i++)
            {
                if (entry.Events[i].EventName == eventName)
                {
                    eventExists = true;
                    break;
                }
            }

            if (eventExists)
            {
                //Modify
                List<BinaryEvent> eventEntries = entry.Events.ToList();
                for (int i = 0; i < eventEntries.Count; i++)
                {
                    if (eventEntries[i].EventName == eventName)
                    {
                        eventEntries.RemoveAt(i);
                        break;
                    }
                }
                entry.Events = eventEntries.ToArray();
                //Save
                EventArray.Save(path, entry);

                eb.Title = "**📁 " + eventName + " has been removed from the " + eventListName + " events list!**";
                eb.WithColor(Color.Green);
            }
            else
            {
                eb.Title = "**🚫 The event " + eventName + " doesn't exists!**";
                eb.WithColor(Color.Red);
            }

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        public async Task AddEventDaily(string eventName, string path, string startingDay, string endingDay, string duration, string[] startTimes)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Checks if the day string passed in is a valid value
            string[] dayNames = Enum.GetNames(typeof(DayOfWeek));
            bool isValidStartingDay = dayNames.Contains(startingDay, StringComparer.InvariantCultureIgnoreCase);
            bool isValidEndingDay = dayNames.Contains(endingDay, StringComparer.InvariantCultureIgnoreCase);

            if (isValidStartingDay && isValidEndingDay)
            {
                TimeSpan parsedDuration;
                bool isValidDuration = TimeSpan.TryParseExact(duration, "hh':'mm", null, out parsedDuration);

                if (isValidDuration)
                {
                    List<TimeSpan> parsedStartTimes = new List<TimeSpan>();
                    bool isValidStartTimes = true;
                    for(int i = 0; i < startTimes.Length; i++)
                    {
                        TimeSpan parsedStartTime;
                        if(TimeSpan.TryParseExact(startTimes[i], "hh':'mm", null, out parsedStartTime)) parsedStartTimes.Add(parsedStartTime);
                        else
                        {
                            isValidStartTimes = false;
                            break;
                        }
                    }
                    parsedStartTimes.Sort();

                    if (isValidStartTimes)
                    {
                        //Loaded data
                        BinaryEventArray entry = new BinaryEventArray(new BinaryEvent[0]);
                        //Load
                        EventArray.Load(path, ref entry);
                        //Modify
                        bool isValidEventName = false;
                        for (int i = 0; i < entry.Events.Length; i++)
                        {
                            if (entry.Events[i].EventName == eventName)
                            {
                                if(entry.Events[i].EventType == -1 || entry.Events[i].EventType == 2)
                                {
                                    entry.Events[i].EventType = 2;

                                    List<long> StartTimeTicks = new List<long>();
                                    for (int j = 0; j < parsedStartTimes.Count; j++) StartTimeTicks.Add(parsedStartTimes[j].Ticks);

                                    List<BinaryEventSchedule> schedules = entry.Events[i].Schedules.ToList();
                                    BinaryEventSchedule eventScheduleEntry = new BinaryEventSchedule(startingDay, endingDay, StartTimeTicks.ToArray(), parsedDuration.Ticks);
                                    schedules.Add(eventScheduleEntry);

                                    entry.Events[i].Schedules = schedules.ToArray();
                                    //Save
                                    EventArray.Save(path, entry);

                                    eb.Title = "**📝 A new schedule has been added to " + eventName + "!**";
                                    eb.WithColor(Color.Green);
                                }
                                else
                                {
                                    eb.Title = "**🚫 " + eventName + " is a different event type!**";
                                    eb.WithColor(Color.Red);
                                }

                                isValidEventName = true;
                                break;
                            }
                        }

                        if (!isValidEventName)
                        {
                            eb.Title = "**🚫 The event " + eventName + " doesn't exists!**";
                            eb.WithColor(Color.Red);
                        }
                    }
                    else
                    {
                        eb.Title = "**🚫 Invalid Start Times!**";
                        eb.WithColor(Color.Red);
                    }
                }
                else
                {
                    eb.Title = "**🚫 Invalid duration!**";
                    eb.WithColor(Color.Red);
                }
            }
            else
            {
                eb.Title = "**🚫 Invalid day!**";
                eb.WithColor(Color.Red);
            }

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        public async Task AddEventDate(string eventName, string path, string startingDate, string endingDate, string duration, string[] startTimes)
        {
            EmbedBuilder eb = new EmbedBuilder();

            DateTime parsedStartingDate;
            DateTime parsedEndingDate;
            bool isValidStartingDate = DateTime.TryParseExact(startingDate, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedStartingDate);
            bool isValidEndingDate = DateTime.TryParseExact(endingDate, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEndingDate);

            if (parsedStartingDate <= parsedEndingDate)
            {
                if (isValidStartingDate && isValidEndingDate)
                {
                    TimeSpan parsedDuration;
                    bool isValidDuration = TimeSpan.TryParseExact(duration, "hh':'mm", null, out parsedDuration);

                    if (isValidDuration)
                    {
                        List<TimeSpan> parsedStartTimes = new List<TimeSpan>();
                        bool isValidStartTimes = true;
                        for (int i = 0; i < startTimes.Length; i++)
                        {
                            TimeSpan parsedStartTime;
                            if (TimeSpan.TryParseExact(startTimes[i], "hh':'mm", null, out parsedStartTime)) parsedStartTimes.Add(parsedStartTime);
                            else
                            {
                                isValidStartTimes = false;
                                break;
                            }
                        }
                        parsedStartTimes.Sort();

                        if (isValidStartTimes)
                        {
                            //Loaded data
                            BinaryEventArray entry = new BinaryEventArray(new BinaryEvent[0]);
                            //Load
                            EventArray.Load(path, ref entry);
                            //Modify
                            bool isValidEventName = false;
                            for (int i = 0; i < entry.Events.Length; i++)
                            {
                                if (entry.Events[i].EventName == eventName)
                                {
                                    if (entry.Events[i].EventType == -1 || entry.Events[i].EventType == 3)
                                    {
                                        entry.Events[i].EventType = 3;

                                        List<long> StartTimeTicks = new List<long>();
                                        for (int j = 0; j < parsedStartTimes.Count; j++) StartTimeTicks.Add(parsedStartTimes[j].Ticks);

                                        List<BinaryEventSchedule> schedules = entry.Events[i].Schedules.ToList();
                                        BinaryEventSchedule eventScheduleEntry = new BinaryEventSchedule(startingDate, endingDate, StartTimeTicks.ToArray(), parsedDuration.Ticks);
                                        schedules.Add(eventScheduleEntry);

                                        entry.Events[i].Schedules = schedules.ToArray();
                                        //Save
                                        EventArray.Save(path, entry);

                                        eb.Title = "**📝 A new schedule has been added to " + eventName + "!**";
                                        eb.WithColor(Color.Green);
                                    }
                                    else
                                    {
                                        eb.Title = "**🚫 " + eventName + " is a different event type!**";
                                        eb.WithColor(Color.Red);
                                    }

                                    isValidEventName = true;
                                    break;
                                }
                            }

                            if (!isValidEventName)
                            {
                                eb.Title = "**🚫 The event " + eventName + " doesn't exists!**";
                                eb.WithColor(Color.Red);
                            }
                        }
                        else
                        {
                            eb.Title = "**🚫 Invalid Start Times!**";
                            eb.WithColor(Color.Red);
                        }
                    }
                    else
                    {
                        eb.Title = "**🚫 Invalid duration!**";
                        eb.WithColor(Color.Red);
                    }
                }
                else
                {
                    eb.Title = "**🚫 Invalid date!**";
                    eb.WithColor(Color.Red);
                }
            }
            else
            {
                eb.Title = "**🚫 Starting date must be earlier or equal to ending date!**";
                eb.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        public async Task AddEventFake(string eventName, string path, string discription, bool isActive)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Loaded data
            BinaryEventArray entry = new BinaryEventArray(new BinaryEvent[0]);
            //Load
            EventArray.Load(path, ref entry);
            //Modify
            bool isValidEventName = false;
            for (int i = 0; i < entry.Events.Length; i++)
            {
                if (entry.Events[i].EventName == eventName)
                {
                    if (entry.Events[i].EventType == -1 || entry.Events[i].EventType == 0 || entry.Events[i].EventType == 1)
                    {
                        entry.Events[i].EventType = 0;
                        if(isActive) entry.Events[i].EventType = 1;

                        List<BinaryEventSchedule> schedules = entry.Events[i].Schedules.ToList();
                        BinaryEventSchedule eventScheduleEntry = new BinaryEventSchedule(discription, "", new long[0], 0);
                        schedules.Add(eventScheduleEntry);

                        entry.Events[i].Schedules = schedules.ToArray();
                        //Save
                        EventArray.Save(path, entry);

                        eb.Title = "**📝 A new schedule has been added to " + eventName + "!**";
                        eb.WithColor(Color.Green);
                    }
                    else
                    {
                        eb.Title = "**🚫 " + eventName + " is a different event type!**";
                        eb.WithColor(Color.Red);
                    }

                    isValidEventName = true;
                    break;
                }
            }

            if (!isValidEventName)
            {
                eb.Title = "**🚫 The event " + eventName + " doesn't exists!**";
                eb.WithColor(Color.Red);
            }

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        public async Task ResetEvent(string eventName, string path)
        {
            EmbedBuilder eb = new EmbedBuilder();

            //Loaded data
            BinaryEventArray entry = new BinaryEventArray(new BinaryEvent[0]);
            //Load
            EventArray.Load(path, ref entry);
            //Modify
            bool isValidEventName = false;
            for (int i = 0; i < entry.Events.Length; i++)
            {
                if (entry.Events[i].EventName == eventName)
                {
                    entry.Events[i].EventType = -1;

                    List<BinaryEventSchedule> schedules = new List<BinaryEventSchedule>();
                    entry.Events[i].Schedules = schedules.ToArray();
                    //Save
                    EventArray.Save(path, entry);

                    eb.Title = "**📝 " + eventName + " has been reset!**";
                    eb.WithColor(Color.Green);

                    isValidEventName = true;
                    break;
                }
            }

            if (!isValidEventName)
            {
                eb.Title = "**🚫 The event " + eventName + " doesn't exists!**";
                eb.WithColor(Color.Red);
            }

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
    }
}
