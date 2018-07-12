using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
namespace Carbon.Modules
{
    [Name("Moderator")]
    [RequireContext(ContextType.Guild)]
    public class Moderator : ModuleBase<SocketCommandContext> 
    {
        [Command("kick")]
        [Summary("Kick the specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick([Remainder]SocketGuildUser user)
        {
            await ReplyAsync($"OWO, seems like {user.Mention} has been kicked, ufufufufu!");
            await user.KickAsync();
        }
    }
}
