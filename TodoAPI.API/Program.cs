using Microsoft.EntityFrameworkCore;
using TodoAPI.API.Context;
using TodoAPI.API.Models;
using TodoAPI.API.Repository;
using TodoAPI.API.Repository.Interfaces;
using TodoAPI.API.Service;
using TodoAPI.API.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Entity Framework Core with SQLite
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TodoConnection")));

// Add the Repository
builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();

// Add the services
builder.Services.AddScoped<IService<TaskItem>, TaskService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
app.UseAuthorization();

app.MapControllers();

app.Run();
