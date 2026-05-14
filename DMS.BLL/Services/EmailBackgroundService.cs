using DMS.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace DMS.BLL.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailBackgroundService> _logger;

        public EmailBackgroundService(
            IServiceScopeFactory scopeFactory,
            IConfiguration configuration,
            ILogger<EmailBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email Background Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var repository = scope.ServiceProvider
                        .GetRequiredService<INotificationRepository>();

                    var emails = await repository.GetPendingEmailsAsync();

                    if (emails != null && emails.Any())
                    {
                        foreach (var email in emails)
                        {
                            try
                            {
                                await SendEmailAsync(email, stoppingToken);

                                await repository.MarkEmailSentAsync(email.EmailQueueID);

                                _logger.LogInformation(
                                    "Email sent to {ToEmail}",
                                    email.ToEmail);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex,
                                    "Failed to send email to {ToEmail}",
                                    email.ToEmail);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error in EmailBackgroundService main loop");
                }

                await Task.Delay(10000, stoppingToken);
            }

            _logger.LogInformation("Email Background Service Stopped");
        }

        private async Task SendEmailAsync(dynamic email, CancellationToken token)
        {
            var smtpHost = _configuration["Email:Smtp"]
                ?? throw new Exception("SMTP Host missing");

            var port = int.Parse(_configuration["Email:Port"]
                ?? throw new Exception("SMTP Port missing"));

            var username = _configuration["Email:Username"]
                ?? throw new Exception("SMTP Username missing");

            var password = _configuration["Email:Password"]
                ?? throw new Exception("SMTP Password missing");

            var from = _configuration["Email:From"]
                ?? throw new Exception("SMTP From missing");

            using var smtp = new SmtpClient(smtpHost)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(from),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };

            mail.To.Add(email.ToEmail);

            await smtp.SendMailAsync(mail, token);
        }
    }
}