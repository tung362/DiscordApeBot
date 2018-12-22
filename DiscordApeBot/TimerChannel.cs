using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Audio;
using DiscordApeBot.SaveData;

namespace DiscordApeBot
{
    class TimerChannel
    {
        public static async Task PeriodicUpdate()
        {
            while(true)
            {
                await UpdateTimers();
                await Task.Delay(Setup.TimerUpdateInterval);
            }
        }

        static async Task UpdateTimers()
        {
            //Loaded data
            BinaryHouseNormal houseEntry = new BinaryHouseNormal(0, 0, 0);
            BinaryEventArray generalEventsEntry = new BinaryEventArray(new BinaryEvent[0]);
            BinaryEventArray cbInfoEventsEntry = new BinaryEventArray(new BinaryEvent[0]);
            BinaryEventArray cbGenEventsEntry = new BinaryEventArray(new BinaryEvent[0]);
            BinaryEventArray cbSpecEventsEntry = new BinaryEventArray(new BinaryEvent[0]);
            ulong pointsDynamicID = 0;
            ulong eventsDynamicID = 0;
            ulong serverDynamicID = 0;
            //Load
            HouseNormal.Load(Setup.HousePath, ref houseEntry);
            EventArray.Load(Setup.GeneralEventsPath, ref generalEventsEntry);
            EventArray.Load(Setup.CBInfoEventsPath, ref cbInfoEventsEntry);
            EventArray.Load(Setup.CBGenEventsPath, ref cbGenEventsEntry);
            EventArray.Load(Setup.CBSpecEventsPath, ref cbSpecEventsEntry);
            ULongNormal.Load(Setup.PointsDynamicIDPath, ref pointsDynamicID);
            ULongNormal.Load(Setup.EventsDynamicIDPath, ref eventsDynamicID);
            ULongNormal.Load(Setup.ServerDynamicIDPath, ref serverDynamicID);

            IMessage pointsMessage = await Setup.Client.GetGuild(Setup.ApeGangGuildID).GetTextChannel(Setup.TimerChannelID).GetMessageAsync(pointsDynamicID);
            IMessage eventsMessage = await Setup.Client.GetGuild(Setup.ApeGangGuildID).GetTextChannel(Setup.TimerChannelID).GetMessageAsync(eventsDynamicID);
            IMessage serverMessage = await Setup.Client.GetGuild(Setup.ApeGangGuildID).GetTextChannel(Setup.TimerChannelID).GetMessageAsync(serverDynamicID);

            List<IEmbed> pointsEmbeds = pointsMessage.Embeds.ToList();
            List<IEmbed> eventsEmbeds = eventsMessage.Embeds.ToList();
            List<IEmbed> serverEmbeds = serverMessage.Embeds.ToList();

            EmbedBuilder pointsEB = pointsEmbeds[0].ToEmbedBuilder();
            EmbedBuilder eventsEB = eventsEmbeds[0].ToEmbedBuilder();
            EmbedBuilder serverEB = serverEmbeds[0].ToEmbedBuilder();

            //points
            for (int i = 0; i < pointsEB.Fields.Count; i++)
            {
                if (pointsEB.Fields[i].Name == Setup.GorillaEmoji + "House of Gorilla") pointsEB.Fields[i].WithValue("`" + houseEntry.GorillaPoints + " Points`");
                else if (pointsEB.Fields[i].Name == Setup.MonkeyEmoji + "House of Monkey") pointsEB.Fields[i].WithValue("`" + houseEntry.MonkeyPoints + " Points`");
                else if (pointsEB.Fields[i].Name == Setup.BaboonEmoji + "House of Baboon") pointsEB.Fields[i].WithValue("`" + houseEntry.BaboonPoints + " Points`");
            }

            //events
            //Splits the events into two category
            bool saveEventsEntry = false;
            List<string> eventsInactive = new List<string>();
            List<string> eventsActive = new List<string>();

            TimeCalculation(ref generalEventsEntry, ref eventsInactive, ref eventsActive, ref saveEventsEntry);
            //Save
            if (saveEventsEntry) EventArray.Save(Setup.GeneralEventsPath, generalEventsEntry);

            for (int i = 0; i < eventsEB.Fields.Count; i++)
            {
                string fieldText = "";
                if (eventsEB.Fields[i].Name == "Events")
                {
                    if (eventsInactive.Count == 0) fieldText = "`None`";
                    for (int j = 0; j < eventsInactive.Count; j++) fieldText += "`" + eventsInactive[j] + "`" + "\n";
                    eventsEB.Fields[i].WithValue(fieldText);
                }
                else if (eventsEB.Fields[i].Name == "Active events")
                {
                    if (eventsActive.Count == 0) fieldText = "`None`";
                    for (int j = 0; j < eventsActive.Count; j++) fieldText += "`" + eventsActive[j] + "`" + "\n";
                    eventsEB.Fields[i].WithValue(fieldText);
                }
            }

            //server
            bool saveCBInfoEntry = false;
            bool saveCBGenEntry = false;
            bool saveCBSpecEntry = false;
            List<string> CBInfoInactive = new List<string>();
            List<string> CBInfoActive = new List<string>();
            List<string> CBGenInactive = new List<string>();
            List<string> CBGenActive = new List<string>();
            List<string> CBSpecInactive = new List<string>();
            List<string> CBSpecActive = new List<string>();

            TimeCalculation(ref cbInfoEventsEntry, ref CBInfoInactive, ref CBInfoActive, ref saveCBInfoEntry);
            TimeCalculation(ref cbGenEventsEntry, ref CBGenInactive, ref CBGenActive, ref saveCBGenEntry);
            TimeCalculation(ref cbSpecEventsEntry, ref CBSpecInactive, ref CBSpecActive, ref saveCBSpecEntry);
            //Save
            if (saveCBInfoEntry) EventArray.Save(Setup.CBInfoEventsPath, cbInfoEventsEntry);
            if (saveCBGenEntry) EventArray.Save(Setup.CBGenEventsPath, cbGenEventsEntry);
            if (saveCBSpecEntry) EventArray.Save(Setup.CBSpecEventsPath, cbSpecEventsEntry);

            string serverText = "`Server time: " + DateTimeOffset.Now.ToOffset(Setup.CurrentTimeZone.BaseUtcOffset).ToString("HH:mm:ss (M/dd/yyyy)") + "`" + "\n";
            for (int j = 0; j < CBInfoInactive.Count; j++) serverText += "`" + CBInfoInactive[j] + "`" + "\n";
            for (int j = 0; j < CBInfoActive.Count; j++) serverText += "`" + CBInfoActive[j] + "`" + "\n";
            serverEB.WithDescription(serverText);

            for (int i = 0; i < serverEB.Fields.Count; i++)
            {
                string fieldText = "";
                if (serverEB.Fields[i].Name == "General events")
                {
                    if (CBGenInactive.Count == 0) fieldText = "`None`";
                    for (int j = 0; j < CBGenInactive.Count; j++) fieldText += "`" + CBGenInactive[j] + "`" + "\n";
                    serverEB.Fields[i].WithValue(fieldText);
                }
                else if (serverEB.Fields[i].Name == "Active general events")
                {
                    if (CBGenActive.Count == 0) fieldText = "`None`";
                    for (int j = 0; j < CBGenActive.Count; j++) fieldText += "`" + CBGenActive[j] + "`" + "\n";
                    serverEB.Fields[i].WithValue(fieldText);
                }
                else if (serverEB.Fields[i].Name == "Special events")
                {
                    if (CBSpecInactive.Count == 0) fieldText = "`None`";
                    for (int j = 0; j < CBSpecInactive.Count; j++) fieldText += "`" + CBSpecInactive[j] + "`" + "\n";
                    serverEB.Fields[i].WithValue(fieldText);
                }
                else if (serverEB.Fields[i].Name == "Active special events")
                {
                    if (CBSpecActive.Count == 0) fieldText = "`None`";
                    for (int j = 0; j < CBSpecActive.Count; j++) fieldText += "`" + CBSpecActive[j] + "`" + "\n";
                    serverEB.Fields[i].WithValue(fieldText);
                }
            }

            await (pointsMessage as IUserMessage).ModifyAsync(msg =>
            {
                msg.Embed = pointsEB.Build();
            });

            await (eventsMessage as IUserMessage).ModifyAsync(msg =>
            {
                msg.Embed = eventsEB.Build();
            });

            await (serverMessage as IUserMessage).ModifyAsync(msg =>
            {
                msg.Embed = serverEB.Build();
            });

        }

        public static DateTimeOffset GetNextWeekday(DateTimeOffset start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd + 1);
        }

        public static int GetNextWeekdayDays(DayOfWeek start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start + 7) % 7;
            return daysToAdd + 1;
        }

        public static void TimeCalculation(ref BinaryEventArray EventsEntry, ref List<string> inactives, ref List<string> actives, ref bool save)
        {
            DateTimeOffset currentDateTime = DateTimeOffset.Now.ToOffset(Setup.CurrentTimeZone.BaseUtcOffset);

            //Each event
            for (int i = 0; i < EventsEntry.Events.Length; i++)
            {
                //-1 = no type, 0 = fake inactive, 1 = fake active, 2 = daily, 3 = date
                if (EventsEntry.Events[i].EventType == -1) inactives.Add(EventsEntry.Events[i].EventName + ": No data");
                else if (EventsEntry.Events[i].EventType == 0) inactives.Add(EventsEntry.Events[i].EventName + ": " + EventsEntry.Events[i].Schedules[0].StartData);
                else if (EventsEntry.Events[i].EventType == 1) actives.Add(EventsEntry.Events[i].EventName + ": " + EventsEntry.Events[i].Schedules[0].StartData);
                else if (EventsEntry.Events[i].EventType == 2)
                {
                    //Get closest next schedule
                    int closestDayCount = int.MaxValue;
                    int closestScheduleIndex = 0;
                    for (int j = 0; j < EventsEntry.Events[i].Schedules.Length; j++)
                    {
                        DayOfWeek startingDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), EventsEntry.Events[i].Schedules[j].StartData, true);
                        int daysToNextStartingDay = GetNextWeekdayDays(currentDateTime.DayOfWeek + 1, startingDay);

                        if (daysToNextStartingDay < closestDayCount)
                        {
                            closestDayCount = GetNextWeekdayDays(currentDateTime.DayOfWeek + 1, startingDay);
                            closestScheduleIndex = j;
                        }
                    }

                    bool matchEvent = false;

                    //Each event schedule
                    for (int j = 0; j < EventsEntry.Events[i].Schedules.Length; j++)
                    {
                        DayOfWeek startingDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), EventsEntry.Events[i].Schedules[j].StartData, true);
                        DayOfWeek endingDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), EventsEntry.Events[i].Schedules[j].EndData, true);

                        bool inDayRange = false;

                        if (startingDay <= endingDay)
                        {
                            if (currentDateTime.DayOfWeek >= startingDay && currentDateTime.DayOfWeek <= endingDay) inDayRange = true;
                        }
                        else
                        {
                            if (currentDateTime.DayOfWeek >= startingDay || currentDateTime.DayOfWeek <= endingDay) inDayRange = true;
                        }

                        //If in day of week range
                        if (inDayRange)
                        {
                            //If there is specific time
                            if (EventsEntry.Events[i].Schedules[j].StartTimes.Length != 0)
                            {
                                bool RangeNotPassed = false;
                                for (int k = 0; k < EventsEntry.Events[i].Schedules[j].StartTimes.Length; k++)
                                {
                                    TimeSpan startingTime = new TimeSpan(EventsEntry.Events[i].Schedules[j].StartTimes[k]);
                                    TimeSpan endingTime = startingTime.Add(new TimeSpan(EventsEntry.Events[i].Schedules[j].Duration));

                                    //If hasn't passed the time range
                                    if (currentDateTime.TimeOfDay <= endingTime)
                                    {
                                        //If outside the time range
                                        if (currentDateTime.TimeOfDay <= startingTime)
                                        {
                                            TimeSpan remainingTime = startingTime.Subtract(currentDateTime.TimeOfDay);
                                            inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                        }
                                        //If in the time range
                                        else
                                        {
                                            TimeSpan remainingTime = endingTime.Subtract(currentDateTime.TimeOfDay);
                                            actives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                        }
                                        RangeNotPassed = true;
                                        matchEvent = true;
                                        break;
                                    }
                                    //Keep searching if passed the time range
                                    else continue;
                                }

                                //Find closest next day in range
                                if(!RangeNotPassed)
                                {
                                    DayOfWeek tomorrow = currentDateTime.AddDays(1).DayOfWeek;

                                    bool tomorrowInRange = false;
                                    if (startingDay <= endingDay)
                                    {
                                        if (tomorrow >= startingDay && tomorrow <= endingDay) tomorrowInRange = true;
                                    }
                                    else
                                    {
                                        if (tomorrow >= startingDay || tomorrow <= endingDay) tomorrowInRange = true;
                                    }

                                    if (tomorrowInRange)
                                    {
                                        TimeSpan startingTime = new TimeSpan(EventsEntry.Events[i].Schedules[j].StartTimes[0]);
                                        DateTimeOffset modifiedTomorrow = currentDateTime.AddDays(1).Date.Add(startingTime);
                                        TimeSpan remainingTime = modifiedTomorrow.Subtract(currentDateTime);
                                        inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                        matchEvent = true;
                                    }
                                }
                            }
                            //If doesn't have a specific time
                            else
                            {
                                DateTimeOffset endingDate = GetNextWeekday(currentDateTime.Date, endingDay);
                                TimeSpan remainingTime = endingDate.Subtract(currentDateTime);

                                actives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                matchEvent = true;
                            }
                            break;
                        }
                        //Keep searching if not in day of week range
                        else continue;
                    }

                    //If not in day of week range
                    if (!matchEvent)
                    {
                        //If there is specific time
                        if (EventsEntry.Events[i].Schedules[closestScheduleIndex].StartTimes.Length != 0)
                        {
                            DateTimeOffset nextStartingDate = currentDateTime.Date.AddDays(closestDayCount);
                            //Find closest time
                            DateTimeOffset modifiedNextStartingDate = nextStartingDate.Add(new TimeSpan(EventsEntry.Events[i].Schedules[closestScheduleIndex].StartTimes[0]));
                            TimeSpan remainingTime = modifiedNextStartingDate.Subtract(currentDateTime);
                            inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                        }
                        //If doesn't have a specific time
                        else
                        {
                            DateTimeOffset nextStartingDate = currentDateTime.Date.AddDays(closestDayCount);
                            TimeSpan remainingTime = nextStartingDate.Subtract(currentDateTime);
                            inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                        }
                    }
                }
                else if (EventsEntry.Events[i].EventType == 3)
                {
                    //Get closest next schedule
                    long closestDayCount = long.MaxValue;
                    int closestScheduleIndex = 0;
                    List<BinaryEventSchedule> schedules = EventsEntry.Events[i].Schedules.ToList();
                    for (int j = 0; j < schedules.Count; j++)
                    {
                        DateTimeOffset startingDay = DateTimeOffset.ParseExact(schedules[j].StartData, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                        DateTimeOffset endingDay = DateTimeOffset.ParseExact(schedules[j].EndData, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                        //Remove outdated schedules
                        if (currentDateTime > endingDay)
                        {
                            save = true;
                            schedules.RemoveAt(j);
                            j--;
                            continue;
                        }

                        if (Math.Abs(startingDay.Ticks - currentDateTime.Ticks) < closestDayCount)
                        {
                            closestDayCount = Math.Abs(startingDay.Ticks - currentDateTime.Ticks);
                            closestScheduleIndex = j;
                        }
                    }
                    //Apply modifications
                    EventsEntry.Events[i].Schedules = schedules.ToArray();

                    //If outdated
                    if (schedules.Count == 0)
                    {
                        inactives.Add(EventsEntry.Events[i].EventName + ": Outdated Data");
                        continue;
                    }

                    bool matchEvent = false;

                    //Each event schedule
                    for (int j = 0; j < schedules.Count; j++)
                    {
                        DateTimeOffset startingDay = DateTimeOffset.ParseExact(schedules[j].StartData, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                        DateTimeOffset endingDay = DateTimeOffset.ParseExact(schedules[j].EndData, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                        //If in day of week range
                        if (currentDateTime.Date >= startingDay && currentDateTime.Date <= endingDay)
                        {
                            //If there is specific time
                            if (schedules[j].StartTimes.Length != 0)
                            {
                                bool RangeNotPassed = false;
                                for (int k = 0; k < schedules[j].StartTimes.Length; k++)
                                {
                                    TimeSpan startingTime = new TimeSpan(schedules[j].StartTimes[k]);
                                    TimeSpan endingTime = startingTime.Add(new TimeSpan(schedules[j].Duration));

                                    //If hasn't passed the time range
                                    if (currentDateTime.TimeOfDay <= endingTime)
                                    {
                                        //If outside the time range
                                        if (currentDateTime.TimeOfDay <= startingTime)
                                        {
                                            TimeSpan remainingTime = startingTime.Subtract(currentDateTime.TimeOfDay);
                                            inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                        }
                                        //If in the time range
                                        else
                                        {
                                            TimeSpan remainingTime = endingTime.Subtract(currentDateTime.TimeOfDay);
                                            actives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                        }
                                        RangeNotPassed = true;
                                        matchEvent = true;
                                        break;
                                    }
                                    //Keep searching if passed the time range
                                    else continue;
                                }

                                //Find closest next day in range
                                if (!RangeNotPassed)
                                {
                                    DateTimeOffset tomorrow = currentDateTime.AddDays(1);

                                    bool tomorrowInRange = false;
                                    if(tomorrow.Date >= startingDay && tomorrow.Date <= endingDay) tomorrowInRange = true;

                                    if (tomorrowInRange)
                                    {
                                        TimeSpan startingTime = new TimeSpan(schedules[j].StartTimes[0]);
                                        DateTimeOffset modifiedTomorrow = currentDateTime.AddDays(1).Date.Add(startingTime);
                                        TimeSpan remainingTime = modifiedTomorrow.Subtract(currentDateTime);
                                        inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                        matchEvent = true;
                                    }
                                }
                            }
                            //If doesn't have a specific time
                            else
                            {
                                TimeSpan remainingTime = endingDay.Subtract(currentDateTime);

                                actives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                                matchEvent = true;
                            }
                            break;
                        }
                        //Keep searching if not in day of week range
                        else continue;
                    }

                    //If not in day of week range
                    if (!matchEvent)
                    {
                        DateTimeOffset closestNextDay = DateTimeOffset.ParseExact(schedules[closestScheduleIndex].StartData, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                        //If there is specific time
                        if (schedules[closestScheduleIndex].StartTimes.Length != 0)
                        {
                            //Find closest time
                            DateTimeOffset modifiedClosestNextDay = closestNextDay.Add(new TimeSpan(schedules[closestScheduleIndex].StartTimes[0]));
                            TimeSpan remainingTime = modifiedClosestNextDay.Subtract(currentDateTime);
                            inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                        }
                        //If doesn't have a specific time
                        else
                        {
                            TimeSpan remainingTime = closestNextDay.Subtract(currentDateTime);
                            inactives.Add(EventsEntry.Events[i].EventName + ": " + remainingTime.Days + "d" + remainingTime.Hours + "h" + remainingTime.Minutes + "m");
                        }
                    }
                }
            }
        }
    }
}
