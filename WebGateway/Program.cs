using Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using WebGateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Configuration.AddOcelot(
    folder: "./ocelot",
    env: builder.Environment,
    mergeTo: MergeOcelotJson.ToMemory,
    optional: false, reloadOnChange: true);

builder.Services
    .AddOcelot()
    .AddConsul<IPConsulServiceBuilder>();

builder.Services.AddJwtBearer(builder.Configuration);

//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway V1");
        options.SwaggerEndpoint("/auth/swagger.json", "AuthServer V1");
        options.SwaggerEndpoint("/user/swagger.json", "UserService V1");
    });
}

app.UseOcelot().Wait();

app.Run();
