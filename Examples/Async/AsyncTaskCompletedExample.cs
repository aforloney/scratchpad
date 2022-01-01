using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Scratchpad.Examples
{
    internal class AsyncTaskCompletedExample : IExample
    {
        private readonly ILogger _logger;

        public AsyncTaskCompletedExample(ILogger<AsyncTaskCompletedExample> logger)
        {
            _logger = logger;
        }
        
        public Task InvokeAsync()
        {
            _logger.LogInformation("No compile time exceptions here!");
            return Task.CompletedTask;
        }

        /*
        // the following cannot be completed due to "async Task" is equivalent to "async void"
        //  which does not return anything
        public async Task InvokeAsync()
        {
            return Task.CompletedTask;
        }
        */
    }
}
