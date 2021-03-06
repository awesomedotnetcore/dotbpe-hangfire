// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ProtobufObjectfactory

#region Designer generated code

using Google.Protobuf;
using DotBPE.Protobuf;

namespace DotBPE.Hangfire
{
    public class ProtobufObjectFactory:IProtobufObjectFactory
    {
        public IMessage GetRequestTemplate(int serviceId, int messageId)
        {

            if (serviceId == 2000 && messageId == 100)
            {
                return new CommonTaskReq();
            }

            if (serviceId == 2001 && messageId == 101)
            {
                return new EnqueueTaskReq();
            }

            if (serviceId == 2001 && messageId == 102)
            {
                return new ScheduleTaskReq();
            }

            if (serviceId == 2001 && messageId == 103)
            {
                return new ScheduleTaskReq();
            }

            if (serviceId == 2001 && messageId == 104)
            {
                return new EnqueueTaskReq();
            }

            if (serviceId == 2001 && messageId == 105)
            {
                return new ScheduleTaskReq();
            }

            if (serviceId == 2001 && messageId == 106)
            {
                return new ScheduleTaskReq();
            }

            return null;
        }

        public IMessage GetResponseTemplate(int serviceId, int messageId)
        {

            if (serviceId == 2000 && messageId == 100)
            {
                return new TaskVoidRes();
            }

            if (serviceId == 2001 && messageId == 101)
            {
                return new TaskVoidRes();
            }

            if (serviceId == 2001 && messageId == 102)
            {
                return new TaskVoidRes();
            }

            if (serviceId == 2001 && messageId == 103)
            {
                return new TaskVoidRes();
            }

            if (serviceId == 2001 && messageId == 104)
            {
                return new TaskVoidRes();
            }

            if (serviceId == 2001 && messageId == 105)
            {
                return new TaskVoidRes();
            }

            if (serviceId == 2001 && messageId == 106)
            {
                return new TaskVoidRes();
            }

            return null;
        }
    }
}

#endregion Designer generated code