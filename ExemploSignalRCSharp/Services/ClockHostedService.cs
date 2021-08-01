using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExemploSignalRCSharp.Hubs;

namespace ExemploSignalRCSharp.Services
{
	public class ClockHostedService : IHostedService, IDisposable
	{
		private readonly IHubContext<ClockHub> clockHubContext;
		private Timer timer;

		public ClockHostedService(IHubContext<ClockHub> clockHubContext)
		{
			this.clockHubContext = clockHubContext;

		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			timer = new Timer(OnTimer, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));

			return Task.CompletedTask;
		}


		public Task StopAsync(CancellationToken cancellationToken)
		{
			timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		private async void OnTimer(object state)
		{
			var time = DateTime.Now.ToString("HH:mm:ss");

			Debug.WriteLine($"ClockHostedService.OnTimer: {time}");

			await clockHubContext.Clients.All.SendAsync("ReceiveTime", time);
		}

		public void Dispose()
		{
			timer?.Dispose();
		}



	}
}
