using Hyland.Server.Updater.OnBase.CLI.Enumerations;
using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal abstract class CommandWrapperBase : ICommandWrapper
{
    protected abstract string Name { get; }
    protected abstract string Description { get; }
    public Command Command { get; }
    protected abstract IReadOnlyList<Option> CommandOptions { get; }

    public CommandWrapperBase()
    {
        Command = new Command(Name, Description);
        
        foreach (Option option in CommandOptions)
        {
            Command.Add(option);
        }

        Command.SetAction(Execute);
    }

    public CommandWrapperBase(CommandType type, IEnumerable<ISubCommandWrapper> subCommands)
        : this()
    {
        foreach (Command command in subCommands.Where(s => s.BaseCommand == type).Select(s => s.Command))
        {
            Command.Add(command);
        }
    }

    public void Execute(ParseResult parseResult)
    {
        try
        {
            ExecuteInternal(parseResult);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected exception while executing {Name}");
            Console.WriteLine(e);
        }
    }

    protected abstract void ExecuteInternal(ParseResult parseResult);
}