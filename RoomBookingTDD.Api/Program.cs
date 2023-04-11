using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomBokingTDD.Core.Services;
using RoomBokingTDD.Core.Services.Abstract;
using RoomBookingTDD.Core;
using RoomBookingTDD.Persistance;
using RoomBookingTDD.Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = "DaSource=:memory:";
var conn = new SqliteConnection(connString);
builder.Services.AddDbContext<RoomBookingDbContext>(options => options.UseSqlite(conn));
builder.Services.AddScoped<IRoomBookingInterface, RoomBookingService>();
builder.Services.AddScoped<IRoomBookingProcessor, RoomBookingProcessor>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
