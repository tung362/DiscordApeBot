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
    public class ActionCommands : ModuleBase<SocketCommandContext>
    {
        [Command("clap")]
        public async Task clapAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.ClapActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " claps to " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.ClapActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("cuddle")]
        public async Task CuddleAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.CuddleActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " Cuddles with " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.CuddleActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("dance")]
        public async Task DanceAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.DanceActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " dances with " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.DanceActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("fight")]
        public async Task FightAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.FightActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " fights with " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.FightActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("grab")]
        public async Task GrabAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.GrabActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " grabs " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.GrabActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("hate")]
        public async Task HateAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.HateActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " hates " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.HateActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("highfive")]
        public async Task HighFiveAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.HighFiveActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " high fives " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.HighFiveActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("holdhands")]
        public async Task HoldHandsAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.HoldHandsActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " hold hands with " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.HoldHandsActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("hug")]
        public async Task HugAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.HugActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " hugs " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.HugActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("kiss")]
        public async Task KissAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.KissActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " kisses " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.KissActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("mate")]
        public async Task MateAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.MateActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " mates with " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.MateActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("poke")]
        public async Task PokeAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.PokeActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " pokes " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.PokeActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("punch")]
        public async Task PunchAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.PunchActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " punches " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.PunchActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("slap")]
        public async Task SlapAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.SlapActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " slaps " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.SlapActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }

        [Command("stare")]
        public async Task StareAction(SocketUser mentionedUser)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(Setup.StareActionPath);
            List<FileSystemInfo> actionFiles = fileInfo.GetFileSystemInfos().ToList();

            string text = "💬 " + Tools.GetNickname(Context, Context.User.Id) + " stares at " + Tools.GetNickname(Context, mentionedUser.Id) + "!";
            await Context.Channel.SendFileAsync(Setup.StareActionPath + "/" + actionFiles[Setup.Rand.Next(0, actionFiles.Count)].Name, text);
            await Context.Message.DeleteAsync();
        }
    }
}
