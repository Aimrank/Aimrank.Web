namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Emails
{
    public class EmailSettings
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SmtpAddress { get; set; }
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
        public bool UseAuthentication { get; set; }
        public string AuthenticationUsername { get; set; }
        public string AuthenticationPassword { get; set; }
    }
}