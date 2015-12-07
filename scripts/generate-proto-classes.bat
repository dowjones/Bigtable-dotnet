@echo off

:# Run this tool from root git folder:  scripts\generate-proto-classes.bat

:# Locals
SETLOCAL
set array=( bigtable\v1 bigtable\admin\cluster\v1 bigtable\admin\table\v1 api longrunning protobuf rpc type )
set dest=src\Native\Generated

:# Loops through proto folders
FOR /D %%i in %array% DO CALL :GENERATE %%i
GOTO:EOF
:#done

:# Generate classes for each proto file
:GENERATE
echo Generating %*...
for %%f in ( protos\google\%*\*.proto ) DO (
	tools\protoc.exe -Iprotos --csharp_out %dest% --grpc_out %dest% --plugin=protoc-gen-grpc=tools\grpc_csharp_plugin.exe  %%f
)
GOTO:EOF

:# Woot
echo Done!
ENDLOCAL
:EOF



