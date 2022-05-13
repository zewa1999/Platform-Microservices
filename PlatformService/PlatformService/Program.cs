using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Repository;
using PlatformService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseInMemoryDatabase("InMemoryDB"));
builder.Services.AddScoped<IPlatformRepo<Platform>, PlatformRepo>();
builder.Services.AddControllers();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();