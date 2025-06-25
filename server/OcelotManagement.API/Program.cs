using Newtonsoft.Json.Serialization;
using OcelotManagement.API;
using OcelotManagement.Application.Interfaces;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Extensions;
using OcelotManagement.Infrastructure.Extensions;
using OcelotManagement.Infrastructure.Services;

TinyMapperConfig.Configure(); //配置TinyMapper映射

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    //o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; 
    //o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //o.SerializerSettings.ContractResolver = new DefaultContractResolver();
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor(); //在自制权限过滤器 需要获取请求路径，需要增加这个依赖

//增加Swagger
builder.Services.AddSwagger();
//增加跨域
builder.Services.AddOcelotCors();

// 添加JWT认证
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<ConfigCenterService>();
builder.Services.AddTransient<ProjectService>();
builder.Services.AddTransient<RouteService>();
builder.Services.AddTransient<ProjInfoService>();
builder.Services.AddTransient<ConfigurationSyncService>();
builder.Services.AddTransient<SettingBackupService>();
builder.Services.AddTransient<GlobalConfigurationService>();



builder.Services.AddTransient<IConsulService, ConsulService>();

//添加数据库仓储
builder.Services.AddSQLRepository(builder.Configuration);
// 添加授权策略
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("Admin", "User"));
});

var app = builder.Build();
app.UseCors();
app.UseHTML();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

// 使用认证和授权中间件
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Services.InitSql(); //初始化数据库表
app.Run();
