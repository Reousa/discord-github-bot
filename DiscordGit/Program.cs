using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordGit
{
	class Program
	{
		private DiscordSocketClient client;

		static void Main(string[] args)
		{
			new Program().MainAsync().GetAwaiter().GetResult();
		}

		public async Task MainAsync()
		{
			client = new DiscordSocketClient();
			var cmdHandler = new CommandHandler(client, new CommandService());

			client.Log += Log;

			await cmdHandler.InstallCommandsAsync();
			await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken"));
			await client.StartAsync();

			await Task.Delay(-1);
		}

		private Task Log(LogMessage arg)
		{
			Console.WriteLine($"{arg.Message}");
			return Task.CompletedTask;
		}
	}
}
