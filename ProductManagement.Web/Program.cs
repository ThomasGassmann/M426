namespace ProductManagement.Web
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Starts the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Builds the web host and runs it.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();            
    }
}
