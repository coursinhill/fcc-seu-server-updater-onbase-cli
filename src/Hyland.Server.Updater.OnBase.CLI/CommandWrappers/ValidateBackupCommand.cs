using Hyland.Server.Updater.Interfaces.Interfaces;
using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class ValidateBackupCommand : CommandWrapperBase, ISubCommandWrapper
{
    public CommandType BaseCommand => CommandType.Backup;

    protected override string Name => "validate";

    protected override string Description => "Validate the specified backup";

    protected override IReadOnlyList<Option> CommandOptions => [Options.BackupFile, Options.Salt];

    private readonly IBackupApplicationService _backupService;

    public ValidateBackupCommand(IBackupApplicationService backupService)
    {
        _backupService = backupService;
    }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        string backupFile = parseResult.GetValue(Options.BackupFile) ?? string.Empty;
        string salt = parseResult.GetValue(Options.Salt) ?? string.Empty;

        bool result = _backupService.ValidateBackup(backupFile, salt);

        if (result)
        {
            Console.WriteLine("Backup Validation Successful.");
        }
        else
        {
            Console.WriteLine("Backup Validation Failed.");
        }
    }
}