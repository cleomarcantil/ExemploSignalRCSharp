using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploSignalRCSharp.Hubs
{
	public interface IChatHubClient
	{
		Task ReceiveMessage(string user, string message);
	}

	public class ChatHub : Hub<IChatHubClient>
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.ReceiveMessage(user, message);
		}

		public override async Task OnConnectedAsync()
		{
			//await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			//await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
			await base.OnDisconnectedAsync(exception);
		}
	}
}
