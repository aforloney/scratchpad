using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Scratchpad.Examples;

namespace Scratchpad
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();

                # region DependencyInjection example registration
                services.AddTransient<IMyConfiguration, MyConfiguration>();
                //services.AddScoped<IMyConfiguration, MyConfiguration>(); // <-- exception thrown
                //      a transient registration does not throw an exception as it's technically possible
                //      for the dependency to be created each time its requested but
                //      a new instance cannot be created per page request
                services.AddSingleton<IRepository, MyRepository>();
                # endregion

                // setu examples to execute
                services.RegisterAllTypes<IExample>();
            })
            .UseDefaultServiceProvider((env, serviceProviderOptions) =>
            {
                serviceProviderOptions.ValidateScopes = true;
            });

            builder.ConfigureLogging(logging =>
            {
                // clear default logging providers
                logging.ClearProviders();
                // add built-in providers manually, as needed 
                logging.AddConsole();
                logging.AddDebug();
            });

            await builder.RunConsoleAsync();
        }
    }

    public static class ServiceCollectionExtenstions
    {
        /// <summary>
        /// Helper to aggregate all inherited classes and register them as transients to the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        public static void RegisterAllTypes<T>(this IServiceCollection services)
        {
            var type = typeof(T);
            var examples = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
            
            foreach (var example in examples)
                services.AddTransient(type, example);
        }
    }
}
