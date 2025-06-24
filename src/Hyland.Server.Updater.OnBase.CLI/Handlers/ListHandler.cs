using Hyland.Server.Updater.Interfaces.Entities;
using Hyland.Server.Updater.Interfaces.Interfaces;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Handlers
{
    internal sealed class ListHandler
    {
        private readonly ICombinedApplicationsGenerationService _appService;
        public ListHandler(ICombinedApplicationsGenerationService applicationService)
        {
            _appService = applicationService;
        }

        public void HandleRequest(ParseResult parseResult)
        {
            IEnumerable<Application> applications = _appService.GetCombinedApplications();

            Console.WriteLine("The following applications are installed");

            foreach (Application app in applications.OrderBy(a => a.AppType.ToString()).ThenBy(a => a.RootPath))
            {
                Console.WriteLine("Application: {0}", app.AppType);
                Console.WriteLine("\tPath: {0}", app.RootPath);
                Console.WriteLine("\tManagedComponentId: {0}", app.ManagedComponentId);
            }

            Console.WriteLine();
        }
    }
}
