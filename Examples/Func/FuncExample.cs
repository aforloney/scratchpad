using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Scratchpad;

namespace Examples.Func
{
    internal class FuncExample : IExample
    {
        private readonly ILogger _logger;

        public FuncExample(ILogger<FuncExample> logger)
        {
            _logger = logger;
        }
        public Task InvokeAsync()
        {
            void ConvertToInt(Func<string, int> function)
            {
                _logger.LogInformation($"Value = {function("13377")}");
            }

            ConvertToInt((str) => int.Parse(str));

            return Task.CompletedTask;
        }
    }
}