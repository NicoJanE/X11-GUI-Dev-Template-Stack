---
layout: default_c
RefPages:
 - howto_create_a_dev_container
--- 

<small>
<br>
_This file is part of: **X11-GUI-Development-Template-Stack**_
_Copyright (c) 2025 Nico Jan Eelhart_
_This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree._
</small>
<br><br>

> **Announcement:**{: style="color: blue;font-size:12px; "} <br>
*This container stack replaces the stack **App X11 Forward GUI**, which is now deprecated and will be removed on **June 1, 2025.***{: style="color: Darkgray;font-size:12px; "}   <br><br>
**What is new:**{: style="color: Darkgray;font-size:12px; "} <br>
<small>🌟</small> *This new stack includes all the sub-containers previously available in App X11 Forward GUI.*{: style="color: Darkgray;font-size:12px; "} <br>
<small>🌟</small> *The documentation has been simplified.*{: style="color: Darkgray;font-size:12px; "} <br>
<small>🌟</small> *Irrelevant actions have been removed.*{: style="color: Darkgray;font-size:12px; "}<br>
<small>🌟</small> *Names have been shortened.*{: style="color: Darkgray;font-size:12px; "} <br>
<small>🌟</small> *Instructions have been improved for clarity and usability.*{: style="color: Darkgray;font-size:12px; "} <br>

<hr>

# What
This is a Docker Linux (Ubuntu 24.04) template container with GUI output to an X11 server on a Windows host. The container is **designed** for use on a Windows Docker Desktop host, enabling the **development** of **GUI applications** within a **Docker container**. Additionally, the container can be used to run other Linux GUI applications.

This container consists of a **Base Container** and several **Sub Containers**. **The Base Container** is *required*{: style="color: red"} for any **Sub Container** and provides the infrastructure for outputting GUI data from the Linux application to the X11 server on Windows. The **Sub Containers** contain development environments for GUI application running on Linux. 

### Available containers

| ***Base-container name***            | ***Reference*** |
|:-----------------                   |:----------------|
| Required Base container instructions         | [here](./Howtos/howto_create_a_dev_container#the-basic-container-setup) |

<br>

| ***Sub-container name***            | ***Reference*** |
|:-----------------                   |:----------------|
| Afx-X11-Forward-NET-Service         | [here](./Howtos/howto_create_a_dev_container#31-creating-the-net-sub-container-afx-x11-forward-net-service) |
| Afx-X11-Forward-Avalonia-Service    | [here](./Howtos/howto_create_a_dev_container#32-creating-an-avalonia-net-sub-container-afx-x11-forward-avalonia-service)|
| Afx-X11-Forward-NET-Service-GTK#    | [here](./Howtos/howto_create_a_dev_container#33-creating-an-net-gtk-sub-container-afx-x11-forward-net-service-gtk)  |
| Afx-X11-Forward-PHP-Python-Rust#    | [here](./Howtos/howto_create_a_dev_container#34---a-phppythonrust-sub-container)  |
