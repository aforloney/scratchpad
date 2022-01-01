using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Scratchpad
{
    class Worker : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IEnumerable<IExample> _examples;
        private readonly ILogger<Worker> _logger;

        public Worker(IHostApplicationLifetime appLifeTime,
                      IEnumerable<IExample> examples,
                      ILogger<Worker> logger)
        {
            _appLifetime = appLifeTime;
            _examples = examples;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await Task.WhenAll(
                                _examples.Select(ex =>
                                {
                                    _logger.LogDebug($"Invoking {ex.GetType()}...");
                                    return ex.InvokeAsync();
                                }
                                )
                            );
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Exception handled.");
                    }
                    finally
                    {
                        // Stop the application once the work is done
                        _appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
