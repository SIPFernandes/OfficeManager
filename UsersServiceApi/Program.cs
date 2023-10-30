using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using OfficeManager.Shared.Entities;
using RabbitMQClient.Shared.Areas.Services;
using UsersServiceApi.BusinessLogic.Infrastructure;
using UsersServiceApi.BusinessLogic.Interfaces;
using UsersServiceApi.Data;
using UsersServiceApi.Data.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization();

#if DEBUG
var connectionString = builder.Configuration.GetConnectionString("DebugConnection");
#else    
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#endif

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRabbitMQSenderService, RabbitMQService>();

builder.Services.AddDataAccessDependencies(connectionString);
builder.Services.AddBusinessLogicDependencies();

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User API",
        Version = "v1.1",
        Description = "API request and response schema.",
    });

    swagger.EnableAnnotations();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API");
    });
}

#if !DEBUG
    app.UseHttpsRedirection();
#endif

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

//USER
app.MapGet("/api/User", async (IUserBusiness business) =>
{
    return await business.GetAll();
}).WithName("GetAllUsers");

app.MapGet("/api/User/Ids/{usersIds}", async (string usersIds, IUserBusiness business) =>
{
    IList<int> usersIdsList = usersIds.Split(',').Select(Int32.Parse).ToList();

    return await business.GetUsersByIds(usersIdsList);
}).WithName("GetUsersById");

app.MapGet("/api/User/{id}", async (int id, IUserBusiness business) =>
{
    return await business.Get(id);
}).WithName("GetUser");

app.MapGet("/api/User/Email/{email}", async (string email, IUserBusiness business) =>
{
    return await business.GetByEmail(email);
}).WithName("GetUserByEmail");

app.MapPost("/api/User", async (User user, IUserBusiness business) =>
{
    await business.Insert(user);

    return Results.Created($"User created successfully", user);
}).WithName("InsertUser");

app.MapPut("/api/User", async (User user, IUserBusiness business) =>
{
    await business.Update(user);

    return Results.Ok(user);
}).WithName("UpdateUser");

app.MapDelete("/api/User/{id}", async (int id, IUserBusiness business) =>
{
    var user = await business.Get(id);

    if (user == null)
    {
        return Results.NotFound();
    }

    await business.DeleteById(user.Id);

    return Results.Ok();
}).WithName("DeleteUser");

//ROLE
app.MapGet("/api/Role", async (IRoleBusiness business) =>
{
    return await business.GetAll();
}).WithName("GetAllRoles");

app.MapGet("/api/Role/{id}", async (int id, IRoleBusiness business) =>
{
    return await business.Get(id);
}).WithName("GetRole");

app.MapPost("/api/Role", async (Role role, IRoleBusiness business) =>
{
    await business.Insert(role);

    return Results.Created($"Role created successfully", role);
}).WithName("InsertRole");

app.MapPut("/api/Role", async (Role role, IRoleBusiness business) =>
{
    await business.Update(role);

    return Results.Ok(role);
}).WithName("UpdateRole");

app.MapDelete("/api/Role/{id}", async (int id, IRoleBusiness business) =>
{
    var role = await business.Get(id);

    if (role == null)
    {
        return Results.NotFound();
    }

    await business.DeleteById(role.Id);

    return Results.Ok();
}).WithName("DeleteRole");

app.Run();
