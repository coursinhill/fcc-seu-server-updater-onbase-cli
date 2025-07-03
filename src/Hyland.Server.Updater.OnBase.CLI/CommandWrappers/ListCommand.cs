using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal sealed class ListCommand : CommandWrapperBase
{
    protected override string Name => "list";

    protected override string Description => "List all applications found by the Server Updater";

    protected override IReadOnlyList<Option> CommandOptions => [];

    private readonly ICombinedApplicationsGenerationService _appService;

    public ListCommand(ICombinedApplicationsGenerationService appService)
    {
        _appService = appService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        IEnumerable<Application> applications = _appService.GetCombinedApplications();

        Console.WriteLine("The following applications are installed");

        foreach (Application app in applications.OrderBy(a => a.AppType.ToString()).ThenBy(a => a.RootPath))
        {
            Console.WriteLine("Application: {0}", app.AppType);
            Console.WriteLine("\tPath: {0}", app.RootPath);
            Console.WriteLine("\tManagedComponentId: {0}", app.ManagedComponentId);
        }

        Console.WriteLine();
    }
}