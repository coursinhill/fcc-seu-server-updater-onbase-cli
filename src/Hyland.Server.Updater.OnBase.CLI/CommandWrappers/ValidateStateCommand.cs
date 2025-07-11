using Hyland.Server.Updater.Interfaces.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class ValidateStateCommand : CommandWrapperBase, ISubCommandWrapper
{
    public CommandType BaseCommand => CommandType.State;

    protected override string Name => "validate";

    protected override string Description => "Validate that the current state is the same as when stop was called. This will invalidate the state.";

    protected override CommandType CommandType => CommandType.State;

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id];

    private readonly IApplicationManipulationService _service;

    public ValidateStateCommand(IApplicationManipulationService service)
    {
        _service = service;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        bool result = _service.ValidateApplicationState(id);

        if (result)
        {
            Console.WriteLine("State was successfully validated.");
        }
        else
        {
            Console.WriteLine("State did not match. Validation failed.");
        }
    }
}