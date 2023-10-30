using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfficeManager.Shared.Entities;
using RabbitMQClient.Shared.Areas.Services;
using BookingsServiceApi.BusinessLogic.Infrastructure;
using BookingsServiceApi.BusinessLogic.Interfaces;
using BookingsServiceApi.Data;
using BookingsServiceApi.Data.Infrastructure;
using OfficeManager.Shared.Exceptions;
using OfficeManager.Shared.Response_Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddMicrosoftIdentityWebApi(builder.Configuration);
//builder.Services.AddAuthorization();

#if DEBUG
var connectionString = builder.Configuration.GetConnectionString("DebugConnection");
#else    
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#endif

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

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
        Title = "Booking API",
        Version = "v1.1",
        Description = "API request and response schema.",
    });

    swagger.EnableAnnotations();

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API");
    });
}

#if !DEBUG
    app.UseHttpsRedirection();
#endif

//app.UseAuthentication();
//app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    context.Database.Migrate();
}

//Booking
app.MapGet("/api/Booking", async (IBookingBusiness business) =>
{
    return await business.GetAll();
}).WithName("GetAllBookings");
//.RequireAuthorization();

app.MapGet("/api/Booking/{id}", async (int id, IBookingBusiness business) =>
{
    return await business.Get(id);
}).WithName("GetBooking");
//.RequireAuthorization();

app.MapGet("/api/Booking/{id}/{date}", async (int id, DateTime date, IBookingBusiness business) =>
{
    return await business.GetBookingsByDateRoomId(id, date);
}).WithName("GetBookingsByDateRoom");
//.RequireAuthorization();

app.MapGet("/api/Booking/Date/{date}", async (DateTime date, IBookingBusiness business) =>
{
    return await business.GetBookingsByDate(date);
}).WithName("GetBookingsByDate");
//.RequireAuthorization();

app.MapPost("/api/Booking", async (Booking booking, IBookingBusiness business) =>
{
    try
    {
        await business.Insert(booking);
    }
    catch (EntityDuplicateException msg)
    {
        var errors = new List<string> { msg.Message };

        return Results.BadRequest(new BadResponseModel { Errors = errors });
    }
    catch (Exception ex)
{
        var errors = new List<string> { "Something went wrong!" };

        return Results.BadRequest(new BadResponseModel { Errors = errors });
    }

    return Results.Created($"Booking created successfully", booking);
}).WithName("InsertBooking");
//.RequireAuthorization();

app.MapPut("/api/Booking/{id}", async (int id, Booking booking, IBookingBusiness business) =>
{
    booking.Id = id;

    await business.Update(booking);

    return Results.Ok(booking);
}).WithName("UpdateBooking");
//.RequireAuthorization();

app.MapDelete("/api/Booking/{id}", async (int id, IBookingBusiness business) =>
{
    var booking = await business.Get(id);

    if (booking == null)
    {
        return Results.NotFound();
    }

    await business.DeleteById(booking.Id);

    return Results.Ok();
}).WithName("DeleteBooking");
//.RequireAuthorization();


//SeatsAvailable
app.MapGet("/api/SeatsAvailable", async (ISeatsAvailableBusiness business) =>
{
    return await business.GetAll();
}).WithName("GetAllSeatsAvailable");
//.RequireAuthorization();

app.MapGet("/api/SeatsAvailable/Date/{date}", async (DateTime date, ISeatsAvailableBusiness business) =>
{
    return await business.GetSeatsUnavailableByDate(date);
}).WithName("GetSeatsUnavailableByDate");
//.RequireAuthorization();

app.MapGet("/api/SeatsAvailable/Room/{roomId}/Date/{date}", async (int roomId, DateTime date, ISeatsAvailableBusiness business) =>
{
    return await business.GetSeatsLeftByRoomIdDate(roomId, date);
}).WithName("GetSeatsLeftByRoomIdDate");
//.RequireAuthorization();

app.MapGet("/api/SeatsAvailable/{id}", async (int id, ISeatsAvailableBusiness business) =>
{
    return await business.Get(id);
}).WithName("GetSeatsAvailable");
//.RequireAuthorization();

app.MapPost("/api/SeatsAvailable", async (SeatsAvailable seatsAvailable, ISeatsAvailableBusiness business) =>
{
    await business.Insert(seatsAvailable);

    return Results.Created($"SeatsAvailable created successfully", seatsAvailable);
}).WithName("InsertSeatsAvailable");
//.RequireAuthorization();

app.MapPut("/api/SeatsAvailable", async (SeatsAvailable seatsAvailable, ISeatsAvailableBusiness business) =>
{
    await business.Update(seatsAvailable);

    return Results.Ok(seatsAvailable);
}).WithName("UpdateSeatsAvailable");
//.RequireAuthorization();

app.MapDelete("/api/SeatsAvailable/{id}", async (int id, ISeatsAvailableBusiness business) =>
{
    var booking = await business.Get(id);

    if (booking == null)
    {
        return Results.NotFound();
    }

    await business.DeleteById(booking.Id);

    return Results.Ok();
}).WithName("DeleteSeatsAvailable");
//.RequireAuthorization();

app.Run();