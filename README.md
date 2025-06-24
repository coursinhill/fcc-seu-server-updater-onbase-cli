# Hyland.Server.Updater.OnBase.CLI

```
Description:
  CLI tool to test the OnBase Server Updater

Usage:
  [command] [options]

Options:
  -?, -h, --help  Show help and usage information
  --version       Show version information

Commands:
  backup    Create a backup of the application with the supplied ManagedComponentId
  list      List all applications found by the Server Updater
  rollback  Roll back the application with the supplied ManagedComponentId with the specified backup
  stage     Stage an update for the application with the supplied ManagedComponentId
  start     Start the application with the supplied ManagedComponentId
  stop      Stop the application with the supplied ManagedComponentId
  update    Install an update for the application with the supplied ManagedComponentId
  verify    Verify the installation of application with the supplied ManagedComponentId
```

## Backup

```
Description:
  Create a backup of the application with the supplied ManagedComponentId

Usage:
  backup [options]

Options:
  -i, --id (REQUIRED)        The Managed Component ID of the desired Application
  -t, --tempPath (REQUIRED)  The temp directory for Backups
  -?, -h, --help             Show help and usage information
```

## List

```
Description:
  List all applications found by the Server Updater

Usage:
  list [options]

Options:
  -?, -h, --help  Show help and usage information
```

## Rollback

```
Description:
  Roll back the application with the supplied ManagedComponentId with the specified backup

Usage:
  rollback [options]

Options:
  -i, --id (REQUIRED)          The Managed Component ID of the desired Application
  -b, --backupFile (REQUIRED)  The full path to the backup file
  -?, -h, --help               Show help and usage information
```

## Stage

```
Description:
  Stage an update for the application with the supplied ManagedComponentId

Usage:
  stage [options]

Options:
  -i, --id (REQUIRED)         The Managed Component ID of the desired Application
  -n, --nugetPath (REQUIRED)  The directory for Nuget files
  -v, --version               The version of the package to use [default is latest]
  -?, -h, --help              Show help and usage information
```

## Start

```
Description:
  Start the application with the supplied ManagedComponentId

Usage:
  start [options]

Options:
  -i, --id (REQUIRED)  The Managed Component ID of the desired Application
  -?, -h, --help       Show help and usage information
```

## Stop

```
Description:
  Stop the application with the supplied ManagedComponentId

Usage:
  stop [options]

Options:
  -i, --id (REQUIRED)  The Managed Component ID of the desired Application
  -?, -h, --help       Show help and usage information
```

## Update

```
Description:
  Install an update for the application with the supplied ManagedComponentId

Usage:
  update [options]

Options:
  -i, --id (REQUIRED)         The Managed Component ID of the desired Application
  -n, --nugetPath (REQUIRED)  The directory for Nuget files
  -v, --version               The version of the package to use [default is latest]
  -?, -h, --help              Show help and usage information
```

## Verify

```
Description:
  Verify the installation of application with the supplied ManagedComponentId

Usage:
  verify [options]

Options:
  -i, --id (REQUIRED)  The Managed Component ID of the desired Application
  -?, -h, --help       Show help and usage information
```