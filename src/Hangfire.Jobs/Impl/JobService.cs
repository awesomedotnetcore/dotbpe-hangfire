using Hangfire.Server;
using Microsoft.Extensions.Logging;
using Hangfire.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DotBPE.Rpc;
using DotBPE.Protocol.Amp;
using Google.Protobuf;

namespace Hangfire.Jobs.Impl
{
    /// <summary>
    /// 通用的，不需要传递任何参数的，任务调度
    /// </summary>
    /// <seealso cref="Hangfire.Jobs.IJobService" />
    public class JobService : IJobService
    {
        private ILogger<JobService> _logger;
        private ICallInvoker<AmpMessage> _invoker;
        public JobService(ILogger<JobService> logger, ICallInvoker<AmpMessage> invoker)
        {
            _logger = logger;
            _invoker = invoker;
        }

        [DisplayName("Enqueue Task for {0}_{1}")]
        public void Enqueue(int serviceId, int messageId,string data, PerformContext context)
        {        
            _logger.LogDebug("recieve enqueue job serviceId={serviceId},messageId={messageId}"
                , serviceId, messageId);
            Excute(serviceId, messageId,data, context);
        }

        [DisplayName("Schedule Task for {0}_{1}")]
        public void Schedule(int serviceId, int messageId, string data,PerformContext context)
        {
            _logger.LogDebug("recieve schedule job serviceId={serviceId},messageId={messageId}"
              , serviceId, messageId);
            Excute(serviceId, messageId, data,context);
        }

        private void Excute(int serviceId, int messageId,string data, PerformContext context)
        {

            CommonReq req = new CommonReq();
            req.JobId = context.BackgroundJob.Id;
            req.ServiceId = serviceId;
            req.MessageId = messageId;
            req.Data = data;

            var reqMsg = AmpMessage.CreateRequestMessage(Constants.CONSUMER_SERVER_ID, Constants.CONSUMER_DEFAULT_MESSAGE_ID);
            reqMsg.Data = req.ToByteArray();

            context.WriteLine("Start Excute Task With serviceId={0},messageId={1},data ={2}", serviceId, messageId, data);

            var resMsg = _invoker.BlockingCall(reqMsg, 30000); //30秒超时
          
            if(resMsg != null)
            {
                if (resMsg.Code == 0)
                {
                    context.WriteLine("Excute Success!");
                }
                else
                {                 
                    context.SetTextColor(ConsoleTextColor.Red);
                    context.WriteLine("Excute failed, Code = {0}",resMsg.Code);
                    context.ResetTextColor();
                }
            }
            else
            {
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine("Excute Error, Response is null");
                context.ResetTextColor();
            }
          
        }
    }
}
