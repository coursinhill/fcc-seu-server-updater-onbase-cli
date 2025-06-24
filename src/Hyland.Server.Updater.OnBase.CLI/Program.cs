using Hyland.Server.Updater.Common.Extensions;
using Hyland.Server.Updater.OnBase.CLI.Extensions;
using Hyland.Server.Updater.OnBase.CLI.Handlers;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
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

        IServiceProvider provider = new ServiceCollection()
                 .AddLogging(builder =>
                 {
                     builder.AddConfiguration(root.GetSection("Logging"));
                     builder.AddConsole();
                 })
                .AddServerUpdaterOnBase()
                .AddCliCommandHandlers()
                .BuildServiceProvider();

        //handlers
        ListHandler listHandler = provider.GetRequiredService<ListHandler>();
        StartHandler startHandler = provider.GetRequiredService<StartHandler>();
        StopHandler stopHandler = provider.GetRequiredService<StopHandler>();
        StageHandler stageHandler = provider.GetRequiredService<StageHandler>();
        UpdateHandler updateHandler = provider.GetRequiredService<UpdateHandler>();
        BackupHandler backupHandler = provider.GetRequiredService<BackupHandler>();
        VerifyHandler verifyHandler = provider.GetRequiredService<VerifyHandler>();
        RollbackHandler rollbackHandler = provider.GetRequiredService<RollbackHandler>();

        //Commands
        Command listCommand = new Command("list", "List all applications found by the Server Updater");
        listCommand.SetAction(listHandler.HandleRequest);

        Command startCommand = new Command("start", "Start the application with the supplied ManagedComponentId")
        {
            Options.Id
        };
        startCommand.SetAction(startHandler.HandleRequest);

        Command stopCommand = new Command("stop", "Stop the application with the supplied ManagedComponentId")
        {
            Options.Id
        };
        stopCommand.SetAction(stopHandler.HandleRequest);

        Command stageCommand = new Command("stage", "Stage an update for the application with the supplied ManagedComponentId")
        {
            Options.Id,
            Options.NugetPath,
            Options.Version,
        };
        stageCommand.SetAction(stageHandler.HandleRequest);

        Command updateCommand = new Command("update", "Install an update for the application with the supplied ManagedComponentId")
        {
            Options.Id,
            Options.NugetPath,
            Options.Version
        };
        updateCommand.SetAction(updateHandler.HandleRequest);

        Command verifyCommand = new Command("verify", "Verify the installation of application with the supplied ManagedComponentId")
        {
            Options.Id
        };
        verifyCommand.SetAction(verifyHandler.HandleRequest);

        Command backupCommand = new Command("backup", "Create a backup of the application with the supplied ManagedComponentId")
        {
            Options.Id,
            Options.TempPath
        };
        backupCommand.SetAction(backupHandler.HandleRequest);

        Command rollbackCommand = new Command("rollback", "Roll back the application with the supplied ManagedComponentId with the specified backup")
        {
            Options.Id,
            Options.BackupFile
        };
        rollbackCommand.SetAction(rollbackHandler.HandleRequest);

        RootCommand command = new RootCommand("CLI tool to test the OnBase Server Updater")
        {
            backupCommand,
            listCommand,
            rollbackCommand,
            stageCommand,
            startCommand,
            stopCommand,
            updateCommand,
            verifyCommand
        };

        Console.WriteLine("Hello. Enter desired command (--help for help. exit to exit):");
        string userInput = Console.ReadLine() ?? string.Empty;

        while (!_exitTerms.Contains(userInput, StringComparer.OrdinalIgnoreCase))
        {
            ParseResult result = command.Parse(userInput);
            result.Invoke();

            Console.WriteLine();
            Console.WriteLine("Awaiting Input:");

            userInput = Console.ReadLine() ?? string.Empty;
        }

        Console.WriteLine("Goodbye");
    }
}