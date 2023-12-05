using AutoMapper;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.EntityFrameworkCore;
using ParserDbContext;
using WebParser.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ParserContext>(opt => opt.UseSqlite(connectionString));


var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");
builder.Services.AddHangfire(opt => opt.UseSQLiteStorage(hangfireConnectionString));
builder.Services.AddHangfireServer();

var mapper = new MapperConfiguration(opt => opt.AddProfile<ParserMapperProfile>()).CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<FixsenParserService>();
builder.Services.AddScoped<GroheParserService>();
builder.Services.AddTransient<ShopService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ProductsNamesService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHangfireDashboard("/dashboard");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(opt => 
        opt.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var fixsenParserService = scope.ServiceProvider.GetService<FixsenParserService>();
var groheParserService = scope.ServiceProvider.GetService<GroheParserService>();

RecurringJob.AddOrUpdate("Fetch fixsen prices", () => fixsenParserService.ParseUrls(), Cron.Hourly());
RecurringJob.AddOrUpdate("Fetch grohe prices", () => groheParserService.ParseUrls(), Cron.Hourly());

app.Run();
