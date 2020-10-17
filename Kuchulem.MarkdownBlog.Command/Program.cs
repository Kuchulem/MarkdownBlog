using CommandLine;
using Kuchulem.ConsoleDrawer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;

namespace Kuchulem.MarkdownBlog.Command
{
    class Program
    {
        private enum Scopes { None, Articles, Pages, All };
        class Options
        {
            [Option('c', "cache", SetName = "cache", HelpText = "Enables cache management for the Kuchulem.MarkdownBlog application")]
            public bool Cache { get; set; }

            [Option('r', "reload", SetName = "cache", HelpText = "Reloads the cache of the Kuchulem.MarkdownBlog application")]
            public bool Reload { get; set; }

            [Option('s', "scope", SetName = "cache", HelpText = "Sets the scope (Articles / Pages) for witch to manage the cache")]
            public string Scope { get; set; }

            [Option('b', "blog", HelpText = "The blog url")]
            public string BlogUrl { get; set; }
        }

        static void Main(string[] args)
        {
            ConsoleExtended.DrawPattern(
                "                       " + Environment.NewLine +
                "  **** **        ****  " + Environment.NewLine +
                "  **   **          **  " + Environment.NewLine +
                "  **   **     **   **  " + Environment.NewLine +
                "  **   **   **     **  " + Environment.NewLine +
                "  **   ** **       **  " + Environment.NewLine +
                "  **   **   **     **  " + Environment.NewLine +
                "  **   **     **   **  " + Environment.NewLine +
                "  **   **          **  " + Environment.NewLine +
                "  **** **        ****  ",
                new Dictionary<char, ConsoleColor?>
                {
                    { ' ', ConsoleColor.DarkGray },
                    { '*', ConsoleColor.Black }
                },
                ConsoleTextAlignment.Center
            );
            ConsoleExtended.WriteLine("       Kuchulem       ", ConsoleTextAlignment.Center, ConsoleColor.DarkGray, ConsoleColor.Black);
            Console.WriteLine("");
            ConsoleExtended.WriteLine("", ConsoleTextAlignment.Center, ConsoleColor.DarkGray, ConsoleColor.Black, true);
            ConsoleExtended.WriteLine("Welcome on Kuchulem.MarkdownBlog Command", ConsoleTextAlignment.Center, ConsoleColor.DarkGray, ConsoleColor.Black, true);
            ConsoleExtended.WriteLine("", ConsoleTextAlignment.Center, ConsoleColor.DarkGray, ConsoleColor.Black, true);
            Console.WriteLine("");
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                if(string.IsNullOrEmpty(o.BlogUrl))
                {
                    WriteError("No blog url provided, use option --blog [url].");
                    return;
                }    

                if (o.Cache)
                    RunCacheManager(o);
                else
                    WriteError("Use --help option to see available options.");

            });
        }

        private static void RunCacheManager(Options o)
        {
            WriteInfo("Entering on Kuchulem.MarkdownBlog cache management.");

            var scope = Scopes.All;

            if(!string.IsNullOrEmpty(o.Scope))
                scope = o.Scope switch
                {
                    "Articles" => Scopes.Articles,
                    "Pages" => Scopes.Pages,
                    _ => Scopes.None
                };

            if(scope == Scopes.None)
            {
                WriteError("Invalid scope provided");
                return;
            }

            WriteInfo($"Targeted scopes : {scope}");

            if (o.Reload)
            {
                WriteInfo("Will reload cache");
                var key = GenerateKey();
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = delegate { return true; }
                })
                {
                    BaseAddress = new Uri(o.BlogUrl)
                };
                try
                {
                    var path = $"/Cache/Reload?scope={scope}&key={key}";
                    WriteInfo($"Sending command to the blog : {path}");
                    var response = client.GetAsync(path).GetAwaiter().GetResult();

                    if (!response.IsSuccessStatusCode)
                    {
                        WriteError($"An error occured whil calling the blog : {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch(Exception e)
                {
                    WriteError($"An error occured whil calling the blog : {e.Message}");
                }

                DropKeyFile();
            }
            else
            {
                WriteError("No command provided for cache.");
                return;
            }
        }

        private static void WriteError(string message)
        {
            ConsoleExtended.WritePrefixedLine("Error   :", message, ConsoleColor.DarkGray, ConsoleColor.DarkRed, ConsoleColor.Black);
        }

        private static void WriteInfo(string message)
        {
            ConsoleExtended.WritePrefixedLine("Info    :", message, ConsoleColor.DarkGray, ConsoleColor.DarkGreen, ConsoleColor.Black);
        }

        private static string GenerateKey()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            WriteInfo($"Creating key file in {currentDirectory}");
            var key = Guid.NewGuid().ToString();

            using var stream = File.OpenWrite(Path.Combine(currentDirectory, "command_access_key.key"));
            using var writer = new StreamWriter(stream);
            writer.Write(key);

            WriteInfo("Created access key file");

            return key;
        }

        private static void DropKeyFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            WriteInfo($"Dropping key file in {currentDirectory}");
            var path = Path.Combine(currentDirectory, "command_access_key.key");
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
