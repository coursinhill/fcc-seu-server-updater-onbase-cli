using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.Interfaces.Interfaces;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class BackupCommand : CommandWrapperBase
{
    protected override string Name => "backup";

    protected override string Description => "Create a backup of the application with the supplied ManagedComponentId";

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id, Options.TempPath, Options.Salt];

    private readonly IRepository<Application> _applicationRepo;
    private readonly IBackupApplicationService _backupService;

    public BackupCommand(IEnumerable<ISubCommandWrapper> subCommands, IRepository<Application> applicationRepo, IBackupApplicationService backupService)
        : base(CommandType.Backup, subCommands)
    {
        _applicationRepo = applicationRepo;
        _backupService = backupService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        if (!_applicationRepo.TryGet(id, out Application? application))
        {
            Console.WriteLine("Unable to find Application with ManagedComponentId '{0}'", id);
            return;
        }

        string tempFolder = parseResult.GetValue(Options.TempPath) ?? string.Empty;
        string salt = parseResult.GetValue(Options.Salt) ?? string.Empty;

        Backup backup = _backupService.BackupApplication(application, tempFolder, salt);

        Console.WriteLine("Successfully created backup '{0}'", backup.File.SourcePath);
    }
}