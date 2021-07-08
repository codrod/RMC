set Version=1.2
set ConfigurationName=%1
set ProjectDir=%2

xcopy /yi .\RMC*.dll "RimWorld Military Colony\%Version%\Assemblies"
xcopy /yei "%ProjectDir%\Assets\XML" "RimWorld Military Colony\%Version%"
xcopy /yei "%ProjectDir%\Assets\About" "RimWorld Military Colony\About"

REM If in debug then deploy to local rimworld install
if "%ConfigurationName%" == "Debug" (
call "%ProjectDir%\local-settings.bat"
del /s /q "%RimWorldDir%\RimWorld Military Colony"
xcopy /yei "RimWorld Military Colony" "%RimWorldDir%\RimWorld Military Colony"
xcopy /yei "..\..\Assets\Scenarios" "%userprofile%\AppData\LocalLow\Ludeon Studios\RimWorld by Ludeon Studios\Scenarios"
)

REM If in release then create a package
if "%ConfigurationName%" == "Release" (
powershell Compress-Archive -Force 'RimWorld Military Colony' 'RimWorld Military Colony.zip'
)
