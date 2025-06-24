using Hyland.Server.Updater.Interfaces.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Handlers
{
    internal sealed class StopHandler
    {
        private readonly IApplicationManipulationService _manipulationService;

        public StopHandler(IApplicationManipulationService mainpulationService)
        {
            _manipulationService = mainpulationService;
        }

        public void HandleRequest(ParseResult parseResult)
        {
            try
            {
                string id = parseResult.GetValue(Options.Id) ?? string.Empty;

                Console.WriteLine("Attempting to stop service with id '{0}'", id);

                _manipulationService.StopApplication(id);

                Console.WriteLine("Stopped Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error stopping application");
                Console.WriteLine(e);
            }
        }
    }
}
