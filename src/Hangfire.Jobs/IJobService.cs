using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hangfire.Jobs
{
    public interface IJobService
    {
        /// <summary>
        /// Enqueues the specified service identifier.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        [DisplayName("Enqueue Task for {0}_{1}")]
        void Enqueue(int serviceId,int messageId, string data,PerformContext context);

        /// <summary>
        /// Schedules the specified service identifier.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        [DisplayName("Schedule Task for {0}_{1} after {2} minutes")]
        void Schedule(int serviceId, int messageId, string data, PerformContext context);

    }
}
