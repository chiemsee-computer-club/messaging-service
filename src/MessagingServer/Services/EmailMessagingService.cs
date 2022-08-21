using System;
using System.Net;
using System.Net.Mail;
using MessagingServer.Dto;
using MessagingServer.Models;

namespace MessagingServer.Services
{
	public class EmailMessagingService : IMessagingService
	{
        private readonly SmtpClient _smtpClient;
        private readonly MessagingEmailProvider _config;

        public EmailMessagingService(MessagingEmailProvider config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            _smtpClient = new SmtpClient(config.Host);
            var credentials = new NetworkCredential
            {
                UserName = _config.Email,
                Password = _config.Password
            };

            _smtpClient.Credentials = credentials;
        }

		public MessagingEmailProvider Config { get; set; }

        public Task<SendResult> SendAsync(SendRequest request)
        {
            var message = new MailMessage
            {
                Subject = "Hello World",
                Body = "Test",
                From = new MailAddress(_config.Email),
                To = { "mail@bauer-jakob.de" }
            };

            _smtpClient.Send(message);

            return Task.FromResult(new SendResult {Success = true});
        }
    }
}

