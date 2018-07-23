using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hangfire.ServerNode
{
    public class HangfireHostService : IHostedService
    {
        private Task _executingTask;
        private CancellationTokenSource _cts;

        private BackgroundJobServer _server;

        private readonly ILogger<HangfireHostService> _logger;
        private readonly IServiceProvider _hostProvider;

        public HangfireHostService(IServiceProvider hostProvider, IConfiguration config, ILogger<HangfireHostService> logger)
        {
            _logger = logger;
            _hostProvider = hostProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a linked token so we can trigger cancellation outside of this token's cancellation
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Store the task we're executing
            _executingTask = StartServerAsync(_cts.Token);

            // If the task is completed then return it, otherwise it's running
            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (this._server != null)
            {
                this._server.Dispose();
            }
            return Task.CompletedTask;
        }

        private Task StartServerAsync(CancellationToken token)
        {
            var storage = _hostProvider.GetRequiredService<JobStorage>();
            var options = _hostProvider.GetService<BackgroundJobServerOptions>() ?? new BackgroundJobServerOptions();
            var additionalProcesses = _hostProvider.GetServices<IBackgroundProcess>();
            _server = new BackgroundJobServer(options, storage, additionalProcesses);

            return Task.CompletedTask;
        }
    }
}
