using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class FindPackageCommand : CommandWrapperBase, ISubCommandWrapper
{
    public CommandType BaseCommand => CommandType.Package;

    protected override string Name => "find";

    protected override string Description => "Find the package with the specified version, or latest if not supplied";

    protected override CommandType CommandType => CommandType.Package;

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id, Options.NugetPath, Options.Version];

    private readonly IRepository<Application> _applicationRepository;
    private readonly IPackageRepository _packageRepository;

    public FindPackageCommand(IPackageRepository packageRepository, IRepository<Application> applicationRepository)
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
        }
        else
        {
            Console.WriteLine("Found {0}", package.Identity);
        }
    }
}