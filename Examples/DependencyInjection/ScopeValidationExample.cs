using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Scratchpad.Examples
{
    internal class ScopeValidationExample : IExample
    {
        private readonly IRepository _repository;
        private ILogger _logger;
        public ScopeValidationExample(IRepository repository,
            ILogger<ScopeValidationExample> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Task InvokeAsync()
        {
            _logger.LogInformation($"Get: {_repository.Get()}");
            return Task.CompletedTask;
        }
    }

    // https://medium.com/dotnet-hub/captive-dependency-with-asp-net-core-what-is-captive-dependency-8698b588e048
    internal class MyConfiguration : IMyConfiguration
    {
        public MyConfiguration() {}
        public void Configure() {}
    }

    internal interface IMyConfiguration 
    {
        void Configure();
    }

    internal class MyRepository : IRepository
    {
        private readonly IMyConfiguration _configuration;
        public MyRepository(IMyConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get() => "Got em!";
    }

    internal interface IRepository
    {
        string Get();
    }
}