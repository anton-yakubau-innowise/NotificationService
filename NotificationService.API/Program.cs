using Microsoft.EntityFrameworkCore;
using NotificationService.API;
using NotificationService.API.Middleware;
using NotificationService.Application;
using NotificationService.Application.Background;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHostedService<NotificationSendingWorker>();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
