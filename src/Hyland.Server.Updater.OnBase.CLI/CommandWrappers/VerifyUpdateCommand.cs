using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class VerifyUpdateCommand : CommandWrapperBase, ISubCommandWrapper
{
    public CommandType BaseCommand => CommandType.Update;

    protected override string Name => "verify";

    protected override string Description => "Verify the installation of application with the supplied ManagedComponentId";

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id];

    private readonly IRepository<Application> _applicationRepo;
    private readonly IUpdateService _updateService;

    public VerifyUpdateCommand(IRepository<Application> applicationRepo, IUpdateService updateService)
    {
        _applicationRepo = applicationRepo;
        _updateService = updateService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        if (!_applicationRepo.TryGet(id, out Application? application))
        {
            Console.WriteLine("Unable to find Application with ManagedComponentId '{0}'", id);
            return;
        }

        bool result = _updateService.VerifyUpdate(application);

        if (result)
        {
            Console.WriteLine("Successfully verified update.");
        }
        else
        {
            Console.WriteLine("Update verification failed.");
        }
    }
}