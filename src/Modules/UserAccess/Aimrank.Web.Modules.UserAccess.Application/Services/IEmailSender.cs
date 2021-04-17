using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Services
{
    public record EmailRecipient(string Name, string Email);
    
    public interface IEmailSender
    {
        Task SendAsync(IEnumerable<EmailRecipient> recipients, string subject, string text, string html, CancellationToken cancellationToken);
    }
}