# X11 General GUI Development <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  Docker Container</span>

**What is new:** <br>
<small>🌟</small> *This new stack includes all the sub-containers previously available in App X11 Forward GUI.* <br>
<small>🌟</small> *The documentation has been simplified.* <br>
<small>🌟</small> *Irrelevant actions have been removed.*<br>
<small>🌟</small> *Names have been shortened.* <br>
<small>🌟</small> *Instructions have been improved for clarity and usability.* <br>

## ℹ️ Introduction

This is a Docker Linux (Ubuntu 24.04) general purpose template container with GUI output to an X11 server on a Windows host. The container is **designed** for use on a Windows Docker Desktop host, enabling the **development** of **GUI applications** within a **Docker container**. Additionally, the container can be used to run other Linux GUI applications.

This container consists of a **Base Container** and several **Sub Containers**. **The Base Container** is ***required*** for any **Sub Container** and provides the infrastructure for outputting GUI data from the Linux application to the X11 server on Windows.

The **sub-containers** provide development environments for GUI applications running on Linux.

### Available containers

| ***Base-container name***            | ***Reference*** |
|:-----------------                   |:----------------|
| Full instructions (including required  Base container)         | [here](https://nicojane.github.io/X11-GUI-Dev-Template-Stack/) |

You can choose to install one or more sub-containers.

| ***Sub-container name***            | ***Reference*** |
|:-----------------                   |:----------------|
| X11 NET Service         | [here](https://nicojane.github.io/X11-GUI-Dev-Template-Stack/Howtos/howto_create_a_dev_container.html#31-creating-the-net-sub-container-afx-x11-forward-net-service) |
| X11 Avalonia Service    | [here](https://nicojane.github.io/X11-GUI-Dev-Template-Stack/Howtos/howto_create_a_dev_container.html#32-creating-an-avalonia-net-sub-container-afx-x11-forward-avalonia-service)|
| X11 NET Service GTK#    | [here](https://nicojane.github.io/X11-GUI-Dev-Template-Stack/Howtos/howto_create_a_dev_container.html#33-creating-a-net-gtk-sub-container-afx-x11-forward-net-service-gtk)  |
| X11 PHP Python Rust     | [here]( https://nicojane.github.io/X11-GUI-Dev-Template-Stack/Howtos/howto_create_a_dev_container.html#35---sub-container-slintsdl2-c-and-python)  |
| X11 Slint C++ Python    | [here](https://nicojane.github.io/X11-GUI-Dev-Template-Stack/Howtos/howto_create_a_dev_container#35---sub-container-slintsdl2-c-and-python)  |

 <br> <br>

<details closed>  
  <summary class="clickable-summary">
  <span  class="summary-icon"></span>
  Side note: Preview Markdown Files(.md)
  </summary> <!-- On same line is failure, Don't indent the following Markdown lines!  -->

>
> ### Preview Markdown Files(.md)
>
>To preview the Markdown (.md) files in this project, one of the solutions is to open these files in Visual Studio Code (VSC) and install the plugin: **Markdown Preview GitHub Styling** (Tested with version 2.04). Other plugins, or plugins for other programs, may not always work correctly with the file links in the documentation. I use the file link syntax supported by GitHub (Jekyll), which is also compatible with the above-mentioned plugin.
>
> To display the Preview screen in VSC: 
>- Ensure that you are **not** working in ***Restricted mode***.
>- Click on the "file.md" tab and choose: "Open preview."
>- Alternatively, you can click the 'Open Preview to the Side' button at the top right.
>
><br>
<a href="https://github.com/mjbvz/vscode-github-markdown-preview-style" target="_blank">Click here for more information on the Markdown Preview GitHub Styling plugin</a>
</details>

<p align="center">
  <a href="https://nicojane.github.io/Docker-Template-Stacks-Home/">
    <img src="assets/images/DTSfooter.svg" alt="DTS Template Stacks" width="400" />
  </a>
</p>

<sub><i> This file is part of:  X11 GUI Development Template Stack
Copyright (c) 2025 Nico Jan Eelhart. This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree.
</i></sub>


<p align="center">─── ✦ ───</p>