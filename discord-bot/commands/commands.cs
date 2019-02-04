using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using discord_bot;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace discord_bot.commands
{
    public class Info : ModuleBase
    {
        // ~say hello -> hello
        [Command("say"), Summary("Echos a message.")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync(echo);
        }

        //TODO create ow module group       
        [Command("ow"), Summary("Retrieves stats given a tag.")]
        public async Task OverwatchStats([Summary("The player to search")] string user)
        {
            WebClient web = new WebClient();
            JObject stats = JObject.Parse(web.DownloadString($"https://ow-api.com/v1/stats/pc/us/{user}/profile"));

            string prestigeIcon = (string)stats["prestigeIcon"];
            string icon = (string)stats["icon"];

            //TODO format data
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync(icon);
        }
    }

    // Create a module with the 'sample' prefix
    [Group("sample")]
    public class Sample : ModuleBase
    {
        // ~sample square 20 -> 400
        [Command("square"), Summary("Squares a number.")]
        public async Task Square([Summary("The number to square.")] int num)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
        }

        [Command("userinfo"), Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}
