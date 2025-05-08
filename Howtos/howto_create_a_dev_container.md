---
layout: default_c
RefPages:
 - howto_create_a_dev_container   
--- 

<small><br><br>
_This file is part of: **X11-GUI-Development-Template-Stack**_
_Copyright (c) 2025 Nico Jan Eelhart_
_This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree._
</small>
<br><br>
<div class="custom-style" style="--nje-color: #3077C8; --nje-size:12px; --nje-bg-color:#EDEDED">
**Quick content links**{: style="color: #474747;font-size:12px; "} *(Reference to most common page sections)*{: style="color: #474747;font-size:11px; "}
- ***Basics***
  - [Introduction](#introduction)<br>
  - [Docker with WSL Architecture & Requirements](#docker-with-wsl-architecture--requirements)<br>
  - [The Basic Container Setup](#the-basic-container-setup)<br>
  - [Create the basic Docker Container](#create-the-basic-docker-container)<br>
  - [Verify the Setup](#verify-the-setup)<br>

- ***Sub Containers***
  - [Create .NET project template sub container](#31-creating-the-net-sub-container-afx-x11-forward-net-service)<br> 
  - [Create .NET Avolonia project template sub container](#32-creating-an-avalonia-net-sub-container-afx-x11-forward-avalonia-service)<br>
  - [Create .NET GTK# project template sub container](#33-creating-an-net-gtk-sub-container-afx-x11-forward-net-service-gtk)<br>  
  - [Create Pyton/Rust/PHP sub container](#34---a-phppythonrust-sub-container)<br>

  [See also: How X11 and WSL works in Docker ](howto_x11_wsl_in_Docker)<br> <br>
  
</div>  

# Introduction

This Docker Linux (Ubuntu 24.04) container is designed for use on a Windows host running Docker Desktop. Its purpose is to enable the development of GUI applications (such as those written in C++, .NET, Rust, and others<sup>[1](#footnote-1)</sup>) within a Linux Docker container, and to display the running application on the Windows host <br>

Since Docker containers are headless by default (i.e., they lack native GUI support), we need a way to display GUI application output—whether during debugging or when running the application in release mode—on the Windows system. This is achieved by combining Docker Desktop with a **WSL2** environment and the **VcXsrv** server (via XLaunch).

This container stack consists of one required **base container** and one or more optional **sub-containers**. The sub-containers are built on top of the base container. The base container provides the core environment, while the sub-containers add programming languages, frameworks, and libraries, such as .NET, Avalonia, or C++ toolkits.

## Docker with WSL Architecture & Requirements

To better understand the Docker container and WSL setup, and to verify the system requirements, please refer to the following document: [WSL architecture and requirements](howto_wsl_using_x11.md)

<details closed>  
 <summary class="clickable-summary">
 <span  class="summary-icon"></span> 
 **Side note**: Security Considerations and Network Configuration
 </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->
  
>### Security Considerations and Network Configuration <br>
>
>For personal use on a developer's PC or laptop, security risks are generally lower than in a production environment. However, it's still a good idea to follow some basic security practices. When running an X server (like vcxsrv) on your Windows host, configure vcxsrv to only accept connections from the local machine. This minimizes exposure and ensures that only applications on your PC can access the server. Additionally, keep your firewall enabled and set to block unsolicited incoming connections. While this setup is for development purposes and may not require strict security measures, these simple steps can help protect your system against unexpected threats, especially when connected to less secure networks.
<br>
In practice, this means that as a developer, you should leave the XLaunch ***'Extra option' -> Disable access control*** ***unchecked***
</details>

# Create the Base Container

This chapter covers the setup of the **Base Container Service** (folder: `Base-Container`) and all the components required to run a GUI application on a Windows host. At the end of this setup, we will verify that everything works as expected by launching a sample X application (**xeyes**) from within the Base Container.

## The Basic Container Setup

Before executing the Docker Compose file, ensure that VcXsrv XLaunch is installed on the Windows host, this make sure we receive graphical output from the docker application. follow these instructions:

- [Download]( https://sourceforge.net/projects/vcxsrv/) and Install the VcXsrv software.
- After installation start XLaunch
  - Select **Multiple Windows** and click **Next**
  - Select Start **no client** and click **Next**
  - Ensure that **Clipboard** and **Native opengl** are **enabled**'
  - Ensure that **Disable access control** is **not enabled** ( this is more secure; only enable it if you encounter issues) click **Next**, then **Finish**

<hr>

## Create the Basic Docker Container

Now we can create and start the base container.

- Open the service subfolder: ***.\Base-Container\X11-Gui-Base-Service\\*** in a CMD window.
- Decide whether you want to use an **internal** or an **external network**. External networks are recommended, as they allow multiple Docker service containers to communicate within the same network, giving you the flexibility to extend your environment. This setting is configured in the Compose file of the **base-container**.For more generic and detailed information, refer to: <br>
[WSL architecture and requirements](https://nicojane.github.io/Docker-Template-Stacks-Home/pluggable)

- In any case, we use a fixed IP address in the Compose file to simplify communication with services such as an SSH server (though SSH is not used in this setup). While not strictly necessary, it helps avoid name resolution issues
- The Subnet and IP address can be found in the `.env` file:

<pre class="nje-cmd-one-line-sm-ident"> FIXED_SUBNET  # Default: 172.20.0.0/16            FIXED_IP      # Default: 172.20.0.15</pre>

- Next install the Container. Execute this command in the service sub folder

<pre class="nje-cmd-one-line-sm-ident"> docker-compose -f compose_x11_gui_base.yml up -d --build --force-recreate  --remove-orphans </pre>

> *Warning!*{: style="color: red;font-size:13px; "} <br>
> <small> When recreating the same container(service name) avoid subtle/annoying caching issues, When needed make sure to:</small>
- <small> delete the container</small>
- <small> delete the volume and </small
- <small> use the Docker prune command,so: </small>
>
> <pre class="nje-cmd-one-line-sm-ident"> docker system prune -a --volumes</pre>

<hr>

## Verify the Setup

- After running the commands you can open **Docker Desktop** and in the container section a new container is created under the name: ***'x11-gui-basic-service/x11-gui-basic-service-1'***. Open a terminal session in this container

- Under the Images tab of Docker Desktop you should see the image **eelhart/x11-gui-base:latest** This is the image that will be used by the **Sub Containers** that you may create (see next paragraph) to add different development environments to develop GUI applications.

> *Warning!*{: style="color: red;font-size:13px; "} <br>
> <small> For the usage of the Sub Containers you may remove the container 'x11-gui-basic-service/x11-gui-basic-service-1' but you will need to hold on to the image **eelhart/x11-gui-base:latest**</small>

- Make sure the X-server(XLaunch) is started (see 2.1.4)
- Enter the following command in the **Docker Terminal shell**:

<pre class="nje-cmd-one-line-sm-ident"> xeyes      # This should display a window with eyes on your Windows desktop</pre>

<span class="nje-ident"></span> When successful a Window with a pair of eyes should be displayed

<details closed>  
  <summary class="clickable-summary">
  <span  class="summary-icon"></span> 
  **Side note**: Docker call syntax
  </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->
  
>### Docker calling context <small> (***Skip this if you know Docker basics***) </small><br>
**Docker calling context**
Because we use Docker files (Dockerfile and compose) with descriptive names, such as **Dockerfile_Nodejs_React_Cont** instead of just **Dockerfile**, this affects how Docker commands are executed. For example, with a standard **Dockerfile**, we would use this command to reference  the Docker file in the **Docker Compose** file:
><pre class="nje-cmd-multi-line">
context: .
dockerfile: Dockefile
></pre>
In our case, we cannot use the default name but have to specify the name we gave, thus:<br>
><pre class="nje-cmd-multi-line">
build: 	    
context: .
dockerfile: Dockerfile_Nodejs_React_Cont
></pre>
 The same applies for using the build command. With the default Dockerfile, you can use this:
 ><pre class="nje-cmd-multi-line">
docker build 
# This will assume a file: Dockerfile is available
></pre>
With the named file, we have to use
><pre class="nje-cmd-one-line">docker build -f MyDockerFileNameHere </pre> <br>
The same applies for running the Compose file (use **-f** option) 
<br>
</details>

# 3. Creating the Sub Containers

This section describes several combinations of Dockerfiles and Compose files that can be used to create different **sub-containers**. Before continuing, make sure you have already created the **Base Container**, as described in section 2.

In the **Sub-Containers folder**, each sub-container is stored in its own subfolder. The following sub-sections provide instructions for installing one or more sub-containers.

<hr>

## 3.1 Creating the .NET Sub Container (`afx-x11-forward-net-service`)

This will create a basic .NET command-line application that returns—guess what? Yes! "Hello World."  
This program does **not** display its output in an X11 window on the host; instead, it is meant to demonstrate that the **Base Container** can be extended, and to show how this can be done.

In addition it will  create a project, based on a template, and demonstrate how to use Visual Studio Code (VSC) to Debug and run the application on the Windows host. _**See paragraph 4**_ for general VSC instructions. <br>

Additionally, these steps create a project from a template and show how to build the application using the CLI prompt.



> *Note:*{: style="color: Grey;font-size:13px; "}
> <small>We already have proven that that GUI output from the Linux application is displayed in our XLaunch Window on the Windows Host, if you did not see this you can open a terminal in the Docker container and enter the command:</small>
><pre class="nje-cmd-one-line-sm-ident">xeyes</pre>
 

**Create the  Application project:**

1\. Open a CMD in the folder .\Sub-Containers\Afx-X11-Forward-NET-Service\ <br>
2\. **Define the Network**: an **External** network configuration is used by **default** <br><br>
***Defaults checklist:***{: style="color: #999999;font-size:12px;margin-left:20px "}

<div class="nje-colored-block">

1. In the file `.env` of the **sub** container make sure the **FIXED_SUBNET** is set to the **same** value, of the `.env` file in the **base** container
2. In the file `.env` of the **sub** container make sure that a free IP address variable is used (i.e. **FIXED_IP2**), this can be taken from the `.env` file in the **base** container
3. In the `compose` file make sure that the **same** network (name) as in the base container `compose` file is used,
4. In the `compose` file(sub container) make sure that the IP variable, from step 2 is used.

 📍Alternatively you can use an internal network, these are commented around the same location in the compose file.
 </div>

3\. In the `.env` file, set the variable **PRJ_NAME_ARG** to the desired project name.

- Alternatively, you can also set this environment variable from the command line.This value will be used for both the project name and the project directory. If you omit this step, a **default** will be used (see the **PRJ_NAME_ARG** variable in the `.env file`:

<pre class="nje-cmd-one-line">$env:PRJ_NAME_ARG="my-project"		# From Command line </pre>

4\. Create the **external network**, if does not exists
<pre class="nje-cmd-multi-line"> 
docker network ls   # check if already exists (don't include in copy)
docker network create --subnet=192.168.52.0/28 network_common_X11_gui

</pre>

5\. Then execute the **docker command**, to create and builds the project
<pre class="nje-cmd-one-line"> docker  compose -f compose_net_x11_project.yml up -d  --remove-orphans --build --force-recreate </pre>

6\. Test <br>
***Result checklist:***{: style="color: #414141;font-size:12px;margin-left:20px "}

<div class="nje-colored-block" style="--nje-bgcolor:#414141; --nje-textcolor:#efefef;">
- Open **Docker Desktop**, in the container section a new container should be created with the name:<br>
***'afx-x11-forward-net-Service/afx-dotnet-container-1'***.
- Open a terminal session in this container, navigate to directory:<br>
***/projects/net-gtk-sharp/project_name2***
- Enter the following command in the terminal session:<br>
<pre class="nje-cmd-one-line-sm-ident">dotnet run</pre> <br>
This should creat your project and display 'Hello world'
 </div>

<hr>

## 3.2 Creating an Avalonia .NET Sub-Container (`afx-x11-forward-avalonia-service`)

This sub-container sets up a GUI project using the Avalonia UI framework for .NET. For more information about Avalonia, visit the [official Avalonia site](https://avaloniaui.net/). <br>

Avalonia supports creating multiple types of projects using its built-in project templates. In this setup, a custom sample project—based on one of these templates—is included and installed by default.
<br>

**Steps to Create an Avalonia Base Application Project:**

1\. Open a Command Prompt in the folder: ***.\Sub-Containers\X11-Gui-Avalonia-Service\\***

2\. **Define the Network**: an **External** network configuration is used by **default** <br><br>
***Defaults checklist:***{: style="color: #999999;font-size:12px;margin-left:20px "}
<div class="nje-colored-block">
1. In the file `.env` of the **sub** container make sure the **FIXED_SUBNET** is set to the **same** value, of the `.env` file in the **base** container
2. In the file `.env` of the **sub** container make sure that a free IP address variable is used (i.e. **FIXED_IP2**), this can be taken from the `.env` file in the **base** container
3. In the `compose` file make sure that the **same** network (name) as in the base container `compose` file is used,
4. In the `compose` file(sub container) make sure that the IP variable, from step 2 is used.

 📍Alternatively you can use an internal network, these are commented around the same location in the compose file.
 </div>

3\. Open the ***.env*** file to adjust the necessary settings:

- **Project Type** By default ***PRJ_TYPE_USE_CUSTOM_APP*** is set to create a basic application based on our custom template. You can disable this to choose one of the Avalonia-provided templates by setting the variable: **PRJ_TYPE_ARG**.
- **Project Name**: Set the variable **PRJ_NAME_ARG** to your desired project name. This will be used for both the project name and the project directory.If omitted, the default value from **PRJ_NAME_ARG** in the **.env** file will be used.

4\. Create the **external network** if does not exists
<pre class="nje-cmd-multi-line"> 
docker network ls   # check if already exists (don't include in copy)
docker network create --subnet=192.168.52.0/28 network_common_X11_gui

</pre>

5\. Execute the **Docker command** to create the project:

<pre class="nje-cmd-one-line"> docker  compose -f compose_avalonia_x11_project.yml up -d  --remove-orphans --build --force-recreate </pre>
<span class="nje-ident"></span>*Note that this compose creates and builds the project.*

6\. Setup Result <br>
***Result checklist:***{: style="color: #414141;font-size:12px;margin-left:20px "}

<div class="nje-colored-block" style="--nje-bgcolor:#414141; --nje-textcolor:#efefef;">
- Open **Docker Desktop**, in the container section a new container should be created with the name:<br>
***'x11-gui-avalonia-service/x11-gui-avalonia-dotnet-1'***.
- Open a terminal session in this container, navigate to directory:<br>
***/projects/ava/project_name***
- Make sure XLaunch is started on the host
- Enter the following command in the terminal session:<br>
<pre class="nje-cmd-one-line-sm-ident">dotnet run</pre> <br>
This should display Application GUI on your host via the XLaunch program
 </div>
When all this works you should be able to open, build, and debug the project in ***Visual Studio code***, **See paragraph 4** for details

### 3.2.1 Visual Studio Code Specifics

This section outlines the required Visual Studio Code (VSC) plugins and configurations. These should **already** be **installed** and **configured** if you are using our custom application template (see **PRJ_TYPE_USE_CUSTOM_APP**). In this case, the project file and .vscode directory  will already contain the necessary settings. For other project types (**e.g., PRJ_TYPE_ARG**), you will need to configure these manually. If something doesn’t work as expected, it’s a good idea to verify your settings. (Click on **Side note** below to expand the section)

#### Available  tasks

In the **Terminal → Run Task... menu**, you can find the build tasks for the Avalonia project, which are defined in the workspace settings file (see side note above). The following tasks are defined for our project:

- Clean: All Build Output<br> 
- Debug: Build Configuration <br>
- Debug: Restores NuGet Packages <br>
<span class="nje-ident"/> <small>*Almost all ways called automatically*</small>
- Release: Build Configuration <br>
- Release: Restore NuGet Packages <br>
<span class="nje-ident"/> <small>*Almost all ways called automatically*</small>
- Watch XAML Build <br>
<span class="nje-ident"/> <small>*This makes sure you can see the 'live output' of the application while your updating the XAML file(s)*</small>
For other sub-container other task might be available, but most of them should be self explaining.

> *Warning 1*{: style="color: red;font-size:13px; "} <small>As of Sept. 2024, you might need to change one thing. Be sure to install **version 0.29** of the **Avalonia for Visual Studio Code** plugin. There is a persistent parsing error in versions up to 0.31 that pops up whenever you type. See more details [here](https://github.com/AvaloniaUI/AvaloniaVSCode/issues/113)</small> <br> 

<details closed>  
<summary class="clickable-summary">
<span  class="summary-icon"></span> 
**Side note**: **Visual Code Plugins**  installed and configured in the Container
</summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->
 
>***Visual Code*** Plugins installed and configured in the Container 
1. General plugins
  -  C# Dev Kit
  -  Avalonia for vscode (by Avalonia team)<br>
This feature for XAML development and auto complete features (Requires .NET Core 8.0)
**WARNING** Make sure to install **version 0.29**, untill version 0.31 there is a super annoying parse error which pops-up when every you type something, see [here](https://github.com/AvaloniaUI/AvaloniaVSCode/issues/113). When a higher version then 0.31 is released you can test that version to check if it still there.

2. **Kir-AntipovHot Reload** plugin should already be part of this project, this is how it defined:
	- Project file should contain (Copy this if not)	 	  
	  ```
	  <PropertyGroup Condition="'$(Configuration)' == 'Debug'">
		<DefineConstants>$(DefineConstants);ENABLE_XAML_HOT_RELOAD</DefineConstants>
	  </PropertyGroup>
	  <ItemGroup>
		<PackageReference Condition="$(DefineConstants.Contains(ENABLE_XAML_HOT_RELOAD))" Include="Avalonia.Markup.Xaml.Loader" Version="11.1.0" />
		<PackageReference Condition="$(DefineConstants.Contains(ENABLE_XAML_HOT_RELOAD))" Include="HotAvalonia" Version="1.1.1" />
		<PackageReference Include="HotAvalonia.Extensions" Version="1.1.1" PrivateAssets="All" />
	  </ItemGroup>
	  ```
	- In project ReactiveUI Package should be present, check if it is	
	 ```
	 PackageReference Include="Avalonia.ReactiveUI" Version="your_version_here" # alternatively added it with: dotnet add package Avalonia.ReactiveUI
	 ```
	- Next initialize **HotAvalonia** in the ***App.axaml.cs*** (This should already be done, but for your reference)
		- Using directive ``using HotAvalonia;``
		- In the method ' public override void Initialize()' add:<br>
		``this.EnableHotReload(); // HotAvalonia Ensure this line **precedes** AvaloniaXamlLoader.Load(this);``
	- Make sure the ``.UseReactiveUI();`` is called in **AppBuilder.Configure()** see ***program.cs***. This also requires: ``using Avalonia.ReactiveUI;``
	- **Run task** -> **Watch run Avalonia** Change an \*.axaml and the program output should be adapted
	WARNING: When Visual Code complains about multiple project and refuses to start, it is likely due to the fact that in the same folder a solution file automatically created for you :(
  removing/relocating this file might solve the issue, to prevent it from creating:
  - Look for installed plugin **'C# Dev Kit'**
  - press Extensions settings
  - Uncheck 'Automatically **Create Solution In Workspace'**
  - Reload your project.
>
<small> *Thanks: to **Kir_Antipov** [Github](https://github.com/Kir-Antipov) for providing the required tools to make this work!  </small>
<br>
</details>

<hr>

### 3.3 Creating a .NET GTK Sub-Container (Afx-X11-Forward-NET-Service-GTK#)

This instruction set describes how to install the .NET GTK# Sub-Container, which is based on GTK 3.24.24.

**Steps to Create a .NET GTK# Sub-Container Application Project:**

1\. Open a Command Prompt in the following folder: ***.\Sub-Containers\X11-Gui-NET-Service-GTK#\\***
      
2\. **Define the Network**: an **External** network configuration is used by **default** <br><br>
***Defaults checklist:***{: style="color: #999999;font-size:12px;margin-left:20px "}
<div class="nje-colored-block">
1. In the file `.env` of the **sub** container make sure the **FIXED_SUBNET** is set to the **same** value, of the `.env` file in the **base** container
2. In the file `.env` of the **sub** container make sure that a free IP address variable is used (i.e. **FIXED_IP2**), this can be taken from the `.env` file in the **base** container
3. In the `compose` file make sure that the **same** network (name) as in the base container `compose` file is used,
4. In the `compose` file(sub container) make sure that the IP variable, from step 2 is used.

 📍Alternatively you can use an internal network, these are commented around the same location in the compose file.
 </div>

3\. Create the **external network** if does not exists
<pre class="nje-cmd-multi-line"> 
docker network ls   # check if already exists (don't include in copy)
docker network create --subnet=192.168.52.0/28 network_common_X11_gui

</pre>

5\. Execute the **Docker command** to create the project (folder: 'X11-Gui-NET-Service-GTK#')
<pre class="nje-cmd-one-line">docker-compose -f compose_net_x11_gtksharp_project.yml up -d --build --force-recreate --remove-orphans
</pre><br>

6\. Setup Result <br>
***Result checklist:***{: style="color: #414141;font-size:12px;margin-left:20px "}

<div class="nje-colored-block" style="--nje-bgcolor:#414141; --nje-textcolor:#efefef;">
- Open **Docker Desktop**, in the container section a new container should be created with the name:<br>
***'x11-gui-avalonia-service/x11-gui-avalonia-dotnet-1'***.
- Open a terminal session in this container, navigate to directory:<br>
***/projects/net-gtk/project_name***
- Make sure XLaunch is started on the host
- Enter the following command in the terminal session:<br>
<pre class="nje-cmd-one-line-sm-ident">dotnet run -f net8.0-windows</pre> <br>
This should display Application GUI on your host via the XLaunch program
 </div>
When everything is set up correctly, you should be able to open, build, and debug the project in Visual Studio Code. See **paragraph 4** for details.
7\. Specific for Visual Studio Code <br>

- Start the new container in VSC
- open the directorie: `projects/net-gtk/project_name`
- Within a new terminal session in VSC Install the following extension(s) in the container"

<pre class="nje-cmd-multi-line">code --install-extension ms-dotnettools.csharp
code --install-extension ms-dotnettools.csdevkit
code --install-extensionms-dotnettools.vscode-dotnet-runtime
</pre>

See the next paragraph for the specifics regarding Visual Studio Code

#### 3.3.1 The project Template and specific tasks in VSC

**Project structure** <br>
After opening your GTK3 .NET project in **VSC** you find a **'src'** folder with the following structure: <br>

- **src** -->> *root source folder*
  - **backend** -->> *.NET class libraries*. <small>**See VSC task: '2.1 AFX ...' below**</small>
    - **cl_example** -->> *simple sample class library*
    - **cl_example_rest** -->> *other sample class library, with simple Rest example*
  - **frontend** -->> *GTK3 csharp code*
    - **Program.cs** -->> main program using the sample libraries

**VSC Tasks** <br>
You can use the following tasks in Visual Studio Code: 

| Tasks                                                                       | Used for                                  |
|:-----                                                                       |:--------                                  |
|<small>1.1 AFX Build GTK App (Debug Windows)</small>                         | <small>Debug Build all for the Windows platform </small> |
|<small>1.2 AFX Build Release GTK App (Windows & Linux)</small>&nbsp; | <small>Release build ofr Linux and Windows Platforms </small>
|<small>2.1 AFX CREATE: Class Source Library </small>                         | <small>Creates a class library in the directory: src/backend/ see **Remark 1** </small>
|<small>3.1 AFX Run GTKApp (Windows Release) </small>                         | <small>Run the app in release mode </small>
|<small>4.1 AFX Clean a specific Library </small>                             | <small>Cleans a specific class library </small>
|<small>4.2 AFX Clean the application </small>                                | <small>Cleans the frontend application  </small>
|<small>4.3 AFX Clean ALL </small>                                            | <small>Clean all libraries and frontend application </small>

>***Remark 1***{: style="color: green;font-size:14px; "} <small> <br>Make sure the class library start with: **'cl_'** followed by a descriptive name. This makes sure:
<br><span class="nje-ident" style="--nje-number-of-spaces: 30px;"></span> * That the class libraries are created in a **folder** with that name on **src\backend\cl_yourname**
<br><span class="nje-ident" style="--nje-number-of-spaces: 30px;"></span> * During the build process the **cl_** is replaced with with **'lib_'**  resulting into library: ***lib_yourname***
<br><span class="nje-ident" style="--nje-number-of-spaces: 30px;"></span> * The **clean tasks 4.x** wil work properly, because they act on these names!
 </small> 

<br>

### 3.4 - A PHP/Python/Rust Sub container

This is a general-purpose container for PHP, Python, and Rust development. It includes a default project template that integrates all three languages.
Below are the instructions to install this sub-container:


**Steps to Create an PHP, Python, Rust based  Application Project:**

1\. Open a Command Prompt in the folder: ***.\Sub-Containers\X11-Gui-PyPHP-Service\\***

2\. **Define the Network**: an **External** network configuration is used by **default** <br><br>
***Defaults checklist:***{: style="color: #999999;font-size:12px;margin-left:20px "}
<div class="nje-colored-block">
1. In the file `.env` of the **sub** container make sure the **FIXED_SUBNET** is set to the **same** value, of the `.env` file in the **base** container
2. In the file `.env` of the **sub** container make sure that a free IP address variable is used (i.e. **FIXED_IP5**), this can be taken from the `.env` file in the **base** container
3. In the `compose` file make sure that the **same** network (name) as in the base container `compose` file is used,
4. In the `compose` file(sub container) make sure that the IP variable, from step 2 is used.

 📍Alternatively you can use an internal network, these are commented around the same location in the compose file.
 </div>

3\. Create the **external network** if does not exists
<pre class="nje-cmd-multi-line"> 
docker network ls   # check if already exists (don't include in copy)
docker network create --subnet=192.168.52.0/28 network_common_X11_gui

</pre>

5\. Execute the **Docker command** to create the project (folder: 'X11-Gui-PyPHP-Service')
<pre class="nje-cmd-one-line">docker compose -f compose_Python-Apache-php.yml up -d --build --force-recreate --remove-orphans                                     
</pre><br>


6\. Setup Result <br>
***Result checklist:***{: style="color: #414141;font-size:12px;margin-left:20px "}

<div class="nje-colored-block" style="--nje-bgcolor:#414141; --nje-textcolor:#efefef;">
- Within **Docker Desktop**, in the container section a new container should be created with the name:<br>
***'x11-gui-pyphp-service/x11-gui-python-apache-php-1'***.
- The following wen applications should be reachable <br>
  - `http://localhost:8071/web-app/phpinfo.php`
  - `http://localhost:8071/web-app/home/index.php`
  - `http://localhost:8071/web-rest/wr-api.php?name=John`
  - `http://localhost:8071/web-rest/test-client/client.php`
- Open a terminal session in this container, navigate to directory: ***/projects/pyphp/project*** <br>
You should note the atleast the following files and directories
  - **File**: `.project.code-workspace` to open all projects in VSC
  - **Dir**: `./GUI-app/src_frontend` a GUI Python application 
  - **Dir**: `./GUI-app/src_backend` The location for the **Rust** libaries that can be called from Python, can be generated from a **VSC Task**
  - **Dir**: `./web-app` The PHP home webpage (`http://localhost:8071/web-app/home/index.php`) application
  - **Dir**: `./web-rest` A simple REST PHP application
  - **Dir**: `./rust_lib_demo` A Simple Rust library to check the creation of a **Rust library** <br><br>
***Please note*** that the ./rust_lib_demo library is just a simple sample, unrelated to the GUI application, to demonstrate a Rust function exposed to Python using PyO3. Similar Rust libraries can be generated in the ./GUI-app/src_backend directory via a Visual Studio Code task (see 3.4.1 below). <br><br>
  - Lets test if Python can be found: 
  <pre class="nje-cmd-one-line-sm-ident">python --version </pre>
  - Let's test if we can build the `./rust_lib_demo` with **`maturin`**  Change directory to `rust_lib_demo` in the CLI and exceute:
  <pre class="nje-cmd-multi-line" style="--nje-font-size:smaller;">
  maturin build --release
  pip install target/wheels/*.whl  # install the wheel
  python -c "import rust_lib; print(rust_lib.add(3, 7));" # Sample calls the Rust Function in Python
  &nbsp;</pre>

><small>***Maturin*** <br>
Maturin creates a **Python module** from our Rust library. It first creates a shared library and then it Packages everything into a wheel that can be installed with **pip**. *Note* that in the VSC Task that builds the rust library in (`./GUI-app/src_backend` we use **cargo** to build the shared and statical library.
 </small>

When the above links work and the rust generates a library and python is install, the installation was successful. You can Continue with the Visual Studio Code section, to explorer the projects and tools.

 </div>
When all this works you should be able to open, build, and debug the project in ***Visual Studio Code***, **See paragraph 3.4.1** for details

<hr>

#### 3.4.1 - Visual Studio Code Specifics

This project template consists of the following projects and tools

- Two **PHP** application `web-app` and `web-rest`
- A **Python** GUI application `GUI-app`
  - **PySide** to us Qt inside Python
- **Rust** to create library files for Python
  - **PyO3**, to make the integrationpossible
  - **Maturin**, To creat a Python module from a Rust library (enables pip install) 
- A **make** based tool that generates a **Rust** source library in the GUI-app (`./GUI-app/src_backend`) which can be called via a VSC tasks

Because multiple project definitions are used, a **project workspace** is defined to open all these (sub)projects: `.project.code-workspace.`

##### To open the project workspace

- First open the folder in VSC: 'File -> Open folder' and choos `/projects/pyphp/project/`
- Then open the project workspace: 'File-> open workspace from file' choose: `.project.code-workspace`
- This result in:

<div class="nje-colored-block" style="--nje-bgcolor:#414141; --nje-textcolor:#efefef;--nje-offset:22px; --nje-vmove2:-14px">
***Project workspace layout:***{: style="color:rgb(204, 186, 186);font-size:12px;margin-left:2px;"} <br>
📦 ***Desktop App (PY, Main)*** ➜ This the Pyton GUI application <br>
📦 ***Rust_lib_demo*** ➜ A simple sample Rust library <br>
📦 ***Web-app (PHP)*** ➜ The main Web Application <br>
📦 ***Web-rest (PHP)*** ➜ Another Web Application, demostarting a REST call  <br>
📝 ***About & info & tools*** ➜ Documentation and tools  <br>
🧾 ***Logs*** ➜ Log files from apache, php and others  <br>
</div>

<hr>

##### 3.4.1.1 - Desktop App (PY, Main)

In **VSC** basic actions and task can be run for the python application ➜ ***Project: Desktop App (PY, Main)***

1. **Debug and Run**
- Make sure XLaunch is started in the Windows host.
- In the project:`Project:Desktop App (PY, main)` 
- Open the file: ***./src_frontend/main.py*** and set a breakpoint
- Make Sure that in the **Run and Debug** tab the **AFX PY(Debugpy)** is selected
- Press F5, you should hit the breakpoint 
- Press F5 again, the program should run and you should see a simple program in the XLaunch Window.

2.**Create a Rust Source Library** <br>
By default the directorie: ***./src-backend/api_interface_rust*** is empty. This directorty is used for **Rust libraries** that we can **generate**

  - Terminal ➜ Run Task...
  - Choose ***AFX Create 4: business Library (Rust)*** and follow the instruction (Use the mentioned conventions!, i.e. **name_lib**) After this you have a Rust library in: <br>
  ***src_backend/api_interface_rust/name_lib***

3.**Build the Rust library** <br>
The above created Rust libary can be build:

- Terminal ➜ Run Task...
- Choose ***"AFX BUILD 1: Libraries (Rust,LINUX)*** This will create the library and dislays where it can be found, after this you can call it from Python using PyO3 (which is instaled [see](https://github.com/PyO3/pyo3) ).

> ***Remark:***{: style="color: red;font-size:13px; "} <br>
> <small>While there is a tasks to cross-compile the Rust library for Windows (***AFX BUILD 2: Libraries (Rust,WINDOWS)***) 
This is not supported because it uses Python Windows bindings which at this time(2025) are not provide. The compile should probably work but linkage will result into errors     <br>
</small>

4.**Optional. Client Rust Libary Test App** <br>
Optional you can create a simple 'client test' application for your Rust Library.
- Terminal ➜ Run Task...
- Choose ***AFX Create 5: Client Library Tester (Rust LINUX)*** and follow the instruction.
- This will create in the directorie  ***src_backend/api_interface_rust/tests*** a sub directory **test[libname]** with the test code.

5.**Optional. Build the Test Client** <br>
The created Rust client Test can be build with:
- Terminal ➜ Run Task...
- Choose ***AFX BUILD 3: Client Library Tester (Rust LINUX, debug)***
- It ask for the test, which is named like **test_[lib_name]**
- After that follow the the instructions.

> ***Error?***{: style="color: red;font-size:13px; "} <br>
> <small>If you get an error; **'Can not find library...'** make sure you have the following in your **Cargo.toml** of the library:</small>  <br>
> <small>- `crate-type = ["rlib", "cdylib"]`  instead of: <br></small>
> <small>- `crate-type = ["cdylib"]` <br>
>  rlib makes sure the library can be found statically
</small>

<hr>

##### 3.4.1.2  PHP application
In VSC basic actions and task can be run for the PHP web applications (***Project: web-app (PHP)*** and ***Project: web-rest (PHP)***)

1. **Debug and Run**
- In one of the project files set a breakpoint
- In the RUN AND DEBUG make sure you have selected: **PHP(A): Xdebug AFX"** for the PHP application and **PHP(R): Xdebug AFX"**  for the rest version
- Load the relevant page:
  - `http://localhost:8071/web-app/home/index.php`
  - `http://localhost:8071/web-rest/wr-api.php?name=John`
- You should hit the breakpoint 
- Press F5 again the program should run and you should see a simple program in the XLaunch Window





<br>
<hr><hr>

# Appendix 1 Develop with VSC in the host

To develop in **V**isual **S**tudio **C**ode we advice the following instructions. Note that this is a general instruction applicable for all sub containers, any specific issue will  be described in the **'README.md'** in the root directory of the sub container, this all includes specific VSC task and project structure.

## 1. Open an application  from a container VSC (@host)

- Press CTRL-SHIFT-P or F1 and select (start typing) **Attach to running container...**
- Select fore example the  **afx-x11-forward-avalonia-service** sub container, or any other sub container
- Alternatively you might click on the **Docker boot** on the left toolbar and select the container from there, this opens a new Window with the container information

## 2. Open Folder and building your app.

- Use the **VSC Explorer** and the **Open Folder** to open the remote container's folder. **Ensure** you open the correct folder so that the **.vscode** directory settings are applied properly.
- Select Open Folder and enter: **/projects/ava/your_project_name** (for Avalonia). This will ensure the project is loaded along with the settings configured in the .vscode folder. (Alternatively, you can obtain the path by opening a terminal inside the Docker container. The initial folder shown by the pwd command will give you the correct path.)

When opening the application container (i.e..NET container) and the project root folder in Visual Studio Code, a dedicated Visual Studio Code server will be installed within the container. This server provides a full Visual Studio Code environment with its own settings and extensions. Upon opening the folder for the first time, the system will detect any required extensions and prompt you to install them. Follow the instructions to complete the installation if prompted.

In case the intellisense indicates errors in one of the project files this is probably due to the missing of an extension.

## 3 VSC Build tasks

Most of the build an launch task are pre-created for you in th sub containers

<details closed>  
  <summary class="clickable-summary">
  <span  class="summary-icon"></span> 
  **Side note**: VSC Settings files
  </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->
  
>### VSC Settings files 
>
>**Settings file**
>
>To change the build targets and related settings you will need to update the files in the **.vscode directory** of the project (this is the reason you have to open the right project directory, opening the right directory make sure the project directory is the root and VSC searches for a '.vscode' directory in the root to apply settings to this project) the following settings files are available in '.vscode':
> - .vscode/tasks.json: For build tasks.
> - .vscode/launch.json: For debugging configurations.
> - .vscode/settings.json: For general VS Code settings specific to your project (e.g., editor preferences, linting settings).
In case you have to customize the build properties or other settings these files should be updated.


</details>

<hr><hr><hr>

<br>
### Footnotes
<small>1. <a name="footnote-1"></a>Of course, any Linux GUI application can be displayed via Docker on the Windows host; it is not limited to development.</small>
