using Application;
using Application.Orders;
using Autofac;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Events;
using Infrastructure.Repositories;

namespace SystemDesignSimple_Ecom_WebAPI;

public class WebModule : Module
{
    private readonly string _migrationAssembly;
    private readonly string _connectionString;
    private const string CONNECTION_STRING = "connectionString";
    private const string MIGRATION_ASSEMBLY = "migrationAssembly";

    public WebModule(string migrationAssembly, 
        string connectionString)
    {
        _migrationAssembly = migrationAssembly;
        _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EComDbContext>().AsSelf()
            .WithParameter(CONNECTION_STRING, _connectionString)
            .WithParameter(MIGRATION_ASSEMBLY, _migrationAssembly)
            .InstancePerLifetimeScope();

        builder.RegisterType<EComUnitOfWork>()
            .As<IEComUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<OrderRepository>()
            .As<IOrderRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QueuedEmailRepository>()
            .As<IQueuedEmailRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<IIdempotencyKeyRecordRepository>()
            .As<IIdempotencyKeyRecordRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<IOrderService>()
            .As<OrderService>()
            .InstancePerLifetimeScope();

        base.Load(builder);
    }
}
