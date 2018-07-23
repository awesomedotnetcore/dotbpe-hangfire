HTTP API 接口文档一览  
--------------------------  
## 1 接口说明  
### 1.1 公共参数  
	绝大多数接口中都包含公共参数，公共参数有App内置传递，公共参数如下所示 
|  字段名  |  类型  |  注释  |  
| ------------ | ------------ | ------------ |  
|  clientIp    |  string  |  用户端IP，服务自动获取，传递也无效   |  
|  identity    |  string  |  用户标识，服务自动获取，传递也无效  |  
|  xRequestId  |  string  |  请求的唯一标识，用于服务间传递，HEAD中传递X-REQUEST-ID 传递，不传则自动创建  |  


以上为DotBPE默认建议的公共参数，建议在接口入参定义，不同项目还需定义额外的公共参数，如手机App客户端项目，如下表格说是

|  字段名  |  类型  |  注释  |  
| ------------ | ------------ | ------------ |  
|  deviceOs    |  string  |  设备操作系统,例如ios9.3  | 
|  srcCode     |  int32   |  客户端来源 4=Android 6 =h5 7=web 8=iso 11=window客户端  |  
|  appVersion  |  int32   |  app版本（纯数字类型）  |  
|  deviceName  |  string  |  设备名称  |  
|  deviceId    |  string  |  设备编号  |  
   


### 1.2 返回格式  
> 所有的http接口都包括固定的返回格式，如下所示 

```json
 {"returnCode": 0,"returnMessage": "success","data": {}} 
``` 

其中data 节点中的数据为返回的业务数据，调用者通过`returnCode` 来判断是否调用正确 

##  2 服务一览表  


### 2.1 TaskConsumerService
>  任务消费服务

| 序号 |  服务名  |  消息ID  |  请求地址  |  说明  |  
| ------------| ------------ | ------------ | ------------ | ------------ |
| 1 | Excute |  2000.100  |  /api/hangfire/excute  |  任务执行，由调度服务 调用  |

### 2.2 TaskProducerService
>  任务产生服务

| 序号 |  服务名  |  消息ID  |  请求地址  |  说明  |  
| ------------| ------------ | ------------ | ------------ | ------------ |
| 1 | Enqueue |  2001.101  |  /api/hangfire/enqueue  |  队列任务  |
| 2 | Schedule |  2001.102  |  /api/hangfire/schedule  |  延迟任务，延迟单位分钟  |
| 3 | ScheduleSecond |  2001.103  |  /api/hangfire/schedulesecond  |  延迟任务，延迟单位为秒，其实是不准的  |
| 4 | EnqueueTransfor |  2001.104  |  /api/hangfire/enqueuetransfor  |  队列任务 转发服务请求异步调用  |
| 5 | ScheduleTransfor |  2001.105  |  /api/hangfire/scheduletransfor  |  延迟任务，延迟单位分钟  转发服务请求异步调用  |
| 6 | ScheduleSecondTransfor |  2001.106  |  /api/hangfire/schedulesecondtransfor  |  延迟任务，延迟单位为秒， 转发服务请求异步调用  |
