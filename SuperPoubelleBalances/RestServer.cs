using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class RestServer
{
    Task _webApiTask;
    WebApplication _app;

    public Task Start()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            ApplicationName = "SuperPoubelleBalances",
            EnvironmentName = "Development",

        });

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = _app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Urls.Add("http://*:5003");
        _webApiTask = app.RunAsync();
        return _webApiTask;
    }

    public async Task StopAsync()
    {
        _app.Lifetime.StopApplication();
        await _webApiTask;
    }
}