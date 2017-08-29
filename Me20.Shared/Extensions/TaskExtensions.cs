using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Me20.Shared.Extensions
{
    public static class TaskExtensions
    {
        public static IEnumerable<Task<TResult>> OrderByCompletion<TResult>(this IEnumerable<Task<TResult>> source)
        {
            var materializedSourceTasks = source.ToList();
            var containers = materializedSourceTasks.Select(x => new TaskCompletionSource<TResult>()).ToList();

            var index = -1;
            foreach (var t in materializedSourceTasks)
            {
                t.ContinueWith(completed =>
                {
                    var nextContainer = containers[Interlocked.Increment(ref index)];
                    SetTaskResultToCompletionSource(completed, nextContainer);
                }, TaskContinuationOptions.ExecuteSynchronously);
            }

            return containers.Select(c => c.Task);
        }

        private static void SetTaskResultToCompletionSource<TResult>(Task<TResult> finishedTask, TaskCompletionSource<TResult> completionSource)
        {
            switch (finishedTask.Status)
            {
                case TaskStatus.RanToCompletion:
                    completionSource.TrySetResult(finishedTask.Result);
                    break;
                case TaskStatus.Canceled:
                    completionSource.TrySetCanceled();
                    break;
                case TaskStatus.Faulted:
                    completionSource.TrySetException(finishedTask.Exception.InnerException);
                    break;
                default:
                    throw new ArgumentException("Task was not completed");
            }
        }
    }
}
