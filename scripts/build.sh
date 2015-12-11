#!/bin/bash

# Run this tool from root git folder:  scripts/build.sh

# Ensure submodules are up-to-date
git submodule update --init

# Move to grpc folder
cd submodules
cd grpc

# Ensure grpc submodules are up-to-date
git submodule update --init

# Move back to root, then to build folder
cd ..
cd ..
cd build

# Build
xbuild $@

# Woot
echo Done!
