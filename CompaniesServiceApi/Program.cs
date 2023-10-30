using CompaniesServiceApi.BusinessLogic.Infrastructure;
using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //new

#if DEBUG
var cs = "DebugConnection";
#else    
var cs = "DefaultConnection";
#endif

builder.Services.AddDataAccessDependencies(builder.Configuration.GetConnectionString(cs));
builder.Services.AddBusinessLogicDependencies();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Company API",
        Version = "v1.1",
        Description = "API request and response schema.",
    });

    swagger.EnableAnnotations();

    var filePath = Path.Combine(AppContext.BaseDirectory, "CompaniesServiceApi.xml");
    var filePath2 = Path.Combine("../OfficeManagerApp.shared/bin/Debug/net6.0", "OfficeManagerApp.shared.xml");
    swagger.IncludeXmlComments(filePath);
    swagger.IncludeXmlComments(filePath2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(builder => //new
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company API");
    });
}

#if !DEBUG
    app.UseHttpsRedirection();
#endif

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();