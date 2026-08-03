# Security policy

## Security and trust

The Factorio module executes C# with the current user's Windows permissions and starts the vendor server executable. WindowsGSH cannot guarantee arbitrary third-party modules. Review this repository, its manifest, and download origins before use.

## Download modules safely

Use the official [WindowsGSH.Factorio repository](https://github.com/WindowsGSH/WindowsGSH.Factorio) or an independently verified source, and review its manifest and executable code before installing.

## Protect credentials and server data

This module does not collect or write Factorio account credentials. If your vendor `data/server-settings.json` contains a game/authentication token or admin list, restrict filesystem access to it and never post it, along with save files, logs, or backups, in issues or support requests.

## Report a vulnerability

Use the [private repository advisory page](https://github.com/WindowsGSH/WindowsGSH.Factorio/security/advisories/new) or contact maintainers privately. Do not publicly disclose an unpatched issue or credential.

## Include in a report

Include the module and WindowsGSH versions, affected workflow, reproduction steps, impact, and the smallest redacted diagnostic sample needed to reproduce the issue.

## Supported versions

Security fixes target the latest module release and current WindowsGSH module API unless stated otherwise.
