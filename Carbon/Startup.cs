using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Carbon.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Carbon
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()                            // Create a new instance of the config builder
                .SetBasePath(AppContext.BaseDirectory)                          // Specify the default location for the config file
                .AddJsonFile("_configuration.json");                            // Add this (json encoded) file to the configuration 
            Configuration = builder.Build();                                    // Build the configuration 
        }

        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }
        public async Task RunAsync()
        {
            Colorful.Console.WriteAscii("Dogey", System.Drawing.Color.DarkGreen);

            var services = new ServiceCollection();                             // Create a new instance of a service collection
            ConfigurationServices(services);

            var provider = services.BuildServiceProvider();                     // Build the service provider
            provider.GetRequiredService<LoggingService>();                      // Start the logging service
            provider.GetRequiredService<CommandHandler>();                      // Start the command handler service

            await provider.GetRequiredService<StartupService>().StartAsync();   // Start the startup service
            await Task.Delay(-1);                                               // Keep the program alive
        }
        private void ConfigurationServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {                                                                   // Add the discord collection   
                LogLevel = LogSeverity.Verbose,                                 // Tell the logger to give verbose amount of info
                MessageCacheSize = 1000                                         // Cache 1,000 messages per channel
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {                                                                   // Add the command service to the collection    
                LogLevel = LogSeverity.Verbose,                                 // Tell the logger to give verbose amount of info
                DefaultRunMode = RunMode.Async,                                 // Force all commands to run async by default
                CaseSensitiveCommands = false                                   // Ignore case when executing commands
            }))
            .AddSingleton<StartupService>()                                     // Add startupservice to the collection
            .AddSingleton<LoggingService>()                                     // Add loggingservice to the collection
            .AddSingleton<CommandHandler>()                                     // Add commandhandler to the collection
            .AddSingleton<Random>()                                             // Add random to the collection
            .AddSingleton(Configuration);                                       // Add the configuration to the collection
        }
    }
}