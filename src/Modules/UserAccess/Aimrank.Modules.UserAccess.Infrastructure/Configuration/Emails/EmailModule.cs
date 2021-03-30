using Aimrank.Modules.UserAccess.Application.Services;
using Autofac;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration.Emails
{
    internal class EmailModule : Autofac.Module
    {
        private readonly EmailSettings _emailSettings;

        public EmailModule(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterInstance(_emailSettings).SingleInstance();
        }
    }
}