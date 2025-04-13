[Setup]
AppName=Phase
AppVersion=0.09
DefaultDirName={userdocs}\Phase Launcher
DefaultGroupName=Phase Launcher
OutputDir=..\Output
OutputBaseFilename=PhaseLauncher_0.02_x64_en-US
Compression=lzma
SolidCompression=yes
PrivilegesRequired=lowest
DisableDirPage=no
DisableProgramGroupPage=yes
DisableReadyPage=yes
DisableWelcomePage=yes
CloseApplications=yes

[Files]
Source: "..\Legacy\bin\release\net8.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Excludes: "..\Bundle\*"
Source: "..\Legacy\Resources\*"; DestDir: "{app}\Resources"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{userdesktop}\Phase Launcher"; Filename: "{app}\Phase.exe"; WorkingDir: "{app}"; IconFilename: "{app}\Resources\legacy.ico"; IconIndex: 0
Name: "{group}\Phase Launcher"; Filename: "{app}\Phase.exe"; WorkingDir: "{app}"; IconFilename: "{app}\Resources\legacy.ico"; IconIndex: 0

[Run]
Filename: "{app}\Phase.exe"; Description: "Launch Phase"; Flags: nowait postinstall skipifsilent
