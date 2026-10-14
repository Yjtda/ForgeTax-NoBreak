@echo off
setlocal
title ForgeTax No Break 1.1.0 Installer

echo ForgeTax No Break 1.1.0
echo Installing from this folder into the ForgeTax game directory...
echo.

powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0Install-ForgeTaxNoBreak.ps1"
if errorlevel 1 (
    echo.
    echo [ERROR] Installation failed. Please check the message above.
) else (
    echo.
    echo [OK] Installation completed. Start ForgeTax through Steam.
)

echo.
pause

