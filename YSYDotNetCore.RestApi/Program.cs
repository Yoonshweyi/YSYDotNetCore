using log4net.Config;
using NLog;
using NLog.Web;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/MyApp.log", rollingInterval: RollingInterval.Hour)
    .WriteTo
    .MSSqlServer(
   connectionString: "Server=NYEINCHANMOE\\SQL2022;Database=YSYDotNetCore;User ID=sa;Password=201328;TrustServerCertificate = true;",
    sinkOptions: new MSSqlServerSinkOptions { TableName = "Tbl_Logger", AutoCreateSqlTable = true })
   .CreateLogger();


 var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");



try
{

    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();

   
    builder.Logging.AddLog4Net();
    builder.Host.UseNLog();

    builder.Host.UseSerilog();


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
}
catch (Exception exception)
{
    // Log.Fatal(ex, "Application terminated unexpectedly");
   // logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Log.CloseAndFlush();
   // NLog.LogManager.Shutdown();
}
