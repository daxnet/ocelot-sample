# Ocelot API网关演示案例
本代码库包含了有关Ocelot API网关实际应用的一些案例。

## 案例介绍

### 案例一：Ocelot API网关的基本应用
- 博客文章：[《ASP.NET Core中Ocelot的使用：API网关的应用》](http://sunnycoding.cn/2018/10/29/aspnetcore-ocelot-get-started/)
- 源代码：[查看/下载](https://github.com/daxnet/ocelot-sample/releases/tag/chapter_1)

### 案例二：Ocelot API网关与Spring Cloud Eureka集成
- 博客文章：[《ASP.NET Core中Ocelot的使用：基于Spring Cloud Netflix Eureka的动态路由》](http://sunnycoding.cn/2018/11/03/aspnetcore-ocelot-dynamic-routing-with-eureka/)
- 源代码：[查看/下载](https://github.com/daxnet/ocelot-sample/releases/tag/chapter_2)

## 源码使用
- C#开发环境：Visual Studio 2017与.NET Core工作负载，或者Visual Studio Code
- Java开发环境：Eclipse，Maven
- .NET Core SDK版本：2.1
- 容器化工具：docker，docker-compose，Docker for Windows
- Compose File Version: 3.0

### 使用docker-compose编译和运行程序
```
cd src
sudo docker-compose -f docker-compose.yml up --build
```
