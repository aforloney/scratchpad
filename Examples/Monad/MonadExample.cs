using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Scratchpad;

namespace Examples.Monad
{
    internal class MonadExample : IExample
    {
        private readonly ILogger _logger;

        public MonadExample(ILogger<MonadExample> logger)
        {
            _logger = logger;
        }

        public Task InvokeAsync()
        {
            // random string and functions,
            // explicitly calling .Return for each function as T0 is returned, not Maybe<T0>    
            string x = "hello      ";
            var monadExample = x.Return()
                    .Bind(s => s.Trim().Return())
                    .Bind(s => s.Substring(0,5).Return());
            _logger.LogInformation($"Valid Type: {monadExample.GetType()} Value: {monadExample.Value}");

            // testing how None is returned
            string n = null;
            var noneExample = n.Return()
                    .Bind(s => s.Trim().Return())
                    .Bind(s => s.Substring(0,5).Return());
            _logger.LogInformation($"None Type: {noneExample.GetType()} Value: {noneExample.Value}");

            // as Maybe object, without type returned of Maybe<T0> construct
            Maybe<string> maybe = "hello      ".Return();
            var maybeExample = maybe
                    .Bind(s => s.Trim())
                    .Bind(s => s.Substring(0,5));
            _logger.LogInformation($"Maybe wrapped Type: {monadExample.GetType()} Value: {monadExample.Value}");

            return Task.CompletedTask;
        }
    }

    internal class Maybe<T>
    {
        private readonly T _instance;
        public T Value => _instance;
        public Maybe(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            _instance = instance;
        }
        private Maybe() { }

        // supports style where function being executed returns T0 type and not Maybe<T0>
        public Maybe<T0> Bind<T0>(Func<T, T0> func) =>
            _instance != null ? func(_instance).Return() : Maybe<T0>.None();

        public Maybe<T0> Bind<T0>(Func<T, Maybe<T0>> func) =>
            _instance != null ? func(_instance) : Maybe<T0>.None();

        public static Maybe<T> None() => new Maybe<T>();
    }

    internal static class MaybeExtensions
    {
        public static Maybe<T> Return<T>(this T value) =>
            value != null ? new Maybe<T>(value) : Maybe<T>.None();
    }
}