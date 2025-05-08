# Visual Studio Code Multi-Project Template.

## Overview
This developer template provides a foundation for a multi-project system, where a desktop Python project may collaborate with a PHP service project and a PHP web-site project. The goal is to create a cohesive system that provides a seamless user experience across different platforms. Of course, you can choose to use only the projects that fit your needs.
In that case, unused projects can be removed. The projects are combined in Visual Studio Code using a code-workspace file. The code-workspace file enables the following:

- To **structurally** combine different sub-projects. 
- Enables specific **launch** configurations for each sub-project and allow different **tasks** and settings per sub-project (see the folder .vscode).
- Shared **launch** and **task** configurations across sub-projects in VS Code. It is recommended to use **prefixes** for each project to make them easily distinguishable.
- Support for specific **devcontainer** features, such as required and recommended extensions.
- The ability to define **workspace-wide settings** as well as **project-specific settings** that override global workspace settings in case of conflicts.

## Goals
- Enable a modular development approach by using different technologies and tools per sub-project, making each sub-project self-contained and easily replaceable.
- Develop a high-performance desktop application with a user-friendly UI.
- Develop a responsive and user-friendly web application.
- Implement a robust and scalable RESTful API to support both applications.

## Getting Started
1. Clone the repository.
