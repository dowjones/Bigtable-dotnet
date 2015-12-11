#!/bin/bash

# Run this tool from root git folder:  scripts/build.sh

# Ensure submodules are up-to-date
git submodule update --init

# Move to grpc folder
cd submodules
cd grpc

# Ensure grpc submodules are up-to-date
git submodule update --init

# Ensure submodule packages are restored
cd vsprojects
../../../tools/nuget.exe restore -NonInteractive grpc_csharp_ext.sln
cd ..
cd src
cd csharp
../../../../tools/nuget.exe restore -NonInteractive Grpc.sln

# Move back to root
cd ..
cd ..
cd ..
cd ..

# Ensure solution packages are restored
cd src
../tools/nuget.exe restore -NonInteractive Bigtable.NET.sln
cd ..
cd build

# Build
xbuild $@

# Woot
echo Done!
