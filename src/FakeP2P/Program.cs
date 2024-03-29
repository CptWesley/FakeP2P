using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FakeP2P
{
    /// <summary>
    /// Entry class of the program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    if (int.TryParse(Environment.GetEnvironmentVariable("PORT"), out int port))
                    {
                        webBuilder.UseKestrel(options =>
                        {
                            options.ListenAnyIP(port);
                        });
                    }
                })
                .Build()
                .Run();
    }
}
