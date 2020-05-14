using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;

namespace DiscordGit
{
	public class CommandHandler
	{
		private readonly string repoLink = Environment.GetEnvironmentVariable("DiscordBotRepoLink");
		private readonly DiscordSocketClient client;
		private readonly CommandService commands;

		public CommandHandler(DiscordSocketClient client, CommandService commands)
		{
			this.commands = commands;
			this.client = client;
		}

		public async Task InstallCommandsAsync()
		{
			client.MessageReceived += ReferenceIssuesAsync;
		}

		private async Task ReferenceIssuesAsync(SocketMessage messageArgs)
		{
			var message = messageArgs as SocketUserMessage;
			var context = new SocketCommandContext(client, message);

			if (message == null) return;
			var pattern = "#[0-9]*";
			var x = Regex.Matches(message.Content, pattern);
			foreach(Match match in Regex.Matches(message.Content, pattern))
				await context.Channel.SendMessageAsync($"{repoLink}/{match.Value.Substring(1)}");
		}
	}
}
