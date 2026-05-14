using DMS.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;

namespace DMS.BLL.Services
{
    

    public class EmailBackgroundService
        : BackgroundService
    {
        private readonly IServiceScopeFactory
            _scopeFactory;

        private readonly IConfiguration
            _configuration;

        public EmailBackgroundService(
            IServiceScopeFactory scopeFactory,

            IConfiguration configuration)
        {
            _scopeFactory =
                scopeFactory;

            _configuration =
                configuration;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope =
                    _scopeFactory
                        .CreateScope();

                var repository =
                    scope.ServiceProvider
                        .GetRequiredService<
                            INotificationRepository>();

                var emails =
                    await repository
                        .GetPendingEmailsAsync();

                foreach (var email in emails)
                {
                    try
                    {
                        using var smtp =
                            new SmtpClient(
                                _configuration["Email:Smtp"])
                            {
                                Port =
                                    int.Parse(
                                        _configuration["Email:Port"]),

                                Credentials =
                                    new NetworkCredential(
                                        _configuration["Email:Username"],
                                        _configuration["Email:Password"]),

                                EnableSsl = true
                            };

                        var mail =
                            new MailMessage(
                                _configuration["Email:From"],
                                email.ToEmail,
                                email.Subject,
                                email.Body);

                        await smtp.SendMailAsync(mail);

                        await repository
                            .MarkEmailSentAsync(
                                email.EmailQueueID);
                    }
                    catch
                    {

                    }
                }

                await Task.Delay(
                    10000,
                    stoppingToken);
            }
        }
    }
}
