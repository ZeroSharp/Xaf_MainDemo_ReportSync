:: Requires the Debug webserver to be running on port 49660
:: Requires EasyTests to be enabled
:: Requires NetDA to be running
:: Requires admin rights
:: Must be run from a command prompt
::
:: Usage: > launch <numberOfBrowsers>
:: e.g. : > launch 21
:: will launch 21 simultaneous browsers at 3 second intervals

@echo off

:DELETE_OUTPUT
if exist *.jpeg del *.jpeg
if exist *.html del *.html
if exist TestsLog.xml del TestsLog.xml

:CHECK_ADMIN
net session >nul 2>&1
if %ERRORLEVEL% equ 0 goto CHECK_CONSOLE
echo Must be run from an administrative command window
goto ERROR

:CHECK_CONSOLE
echo %CMDCMDLINE% | find /i "/c" >nul
if ERRORLEVEL 1 goto CHECK_PARAMS
echo Must be run from an administrative console (not Windows Explorer)
goto ERROR

:CHECK_PARAMS
IF [%1]==[] GOTO USAGE

:LAUNCH
set /a i=0

:LOOP
if %i%==%1 goto OK
set /a i=%i%+1
start "x" "C:\Program Files (x86)\DevExpress\DXperience 12.2\Tools\eXpressAppFramework\EasyTest\TestExecutor.v12.2.exe" MainDemo_CycleThroughTabs.ets
:: Wait 3 seconds
ping 1.1.1.1 -n 1 -w 3000 >nul
goto LOOP

:USAGE
echo Usage: %0 numberOfBrowsers
echo numberOfBrowsers must be an integer
goto OK

:ERROR

:OK
pause