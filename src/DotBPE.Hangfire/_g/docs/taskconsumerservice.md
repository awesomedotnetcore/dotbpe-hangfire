TaskConsumerService
--------------------------
	任务消费服务
	
## 1. Service Definition


### 1.1 TaskConsumerService.Excute 
> 2000.100 
> 任务执行，由调度服务 调用  


#### HTTP调用
+ **接口地址** : /api/hangfire/excute  
+ **接口说明** : 执行任务，由调度程序调度  
+ **请求方式** : POST  



*公共参数不显示，关于公共参数可参考首页说明*

#### 1.1.1 Request


[CommonTaskReq]  公共任务的请求消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |
|  job_id  |  string  |  任务ID  |   jobId   |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |



#### 1.1.2 Response



[TaskVoidRes]  任务的空返回消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |




## 2. Message Definition

### <span id="commontaskreq">CommonTaskReq</span> 
> 公共任务的请求消息  

| 字段名     | 类型   |  注释  |  JSON Name  |
| --------   | -----  | ----  | ----  |
|  job_id  |  string  |  任务ID  |   jobId   |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |

### <span id="taskvoidres">TaskVoidRes</span> 
> 任务的空返回消息  

| 字段名     | 类型   |  注释  |  JSON Name  |
| --------   | -----  | ----  | ----  |
|  return_message  |  string  |  返回消息  |   returnMessage   |
