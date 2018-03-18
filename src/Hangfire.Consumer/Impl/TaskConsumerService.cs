using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotBPE.Rpc;
using Microsoft.Extensions.Logging;

namespace Hangfire.Consumer
{
    public class TaskConsumerService : TaskConsumerServiceBase
    {
        private ILogger<TaskConsumerService> _logger;
        public TaskConsumerService(ILogger<TaskConsumerService> logger)
        {
            _logger = logger;
        }


        public override Task<RpcResult<VoidRes>> ExcuteAsync(CommonReq req)
        {
            RpcResult<VoidRes> res = new RpcResult<VoidRes>();
            _logger.LogInformation("serviceId={serviceId},messageId={messageId},data = {data}", req.ServiceId, req.MessageId, req.Data);

            return Task.FromResult(res);
        }
    }
}
