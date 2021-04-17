using Aimrank.Web.Modules.UserAccess.Application.Services;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Emails
{
    internal class EmailSender : IEmailSender
    {
        private readonly EmailSettings _mailSettings;

        public EmailSender(EmailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendAsync(
            IEnumerable<EmailRecipient> recipients,
            string subject,
            string text,
            string html,
            CancellationToken cancellationToken = default)
        {
            var message = CreateMimeMessage(recipients, subject, text, html);

            await SendMessageToRecipientsAsync(message, cancellationToken);
        }

        private MimeMessage CreateMimeMessage(
            IEnumerable<EmailRecipient> recipients,
            string subject,
            string text,
            string html)
        {
            var content = new BodyBuilder
            {
                TextBody = text,
                HtmlBody = html
            };

            var message = new MimeMessage
            {
                Subject = subject,
                Body = content.ToMessageBody()
            };
            
            message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));

            foreach (var recipient in recipients)
            {
                message.To.Add(new MailboxAddress(recipient.Name, recipient.Email));
            }

            return message;
        }

        private async Task SendMessageToRecipientsAsync(MimeMessage message, CancellationToken cancellationToken)
        {
            using var client = new SmtpClient();

            await client.ConnectAsync(
                _mailSettings.SmtpAddress,
                _mailSettings.SmtpPort,
                _mailSettings.UseSsl,
                cancellationToken);

            if (_mailSettings.UseAuthentication)
            {
                await client.AuthenticateAsync(
                    _mailSettings.AuthenticationUsername,
                    _mailSettings.AuthenticationPassword, cancellationToken);
            }

            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}