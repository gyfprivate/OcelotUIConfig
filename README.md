# OcelotUIConfig
## Ocelot 路由配置管理系统
Ocelot 作为网关使用时，配置起来相关麻烦，之前使用过Ocelot-Admin项目，但使用起来不是很得心应手

于是自己打造Ocelot的路由配置管理系统，主要功能同样是将配置写到consul keyValue中，希望能帮到大家，如果有什么需求私信联系
## 目录结构


server: 基于net8的Ocelot管理后台接口

ui: 基于layui-vue-admin的管理后台页面


## 运行
### 调试
vs studio 启动 server 目录下的Ocelot_UI_Config 解决方案，运行OcelotManagement.API
vscode 启动 ui 运行 npm run dev
### 打包
#### IIS运行环境
下载.net8运行时（sdk 和 Hosting Bundle!!!）
https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0

增加网站，发布代码就不详细说明，网上有很多教程

在目录下增加wwwroot文件夹，用于存放ui 

ui 运行 npm run build 打包 到dist 

把打包出来的文件放进之前新建的 wwwroot 文件夹