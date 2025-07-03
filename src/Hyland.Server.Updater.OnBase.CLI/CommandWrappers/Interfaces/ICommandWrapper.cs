using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal interface ICommandWrapper
{
    Command Command { get; }

    void Execute(ParseResult parseResult);
}