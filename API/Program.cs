using API.Middleware;
using Core.Interfaces;
using API.Helpers;
using API.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJobStatisticsRepository, JobStatisticsRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddDbContext<JobContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddSingleton<ILoggerService, LoggerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseAuthorization();

app.MapControllers();

app.Run();
