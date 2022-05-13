using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Repository;
using PlatformService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseInMemoryDatabase("InMemoryDB"));
builder.Services.AddScoped<IPlatformRepo<Platform>, PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app);

app.Run();