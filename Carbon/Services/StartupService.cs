using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Carbon.Services
{
    public class StartupService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        // Discord Socketclient, CommandService, and IConfigurationRoot are injected automatically from the IServiceProvider
        public StartupService(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config)
        {
            _config = config;
            _discord = discord;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            string DiscordToken = _config["tokens:discord"];
            if (string.IsNullOrWhiteSpace(DiscordToken))
                throw new Exception("Please enter your bot's token into the '_configuration.json' file found in the applications root directory");

            await _discord.LoginAsync(TokenType.Bot, DiscordToken);         // Login to discord
            await _discord.StartAsync();                                    // Connect to the websocket

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());    // Load commands and modules into the command service
        }
    }
}
