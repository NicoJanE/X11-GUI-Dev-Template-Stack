---
layout: default_c
RefPages:
 - howto_create_a_dev_container
--- 

<small>
<br>
_This file is part of: **App-X11-Forward Development Template Stack**_
_Copyright (c) 2024 Nico Jan Eelhart_
_This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree._
</small>
<br><br>

# What
This is a Docker Linux (Ubuntu 24.04) template container with GUI output to an X11 server on a Windows host. The container is **designed** for use on a Windows Docker Desktop host, enabling the **development** of **GUI applications** within a **Docker container**. Additionally, the container can be used to run other Linux GUI applications.

This container consists of a **Base Container** and several **Sub Containers**. **The Base Container** is required for any **Sub Container** and provides the infrastructure for outputting GUI data from the Linux application to the X11 server on Windows. The **Sub Containers** contain development environments for GUI application running on Linux. 

**Available Sub Containers**
- A generic .NET container with a basic Command Application template.
- An Avalonia project container, with Avalonia GUI project template, forwards X11 GUI to Windows host
- A .NET project container in combination with GTK#, forwards X11 GUI to Windows host


### Quick setup
If you have previously installed this (or a similar) AFX Forward container, you can use the quick setup steps below. Otherwise please first read the **main documentation page**. Also the Sub Containers may contain there own **'README.md'** file for instructions to add the sub container to this base container of this project.
- In case you don't have the **WSL** container, open CMD in the folder: ***'APP-X11-Forward-Development-Template-Stack\Base-Container\Afx-Base-Service\'*** and execute:
<pre class="nje-cmd-one-line"> wsl --import Ubuntu-docker-App-X11-Win32Dev ./wsl2-distro  "install.tar.gz"  </pre>
By default the WSL image is created in the sub folder of the current directory (./wsl2-distro) you may choose to store this image more **central**, for example like in 'd:\wsl-data\afx-stacks', this 
way you can **reuse** this WSL distribution for different **AFX stacks**
- Create docker base container (in folder: 'Afx-Base-Service') if not this is not yet done
 <pre class="nje-cmd-one-line">docker-compose -f  compose_app_forward_x11_base.yml up -d --build --force-recreate  --remove-orphans </pre>
 - Install a sub-container, for example: Afx-X11-Forward-NET-Service-GTK# (in folder: 'Sub-Containers\Afx-X11-Forward-NET-Service-GTK#\')
  <pre class="nje-cmd-one-line">docker-compose -f compose_net_x11_gtk#__project.yml up -d --build --force-recreate --remove-orphans  </pre>
  - Start the WSL and attach a sub container via the  interface docker in the WSL
  <pre class="nje-cmd-multi-line">
# Start WSL
wsl -d Ubuntu-docker-App-X11-Win32Dev  

# Attach docker
docker exec -it PyCRust-Project-Service /bin/bash 
# If the container cannot be found, restart the Docker app and ensure 
# WSL integration is enabled in Docker settings!
</pre>
<br>

> **More information!**{: style="color:#5491fa;font-size:13px; "} <br>
<small>
See the main documentation page [how to create a development container](./Howtos/howto_create_a_dev_container). Which explains the installation of the *Base Container* and any available *Sub Containers* in more details.
</small>



