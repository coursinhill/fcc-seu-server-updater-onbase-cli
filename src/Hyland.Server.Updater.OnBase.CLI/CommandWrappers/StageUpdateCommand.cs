using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.Interfaces.Interfaces;
using Hyland.Server.Updater.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class StageUpdateCommand : CommandWrapperBase, ISubCommandWrapper
{
    public CommandType BaseCommand => CommandType.Update;

    protected override string Name => "stage";

    protected override string Description => "Stage an update for the application with the supplied ManagedComponentId";

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id, Options.NugetPath, Options.Version];

    private readonly IRepository<Application> _applicationRepo;
    private readonly IPackageRepository _packageRepo;
    private readonly IUpdateService _updateService;
    private readonly IApplicationFileInformationService _fileInfoService;

    public StageUpdateCommand(IRepository<Application> applicationRepo, IPackageRepository packageRepo, IUpdateService updateService, IApplicationFileInformationService fileInfoService)
    {
        _applicationRepo = applicationRepo;
        _packageRepo = packageRepo;
        _updateService = updateService;
        _fileInfoService = fileInfoService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        if (!_applicationRepo.TryGet(id, out Application? application))
        {
            Console.WriteLine("Unable to find Application with ManagedComponentId '{0}'", id);
            return;
        }

        string version = parseResult.GetValue(Options.Version) ?? string.Empty;
        string nugetPath = parseResult.GetValue(Options.NugetPath) ?? string.Empty;

        _packageRepo.RecreateRepository(nugetPath);

        Package? package;
        if (string.IsNullOrWhiteSpace(version))
        {
            Console.WriteLine("Version was not specified. The latest version will be used.");
            package = _packageRepo.GetLatestPackage(application.PackageKey);
        }
        else
        {
            package = _packageRepo.GetPackageWithSpecificVersion(application.PackageKey, version);
        }

        if (package == null)
        {
            Console.WriteLine("Unable to find specified package.");
            return;
        }

        _fileInfoService.PopulateFileInformation(application);

        _updateService.StageUpdate(application, package, new InstallationConfiguration());

        Console.WriteLine("Successfully staged update.");
    }
}