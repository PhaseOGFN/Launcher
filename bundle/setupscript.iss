[Setup]
AppName=Legacy
AppVersion=0.09
DefaultDirName={userdocs}\Legacy Launcher
DefaultGroupName=Legacy Launcher
OutputDir=..\Output
OutputBaseFilename=LegacyLauncher_0.09_x64_en-US
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
Name: "{userdesktop}\Legacy Launcher"; Filename: "{app}\Legacy.exe"; WorkingDir: "{app}"; IconFilename: "{app}\Resources\legacy.ico"; IconIndex: 0
Name: "{group}\Legacy Launcher"; Filename: "{app}\Legacy.exe"; WorkingDir: "{app}"; IconFilename: "{app}\Resources\legacy.ico"; IconIndex: 0

[Run]
Filename: "{app}\Legacy.exe"; Description: "Launch Legacy"; Flags: nowait postinstall skipifsilent
