namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public string ServiceName { get; set; }
        public string ExchangeName { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}