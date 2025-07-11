using Hyland.Server.Updater.OnBase.CLI.Enumerations;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers
{
    internal class StateCommand : NoOpBaseCommand
    {
        protected override string Name => "state";

        protected override string Description => "Commands related to the state stored within the updater package";

        public StateCommand(IEnumerable<ISubCommandWrapper> subCommands)
            : base(CommandType.State, subCommands)
        {}
    }
}
