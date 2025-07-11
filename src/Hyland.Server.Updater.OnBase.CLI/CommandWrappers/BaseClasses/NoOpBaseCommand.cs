using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal abstract class NoOpBaseCommand : CommandWrapperBase
{
    protected override IReadOnlyList<Option> CommandOptions => [];

    public NoOpBaseCommand(IEnumerable<ISubCommandWrapper> subCommands)
        : base(subCommands)
    { }

    protected override void ExecuteInternal(ParseResult parseResult)
    {
        Console.WriteLine("Please specify a subcommand. Options are: {0}", string.Join(',', Command.Subcommands.Select(s => s.Name)));
    }
}