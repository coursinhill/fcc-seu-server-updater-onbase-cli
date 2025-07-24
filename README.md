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
  package   Commands for interacting with nuget packages
  rollback  Roll back the application with the supplied ManagedComponentId with the specified backup
  start     Start the application with the supplied ManagedComponentId
  state     Commands related to the state stored within the updater package
  stop      Stop the application with the supplied ManagedComponentId
  update    Install an update for the application with the supplied ManagedComponentId
```

## Backup

```
Description:
  Create a backup of the application with the supplied ManagedComponentId

Usage:
  backup [command] [options]

Options:
  -i, --id (REQUIRED)        The Managed Component ID of the desired Application
  -t, --tempPath (REQUIRED)  The temp directory for Backups
  -s, --salt                 A secret value used to generate and validate the checksum for the backup package
  -?, -h, --help             Show help and usage information

Commands:
  validate  Validate the specified backup
```

### Validate

```
Description:
  Validate the specified backup

Usage:
  backup validate [options]

Options:
  -b, --backupFile (REQUIRED)  The full path to the backup file
  -s, --salt                   A secret value used to generate and validate the checksum for the backup package
  -?, -h, --help               Show help and usage information
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

## Package

```
Description:
  Commands for interacting with nuget packages

Usage:
  package [command] [options]

Options:
  -?, -h, --help  Show help and usage information

Commands:
  delete   Delete the specified package
  find     Find the package with the specified version, or latest if not supplied
  install  Install the specified package
```

### Delete

```
Description:
  Delete the specified package

Usage:
  package delete [options]

Options:
  -i, --id (REQUIRED)         The Managed Component ID of the desired Application
  -n, --nugetPath (REQUIRED)  The directory for Nuget files
  -v, --version               The version of the package to use [default is latest]
  -?, -h, --help              Show help and usage information
```

### Find

```
Description:
  Find the package with the specified version, or latest if not supplied

Usage:
  package find [options]

Options:
  -i, --id (REQUIRED)         The Managed Component ID of the desired Application
  -n, --nugetPath (REQUIRED)  The directory for Nuget files
  -v, --version               The version of the package to use [default is latest]
  -?, -h, --help              Show help and usage information
```

### Install

```
Description:
  Install the specified package

Usage:
  package install [options]

Options:
  -i, --id (REQUIRED)         The Managed Component ID of the desired Application
  -n, --nugetPath (REQUIRED)  The directory for Nuget files
  -v, --version               The version of the package to use [default is latest]
  -?, -h, --help              Show help and usage information
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
  -s, --salt                   A secret value used to generate and validate the checksum for the backup package
  -?, -h, --help               Show help and usage information
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

## State

```
Description:
  Commands related to the state stored within the updater package

Usage:
  state [command] [options]

Options:
  -?, -h, --help  Show help and usage information

Commands:
  validate  Validate that the current state is the same as when stop was called. This will invalidate the state.
```

### Validate

```
Description:
  Validate that the current state is the same as when stop was called. This will invalidate the state.

Usage:
  state validate [options]

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
  update [command] [options]

Options:
  -i, --id (REQUIRED)         The Managed Component ID of the desired Application
  -n, --nugetPath (REQUIRED)  The directory for Nuget files
  -v, --version               The version of the package to use [default is latest]
  -?, -h, --help              Show help and usage information

Commands:
  stage   Stage an update for the application with the supplied ManagedComponentId
  verify  Verify the installation of application with the supplied ManagedComponentId
```

### Stage

```
Description:
  Stage an update for the application with the supplied ManagedComponentId

Usage:
  update stage [options]

Options:
  -i, --id (REQUIRED)         The Managed Component ID of the desired Application
  -n, --nugetPath (REQUIRED)  The directory for Nuget files
  -v, --version               The version of the package to use [default is latest]
  -?, -h, --help              Show help and usage information
```

### Verify

```
Description:
  Verify the installation of application with the supplied ManagedComponentId

Usage:
  update verify [options]

Options:
  -i, --id (REQUIRED)  The Managed Component ID of the desired Application
  -?, -h, --help       Show help and usage information
```