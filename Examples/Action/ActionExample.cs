using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Scratchpad.Examples
{
    internal class ActionExample : IExample
    {
        private readonly ILogger _logger;

        public ActionExample(ILogger<ActionExample> logger)
        {
            _logger = logger;
        }

        public Task InvokeAsync()
        {
            DoSomeWork(i => _logger.LogInformation($"{i * 2} "));
            return Task.CompletedTask;
        }

        private static void DoSomeWork(Action<int> numberConversion) => numberConversion(4);
    }
}