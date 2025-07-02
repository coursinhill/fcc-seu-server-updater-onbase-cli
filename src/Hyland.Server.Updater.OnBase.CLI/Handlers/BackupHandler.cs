using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Handlers
{
    internal sealed class BackupHandler
    {
        private readonly IRepository<Application> _applicationRepo;
        private readonly IBackupApplicationService _backupService;

        public BackupHandler(IRepository<Application> applicationRepo, IBackupApplicationService backupService)
        {
            _applicationRepo = applicationRepo;
            _backupService = backupService;
        }

        public void HandleRequest(ParseResult parseResult)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error creating backup.");
                Console.WriteLine(e);
            }
        }
    }
}
