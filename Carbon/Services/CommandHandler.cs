﻿using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Carbon.Services
{
    class CommandHandler
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;

        // DiscordSocketClient, CommandService, IConfigurationRoot, and IServiceProvider are injected automatically from the IServiceProvider
        public CommandHandler(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IServiceProvider provider)
        {
            _discord = discord;
            _commands = commands;
            _config = config;
            _provider = provider;

            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return;                                  // Ensure the message is from a user/bot

            if (msg.Author.Id == _discord.CurrentUser.Id) return;                       // Ignore self when checking commands

            var context = new SocketCommandContext(_discord, msg);                      // Create the command context

            int argPos = 0;                                                             // Check if the message has a valid command prefix 
            if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _provider);  // Execute the command

                if (!result.IsSuccess)                                                  // If not successful, reply with error
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}
