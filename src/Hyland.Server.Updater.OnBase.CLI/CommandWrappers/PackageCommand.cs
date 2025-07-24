using Hyland.Server.Updater.OnBase.CLI.Enumerations;

namespace Hyland.Server.Updater.OnBase.CLI.CommandWrappers;
internal class PackageCommand : NoOpBaseCommand
{
    protected override string Name => "package";

    protected override string Description => "Commands for interacting with nuget packages";

    protected override CommandType CommandType => CommandType.Package;

    public PackageCommand(IEnumerable<ISubCommandWrapper> subCommands)
        : base(subCommands)
    { }
}