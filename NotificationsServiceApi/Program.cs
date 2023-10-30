using EmailSenderClient.Shared.Areas.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using NotificationsServiceApi.Areas.Services;
using RabbitMQClient.Shared.Areas.Services;
using RabbitMQClient.Shared.Data.Consts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRabbitMQReceiverService, RabbitMQService>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var broker = scope.ServiceProvider.GetRequiredService<IRabbitMQReceiverService>();

    var service = scope.ServiceProvider.GetRequiredService<INotificationService>();

    broker.Receive(RabbitMQConst.Queues.Notification,
        (obj, msg) => service.TreatMessageReceived(obj, msg));
}

//var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"];

//app.MapGet("/weatherforecast", (HttpContext httpContext) =>
//{
//    httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

//    return Results.Ok();
//})
//.WithName("GetWeatherForecast")
//.RequireAuthorization();

app.Run();