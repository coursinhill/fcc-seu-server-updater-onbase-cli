using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces.Repositories;
using Hyland.Server.Updater.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Handlers
{
    internal sealed class VerifyHandler
    {
        private readonly IRepository<Application> _applicationRepo;
        private readonly IUpdateService _updateService;

        public VerifyHandler(IRepository<Application> applicationRepo, IUpdateService updateService)
        {
            _applicationRepo = applicationRepo;
            _updateService = updateService;
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

                bool result = _updateService.VerifyUpdate(application);

                if (result)
                {
                    Console.WriteLine("Successfully verified update.");
                }
                else
                {
                    Console.WriteLine("Update verification failed.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error verifying update.");
                Console.WriteLine(e);
            }
        }
    }
}
