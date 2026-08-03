# Factorio Dedicated Server

[![WindowsGSH](.github/assets/windowsgsh-badge.svg)](https://windowsgsh.com)
[![Status](https://img.shields.io/badge/status-needs_live_test-F59E0B)](#status)
[![Module version](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FWindowsGSH%2FWindowsGSH.Factorio%2Fmain%2FFactorio.mod%2Fmodule.json&query=%24.version&prefix=v&label=module&color=0F766E)](Factorio.mod/module.json)
[![Requires WindowsGSH](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FWindowsGSH%2FWindowsGSH.Factorio%2Fmain%2FFactorio.mod%2Fmodule.json%3Fbadge%3Dminimum&query=%24.minimumWindowsGshVersion&prefix=v&label=requires%20WindowsGSH&color=2563EB)](Factorio.mod/module.json)
[![Licence](https://img.shields.io/badge/licence-MIT-64748B)](LICENSE.md)

This module launches, monitors, and backs up Factorio dedicated servers.

## Status

**NEEDS LIVE TEST.** Executable resolution, launch arguments, and backup paths pass static host validation. End-to-end behavior requires a current live server.

## Installation

WindowsGSH does not download Factorio. Install or copy the authenticated official Windows Factorio package into the server folder yourself, then point WindowsGSH at it; the module launches `bin/x64/factorio.exe` directly.

## Configuration

WindowsGSH starts the server with `--start-server saves/<Save File Name>` (default `world.zip`). If a vendor `data/server-settings.json` file already exists in the install, it is passed with `--server-settings`; WindowsGSH does not create or modify that file.

## Networking

| Purpose | Default | Protocol | Exposure |
| --- | ---: | --- | --- |
| Game traffic | `34197` | UDP | Public; firewall/UPnP eligible |

## Query, console, and administration

WindowsGSH reports supervised process state. Factorio's native RCON protocol is not implemented by this module and is not exposed through WindowsGSH.

## Files and backups

- Executable: `bin/x64/factorio.exe`
- Save file: `saves/<Save File Name>`
- Vendor settings (optional): `data/server-settings.json`
- Backup target: `saves` directory

## Known limitations

- The configured Game Port is declared to WindowsGSH for host/firewall purposes but is not currently passed to Factorio as a `--port` argument or written into `server-settings.json`; the server uses Factorio's own default (`34197`) unless the vendor settings file specifies otherwise.
- RCON is not implemented, even though Factorio supports it natively.
- Installation is manual; WindowsGSH cannot verify the package is the authenticated official build.

## Beta verification checklist

- [ ] Install an authenticated official Factorio package and verify `bin/x64/factorio.exe`.
- [ ] Confirm the configured save file loads and an existing `server-settings.json` is honored.
- [ ] Start, attach, restart WindowsGSH, stop, and confirm the save is preserved.
- [ ] Verify direct connection and player join on the actual listening port.
- [ ] Test direct and WindowsGSM `serverfiles` imports using Copy and Adopt.
- [ ] Back up and restore the `saves` directory.

## Support

Report issues through the [issue tracker](https://github.com/WindowsGSH/WindowsGSH.Factorio/issues) with sanitized version and log details. Never post credentials or private save files.

## Support development

If you like the work I do and would like to support continued WindowsGSH and module development, you can contribute here:

- [Ko-fi](https://ko-fi.com/shenniko)
- [PayPal](https://paypal.me/shenniko)

## Trust and source

Modules execute with WindowsGSH's Windows permissions. Review [`FactorioModule.cs`](Factorio.mod/FactorioModule.cs), [`module.json`](Factorio.mod/module.json), and [SECURITY.md](SECURITY.md) before installing. Obtain Factorio server files only through your authenticated official Factorio account.
