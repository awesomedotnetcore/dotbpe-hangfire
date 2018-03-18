using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotBPE.Rpc;
using Hangfire.Jobs;

namespace Hangfire.Producer.Impl
{
    public class TaskProducerService : TaskProducerServiceBase
    {
        private readonly IBackgroundJobClient _jobClient;
        public TaskProducerService(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }
        public override Task<RpcResult<VoidRes>> EnqueueAsync(EnqueueReq req)
        {
            RpcResult<VoidRes> res = new RpcResult<VoidRes>();
            _jobClient.Enqueue<IJobService>(x => x.Enqueue(req.ServiceId, req.MessageId, req.Data, null));

            return Task.FromResult(res);
        }

        public override Task<RpcResult<VoidRes>> ScheduleAsync(ScheduleReq req)
        {
            RpcResult<VoidRes> res = new RpcResult<VoidRes>();
            _jobClient.Schedule<IJobService>(x => x.Schedule(req.ServiceId, req.MessageId,req.Data, null), TimeSpan.FromMinutes(req.Delay));
            return Task.FromResult(res);
        }

        public override Task<RpcResult<VoidRes>> ScheduleSecondAsync(ScheduleReq req)
        {
            RpcResult<VoidRes> res = new RpcResult<VoidRes>();
            _jobClient.Schedule<IJobService>(x => x.Schedule(req.ServiceId, req.MessageId, req.Data, null), TimeSpan.FromSeconds(req.Delay));
            return Task.FromResult(res);
        }
    }
}
