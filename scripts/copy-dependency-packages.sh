#!/bin/bash

# Run this tool from root git folder:  scripts/copy-dependency-packages.sh

# Copy dependencies
cp -a lib/Dependency/grpc.vspackages.packages submodules/grpc/vsprojects/packages
cp -a lib/Dependency/grpc.src.csharp.packages submodules/grpc/src/csharp/packages

# Copy libraries (these are not actually used, but I did not want to make that kind of change to Grpc.Core)
cp lib/Debug/grpc_csharp_ext-x64.dll submodules/grpc/vsprojects/x64/Debug/grpc_csharp_ext-x64.dll
cp lib/Debug/grpc_csharp_ext.dll submodules/grpc/vsprojects/Debug/grpc_csharp_ext.dll

# Woot
echo Done!