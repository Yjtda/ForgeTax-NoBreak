# ForgeTax-NoBreak

两拳打穿概率论，从此我是铸剑神。

ForgeTax No Break 会把铸剑过程中的“破坏”结果转换为“损坏”，因此不会直接
摧毁正在锻造的剑。默认开启，也可以随时在配置文件中关闭。

## v1.1.0：解压到游戏目录后一键安装

1. 关闭 ForgeTax。
2. 下载并解压 `ForgeTax-NoBreak-v1.1.0-install.zip`。
3. 把解压出来的所有文件直接放进包含 `ForgeTax.exe` 的游戏目录。
4. 双击运行 `Install-ForgeTaxNoBreak.bat`。
5. 看到 `[OK] Installation completed` 后，通过 Steam 启动游戏。

通常的游戏目录是：

`SteamLibrary\steamapps\common\ForgeTax`

安装器会自动使用自身所在目录，不再要求手动输入路径。它会保留已有的
BepInEx 引导文件；如果游戏还没有 BepInEx，则会自动安装随包提供的
BepInEx 5.4.23.3 文件。

## 下载与校验

- [下载 v1.1.0 安装包](https://github.com/Yjtda/ForgeTax-NoBreak/releases/download/v1.1.0/ForgeTax-NoBreak-v1.1.0-install.zip)
- SHA-256：请查看 GitHub Release 页面中的 `SHA256SUMS.txt`

## 开关

首次启动游戏后，编辑：

`BepInEx\config\jacky.forgetax.nobreak.cfg`

设置：

```ini
EnableNoBreak = true
```

设为 `false` 可以关闭本 Mod。修改后重启游戏。

## 卸载

关闭游戏后，删除：

`BepInEx\plugins\ForgeTaxNoBreak.dll`

本 Mod 不会修改 `ForgeTax_Data\Managed\Assembly-CSharp.dll`。如果该游戏目录
中的 BepInEx 只为本 Mod 安装，也可以在删除插件后移除 BepInEx 引导文件；如果
还有其他 BepInEx Mod，请保留它们。

## 验证是否生效

查看：

`BepInEx\LogOutput.log`

启动时应能看到：

`ForgeTax No Break loaded: destruction is converted to damage.`

触发转换时还会记录：

`Converted a destruction outcome to damage.`

## 手动安装

如果不想运行一键安装器，也可以把 `payload` 文件夹内的内容按原目录结构
复制到游戏目录。一般用户直接运行 `Install-ForgeTaxNoBreak.bat` 即可。

