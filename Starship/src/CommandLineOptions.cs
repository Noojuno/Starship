using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace Starship
{
    public class CommandLineOptions
    {
        [Option('d', "dev", Required = false, HelpText = "Developer Mode")]
        public bool Dev { get; set; }

        [Option("fullscreen", Required = false, HelpText = "Developer Mode")]
        public bool Fullscreen { get; set; } = false;
    }
}
