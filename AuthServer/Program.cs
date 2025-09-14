using AuthServer;
using Common.Consul.ServiceRegistration;
using Consul.AspNetCore;
using Common.Consul.ServiceDiscovery;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpApi(builder.Configuration);

#region consul相关配置注入
var serviceCheck = builder.Configuration.GetSection("ServiceCheck").Get<ServiceCheckConfiguration>();
serviceCheck ??= new ServiceCheckConfiguration();

builder.Services.AddConsul();

//配置服务发现中心功能。包括配置Consul中心地址、健康检查
builder.Services.AddConsulService(serviceConfiguration =>
{
    serviceConfiguration.ServiceAddress = new Uri(builder.Configuration["urls"] ?? builder.Configuration["applicationUrl"]);
}, serviceCheck);

//配置服务发现功能
builder.Services.AddConsulDiscovery();

builder.Services.AddHealthChecks();

#endregion

//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks(serviceCheck.Path);

app.UseAuthorization();

app.MapControllers();

app.Run();
