using System.Reflection;
using Gelf.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

 //Add services to the container.
var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfig);
//builder.Logging.AddGelf(options =>
//{
//    options.LogSource = builder.Environment.ApplicationName;
//    options.Host = "127.0.0.1";
//    options.AdditionalFields["machine_name"] = Environment.MachineName;
//    options.AdditionalFields["app_version"] = Assembly.GetEntryAssembly()
//                    ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

