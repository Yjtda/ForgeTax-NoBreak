# ForgeTax-NoBreak

两拳打穿概率论，从此我是铸剑神

## v1.0.0 安装包

直接下载：[ForgeTax-NoBreak-v1.0.0-install.zip](../../raw/refs/heads/main/dist/ForgeTax-NoBreak-v1.0.0-install.zip)

This package prevents sword destruction in ForgeTax by converting destruction
tickets and destruction outcomes into damage.

## Quick installation

1. Close ForgeTax.
2. Extract this package anywhere.
3. Run `Install-ForgeTaxNoBreak.ps1` and enter the folder that contains
   `ForgeTax.exe` when prompted.
4. Start ForgeTax normally through Steam.

The installer preserves an existing BepInEx installation. If BepInEx is not
already installed, it installs the included BepInEx 5.4.23.3 bootstrap files.

## Manual installation

Copy everything inside `payload` into the ForgeTax game folder, preserving the
folder structure. The game folder is normally:

`SteamLibrary\steamapps\common\ForgeTax`

## Toggle

After the first launch, edit:

`ForgeTax\BepInEx\config\jacky.forgetax.nobreak.cfg`

Set `EnableNoBreak = true` or `EnableNoBreak = false`, then restart the game.

## Uninstall

Delete only:

`ForgeTax\BepInEx\plugins\ForgeTaxNoBreak.dll`

The package does not modify `Assembly-CSharp.dll`. If the installer added
BepInEx solely for this plugin, the bootstrap files can be removed after the
plugin has been deleted; keep them if other BepInEx plugins use them.

## Verification

The loader writes to `ForgeTax\BepInEx\LogOutput.log`. A converted outcome is
recorded as:

`Converted a destruction outcome to damage.`

