using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace crackdotnet.Modules
{
    [Name("moderator")]
    [RequireContext(ContextType.Guild)]
    public class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("kick someone's fat ass out of da server")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task Kick([Remainder] SocketGuildUser user)
        {
            await ReplyAsync($"cya {user.Mention} :wave:");
            await user.KickAsync();
        }

        [Command("ban")]
        [Summary("ban someone of your choice")]
        [Remarks("ban {Member}")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanAsync([Remainder] SocketGuildUser user)
        {
            await ReplyAsync($"cya {user.Mention} :wave:");
            await user.BanAsync(0);

        }
    }
}
