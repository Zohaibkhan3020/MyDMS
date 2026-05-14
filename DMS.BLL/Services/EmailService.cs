using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace DMS.BLL.Services
{
    

    public class EmailService
        : IEmailService
    {
        private readonly
            IConfiguration
                _configuration;

        private readonly
            IEmailRepository
                _repository;

        public EmailService(
            IConfiguration configuration,

            IEmailRepository repository)
        {
            _configuration =
                configuration;

            _repository =
                repository;
        }

        public async Task SendAsync(
            SendEmailDto dto)
        {
            try
            {
                var smtp =
                    _configuration["Email:Smtp"];

                var port =
                    Convert.ToInt32(
                        _configuration["Email:Port"]);

                var username =
                    _configuration["Email:Username"];

                var password =
                    _configuration["Email:Password"];

                using var client =
                    new SmtpClient(
                        smtp,
                        port);

                client.Credentials =
                    new NetworkCredential(
                        username,
                        password);

                client.EnableSsl = true;

                var mail =
                    new MailMessage();

                mail.From =
                    new MailAddress(
                        username);

                mail.To.Add(
                    dto.ToEmail);

                mail.Subject =
                    dto.Subject;

                mail.Body =
                    dto.BodyHTML;

                mail.IsBodyHtml =
                    true;

                await client
                    .SendMailAsync(mail);

                await _repository
                    .SaveLogAsync(
                        new EmailLog
                        {
                            ToEmail =
                                dto.ToEmail,

                            Subject =
                                dto.Subject,

                            BodyHTML =
                                dto.BodyHTML,

                            SentOn =
                                DateTime.Now,

                            IsSuccess =
                                true
                        });
            }
            catch (Exception ex)
            {
                await _repository
                    .SaveLogAsync(
                        new EmailLog
                        {
                            ToEmail =
                                dto.ToEmail,

                            Subject =
                                dto.Subject,

                            BodyHTML =
                                dto.BodyHTML,

                            SentOn =
                                DateTime.Now,

                            IsSuccess =
                                false,

                            ErrorMessage =
                                ex.Message
                        });

                throw;
            }
        }
    }
}
