@echo off

:# Run this tool from root git folder:  scripts\copy-dependency-packages.bat

:# Locals
SETLOCAL

:# Copy packages
xcopy /E /Y lib\Dependency\grpc.vspackages.packages submodules\grpc\vsprojects\packages
xcopy /E /Y lib\Dependency\grpc.src.csharp.packages submodules\grpc\src\csharp\packages

:# Copy libraries (these are not actually used, but I did not want to make that kind of change to Grpc.Core)
xcopy /Y lib\Debug\grpc_csharp_ext-x64.dll submodules\grpc\vsprojects\x64\Debug\grpc_csharp_ext-x64.dll
xcopy /Y lib\Debug\grpc_csharp_ext.dll submodules\grpc\vsprojects\Debug\grpc_csharp_ext.dll

:# Woot
echo Done!
ENDLOCAL
:EOF



