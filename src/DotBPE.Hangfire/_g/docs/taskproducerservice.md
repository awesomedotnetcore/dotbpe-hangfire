TaskProducerService
--------------------------
	任务产生服务
	
## 1. Service Definition


### 1.1 TaskProducerService.Enqueue 
> 2001.101 
> 队列任务  


#### HTTP调用
+ **接口地址** : /api/hangfire/enqueue  
+ **接口说明** : 队列任务  
+ **请求方式** : POST  



*公共参数不显示，关于公共参数可参考首页说明*

#### 1.1.1 Request


[EnqueueTaskReq]  队列任务的请求消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |



#### 1.1.2 Response



[TaskVoidRes]  任务的空返回消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |


### 1.2 TaskProducerService.Schedule 
> 2001.102 
> 延迟任务，延迟单位分钟  


#### HTTP调用
+ **接口地址** : /api/hangfire/schedule  
+ **接口说明** : 延迟任务，延迟单位分钟  
+ **请求方式** : POST  



*公共参数不显示，关于公共参数可参考首页说明*

#### 1.2.1 Request


[ScheduleTaskReq]  延迟任务的请求消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  delay  |  int32  |  延迟的单位  |   delay   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |



#### 1.2.2 Response



[TaskVoidRes]  任务的空返回消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |


### 1.3 TaskProducerService.ScheduleSecond 
> 2001.103 
> 延迟任务，延迟单位为秒，其实是不准的  


#### HTTP调用
+ **接口地址** : /api/hangfire/schedulesecond  
+ **接口说明** : 延迟任务，延迟单位分钟  
+ **请求方式** : POST  



*公共参数不显示，关于公共参数可参考首页说明*

#### 1.3.1 Request


[ScheduleTaskReq]  延迟任务的请求消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  delay  |  int32  |  延迟的单位  |   delay   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |



#### 1.3.2 Response



[TaskVoidRes]  任务的空返回消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |


### 1.4 TaskProducerService.EnqueueTransfor 
> 2001.104 
> 队列任务 转发服务请求异步调用  


#### HTTP调用
+ **接口地址** : /api/hangfire/enqueuetransfor  
+ **接口说明** : 队列任务 转发服务请求异步调用  
+ **请求方式** : POST  



*公共参数不显示，关于公共参数可参考首页说明*

#### 1.4.1 Request


[EnqueueTaskReq]  队列任务的请求消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |



#### 1.4.2 Response



[TaskVoidRes]  任务的空返回消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |


### 1.5 TaskProducerService.ScheduleTransfor 
> 2001.105 
> 延迟任务，延迟单位分钟  转发服务请求异步调用  


#### HTTP调用
+ **接口地址** : /api/hangfire/scheduletransfor  
+ **接口说明** : 延迟任务，延迟单位分钟  转发服务请求异步调用  
+ **请求方式** : POST  



*公共参数不显示，关于公共参数可参考首页说明*

#### 1.5.1 Request


[ScheduleTaskReq]  延迟任务的请求消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  delay  |  int32  |  延迟的单位  |   delay   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |



#### 1.5.2 Response



[TaskVoidRes]  任务的空返回消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |


### 1.6 TaskProducerService.ScheduleSecondTransfor 
> 2001.106 
> 延迟任务，延迟单位为秒， 转发服务请求异步调用  


#### HTTP调用
+ **接口地址** : /api/hangfire/schedulesecondtransfor  
+ **接口说明** : 延迟任务，延迟单位为秒， 转发服务请求异步调用  
+ **请求方式** : POST  



*公共参数不显示，关于公共参数可参考首页说明*

#### 1.6.1 Request


[ScheduleTaskReq]  延迟任务的请求消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  delay  |  int32  |  延迟的单位  |   delay   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |



#### 1.6.2 Response



[TaskVoidRes]  任务的空返回消息

|  字段名  |  类型  |  注释  |   JSON Name  |
| ------------ | ------------ | ------------ | ------------ |




## 2. Message Definition

### <span id="enqueuetaskreq">EnqueueTaskReq</span> 
> 队列任务的请求消息  

| 字段名     | 类型   |  注释  |  JSON Name  |
| --------   | -----  | ----  | ----  |
|  client_ip  |  string  |  用户端IP  |   clientIp   |
|  x_request_id  |  string  |  请求的唯一标识，用于服务间传递  |   xRequestId   |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |

### <span id="taskvoidres">TaskVoidRes</span> 
> 任务的空返回消息  

| 字段名     | 类型   |  注释  |  JSON Name  |
| --------   | -----  | ----  | ----  |
|  return_message  |  string  |  返回消息  |   returnMessage   |

### <span id="scheduletaskreq">ScheduleTaskReq</span> 
> 延迟任务的请求消息  

| 字段名     | 类型   |  注释  |  JSON Name  |
| --------   | -----  | ----  | ----  |
|  client_ip  |  string  |  用户端IP  |   clientIp   |
|  x_request_id  |  string  |  请求的唯一标识，用于服务间传递  |   xRequestId   |
|  service_id  |  int32  |  服务ID  |   serviceId   |
|  message_id  |  int32  |  消息ID  |   messageId   |
|  delay  |  int32  |  延迟的单位  |   delay   |
|  data  |  string  |  数据JSON 应该尽量少的传递数据，必要时可以传递ID，在实际执行是去获取数据  |   data   |
