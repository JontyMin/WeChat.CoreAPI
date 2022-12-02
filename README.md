# .NET Core API服务

使用.NETCore 3.1搭建，ORM使用国产数据库`SQLSugar`，集成Autofac、AutoMapper、Redis、Swagger、Quartz、JWT校验、接口请求限流，接入七牛云SDK、TencentCloud SDK，实现了微信服务号部分接口，包含一个供应商业务流程以及另一个移动端项目后台接口。

DDD的第一次练手（当初也不懂~），划分了领域服务、应用服务，使用仓储模式。

## 授权接口

![image-20221202134049871](https://cdn.jonty.top/img/image-20221202134049871.png)

## 供应商接口

![image-20221202220837837](https://cdn.jonty.top/img/image-20221202220837837.png)

## 微信服务号接口

![image-20221202220906822](https://cdn.jonty.top/img/image-20221202220906822.png)

## 仓储APP-API

![image-20221202220942338](https://cdn.jonty.top/img/image-20221202220942338.png)

 