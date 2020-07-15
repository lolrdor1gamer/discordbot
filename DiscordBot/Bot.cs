using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using System.IO;
using Newtonsoft.Json;
using DiscordBot.Commands;

namespace DiscordBot
{
    public class Bot
    {
        public List<string> prefix = new List<string>();
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        
        public Bot(IServiceProvider services)
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json =  sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };
            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                DmHelp = true,
                Services =services
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<FirstCommands>();
            Client.ConnectAsync();
        }

        public void NewPrefix(string newprefix)
        {
            prefix.Add(newprefix);
        }
        private Task OnClientReady(ReadyEventArgs e)
        {
            Console.WriteLine("Bot has started");
            return Task.CompletedTask;
        }
    }
}
