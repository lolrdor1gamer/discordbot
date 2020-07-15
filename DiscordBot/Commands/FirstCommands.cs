using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace DiscordBot.Commands
{
    public class FirstCommands : BaseCommandModule
    {

    [Command("ping")]
        [Description("Returns Pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }
        [Command("add")]
        [Description("Adds two intengers")]
        public async Task Add(CommandContext ctx, int one, int two)
        {
            await ctx.Channel.SendMessageAsync((one + two).ToString()).ConfigureAwait(false);
        }
    }
}
