using AuthServer;
using Common.Consul.ServiceRegistration;
using Consul.AspNetCore;
using Common.Consul.ServiceDiscovery;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpApi(builder.Configuration);

#region consul�������ע��
var serviceCheck = builder.Configuration.GetSection("ServiceCheck").Get<ServiceCheckConfiguration>();
serviceCheck ??= new ServiceCheckConfiguration();

builder.Services.AddConsul();

//���÷��������Ĺ��ܡ���������Consul���ĵ�ַ���������
builder.Services.AddConsulService(serviceConfiguration =>
{
    serviceConfiguration.ServiceAddress = new Uri(builder.Configuration["urls"] ?? builder.Configuration["applicationUrl"]);
}, serviceCheck);

//���÷����ֹ���
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
