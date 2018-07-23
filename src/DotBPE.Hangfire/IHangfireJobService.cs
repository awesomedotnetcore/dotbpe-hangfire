using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DotBPE.Hangfire
{
    public interface IHangfireJobService
    {
        /// <summary>
        /// Enqueues the specified service identifier.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        [DisplayName("Enqueue Task for {0}_{1}, with data:{2}")]
        Task Enqueue(int serviceId, int messageId, string data, PerformContext context);

        /// <summary>
        /// Schedules the specified service identifier.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        [DisplayName("Schedule Task for {0}_{1}, after {2} seconds ,with data:{3}")]
        Task Schedule(int serviceId, int messageId, int after, string data, PerformContext context);
    }
}
