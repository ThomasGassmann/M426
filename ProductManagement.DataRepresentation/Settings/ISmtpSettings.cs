namespace ProductManagement.DataRepresentation.Settings
{
    /// <summary>
    /// Defines all settings for the SMTP client.
    /// </summary>
    public interface ISmtpSettings
    {
        /// <summary>
        /// Gets or sets the SMTP host.
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Gets or sets the SMTP port.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets the SMTP user name.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use SSL or not.
        /// </summary>
        bool UseSsl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the default credentials.
        /// </summary>
        bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets or sets the mail address, from which emails are sent.
        /// </summary>
        string From { get; set; }
    }
}
