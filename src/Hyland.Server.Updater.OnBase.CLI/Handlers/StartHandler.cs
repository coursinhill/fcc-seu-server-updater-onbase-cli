using Hyland.Server.Updater.Interfaces.Interfaces.Services;
using Hyland.Server.Updater.OnBase.CLI.Tokens;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Handlers
{
    internal sealed class StartHandler
    {
        private readonly IApplicationManipulationService _manipulationService;

        public StartHandler(IApplicationManipulationService mainpulationService)
        {
            _manipulationService = mainpulationService;
        }

        public void HandleRequest(ParseResult parseResult)
        {
            try
            {
                string id = parseResult.GetValue(Options.Id) ?? string.Empty;

                Console.WriteLine("Attempting to start service with id '{0}'", id);

                _manipulationService.StartApplication(id);

                Console.WriteLine("Started Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error starting application");
                Console.WriteLine(e);
            }
        }
    }
}
