using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class DeletePackageCommand : CommandWrapperBase, ISubCommandWrapper
{
    public CommandType BaseCommand => CommandType.Package;

    protected override string Name => "delete";

    protected override string Description => "Delete the specified package";

    protected override CommandType CommandType => CommandType.Package;

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id, Options.NugetPath, Options.Version];

    private readonly IRepository<Application> _applicationRepository;
    private readonly IPackageRepository _packageRepository;

    public DeletePackageCommand(IPackageRepository packageRepository, IRepository<Application> applicationRepository)
    {
        _packageRepository = packageRepository;
        _applicationRepository = applicationRepository;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        if (!_applicationRepository.TryGet(id, out Application? application))
        {
            Console.WriteLine("Unable to find application {0}", id);
            return;
        }

        string nugetPath = parseResult.GetValue(Options.NugetPath) ?? string.Empty;
        _packageRepository.RecreateRepository(nugetPath);

        string version = parseResult.GetValue(Options.Version) ?? string.Empty;

        Package? package = null;

        if (string.IsNullOrEmpty(version))
        {
            Console.WriteLine("Version was not specified. Assuming latest version.");
            package = _packageRepository.GetLatestPackage(application.PackageKey);
        }
        else
        {
            package = _packageRepository.GetPackageWithSpecificVersion(application.PackageKey, version);
        }

        if (package == null)
        {
            Console.WriteLine("Unable to find specified package.");
            return;
        }

        _packageRepository.Cleanup(package.Identity);

        Console.WriteLine("Successfully deleted package.");
    }
}