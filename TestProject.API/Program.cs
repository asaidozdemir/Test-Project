using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MediatR;
using TestProject.Application.Handlers;
using TestProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL connection string from ENV
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["POSTGRES_CONNECTION"];

builder.Services.AddSingleton(new DatabaseExecutor(connectionString));

// Register MediatR — scans the Application assembly for handlers
builder.Services.AddMediatR(typeof(GetGenericQueryHandler).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();