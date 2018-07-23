using DotBPE.Rpc;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotBPE.Hangfire
{
    public class TaskProducerService : TaskProducerServiceBase
    {
        private readonly IBackgroundJobClient _jobClient;

        public TaskProducerService(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }

        /// <summary>
        /// 队列任务
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public override Task<RpcResult<TaskVoidRes>> EnqueueAsync(EnqueueTaskReq req)
        {
            return EnqueueTransforAsync(req);
        }

        public override Task<RpcResult<TaskVoidRes>> EnqueueTransforAsync(EnqueueTaskReq req)
        {
            RpcResult<TaskVoidRes> res = new RpcResult<TaskVoidRes>();

            _jobClient.Enqueue<IHangfireJobService>(x => x.Enqueue(0, 1, $"{req.ServiceId}|{req.MessageId}|{req.Data}", null));

            return Task.FromResult(res);
        }

        /// <summary>
        /// 分钟级
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public override Task<RpcResult<TaskVoidRes>> ScheduleAsync(ScheduleTaskReq req)
        {
            return ScheduleAsync(req);
        }

        public override Task<RpcResult<TaskVoidRes>> ScheduleTransforAsync(ScheduleTaskReq req)
        {
            RpcResult<TaskVoidRes> res = new RpcResult<TaskVoidRes>();
            _jobClient.Schedule<IHangfireJobService>(x => x.Schedule(0, 1, req.Delay * 60, $"{req.ServiceId}|{req.MessageId}|{req.Data}", null), TimeSpan.FromMinutes(req.Delay));
            return Task.FromResult(res);
        }

        /// <summary>
        /// 秒级的延迟任务
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public override Task<RpcResult<TaskVoidRes>> ScheduleSecondAsync(ScheduleTaskReq req)
        {
            return ScheduleSecondTransforAsync(req);
        }

        public override Task<RpcResult<TaskVoidRes>> ScheduleSecondTransforAsync(ScheduleTaskReq req)
        {
            RpcResult<TaskVoidRes> res = new RpcResult<TaskVoidRes>();
            _jobClient.Schedule<IHangfireJobService>(x => x.Schedule(0, 1, req.Delay, $"{req.ServiceId}|{req.MessageId}|{req.Data}", null), TimeSpan.FromSeconds(req.Delay));
            return Task.FromResult(res);
        }
    }
}
