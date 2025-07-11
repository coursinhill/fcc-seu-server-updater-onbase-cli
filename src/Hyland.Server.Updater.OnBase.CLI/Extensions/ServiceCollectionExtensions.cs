using Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyland.Server.Updater.OnBase.CLI.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCliCommandHandlers(this IServiceCollection collection)
        {
            collection.AddSingleton<ICommandWrapper, ListCommand>();
            collection.AddSingleton<ICommandWrapper, StartCommand>();
            collection.AddSingleton<ICommandWrapper, StopCommand>();

            collection.AddSingleton<ICommandWrapper, UpdateCommand>();
            collection.AddSingleton<ISubCommandWrapper, StageUpdateCommand>();
            collection.AddSingleton<ISubCommandWrapper, VerifyUpdateCommand>();

            collection.AddSingleton<ICommandWrapper, BackupCommand>();
            collection.AddSingleton<ISubCommandWrapper, ValidateBackupCommand>();

            collection.AddSingleton<ICommandWrapper, RollbackCommand>();

            collection.AddSingleton<ICommandWrapper, StateCommand>();
            collection.AddSingleton<ISubCommandWrapper, ValidateStateCommand>();

            return collection;
        }
    }
}
