# ForgeTax-NoBreak

两拳打穿概率论，从此我是铸剑神。

## v1.0.0 安装包

直接下载：

[ForgeTax-NoBreak-v1.0.0-install.zip](https://github.com/Yjtda/ForgeTax-NoBreak/raw/refs/heads/main/dist/ForgeTax-NoBreak-v1.0.0-install.zip)

SHA-256 校验值：

`CAB8ABC7C31D8FBBE0AFDA4C5680F360DEDBED23CBDDB7CF5410A61F84E37685`

## 功能

这个 Mod 会把游戏中的“破坏”结果转换为“损坏”结果：

- 破坏票会转移到损坏票；
- 最终结算时再次拦截破坏结果；
- 游戏内显示的破坏概率会变为 0；
- 不修改原始 `Assembly-CSharp.dll`；
- 支持通过配置文件随时关闭。

## 自动安装

1. 关闭 ForgeTax。
2. 下载并解压上面的 ZIP 安装包。
3. 运行 `Install-ForgeTaxNoBreak.ps1`。
4. 输入包含 `ForgeTax.exe` 的游戏目录。
5. 通过 Steam 启动游戏。

通常的游戏目录是：

`SteamLibrary\\steamapps\\common\\ForgeTax`

如果电脑中已经安装了 BepInEx，安装脚本会保留已有的加载器文件，只安装本 Mod。

## 手动安装

将安装包中的 `payload` 文件夹内容复制到 ForgeTax 游戏目录，并保持文件夹结构：

```text
ForgeTax/
├─ winhttp.dll
├─ doorstop_config.ini
└─ BepInEx/
   ├─ core/
   └─ plugins/
      └─ ForgeTaxNoBreak.dll
```

然后重新启动游戏。

## 开关

首次启动游戏后，打开：

`ForgeTax\\BepInEx\\config\\jacky.forgetax.nobreak.cfg`

启用：

```ini
EnableNoBreak = true
```

关闭：

```ini
EnableNoBreak = false
```

修改后需要重启游戏。

## 卸载

只需删除：

`ForgeTax\\BepInEx\\plugins\\ForgeTaxNoBreak.dll`

本 Mod 不会修改原始游戏程序集。如果 BepInEx 只用于这个 Mod，也可以在删除插件后移除 BepInEx；如果还有其他 BepInEx Mod，请保留加载器。

## 验证

日志文件位置：

`ForgeTax\\BepInEx\\LogOutput.log`

当游戏抽到原本会导致破坏的结果时，日志会出现：

```text
Converted a destruction outcome to damage.
```

## 文件

- [安装包](https://github.com/Yjtda/ForgeTax-NoBreak/raw/refs/heads/main/dist/ForgeTax-NoBreak-v1.0.0-install.zip)
- [自动安装脚本](https://github.com/Yjtda/ForgeTax-NoBreak/blob/main/Install-ForgeTaxNoBreak.ps1)
- [Mod 源码](https://github.com/Yjtda/ForgeTax-NoBreak/blob/main/src/ForgeTaxNoBreak.cs)
- [SHA-256 校验值](https://github.com/Yjtda/ForgeTax-NoBreak/blob/main/SHA256SUMS.txt)
