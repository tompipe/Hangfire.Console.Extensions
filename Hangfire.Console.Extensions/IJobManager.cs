using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Annotations;
using Hangfire.States;

namespace Hangfire.Console.Extensions
{
    public interface IJobManager
    {
        /// <summary>
        /// Starts a new job and waits for its result
        /// </summary>
        Task<TResult> StartWaitAsync<TResult, TJob>([InstantHandle, NotNull] Expression<Func<TJob, Task>> methodCall, CancellationToken cancellationToken = default);

        /// <summary>
        /// Starts a new job and waits for its result
        /// </summary>
        Task<TResult> StartWaitAsync<TResult, TJob>([InstantHandle, NotNull] Expression<Action<TJob>> methodCall, CancellationToken cancellationToken = default);

        /// <summary>
        /// Starts a new job and wait for it to finish
        /// </summary>
        Task StartWaitAsync<TJob>([InstantHandle, NotNull] Expression<Func<TJob, Task>> methodCall, CancellationToken cancellationToken = default);

        /// <summary>
        /// Starts a new job and wait for it to finish
        /// </summary>
        Task StartWaitAsync<TJob>([InstantHandle, NotNull] Expression<Action<TJob>> methodCall, CancellationToken cancellationToken = default);
        /// <summary>
        /// Starts a new job if we are running inside a job, it marks it as a child job.
        /// </summary>
        /// <param name="methodCall">The method call expression to be executed as a job.</param>
        /// <param name="options">Options to configure a continuation.</param>
        /// <returns>The identifier of the created job.</returns>
        string Start<TJob>([InstantHandle][NotNull] Expression<Action<TJob>> methodCall, JobContinuationOptions options = JobContinuationOptions.OnAnyFinishedState);
        /// <summary>
        /// Starts a new job if we are running inside a job, it marks it as a child job.
        /// </summary>
        /// <param name="methodCall">The method call expression to be executed as a job.</param>
        /// <param name="options">Options to configure a continuation.</param>
        /// <returns>The identifier of the created job.</returns>
        string Start<TJob>([InstantHandle, NotNull] Expression<Func<TJob, Task>> methodCall, JobContinuationOptions options = JobContinuationOptions.OnAnyFinishedState);

        /// <summary>
        /// If there is a job context, it will return an <see cref="AwaitingState"/> with current job ID as parent,
        /// otherwise it will return an <see cref="EnqueuedState"/>.
        /// </summary>
        /// <param name="queue">The queue name to which a background job identifier will be added.</param>
        /// <param name="options">Options to configure a continuation.</param>
        /// <param name="expiration">The expiration time for the continuation.</param>
        /// <returns>The state to be used in the job creation.</returns>
        IState RetrieveContinuationJobState(JobContinuationOptions options = JobContinuationOptions.OnAnyFinishedState, string queue = "default", TimeSpan? expiration = null);
    }
}
