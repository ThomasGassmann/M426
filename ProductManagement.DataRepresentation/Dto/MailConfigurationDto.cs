namespace ProductManagement.DataRepresentation.Dto
{
    /// <summary>
    /// Stores the SMTP configuration.
    /// </summary>
    public class MailConfigurationDto
    {
        /// <summary>
        /// Gets or sets the SMTP host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the SMTP port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the SMTP user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use SSL or not.
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the default credentials.
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets or sets the mail address, from which emails are sent.
        /// </summary>
        public string From { get; set; }
    }
}
