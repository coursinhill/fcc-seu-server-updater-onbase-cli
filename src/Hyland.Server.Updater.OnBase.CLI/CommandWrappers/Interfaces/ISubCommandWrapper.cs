using Hyland.Server.Updater.OnBase.CLI.Enumerations;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal interface ISubCommandWrapper : ICommandWrapper
{
    CommandType BaseCommand { get; }
}