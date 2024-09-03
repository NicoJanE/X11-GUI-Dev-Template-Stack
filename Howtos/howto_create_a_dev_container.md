---
layout: default_c
RefPages:
 - howto_create_a_dev_container   
--- 


<small>
<br><br>
_This file is part of: **App-X11-Forward Development Template Stack**_
_Copyright (c) 2024 Nico Jan Eelhart_
_This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree._
</small>
<br><br><br>


# 1 What 
This Docker Linux (Ubuntu 24.04) container is designed for use on a Windows Docker (Desktop) host. Its purpose is to enable the development of GUI applications (such as C++, .NET, Rust, and others[^1]) within a Linux Docker container and display the running application on a Windows host. Because Docker is headless (meaning no GUI support is available), we need a method to display the GUI application output (while debugging or running the application in release mode) on the Windows host system. Fortunately, this can be accomplished by combining the Docker Desktop container with a **WSL2** environment and the **vcxsrv** server (XLaunch).

[^1]: Of course, any Linux GUI application can be displayed via Docker on the Windows host; it is not limited to development.

This container stack consists of several different containers, each created based on a Dockerfile and Compose file. There is one **generic container** from which the other containers extend; this is referred to as the **Base Container**. It provides the aforementioned environment but does not include any GUI frameworks such as .NET, Avalonia, or other C++ frameworks.. 

The other containers are referred to as sub-containers. These containers require the Base Container, and each adds a specific development framework (.NET Avalonia, wxWidgets, or another chosen framework). You can add any sub-container as you want, as long as it supports the Linux platform. See paragraph 3 for the currently supported sub-containers.

The Base Container is where the difficult work is done, including interacting with WSL and presenting the GUI output on the host. The sub-containers just add frameworks that use these features and help you develop a specific application.


### 1.1 Architecture of the Containers
To help you understand the Container setup better, below is an image that shows how the components interact after the setup is completed. In this example, we assume the .NET Framework is the Sub Container. We install the necessary components on the Windows Host (light blue square). In addition to the processes displayed in the image, this setup also includes fixed folders and  data files, which are detailed in the table *Data locations* below

### The Base Container
![alt text](Context_arch.png)
The Windows Host contains a **vcxsrv server** ([download](https://sourceforge.net/projects/vcxsrv/)) process which handles the X-Protocol data sent from the **docker Base Container**. It uses the **WSL environment** as a back-end to prepare the data.

The Docker Base container (grey square) contains an Ubuntu image, with the Docker files to create itself and a Docker environment settings file called '.env'

The **WSL environment** (yellow square) acts as the back-end for the Docker container. Although WSL2 uses a lightweight virtual machine, it is specifically designed to run Linux distributions natively on Windows, providing efficient and direct access to Windows resources. The WSL environment handles graphical output from the Docker container, which is then sent to the **vcxsrv (XLaunch)** server on the host. To use WSL2, Docker needs to be configured accordingly; this will be described later in this document.

The sub-containers consist of different self-contained containers that handle specific tasks. For example, there is currently one simple sub-container included  called: ***Afx-X11-Forward-NET-Service***. This extends the Base Container with functionality for the .NET runtime and all required libraries. It manages the development of a simple command-line example, including building, debugging, and running .NET applications using Visual Studio Code (VSC).

 <sub>***Data Locations, Within root 'APP-X11-Forward-Development-Template-Stack\'***</sub>

| **Folder**                                                     | **Purpose**                                                             |
|:--------------------------------                               |:------------------------------------------------------------------------|
| .\					                             			 | 'APP-X11-Forward-Development-Template-Stack' Root folder                | 
| .\Base-Container 					                             | Root folder for the Base Containers                                     |
| .\Base-Container\Afx-Base-Service                                  | The Base Container .The docker and compose file are here.               |
| .\Base-Container\Afx-Base-Service\wsl2distro                       | This contains the WSL environment after installation (.vhdx file).      |
| .\Sub Container  					                             | Root folder for the Sub Containers                                      |
| .\Sub-Containers\Afx-X11-Forward-NET-Service                   | Sample Sub Container with .NET with a simple CMD program, no GUI        |
| .\Sub-Containers\Afx-X11-Forward-NET-Service\.vscode           | These are the (prepared) settings of VSC when you open a container via the host. |
| .\Sub-Containers\Afx-X11-Forward-NET-Service\Project-Template  | Template used by Sub Container docker to create your app                |
| .\Sub-Containers\Afx-X11-Forward-NET-Service\Projects          | Bind mount folder, which you can use for your projects                  |
| .\Sub-Containers\Afx-X11-Forward-NET-Service\.env              | Settings use by the Docker Compose and Dockerfile                       |
| .\Howto's					                          			 | Documentation 											               | 


<br>

<details closed>  
 <summary class="clickable-summary">
 <span  class="summary-icon"></span> 
 **Side note**: Security Considerations and Network Configuration
 </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->
  
>### Security Considerations and Network Configuration <br>
>For personal use on a developer's PC or laptop, security risks are generally lower than in a production environment. However, it's still a good idea to follow some basic security practices. When running an X server (like vcxsrv) on your Windows host, configure vcxsrv to only accept connections from the local machine. This minimizes exposure and ensures that only applications on your PC can access the server. Additionally, keep your firewall enabled and set to block unsolicited incoming connections. While this setup is for development purposes and may not require strict security measures, these simple steps can help protect your system against unexpected threats, especially when connected to less secure networks.
<br>
In practice, this means that as a developer, you should leave the XLaunch **'Extra option' -> Disable access control** ***unchecked***
</details>



# 2. Create the Base Container
This chapter will cover the setup of the **Base Container Service** (folder: 'Base-Container') and everything else required to run a GUI application on a Windows host. At the end of this setup, we will demonstrate that the program works as expected by executing a sample X application in the Base Container (**xeyes**).

## 2.1 The Basic Container Setup
Before executing the Docker Compose file, ensure that the following items are installed and configured (refer to section 1.1, Process Architecture). The steps for these items will be explained in more detail in the following paragraphs:

<span class="nje-ident" style="--nje-number-of-spaces: 15px;" ></span>  <small>**Overview**</small>
- **Download the  WSL version of Ubuntu**: Obtain the special version of Ubuntu for WSL   ([Download)](https://learn.microsoft.com/en-us/windows/wsl/install-manual). Scroll to the bottom of the page for manual versions.
- **Install WSL2**: Set up a dedicated WSL2 environment to serve as the backend for the Docker container.
- **Configure the WSL Ubuntu Distribution**: Ensure that the WSL Ubuntu distribution is properly configured.
- **Install and Configure an X-Server**: Install an X-server on the Windows host; we use VcXsrv  ([Download](https://sourceforge.net/projects/vcxsrv/)) for this purpose.
- **Run Docker to Create the Basic Image**: Execute the Docker files to create the basic container image.
- **Verify the Setup**: Display the result to demonstrate that everything works correctly.


#### 2.1.1 Download the Special Ubuntu WSL version
Finding this version can be a bit challenging, especially because we need the manual installation files (with the .Appx or .AppxBundle extensions). The Windows Store provides a direct installer, but we cannot use it because we need to control the installation name and location. Follow these steps:
- ([Download](https://learn.microsoft.com/en-us/windows/wsl/install-manual)) the image from here, Scroll to almost the bottom where it states **'Downloading distributions'** and choose the *Ubuntu 24.04* link (note that this is the distribution  we support, you may try other ones and be fine with it, but we have not tested it)
- Now, as of Aug 2024, a lott of documentation\samples will state that your receive **\*.Appx** extension file and that you need to change the file to **\*.zip.**  But in our case you probably receive a **\*.AppxBundle** file which contains multiple Ubuntu versions. Below is shown how we get access to the right folder so we can install it in the next paragraph (in my case the download name is ***'Ubuntu2204-221101.AppxBundle'*** we use this name in our example:

  - First rename ***'Ubuntu2204-221101.AppxBundle'***' to ***'Ubuntu2204-221101.zip'***
  - Unpack the file with for example **7zip**
  - In the unpacked folder locate the file for your machine distribution ,likely ***'Ubuntu_2204.1.7.0_x64.appx'** rename this file to *.zip
  - Unpack the above renamed zip file
  - In the resulting folder you should see a file called ***'install.tar.gz'*** this is the location where the next command has to point to.

#### 2.1.2 Install the Ubuntu WSL version
When we have the distribution source, we can install the WSL environment. To keep the Base Container files in one place we do this in the root of our Base-Service folder ( **'./Base-Container/Afx-Base-Service/wsl2distro'***).
- **Open** the sub folder: '.\Base-Container\Afx-Base-Service\' with a CMD
- **Execute** this command and replace the ***install.tar.gz.file*** with the result from the previous step
<pre class="nje-cmd-one-line"> wsl --import Ubuntu-docker-App-X11 ./wsl2-distro  install.tar.gz </pre>
<span class="nje-ident"></span>This creates the **Ubuntu-docker-App-X11** WSL in the: **./wsl2-distro**. WSL **check** commands:

<pre class="nje-cmd-multi-line">

wsl --list --verbose    # Displays the distribution name, state, and there version
wsl --unregister YourDistributionName       # Remove the distribution
                                            # More WSL command in the next paragraph
</pre>

#### 2.1.3 Configure the Ubuntu WSL version
To start and manage your WSL2 Ubuntu distribution, use the following command:
<pre class="nje-cmd-multi-line">

wsl -d Ubuntu-docker-App-X11      #  This will open a CLI terminal and start the WSL if needed
                                  #  Use 'exit' to return to Windows. while it remains started
wsl --list --verbose              #  Optional. Check if it is running (in other Windows CMD)
wsl --terminate Ubuntu-docker-App-X11    #  Stops the distribution
wsl -d Ubuntu-docker-App-X11 -- ls /home #  Start, exec command, and returns direct(no CMD)
wsl --set-default Ubuntu-docker-App-X11  #  Set default when running command; wsl

</pre>
Next we need to update and configure our distribution. Make sure our distribution is started, and the execute the following Linux commands:
<pre class="nje-cmd-multi-line">

apt update && apt upgrade -y      # Update the Ubuntu distribution

# The next command will update our DISPLAY environment variable
export DISPLAY=$(grep -oP "(?<=nameserver ).+" /etc/resolv.conf):0

echo $DISPLAY                     # Display the variable afterwards: echo $DISPLAY

# Make sure the Variable the DISPLAY is set permanent in the .bashrc file
echo 'export DISPLAY=$(grep -oP "(?<=nameserver ).+" /etc/resolv.conf):0' >> ~/.bashrc
source ~/.bashrc                  # Reload

# Optional to logout and leave the wsl running
exit

</pre>

#### 2.1.4 Install the X-Server (VcXsrv)
To install the X-server and receive graphical output from the application, follow these instructions:
- [Download]( https://sourceforge.net/projects/vcxsrv/) and Install the VcXsrv software.
- After installation start XLaunch
  - Select **Multiple Windows** and click **Next**
  - Select Start **no client* and click **Next**
  - Ensure that **Clipboard** and **Native opengl** are **enabled**'
  - Ensure that **Disable access control** is **not enabled** ( this is more secure; only enable it if you encounter issues) click **Next**, then **Finish**


#### 2.1.5 Create the basic Docker Container
Finally, to create an start the base container.
- Open the sub folder: '.\Base-Container\Afx-Base-Service\' with a CMD
- We use a fixed IP address in the Compose file to make it easier to communicate with services, such as an SSH server (not used in this setup). While this is not strictly necessary, we have included it by default. If you encounter any issues, you may choose to remove it from the **compose_app_forward_x11_base.yml** file. The pre-configured IP address used can be found in the **.env** file. see:
<pre class="nje-cmd-one-line-sm-ident"> FIXED_SUBNET  # Default: 172.20.0.0/16            FIXED_IP      # Default: 172.20.0.15</pre>


- Execute this command in the service folder (**Net-X11-Service**)
<pre class="nje-cmd-one-line-sm-ident"> docker-compose -f compose_app_forward_x11_base.yml up -d --build --force-recreate  --remove-orphans </pre>

> *Warning!*{: style="color: red;font-size:13px; "} <br>
> <small> When recreating the same container(service name) avoid subtle/annoying caching issues, to avoid irritation, make sure to:</small>
> - <small> delete the container</small>
> - <small> delete the volume and </small
> - <small> use the Docker prune command,so: </small>
> <pre class="nje-cmd-one-line-sm-ident"> docker system prune -a --volumes</pre>


#### 2.1.6 Verify the Setup
- After running the command in 2.1.5 you can open **Docker Desktop** and in the container section a new container is created under the name: ***'afx -basic/axf-basic-service-1'***. Open a terminal session in this container
- Under the Images tab of Docker Desktop you should see the image **eelhart/appforwardx11-base** This is the image that will be used by the **Sub Containers** that you may create (see paragraph 3) to add different development environments to develop GUI applications. 
> *Warning!*{: style="color: red;font-size:13px; "} <br>
> <small> For the usage of the Sub Containers you may remove the container 'afx -basic/axf-basic-service-1' but you will need to hold on to the image **eelhart/appforwardx11-base**</small>

- Make sure the X-server(XLaunch) is started (see 2.1.4) 
- Enter the following command in the **Terminal shell**:
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



<details closed>  
  <summary class="clickable-summary">
  <span  class="summary-icon"></span> 
  **Side note**: Create Project from Template
  </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->
  
>### Create Project from Template
>>  <small> ***Skipp this if you known how to deal with copy\customize docker files*** </small> <br>
>
> To adapt the template directory for your project, follow these steps. This guide assumes you’re using the React stack; if you’re working with a different stack (e.g., PHP, Rust), simply replace “React” with the stack name your are using.
> - Copy the whole directory to a new directory (MyReactStack) for your project:
><pre class="nje-cmd-one-line"> copy "React Development Template Stack" MyReactStack </pre>
> - Within your **MyReactStack** open the ***[name]Service*** directory <br><br>
>**Warning**{: style="color: red;font-size:13px; "} <small>When using multiple containers, it's a good idea to rename this ***[name]Service*** directory (for example, by adding a number) before proceeding. Otherwise, the containers will be grouped together, which is generally helpful, but this can lead to caching issues in certain container stacks, such as React. These issues may manifest as the same directories appearing in the container from a previous instance after running the **compose_nodejs_react_cont.yml** command. Caching problems can be quite troublesome in some Docker stack configurations</small> 
>
> - Customize the Dockerfiles: Since most Docker Compose setups involve a parent-child relationship (i.e., chaining), a change in one Dockerfile requires updates to all related docker files.**Follow these steps:**
>> - In the first compose_\* file change the **'services name'** to an appropriate name for you:
>> <pre class="nje-cmd-one-line"> services: webserver-nodejs-react:	# Us always lowercase! </pre>
>> - The **'service name'** may appear more than once in the same file, update these as well!
>> - Changes the **'service name'** from step 1 in the other 'compose_\* files' 
>> - Check the compose_\* files when it contain a **image name** than update this to your own image name:
>><pre class="nje-cmd-multi-line">
context: .
	dockerfile: Dockerfile_Nodejs_React_Cont
	image: eelhart/react-base:latest  # i.e: [yourname/react-prjx]
>>
>></pre>
>  - Lastly, update the ports to ensure that each host port is unique across all running containers. In your Docker Compose file, you might see this configuration: <br>
><pre class="nje-cmd-multi-line">
ports:
  target: 3001        # Container port.
  published: 3002     # Host port, Make SURE it is unique	
>  
# Alternatively, the syntax might look like this (achieving the same result): 
ports:
  - "3002:3001"      # host:container  
>
></pre>
> **Make sure that Host port: 3002 is not used by any other docker container or other services on your host!**
<br><br>
</details>


# 3. Creating the Sub containers
This section includes several combinations of docker files and compose files, these can be used to create different **Sub containers**. Make sure you have **first** created the ***Base Container*** as described in paragraph 2!

In the folder ***Sub-Containers*** We store our sub containers in a separated folder, per service, currently there is only one:

- Afx-X11-Forward-NET-Service
This installs the .NET framework and the required libraries, and it will create a simple sample command-line application based on a template project.

## 3.1 creating the .NET Sub Container (Afx-X11-Forward-NET-Service)
This will create a basic .NET Command-line application that will return, guess? ...Yes!; "Hello World". This program does not output the text on the X11 Windows in the host. It is meant to prove that the **Base Container** can be extended, and to show how it can be done. In addition it will  create a project, based on a template, and demonstrate how to use Visual Studio Code (VSC) to Debug and run the application on the Windows host. ***See paragraph 4*** for VSC instructions.

> *Note:*{: style="color: Grey;font-size:13px; "}
> <small>We already have proven that that GUI output from the Linux application is displayed in our XLaunch Window on the Windows Host, if did not see this you can open a terminal in the Docker container and enter the command:</small>
><pre class="nje-cmd-one-line-sm-ident">xeyes</pre>
 

**Create the .NET Command Line Application project:** 
1. Open a CMD in the folder .\Sub-Containers\Afx-X11-Forward-NET-Service\
1. In the file ***.env*** file, set the variable **'PRJ_NAME_ARG'** to a value for your project name. Optional you can also set the environment variable from the command line, This value will be used for the project name and the project directory.If you omit this step the **default** will be used (see variable: **PRJ_NAME_ARG** in the ***.env*** file) 
<pre class="nje-cmd-one-line">$env:PRJ_NAME_ARG="my-project"		# From Command line </pre>
3 Then execute the docker command
<pre class="nje-cmd-one-line"> docker  compose -f compose_net_x11_project.yml up -d  --remove-orphans --build --force-recreate </pre>
<span class="nje-ident"></span>*Note that this compose creates and builds the project.*

### 3.2  Setup Result
- After running the commands in 3.1 you can open **Docker Desktop** and in the container section a new container is created under the name: ***'Afx-X11-Forward-NET-Service/afx-dotnet-container-1'***.
- Open a terminal session in this container
- Enter the following command in the terminal session : 
<pre class="nje-cmd-one-line-sm-ident">dotnet run</pre>
<span class="nje-ident"></span>The world famous text (for developers) should appear!





# 4 Develop with VSC in the host
To develop in **V**isual **S**tudio **C**ode we advice the following instructions 

### 4.1. Open the .NET application container in VSC (@host)
- Press CTRL-SHIFT-P or F1 and select (start typing) **Attach to running container...**
- Select our **net-x11-service-net-x11-1** container (If you did not rename the service nam).
This opens a new Window with all your container information

### 4.2. Open Folder and building your app.
- Use the **VSC Explorer** and the **Open Folder** to open a remote container folder, make **sure** that you open the correct folder to **make sure** the **.vscode** directory  settings are used. 
- Select open folder and type: **/projects/net/your_project_name** This makes sure the project is loaded along with settings created for the project in the folder .vscode

During the opening of the .NET container and the project root folder in Visual Studio Code, a dedicated Visual Studio Code server will be installed for the container. This server essentially provides the Visual Studio Code environment within the container, with its own settings and extensions. When you open a folder for the first time, the system will detect any required extensions and prompt you to install them. If prompted to install an extension, simply follow the instructions to complete the installation. As of August 2024, the following extensions are recommended for this project:
- C# Dev Kit

In case the intellisense indicates errors in one of the project files this is probably due to the missing of an extension.
<br>

<details closed>  
  <summary class="clickable-summary">
  <span  class="summary-icon"></span> 
  **Side note**: VSC Settings files
  </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->
  
>### VSC Settings files 
>
>**Settings file**
>
>To change or  the build targets and related settings you will need to update the files in the **.vscode directory** of the project (this is the reason you have to open the right project directory, opening the right directory make sure the project directory is the root and VSC searches for a '.vscode' directory in the root to apply settings to this project) the following settings files are available in '.vscode':
> - .vscode/tasks.json: For build tasks.
> - .vscode/launch.json: For debugging configurations.
> - .vscode/settings.json: For general VS Code settings specific to your project (e.g., editor preferences, linting settings).
In case you have to customize the build properties or other settings these files should be updated.

<br>
</details>

### 4.3 VSC Build tasks
In the menu **'Terminal -> Run Tasks...'** You can find the build task for our project, which are defined in the settings file (see side note above). The following are defined for our project:
- Clean: All Build Output
- Debug: Build Configuration
- Debug: Restores NuGet Packages
- Release: Build Configuration
- Release: Restore NuGet Packages

Most are self-explanatory. The **Restore NuGet Packages** step needs to be executed before the **Build Configuration** step to ensure that the required packages are downloaded before the build is executed (this is also done during the creation of the Docker container). If the Restore NuGet Packages step is not run first, you will see an error after building your app, apply in that case the restore packages command.
