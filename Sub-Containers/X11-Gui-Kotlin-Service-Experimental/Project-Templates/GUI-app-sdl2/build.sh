#!/bin/bash

# Call:
#   ./build.sh              # Builds in Debug mode
#   ./build.sh Release      # Builds in Release mode

App="GUI APP"
BuildType=${1:-Debug}  # Default to Debug if not specified

BuildDir="build-${BuildType,,}"  # lowercase the type (e.g., build-debug)

mkdir -p "$BuildDir"
echo -e "\nSTARTING"

echo -e "CONFIGURE: $App ($BuildType)"
cd "$BuildDir" && cmake -DCMAKE_BUILD_TYPE=${BuildType} ..

echo -e "BUILD:  $App"
cmake --build .

echo -e "\nDONE:  $App executing script 'gui-app-sdl2' "
