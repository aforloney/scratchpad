using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Scratchpad.Examples
{
    internal class EnumerationExample : IExample
    {
        private readonly ILogger _logger;

        public EnumerationExample(ILogger<EnumerationExample> logger)
        {
            _logger = logger;
        }
        
        public Task InvokeAsync()
        {
            IEnumerable<int> numbers = Enumerable.Range(1,3);
            var sb = new StringBuilder();
            foreach (var r in numbers.SelectExample(i => i * 2))
                sb.Append($"{r} ");    
                
            _logger.LogInformation(sb.ToString());

            sb = new StringBuilder();
            IEnumerable<string> letters = new List<string> { "A", "B", "C" };
            foreach (var l in letters.OnEvens(str => str.ToUpper()))
                sb.Append($"{l} ");

            _logger.LogInformation(sb.ToString());

            sb = new StringBuilder();
            foreach (var c in Enumerable.Range(65, 26).Select(c => (char) c))
                sb.Append(c.ToString());

            _logger.LogInformation(sb.ToString());
            return Task.CompletedTask;
        }
    }

    internal static class EnumerableExtensions
    {
        public static IEnumerable<TResult> SelectExample<TSource, TResult>(
                            this IEnumerable<TSource> source,
                            Func<TSource, TResult> func)
        {
            if (source == null) throw new ArgumentNullException();
            return EnumerateFunc(source, func);
        }

        public static IEnumerable<TResult> OnOdds<TSource, TResult>(
                            this IEnumerable<TSource> source, 
                            Func<TSource, TResult> func)
        {
            if (source == null) throw new ArgumentNullException();
            var items = source.Where((v, i) => i % 2 != 0);
            return EnumerateFunc(items, func);
        }

        public static IEnumerable<TResult> OnEvens<TSource, TResult>(
                            this IEnumerable<TSource> source,
                            Func<TSource, TResult> func) {
            if (source == null) throw new ArgumentNullException();
            var items = source.Where((v, i) => i % 2 == 0);
            return EnumerateFunc(items, func);
        }

        private static IEnumerable<TResult> EnumerateFunc<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> func)
        {
            foreach (TSource s in source)
                yield return func(s);
        }
    }
}
