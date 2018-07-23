# 任务管理平台
----

```
├─Hangfire.ConsumerNode 任务消费，即用于实际执行的业务的代码
│  
├─Hangfire.Dashborad 工作台，常看任务信息
│  
├─DotBPE.Hangfire 公共类库，用于定义任务和一般实现
│  封装实际调用业务的接口代码，实现调用简单的DotBPERpc接口
├─Hangfire.ProducerNode 对外的接口，外部接口要新增队列任务或者延迟任务，调用该接口，同时在这里配置周期新任务
│  支持HTTP和RPC调用
├─Hangfire.ServerNode 服务的节点，可用于多处部署
│  
└─protos dotbpe服务描述
```


## 架构图

![架构图][1]



  [1]: ./doc/1.png
