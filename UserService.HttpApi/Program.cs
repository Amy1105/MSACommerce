using Authentication.JwtBearer;
using Common.HttpApi;
using UserService.HttpApi;
using UserService.Infrastructure;
using UserService.UseCases;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddUseCase();

builder.Services.AddHttpApi();

builder.Services.AddControllers();

builder.Services.AddJwtBearer(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpCommon();

app.MapControllers();

app.Run();