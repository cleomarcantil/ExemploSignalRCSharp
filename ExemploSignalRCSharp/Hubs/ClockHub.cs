using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploSignalRCSharp.Hubs
{
	public interface IClockHubClient
	{
		Task ReceiveTime(string value);
	}

    public class ClockHub : Hub<IClockHubClient>
    {

		public override async Task OnConnectedAsync()
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, "group1");
			await base.OnConnectedAsync();

		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, "group1");
			await base.OnDisconnectedAsync(exception);
		}

	}
}
