namespace ProductManagement.Services.ChangeNotification
{
    using ProductManagement.DataRepresentation.Dto;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class ProductMailObserver : IObserver<ObservableProduct>
    {
        private readonly SmtpClient smtpClient;

        private readonly MailConfigurationDto mailConfiguration;

        public ProductMailObserver(MailConfigurationDto mailConfiguration)
        {
            this.mailConfiguration = mailConfiguration;
            this.smtpClient = this.InitializeSmtpClient(
                mailConfiguration.Host,
                mailConfiguration.Port,
                mailConfiguration.UserName,
                mailConfiguration.Password,
                mailConfiguration.UseDefaultCredentials,
                mailConfiguration.UseSsl,
                mailConfiguration.From);
        }

        public async Task Update(ObservableProduct changed)
        {
            var message = new MailMessage(this.mailConfiguration.From, changed.Email)
            {
                Body = $"Product with id {changed.ProductId} has changed!",
                Subject = "Prudct changed"
            };
            await this.smtpClient.SendMailAsync(message);
        }

        private SmtpClient InitializeSmtpClient(string host, int port, string userName, string password, bool useDefaultCredentials, bool useSsl, string from)
        {
            return new SmtpClient
            {
                Host = host,
                Port = port,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = useSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }
    }
}
