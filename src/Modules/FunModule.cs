using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using System;

namespace crackdotnet.Modules
{
    [Name("fun")]
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        [Command("kill")]
        [Summary("commit 3rd degree murder")]
        public async Task KillAsync([Remainder] SocketGuildUser user)
        {
            Random r = new Random();
            string msgToSend = "";
            
            int didKill = r.Next(0, 2);
            int sel = r.Next(0, 4);
            //remove when finished
            Console.WriteLine(didKill);
            if (user.Id != Context.User.Id)
            {
                if (didKill < 1)
                {
                    switch (sel)
                    {
                        case 0: msgToSend = "you tried to kill " + user.Mention + " but they're actually not real, rather a figment of your imagination"; break;
                        case 1: msgToSend = "your puny little gun stood no chance against " + user.Mention + "'s nuclear warhead"; break;
                        case 2: msgToSend = "you are not strong enough to penetrate " + user.Mention + "'s skin. go to a fucking gym"; break;
                        case 3: msgToSend = "you tried to kill " + user.Mention + " but you ended up getting dropkicked"; break;
                        case 4: msgToSend = "you shot at " + user.Mention + " but you missed and hit some random old lady"; break;
                    }
                }
                else
                {
                    switch (sel)
                    {
                        case 0: msgToSend = "you covered " + user.Mention + " in oil, and they fucking drowned"; break;
                        case 1: msgToSend = "you shot " + user.Mention + " 6 times in the chest, causing an immediate death"; break;
                        case 2: msgToSend = "you cut " + user.Mention + "'s head off with a kitchen knife"; break;
                        case 3: msgToSend = "you put " + user.Mention + " in a BlendTec™ blender and turned them into a fine paste"; break;
                        case 4: msgToSend = "you gave" + user.Mention + " a swirly and drowned them on accident"; break;
                    }
                }
            }
            else
            {
                switch (sel)
                {
                    case 0: msgToSend = "you hung yourself from a ceiling fan"; break;
                    case 1: msgToSend = "you touched the elephant's foot in the remains of the Chernobyl nuclear power plant and suffered the consequences"; break;
                    case 2: msgToSend = "you were gay in Iraq"; break;
                    case 3: msgToSend = "you were trans in the United Kingdom"; break;
                    case 4: msgToSend = "you were black in Kentucky"; break;
                }
            }
            //msgToSend = "hell";
            var builder = new EmbedBuilder()
            {
                Color = new Color(185, 76, 225),
                Description = msgToSend
            };
            await ReplyAsync("", false, builder.Build());

        }
    }
}
