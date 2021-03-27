using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace crackdotnet.Modules
{
    [Name("fun")]
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        public static string jsonPath = @"C:\Users\sdani\OneDrive\Documents\GitHub\cow\src\marriage.json";
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
        [Command("marry")]
        [Summary("marry someone")]
        public async Task MarryAsync([Remainder] SocketGuildUser user)
        {
            string mgJson = System.IO.File.ReadAllText(jsonPath);
            JObject parsed = JObject.Parse(mgJson);
            JObject marriages = (JObject)parsed["marriages"];
            JObject pending = (JObject)parsed["pending"];
            string id1Str = Context.User.Id.ToString();
            string id2Str = user.Id.ToString();
            if (mgJson.Contains(id1Str) || mgJson.Contains(id2Str))
            {
                await ReplyAsync("You or the mentioned user is already married or has a pending marriage request");
            }
            else
            {
                pending.Property("start").AddAfterSelf(new JProperty(id2Str, id1Str));
                //pending.Property("start").AddAfterSelf(new JProperty(id1Str, id2Str));
                System.IO.File.WriteAllText(jsonPath, parsed.ToString());
                await ReplyAsync("A marriage request has been entered for" + user.Mention);
            }

        }

        [Command("marry")]
        [Summary("marry someone")]
        public async Task MarryAsync(string response)
        {
            string mgJson = System.IO.File.ReadAllText(jsonPath);
            JObject parsed = JObject.Parse(mgJson);
            JObject marriages = (JObject)parsed["marriages"];
            JObject pending = (JObject)parsed["pending"];
            string id1Str = Context.User.Id.ToString();
            //ulong guildid = Context.Guild.Id;
            IGuild guild = Context.Guild;
            if (pending.ToString().Contains(id1Str))
            {
                if (response.ToLower().Contains("accept"))
                {
                    string id2Str = ((string)pending[id1Str]);
                    ulong id2Ulo = Convert.ToUInt64(id2Str);
                    var user = await guild.GetUserAsync(id2Ulo, CacheMode.AllowDownload);
                    pending.Property(id1Str).Remove();
                    //pending.Property(id2Str).Remove();
                    marriages.Property("start").AddAfterSelf(new JProperty(id1Str, id2Str));
                    marriages.Property("start").AddAfterSelf(new JProperty(id2Str, id1Str));
                    System.IO.File.WriteAllText(jsonPath, parsed.ToString());
                    await ReplyAsync("You have accepted the marriage request. You are now married to " + user.Mention);
                }
                else if (response.ToLower().Contains("deny"))
                {
                    string id2Str = ((string)pending[id1Str]);
                    ulong id2Ulo = Convert.ToUInt64(id2Str);
                    var user = await guild.GetUserAsync(id2Ulo, CacheMode.AllowDownload);
                    pending.Property(id1Str).Remove();
                    System.IO.File.WriteAllText(jsonPath, parsed.ToString());
                    await ReplyAsync("You have denied the marriage request. You will not be married to " + user.Mention);
                }
                else
                {
                    await ReplyAsync("Please choose either \"accept\" or \"deny\"");
                }
            }
            else
            {
                await ReplyAsync("There are no marriage requests in your name.");
            }
        }
        [Command("divorce")]
        [Summary("divorce a user you are married to.")]
        public async Task DivorceAsync()
        {
            string mgJson = System.IO.File.ReadAllText(jsonPath);
            JObject parsed = JObject.Parse(mgJson);
            JObject marriages = (JObject)parsed["marriages"];
            string id1Str = Context.User.Id.ToString();
            if (marriages.ToString().Contains(id1Str))
            {
                IGuild guild = Context.Guild;
                ulong id2Ulo = Convert.ToUInt64((string)marriages[id1Str]);
                var user = await guild.GetUserAsync(id2Ulo, CacheMode.AllowDownload);
                marriages.Property((string)marriages[id1Str]).Remove();
                marriages.Property(id1Str).Remove();
                System.IO.File.WriteAllText(jsonPath, parsed.ToString());
                await ReplyAsync("You have divorced " + user.Mention);
            }
            else
            {
                await ReplyAsync("You are not currently married to a user.");
            }
        }
        [Command("marriageinfo")]
        [Summary("show information about a marriage.")]
        public async Task MarriageinfoAsync([Remainder] SocketGuildUser user)
        {
            string id1Str = user.Id.ToString();
            string mgJson = System.IO.File.ReadAllText(jsonPath);
            JObject parsed = JObject.Parse(mgJson);
            JObject marriages = (JObject)parsed["marriages"];
            IGuild guild = Context.Guild;
            if (marriages.ToString().Contains(user.Id.ToString()))
            {
                ulong id2Ulo = Convert.ToUInt64((string)marriages[id1Str]);
                var user1 = await guild.GetUserAsync(id2Ulo, CacheMode.AllowDownload);
                await ReplyAsync(user.Mention + " is married to " + user1.Mention);
            }

        }
        [Command("marriageinfo")]
        [Summary("show information about a marriage.")]
        public async Task MarriageinfoAsync()
        {
            string id1Str = Context.User.Id.ToString();
            string mgJson = System.IO.File.ReadAllText(jsonPath);
            JObject parsed = JObject.Parse(mgJson);
            JObject marriages = (JObject)parsed["marriages"];
            IGuild guild = Context.Guild;
            if (marriages.ToString().Contains(id1Str))
            {
                ulong id2Ulo = Convert.ToUInt64((string)marriages[id1Str]);
                var user1 = await guild.GetUserAsync(id2Ulo, CacheMode.AllowDownload);
                await ReplyAsync("You are married to " + user1.Mention);
            }
        }
    }
}
