using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 任务消费服务默认的服务ID
        /// </summary>
        public const ushort CONSUMER_SERVER_ID = 2000;


        /// <summary>
        /// 任务消费服务默认的消息ID
        /// </summary>
        public const ushort CONSUMER_DEFAULT_MESSAGE_ID = 1;
    }
}
