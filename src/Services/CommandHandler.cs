using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace crackdotnet
{
    public class CommandHandler
    {
        
        //private readonly ILogger<CommandHandlingService> _logger;
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _serviceProvider;

        public CommandHandler(
            //ILogger<CommandHandler> logger,
            CommandService commandService,
            DiscordSocketClient discord,
            IServiceProvider serviceProvider)
        {
            //_logger = logger;
            _commandService = commandService;
            //_discordSocketClient = discordSocketClient;
            _serviceProvider = serviceProvider;
            _discord = discord;
            _discord.MessageReceived += OnMessageReceivedAsync;
        }


        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            //Console.WriteLine("we recieved");
            if (!(s is SocketUserMessage msg)) return;
            //if (!(s.Channel is SocketGuildChannel)) return;

            int argPos = 0;
            var context = new SocketCommandContext(_discord, msg);     // Create the command context
            if (msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                if (msg.Author.Id != _discord.CurrentUser.Id)
                {
                    var result = await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
                    if (!result.IsSuccess) Console.WriteLine("sommen fuked");
                    if (result.IsSuccess) return;

                    switch (result)
                    {
                        case ExecuteResult execute:
                            Console.WriteLine(execute.Exception?.ToString());
                            return;
                        case ParseResult parse when parse.Error == CommandError.BadArgCount:
                            // Send Help Text
                            return;
                        default:
                            //await context.Channel.SendMessageAsync(result.ErrorReason);
                            if (result.ErrorReason == "Unknown command.") await context.Channel.SendMessageAsync(result.ErrorReason + " Did you accidentally put an unnecessary space somewhere?");
                            else await context.Channel.SendMessageAsync(result.ErrorReason);
                            return;
                    }
                }
            }
            
        }
    }
}