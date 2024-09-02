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
This is a Docker Linux (Ubuntu 24.04) template container with GUI output to an X11 server on a Windows host. The container is **designed** for use on a Windows Docker Desktop host, enabling the **development** of **GUI applications** within a **Docker container**. Note that, the container can be used to run any other Linux GUI applications in a container.

This container consisy of **Base Container** and a number of **Sub Containers**. The base container is required for any Sub Container and provides the infrastructure for outputing GUI data, of the Linux Application on the X11 Windows Window. The Sub Containers contain at least the GUI application that run on Linux, and in our cases this Sub Container also includes the development environment with which the application is created.

This container consists of a Base Container and several Sub Containers. The **Base Container** is required for any **Sub Containers** and provides the infrastructure for outputting GUI data from the Linux application to the X11 server on Windows. The **Sub Containers** contain at least one GUI application that runs on Linux. In our case, these **Sub Containers** also include the development environment in which the application is created.


## Quick Setup
There are no quick setup instructions for this image, one need first to install the Base-Container\Image, which takes about 20 minutes, afterwards one can add Sub Containers fairly easy. Please refer to the I document [how to create a development container](./Howtos/howto_create_a_dev_container). This explains the installation of Bas Container and the any available Sub Containers.



