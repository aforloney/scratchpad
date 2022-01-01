using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scratchpad.Examples
{
    internal class ParallelExample : IExample
    {
        public async Task InvokeAsync()
        {
            async Task LongRunningWork() => await Task.Delay(1000);
            await Task.WhenAll(new List<Task>()
            {
                LongRunningWork(),
                LongRunningWork(),
                LongRunningWork(),
                LongRunningWork(),
                LongRunningWork()
            });
        }
    }
}