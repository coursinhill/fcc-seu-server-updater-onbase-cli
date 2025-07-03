using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.Interfaces.Interfaces;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;
using Hyland.Server.Updater.Interfaces.Entities;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class RollbackCommand : CommandWrapperBase
{
    protected override string Name => "rollback";

    protected override string Description => "Roll back the application with the supplied ManagedComponentId with the specified backup";

    protected override IReadOnlyList<Option> CommandOptions => [Options.Id, Options.BackupFile, Options.Salt];

    private readonly IRepository<Application> _applicationRepo;
    private readonly IBackupApplicationService _backupApplicationService;

    public RollbackCommand(IRepository<Application> applicationRepo, IBackupApplicationService backupApplicationService)
    {
        _applicationRepo = applicationRepo;
        _backupApplicationService = backupApplicationService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string id = parseResult.GetValue(Options.Id) ?? string.Empty;

        if (!_applicationRepo.TryGet(id, out Application? application))
        {
            Console.WriteLine("Unable to find Application with ManagedComponentId '{0}'", id);
            return;
        }

        string backupFile = parseResult.GetValue(Options.BackupFile) ?? string.Empty;
        string salt = parseResult.GetValue(Options.Salt) ?? string.Empty;

        PhysicalFile file = new PhysicalFile()
        {
            RelativePath = Path.GetFileName(backupFile) ?? string.Empty,
            SourcePath = backupFile
        };

        Backup backup = _backupApplicationService.RetrieveBackup(file, application);

        _backupApplicationService.RestoreApplication(application, backup, salt);

        Console.WriteLine("Successfully rolled back application.");
    }
}