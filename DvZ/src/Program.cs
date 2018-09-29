using System;
using CommandLine;
using Starship;

namespace DvZ
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            CommandLineOptions options = new CommandLineOptions();
            var parser = Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(o => options = o);

            using (var game = new DvZGame(options))
            {
                game.Run();
            }
        }
    }
}
