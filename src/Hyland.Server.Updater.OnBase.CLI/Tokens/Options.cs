using System.CommandLine;

namespace Hyland.Server.Updater.OnBase.CLI.Tokens
{
    internal static class Options
    {
        public static Option<string> BackupFile = new Option<string>("--backupFile", ["-b"])
        {
            Description = "The full path to the backup file",
            Required = true
        };

        public static Option<string> Id = new Option<string>("--id", ["-i"])
        {
            Description = "The Managed Component ID of the desired Application",
            Required = true
        };

        public static Option<string> NugetPath = new Option<string>("--nugetPath", ["-n"])
        {
            Description = "The directory for Nuget files",
            Required = true
        };

        public static Option<string> TempPath = new Option<string>("--tempPath", ["-t"])
        {
            Description = "The temp directory for Backups",
            Required = true
        };

        public static Option<string> Version = new Option<string>("--version", ["-v"])
        {
            Description = "The version of the package to use",
            Required = false,
            DefaultValueFactory = _ => string.Empty
        };
    }
}
