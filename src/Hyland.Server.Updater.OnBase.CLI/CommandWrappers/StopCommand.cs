using Hyland.Server.Updater.Interfaces.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal sealed class StopCommand : CommandWrapperBase
{
    protected override string Name => "stop";

    protected override string Description => "Stop the application with the supplied ManagedComponentId";

    protected override CommandType CommandType => CommandType.Stop;

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id];

    private readonly IApplicationManipulationService _manipulationService;

    public StopCommand(IApplicationManipulationService appManipulationService)
    {
        _manipulationService = appManipulationService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        Console.WriteLine("Attempting to stop service with id '{0}'", id);

        _manipulationService.StopApplication(id);

        Console.WriteLine("Stopped Successfully");
    }
}