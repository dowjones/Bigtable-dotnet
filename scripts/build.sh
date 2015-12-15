#!/bin/bash

# Run this tool from root git folder:  scripts/build.sh

# Ensure submodules are up-to-date
scripts/build-prep.sh

# Build
cd build
xbuild $@

# Woot
echo Done!
