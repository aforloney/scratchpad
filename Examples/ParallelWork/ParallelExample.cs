using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scratchpad.Examples
{
    internal class ParallelExample : IExample
    {
        public async Task InvokeAsync()
        {
            async Task LongRunningWork() => await Task.Delay(1000);
            List<Task> tasks = new();
            tasks.Add(LongRunningWork());
            tasks.Add(LongRunningWork());
            tasks.Add(LongRunningWork());
            tasks.Add(LongRunningWork());
            tasks.Add(LongRunningWork());
            await Task.WhenAll(tasks);
        }
    }
}