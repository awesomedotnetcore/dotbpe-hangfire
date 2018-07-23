using DotBPE.Protocol.Amp;
using DotBPE.Rpc;
using Google.Protobuf;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DotBPE.Hangfire.Impl
{
    /// <summary>
    /// 转发调用的队列处理程序
    /// </summary>
    /// <seealso cref="DotBPE.Hangfire.IHangfireJobService" />
    public class TransforHangfireJobService : IHangfireJobService
    {
        private ILogger<TransforHangfireJobService> _logger;
        private ICallInvoker<AmpMessage> _invoker;

        public TransforHangfireJobService(ILogger<TransforHangfireJobService> logger, ICallInvoker<AmpMessage> invoker)
        {
            _logger = logger;
            _invoker = invoker;
        }

        [DisplayName("Enqueue Task for {0}_{1}")]
        public Task Enqueue(int serviceId, int messageId, string data, PerformContext context)
        {
            _logger.LogDebug("recieve enqueue job serviceId={serviceId},messageId={messageId}"
                , serviceId, messageId);
            return Excute(serviceId, messageId, data, context);
        }

        [DisplayName("Schedule Task for {0}_{1}")]
        public Task Schedule(int serviceId, int messageId, int after, string data, PerformContext context)
        {
            _logger.LogDebug("recieve schedule job serviceId={serviceId},messageId={messageId}"
              , serviceId, messageId);
            return Excute(serviceId, messageId, data, context);
        }

        private async Task Excute(int serviceId, int messageId, string data, PerformContext context)
        {
            try
            {
                CommonTaskReq req = new CommonTaskReq();
                req.JobId = context.BackgroundJob.Id;
                req.ServiceId = serviceId;
                req.MessageId = messageId;
                req.Data = data;

                var reqMsg = AmpMessage.CreateRequestMessage(HangfireConstant.CONSUMER_SERVER_ID, HangfireConstant.CONSUMER_DEFAULT_MESSAGE_ID);
                reqMsg.Data = req.ToByteArray();

                context.WriteLine("Start Excute Task With serviceId={0},messageId={1},data ={2}", serviceId, messageId, data);

                var resMsg = await _invoker.AsyncCall(reqMsg, 30000); //30秒超时

                if (resMsg != null)
                {
                    if (resMsg.Code == 0)
                    {
                        context.WriteLine("Excute Success!");
                    }
                    else
                    {
                        context.SetTextColor(ConsoleTextColor.Red);
                        string errorMsg = "";
                        if (resMsg.Data != null)
                        {
                            var resData = TaskVoidRes.Parser.ParseFrom(resMsg.Data);
                            errorMsg = resData.ReturnMessage;
                        }

                        context.WriteLine("Excute failed, Code = {0},ErrorMessage={1}", resMsg.Code, errorMsg);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "调用任务消费服务出错:{0}", ex.Message);
            }
        }
    }
}
