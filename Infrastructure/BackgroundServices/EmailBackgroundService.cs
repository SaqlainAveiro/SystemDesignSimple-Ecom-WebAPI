using Application;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundServices;

public class EmailBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly int _interval;

    public EmailBackgroundService(IServiceScopeFactory serviceScopeFactory,
        IOptions<EmailWorkerOptions> options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _interval = options.Value.IntervalSeconds;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(_interval));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var uow = scope.ServiceProvider.GetRequiredService<IEComUnitOfWork>();
            await ProcessEmailsAsync(uow);
        }
    }

    private async Task ProcessEmailsAsync(IEComUnitOfWork uow)
    {
        var emailsToSend = 
            await uow.QueuedEmailRepository.GetQueuedEmailsByStatusAsync(QueuedEmailStatus.Pending);

        foreach (var email in emailsToSend)
        {
            //send mails
            email.EmailStatus = (int)QueuedEmailStatus.Sent;
            await uow.QueuedEmailRepository.SaveEmailQueueAsync(email);
        }
    }
}
