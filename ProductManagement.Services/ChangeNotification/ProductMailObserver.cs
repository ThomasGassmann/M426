namespace ProductManagement.Services.ChangeNotification
{
    using ProductManagement.DataRepresentation.Settings;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an observer for a product and sends a mail if a product changes.
    /// </summary>
    public class ProductMailObserver : IObserver<ObservableProduct>
    {
        /// <summary>
        /// Contains the <see cref="SmtpClient"/> to send the mails.
        /// </summary>
        private readonly SmtpClient smtpClient;

        /// <summary>
        /// Contains the settings for the SMTP client.
        /// </summary>
        private readonly ISmtpSettings mailConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMailObserver"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="ISmtpSettings"/> dependency.</param>
        public ProductMailObserver(ISmtpSettings settings)
        {
            this.mailConfiguration = settings;
            this.smtpClient = this.InitializeSmtpClient(
                settings.Host,
                settings.Port,
                settings.UserName,
                settings.Password,
                settings.UseDefaultCredentials,
                settings.UseSsl,
                settings.From);
        }

        /// <inheritdoc />
        public async Task Update(ObservableProduct changed)
        {
            var message = new MailMessage(this.mailConfiguration.From, changed.Email)
            {
                Body = $"Product with id {changed.ProductId} has changed!",
                Subject = "Prudct changed"
            };
            await this.smtpClient.SendMailAsync(message);
        }

        /// <summary>
        /// Initializes a new SMTP client.
        /// </summary>
        /// <param name="host">The SMTP host.</param>
        /// <param name="port">The SMTP port.</param>
        /// <param name="userName">The SMTP user name.</param>
        /// <param name="password">The SMTP password.</param>
        /// <param name="useDefaultCredentials">Dertermines whether to use default credentials.</param>
        /// <param name="useSsl">Determines whether to use SSL.</param>
        /// <param name="from">The SMTP sender.</param>
        /// <returns>Returns the created <see cref="SmtpClient"/>.</returns>
        private SmtpClient InitializeSmtpClient(string host, int port, string userName, string password, bool useDefaultCredentials, bool useSsl, string from) =>
            new SmtpClient
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
