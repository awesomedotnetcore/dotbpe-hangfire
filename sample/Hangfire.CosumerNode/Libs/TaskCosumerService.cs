using DotBPE.Hangfire;
using DotBPE.Rpc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.CosumerNode
{
    public class TaskCosumerService : TaskConsumerServiceBase
    {
        private readonly ILogger<TaskCosumerService> _logger;

        public TaskCosumerService(ILogger<TaskCosumerService> logger)
        {
            _logger = logger;
        }

        public override Task<RpcResult<TaskVoidRes>> ExcuteAsync(CommonTaskReq req)
        {
            var result = new RpcResult<TaskVoidRes>();
            _logger.LogInformation("receive task req: jobId={0},data ={1}", req.JobId, req.Data);
            return Task.FromResult(result);
        }
    }
}
