using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Diagnostics;

namespace crackdotnet.Modules
{
    [Name("misc")]
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        [Command("say"), Alias("s")]
        [Summary("make me say some shit u want")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say([Remainder]string text)
            => ReplyAsync(text);

        [Command("Ping")]
        [Summary("Show the Gateway latency to Discord.")]
        [Remarks("ping")]
        public async Task Command_PingAsync()
        {
            var sw = Stopwatch.StartNew();
            var initial = await ReplyAsync("pingin...").ConfigureAwait(false);
            var restTime = sw.ElapsedMilliseconds.ToString();
            await ReplyAsync(restTime + " ms");
        }

        
    }
}
