@echo off

:# Run this tool from root git folder:  scripts\build.bat
call scripts\build-prep.bat

:# Build
"C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" build\build.proj /m /nr:false %*

:# Woot
echo Done!
