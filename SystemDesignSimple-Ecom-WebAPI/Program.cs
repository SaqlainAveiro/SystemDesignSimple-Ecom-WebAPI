using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Events;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SystemDesignSimple_Ecom_WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services
            .Configure<EmailWorkerOptions>(builder.Configuration.GetSection("EmailWorker"));

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(container =>
        {
            container.RegisterModule(new WebModule(connectionString, migrationAssembly));
        });

        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        builder.Services.AddDbContext<EComDbContext>(options =>
            options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));

        builder.Services.AddScoped<IEventPublisher, EventPublisher>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
