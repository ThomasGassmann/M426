namespace ProductManagement.DataRepresentation.Settings
{
    /// <summary>
    /// Stores the SMTP configuration.
    /// </summary>
    public class SmtpSettings : ISmtpSettings
    {
        /// <inheritdoc />
        public string Host { get; set; }

        /// <inheritdoc />
        public int Port { get; set; }

        /// <inheritdoc />
        public string UserName { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }

        /// <inheritdoc />
        public bool UseSsl { get; set; }

        /// <inheritdoc />
        public bool UseDefaultCredentials { get; set; }

        /// <inheritdoc />
        public string From { get; set; }
    }
}
