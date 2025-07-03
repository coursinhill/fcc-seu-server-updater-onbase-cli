using Hyland.Server.Updater.Interfaces.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal sealed class StartCommand : CommandWrapperBase
{
    protected override string Name => "start";

    protected override string Description => "Start the application with the supplied ManagedComponentId";

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id];

    private readonly IApplicationManipulationService _manipulationService;

    public StartCommand(IApplicationManipulationService manipulationService)
    {
        _manipulationService = manipulationService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        Console.WriteLine("Attempting to start service with id '{0}'", id);

        _manipulationService.StartApplication(id);

        Console.WriteLine("Started Successfully");
    }
}