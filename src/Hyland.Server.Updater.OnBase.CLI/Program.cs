using Hyland.Server.Updater.Common.Extensions;
using Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
using Hyland.Server.Updater.OnBase.CLI.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI;

public class Program
{
    private static readonly HashSet<string> _exitTerms = ["exit", "quit", "q", string.Empty];

    public static void Main(string[] args)
    {
        IConfigurationRoot root = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        using ServiceProvider provider = new ServiceCollection()
                 .AddLogging(builder =>
                 {
                     builder.AddConfiguration(root.GetSection("Logging"));
                     builder.AddConsole();
                 })
                .AddServerUpdaterOnBase()
                .AddCliCommandHandlers()
                .BuildServiceProvider();

        IEnumerable<Command> commands = provider.GetRequiredService<IEnumerable<ICommandWrapper>>()
            .Select(w => w.Command)
            .OrderBy(c => c.Name);

        RootCommand rootCommand = new RootCommand("CLI tool to test the OnBase Server Updater");
        
        foreach (Command command in commands)
        {
            rootCommand.Add(command);
        }

        Console.WriteLine("Hello. Enter desired command (--help for help. exit to exit):");
        string userInput = Console.ReadLine() ?? string.Empty;

        while (!_exitTerms.Contains(userInput, StringComparer.OrdinalIgnoreCase))
        {
            ParseResult result = rootCommand.Parse(userInput);
            result.Invoke();

            Console.WriteLine();
            Console.WriteLine("Awaiting Input:");

            userInput = Console.ReadLine() ?? string.Empty;
        }

        Console.WriteLine("Goodbye");
    }
}