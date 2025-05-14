include(CMakeDebug.cmake)       # include Debug functions, to don't clutter this file


# Main
# ===========================================================================================================
#
# Summary
#   1. Global definitions and requirements
#   2. File locations and main commands
#   3. Utility helper: copy of generated files
#
#   Creates a Slint GUI Application with in combination with a SDL GUI Window
#		Changes compared with GUI-app:
#			Added   : find_package(Slint REQUIRED)  
#			Changed : From: target_link_libraries(${APP_TARGET_NAME} PRIVATE Slint::Slint)
#           To      : target_link_libraries(${APP_TARGET_NAME} PRIVATE Slint::Slint SDL2::SDL2)
#


# 1.1 CMake and language version settings
cmake_minimum_required(VERSION 3.21)
set(CMAKE_CXX_STANDARD 20)
#
# 1.2 Project and requirements
project(HelloWorldSlint CXX)
# required CMake files for the libraries
find_package(Slint REQUIRED)                                # Contains SLint specific  syntax
find_package(SDL2 REQUIRED)
set(APP_TARGET_NAME HelloWorld)                             # Set the Target name


# 2 Define Sources and main build commands
# ===========================================================================================================

# 2.1 Set Slint sources locations
set(SLINT_DIR "/src-slint")                                 
set(SLINT_DIR_HEADERS "${SLINT_DIR}/Headers-generated")     # Where generated Slint headers will be copied(see 3)

# 2.2 Find C++ source files
file(GLOB MAIN_SRC "${CMAKE_CURRENT_SOURCE_DIR}/src/*.cpp")
set(SRC_FILES
   ${MAIN_SRC}
)

# 2.3 Find Slint UI source files
file(GLOB SLINT_SRC "${CMAKE_CURRENT_SOURCE_DIR}${SLINT_DIR}/*.slint")


# 2.4 Define the application target
add_executable(${APP_TARGET_NAME} ${SRC_FILES})

# 2.5 Register Slint sources for C++ header generation
#
# ⚠️ Note: This step must follow `add_executable` (2.4).
#
#   This will generate C++ headers from Slint files and ensure the resulting interface code
#   is linked into your application target (${APP_TARGET_NAME}). // CHANGED: expanded explanation
slint_target_sources(${APP_TARGET_NAME} ${SLINT_SRC})

# 2.6  Link the application with the Slint C++ runtime library
target_link_libraries(${APP_TARGET_NAME} PRIVATE Slint::Slint SDL2::SDL2)

# ===========================================================================================================


# 3.  Utility helper: Copy generated headers to source tree
# ===========================================================================================================
#
# This section ensures that the Slint-generated header files are stored in the source directory,
# which may be preferred for IDE integration, version control, or separate processing.


# 3.1 Define header source (build dir) and destination (source dir) directories
set(SLINT_GENERATED_H_SOURCE "${CMAKE_CURRENT_BINARY_DIR}")
set(SLINT_GENERATED_H_DEST "${CMAKE_SOURCE_DIR}${SLINT_DIR_HEADERS}")

# 3.2 Create a custom command to copy the headers
add_custom_command(
    #    COMMAND ${CMAKE_COMMAND} -E sleep 1            # Sleep for 1 second
    OUTPUT "${SLINT_GENERATED_H_DEST}/copied.marker"    # Marker file to track when the copy is done
    COMMAND ${CMAKE_COMMAND} -E make_directory "${SLINT_GENERATED_H_DEST}"
    COMMAND bash -c "cp '${SLINT_GENERATED_H_SOURCE}/*.h'  '${SLINT_GENERATED_H_DEST}' " 
    COMMAND ${CMAKE_COMMAND} -E touch "${SLINT_GENERATED_H_DEST}/copied.marker"    
    DEPENDS ${APP_TARGET_NAME}                          # Make sure it runs after the application target is built
    COMMENT "Copying generated Slint headers after build"
)

# 3.3 PHONY target: Always run this step as part of the build
add_custom_target(copy_slint_headers ALL
    DEPENDS ${SLINT_GENERATED_H_DEST}/copied.marker )


# EXECUTION
# ===========================================================================================================
# To build this project:
#  Use provided build script to built:
#     ./build.sh
#
# Which does just simply:
#     mkdir -p build
#     cd build
#     cmake ..          # Configure
#     cmake --build .   # Build the application
