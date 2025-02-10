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

#### Available Sub-container

| ***Container name***                | ***Quick reference (if available)*** |
|:-----------------                   |:----------------|
| Afx-X11-Forward-NET-Service         | . |
| Afx-X11-Forward-Avalonia-Service    | . |
| Afx-X11-Forward-NET-Service-GTK#    | [Specific quick setup](https://nicojane.github.io/APP-X11-Forward-Development-Template-Stack/Sub-Containers/Afx-X11-Forward-NET-Service-GTK%23/index.html)  |
| Afx-X11-Forward-PHP-Python-Rust#    | [Specific quick setup](https://nicojane.github.io/APP-X11-Forward-Development-Template-Stack/Sub-Containers/Afx-X11-PyPHP-Service/quick-setup)  |



### Quick setup (general)
This is the general quick setup procedure. In the sub-container project you may find index.md file with specific quick setup instruction in case these instruction make you feel insecure.

If you have previously installed this (or a similar) AFX Forward container, you can use the quick setup steps below. Otherwise please first read the main documentation page [howto_create_a_dev_container](Howtos/howto_create_a_dev_container.md). or refer to one of the 'index.md' files in the sub-container, when available it contains detailed instructions for the specific sub-container  


1) **Create the WSLs**{: style="color:green; "} <br>
In case you don't have the **WSL** container, open CMD in a base container folder, for example: ***'APP-X11-Forward-Development-Template-Stack\Base-Container\Afx-Base-Service\'*** and execute:
<pre class="nje-cmd-one-line"> wsl --import Ubuntu-docker-App-X11-Win32Dev ./wsl2-distro  "install.tar.gz"  </pre>

> *Remark:*{: style="color: black;font-size:13px; "} <br>
> <small>By default the WSL image is created in the sub folder of the current directory (./wsl2-distro) you may choose to store this image more **central**, for example like in 'd:\wsl-data\afx-stacks', this way you can **reuse** this WSL distribution for different **AFX stacks**  <br>
</small>

2) **Create docker base container (Afx-Base-PyCRust-Service)**{: style="color:green; "} <br>
Use the following to create the basic docker base container, on which the sub-containers depend
 <pre class="nje-cmd-one-line">docker-compose -f  compose_app_forward_x11_base.yml up -d --build --force-recreate  --remove-orphans 
 </pre><br>
3) **Create a specific  docker sub-container**{: style="color:green; "} <br>
 Create a Command Line Shell in the correct sub-container folder (i.e. Sub-Containers\Afx-X11-Forward-NET-Service-GTK#) and execute:
<pre class="nje-cmd-one-line">docker-compose -f compose_net_x11_gtk#__project.yml up -d --build --force-recreate --remove-orphans  
</pre> <br>

4) **Start the Docker sub container via the WSL**{: style="color:green; "} <br>
Execute the following commands: 
<pre class="nje-cmd-multi-line">wsl -d Ubuntu-docker-App-X11-Win32Dev 
docker exec -it afx-x11-forward-net-service-gtk-axf-dotnet-gtksharp-container-1 /bin/bash   # i.e for NET GTK#
</pre>
> *Warning:*{: style="color: red;font-size:13px; "} <br>
> <small>When  the container cannot be found, restart the Docker app and ensure WSL integration is enabled in Docker settings! <br></small> <br>

5) **Configure and start the sub container in  Visual Studio Code (VSC)**{: style="color:green; "}<br>
After this you should be able to open the container in VSC and start developing, be sure to run the required extension commands(in the container)  

> **More information!**{: style="color:#5491fa;font-size:13px; "} <br>
<small>
See the main documentation page [how to create a development container](./Howtos/howto_create_a_dev_container). Which explains the installation of the *Base Container* and any available *Sub Containers* in more details.
</small>

