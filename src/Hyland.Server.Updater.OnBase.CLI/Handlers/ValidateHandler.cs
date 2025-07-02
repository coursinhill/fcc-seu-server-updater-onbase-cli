using Hyland.Server.Updater.Interfaces.Interfaces;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Handlers
{
    internal sealed class ValidateHandler
    {
        private readonly IBackupApplicationService _backupService;

        public ValidateHandler(IBackupApplicationService backupService)
        {
            _backupService = backupService;
        }

        public void HandleRequest(ParseResult parseResult)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error creating backup.");
                Console.WriteLine(e);
            }
        }
    }
}
