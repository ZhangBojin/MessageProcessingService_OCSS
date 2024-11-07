using MessageProcessingService_OCSS.Infrastructure.EFCore;
using MessageProcessingService_OCSS.Infrastructure.HostService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region 数据库上下文scoped注入
builder.Services.AddDbContext<LogServiceOcssContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LogServiceConn"));
});
#endregion

builder.Services.AddHostedService<LogServer>();

var app = builder.Build();

app.MapGet("/", () => "RabbitMQ Consumer API is running!");

app.Run();
