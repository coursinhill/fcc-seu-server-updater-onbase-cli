using Hyland.Server.Updater.OnBase.CLI.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyland.Server.Updater.OnBase.CLI.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCliCommandHandlers(this IServiceCollection collection)
        {
            collection.AddSingleton<ListHandler>();
            collection.AddSingleton<StartHandler>();
            collection.AddSingleton<StopHandler>();
            collection.AddSingleton<StageHandler>();
            collection.AddSingleton<UpdateHandler>();
            collection.AddSingleton<BackupHandler>();
            collection.AddSingleton<VerifyHandler>();
            collection.AddSingleton<RollbackHandler>();
            collection.AddSingleton<ValidateHandler>();

            return collection;
        }
    }
}
