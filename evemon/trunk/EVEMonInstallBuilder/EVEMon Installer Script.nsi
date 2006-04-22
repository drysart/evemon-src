#
#  This is an NSIS Installer build script
#  for NSIS 2.16
#
!include "Library.nsh"
!include "FileFunc.nsh"
!include "LogicLib.nsh"
!include "MUI.nsh"
# ## !define VERSION "1.0.19.0"

Name "EVEMon"
OutFile "${OUTDIR}\EVEMon-install-${VERSION}.exe"
InstallDir "$PROGRAMFILES\EVEMon\"
InstallDirRegKey HKLM "Software\EVEMon" ""

VIAddVersionKey "ProductName" "EVEMon Installer"
VIAddVersionKey "CompanyName" "evercrest.com"
VIAddVersionKey "LegalCopyright" "Copyright 2006, Timothy Fries"
VIAddVersionKey "FileDescription" "Installs EVEMon on your computer"
VIAddVersionKey "FileVersion" "${VERSION}"
VIProductVersion ${VERSION}

Var STARTMENU_FOLDER
Var MUI_TEMP

!define MUI_ABORTWARNING
!define MUI_ICON "..\eve.exe_I006b_040f.ico"
!define MUI_UNICON "..\eve.exe_I006b_040f.ico"

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY

# Start menu folder page configuration
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKLM"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\EVEMon"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
!insertmacro MUI_PAGE_STARTMENU "EVEMon" $STARTMENU_FOLDER
#-------------------------------------

!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

!include "NETFrameworkCheck.nsh"

Section "Installer Section"
  SetOutPath "$INSTDIR"
  File /r /x *vshost* "..\bin\Release\*.*" 
  File "..\eve.exe_I006b_040f.ico"
  CreateDirectory "$INSTDIR\Debugging Tools"
  SetOutPath "$INSTDIR\Debugging Tools"
  CreateShortCut "$INSTDIR\Debugging Tools\EVEMon (with network logging).lnk" "$INSTDIR\EVEMon.exe" "-netlog"
  SetOutPath "$INSTDIR"
 
  WriteUninstaller "$INSTDIR\uninstall.exe"

  !insertmacro MUI_STARTMENU_WRITE_BEGIN EVEMon
     CreateDirectory "$SMPROGRAMS\$STARTMENU_FOLDER"
     CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\EVEMon.lnk" "$INSTDIR\EVEMon.exe"
     CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Uninstall EVEMon.lnk" "$INSTDIR\uninstall.exe"
  !insertmacro MUI_STARTMENU_WRITE_END

  # Add entry for Add/Remove Programs
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "DisplayName" "EVEMon"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "UninstallString" "$INSTDIR\uninstall.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "DisplayIcon" "$INSTDIR\eve.exe_I006b_040f.ico"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "Publisher" "evercrest.com"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "URLUpdateInfo" "http://evemon.evercrest.com/"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "URLInfoAbout" "http://evemon.evercrest.com/"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "DisplayVersion" "${VERSION}"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon" \
         "NoRepair" 1
SectionEnd

Section "un.Uninstaller Section"
  RMDir /r $INSTDIR

  !insertmacro MUI_STARTMENU_GETFOLDER EVEMon $MUI_TEMP
  Delete "$SMPROGRAMS\$MUI_TEMP\EVEMon.lnk"
  Delete "$SMPROGRAMS\$MUI_TEMP\Uninstall EVEMon.lnk"

  StrCpy $MUI_TEMP "$SMPROGRAMS\$MUI_TEMP"

  startMenuDeleteLoop:
     ClearErrors
     RMDir $MUI_TEMP
     GetFullPathName $MUI_TEMP "$MUI_TEMP\.."
     IfErrors startMenuDeleteLoopDone
     StrCmp $MUI_TEMP $SMPROGRAMS startMenuDeleteLoopDone startMenuDeleteLoop
  startMenuDeleteLoopDone:

  DeleteRegKey /ifempty HKLM "Software\EVEMon"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\EVEMon"
SectionEnd
