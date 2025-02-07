using MessageProcessingService_OCSS.Infrastructure.EFCore;
using MessageProcessingService_OCSS.Infrastructure.HostService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region 数据库上下文scoped注入
builder.Services.AddDbContext<LogServiceOcssContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LogServiceConn"));
});

builder.Services.AddDbContext<CourseSelectionServiceOcssContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CourseSelectionServiceConn"));
});

builder.Services.AddDbContext<CourseServicesOcssContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CourseServicesConn"));
});
#endregion

builder.Services.AddHostedService<LogServer>();
builder.Services.AddHostedService<CourseSelectionServiceHost>();


var app = builder.Build();

app.MapGet("/", () => "RabbitMQ Consumer API is running!");

app.Run();
