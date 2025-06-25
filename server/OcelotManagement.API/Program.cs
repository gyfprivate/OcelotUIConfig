using Newtonsoft.Json.Serialization;
using OcelotManagement.API;
using OcelotManagement.Application.Interfaces;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Extensions;
using OcelotManagement.Infrastructure.Extensions;
using OcelotManagement.Infrastructure.Services;

TinyMapperConfig.Configure(); //����TinyMapperӳ��

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    //o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; 
    //o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //o.SerializerSettings.ContractResolver = new DefaultContractResolver();
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor(); //������Ȩ�޹����� ��Ҫ��ȡ����·������Ҫ�����������

//����Swagger
builder.Services.AddSwagger();
//���ӿ���
builder.Services.AddOcelotCors();

// ���JWT��֤
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

//������ݿ�ִ�
builder.Services.AddSQLRepository(builder.Configuration);
// �����Ȩ����
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

// ʹ����֤����Ȩ�м��
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Services.InitSql(); //��ʼ�����ݿ��
app.Run();
