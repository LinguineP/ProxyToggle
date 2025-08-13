[Setup]
AppName=ProxyToggle
AppVersion=1.0
DefaultDirName={pf}\ProxyToggle
DefaultGroupName=ProxyToggle
UninstallDisplayIcon={app}\ProxyToggle.exe
OutputBaseFilename=ProxyToggleSetup


[Files]
Source: "bin\Release\net8.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{userstartup}\\ProxyToggle"; Filename: "{app}\\ProxyToggle.exe"
Name: "{group}\ProxyToggle"; Filename: "{app}\ProxyToggle.exe"

[Run]
Filename: "{app}\\ProxyToggle.exe"; Description: "Run ProxyToggle"; Flags: postinstall skipifsilent

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "ProxyToggle"; ValueData: """{app}\ProxyToggle.exe"""; Flags: uninsdeletevalue

