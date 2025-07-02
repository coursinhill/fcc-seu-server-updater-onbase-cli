using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Handlers
{
    internal sealed class RollbackHandler
    {
        private readonly IRepository<Application> _applicationRepo;
        private readonly IBackupApplicationService _backupApplicationService;

        public RollbackHandler(IRepository<Application> applicationRepo, IBackupApplicationService backupApplicationService)
        {
            _applicationRepo = applicationRepo;
            _backupApplicationService = backupApplicationService;
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
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error rolling back application.");
                Console.WriteLine(e);
            }
        }
    }
}
