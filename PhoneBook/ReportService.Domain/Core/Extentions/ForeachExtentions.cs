using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ReportService.Domain.Core.Extentions
{
    public static class ForeachExtentions
    {
        public static async Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> asyncAction, int maxDegreeOfParallelism)
        {
            var throttler = new SemaphoreSlim(initialCount: maxDegreeOfParallelism, maxCount: maxDegreeOfParallelism);
            await Task.WhenAll(source.Select(item => ProcessAsync(item, asyncAction, throttler)));
        }

        public static async Task ParallelForEachAsync2<T>(this IEnumerable<T> source,
        Func<T, Task> asyncAction, int maxDegreeOfParallelism)
        {
            var actionBlock = new ActionBlock<T>(asyncAction, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism, BoundedCapacity = maxDegreeOfParallelism });
            foreach (var item in source)
                await actionBlock.SendAsync(item);
            actionBlock.Complete();
            await actionBlock.Completion;
        }

        private static async Task ProcessAsync<T>(T item, Func<T, Task> asyncAction, SemaphoreSlim throttler)
        {
            try
            {
                await throttler.WaitAsync();
                await asyncAction(item);
            }
            finally
            {
                throttler.Release();
            }
        }
    }
}
