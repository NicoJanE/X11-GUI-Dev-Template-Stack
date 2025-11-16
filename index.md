---
layout: default_c
RefPages:
 - howto_create_a_dev_container
--- 
<span class="nje-br1"> </span>
# X11 General GUI Development <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  Docker Container</span>

## ℹ️ Introduction

This Docker template provides a **comprehensive Linux development environment** (Ubuntu 24.04) that enables GUI application development within Docker containers while **displaying output on Windows hosts** via X11 forwarding.

<div class="nje-features-box">
- Develop GUI applications in a **containerized Linux environment**
- Display application output **seamlessly on Windows** through X11 server integration
- Support for **multiple programming languages** and frameworks
- **Modular architecture** with base and specialized containers
</div>

<span class="nje-br1"> </span>

<div class="nje-architecture-box">
The system uses a **two-tier container structure**:

- **Base Container** (*required*{: style="color: red"}): Provides the core X11 forwarding infrastructure and GUI display capabilities
- **Sub Containers**: Specialized development environments for specific languages and frameworks (NET, Avalonia, C++, Python, PHP, Rust)
</div>
<span class="nje-br2"> </span>

<hr>

## Setup Available containers

The following containers are available, press the ***Setup Guide***  button to get the instructions for the page. Press [**this**](https://nicojane.github.io/X11-GUI-Dev-Template-Stack/Howtos/howto_create_a_dev_container) for the **complete instructions**.

<div class="nje-table-base-span">
<span class="nje-table-row">
        <span class="nje-column1-value">Required Base Container </span>
        <span class="nje-column2-desc">Core X11 forwarding infrastructure and GUI display capabilities</span>
        <span class="nje-column3-button"> 
            <a href="./Howtos/howto_create_a_dev_container#the-basic-container-setup">Setup Guide</a>
        </span>
 </span>
 </div>

<div class="nje-table-sub-span">
<span class="nje-table-row">
    <span class="nje-column1-value">X11 Forward NET Service</span>
    <span class="nje-column2-desc">.NET development environment with GUI support</span>
    <span class="nje-column3-button"> 
        <a href="./Howtos/howto_create_a_dev_container#31-creating-the-net-sub-container-afx-x11-forward-net-service">Setup Guide</a>
    </span>
</span>
<span class="nje-table-row">
    <span class="nje-column1-value">X11 Forward Avalonia Service</span>
    <span class="nje-column2-desc">Avalonia UI framework for cross-platform applications</span>
    <span class="nje-column3-button">
        <a href="./Howtos/howto_create_a_dev_container#32-creating-an-avalonia-net-sub-container-afx-x11-forward-avalonia-service">Setup Guide</a>
    </span>
</span>
<span class="nje-table-row">
    <span class="nje-column1-value">X11 Forward NET Service GTK# </span>
    <span class="nje-column2-desc">.NET development with GTK# framework</span>
    <span class="nje-column3-button">
        <a href="./Howtos/howto_create_a_dev_container#33-creating-a-net-gtk-sub-container-afx-x11-forward-net-service-gtk">Setup Guide</a> 
    </span>
</span>
<span class="nje-table-row">
    <span class="nje-column1-value">X11 Forward PHP Python Rust#</span>
    <span class="nje-column2-desc">Multi-language environment (PHP, Python, Rust)</span>
    <span class="nje-column3-button">
        <a href="./Howtos/howto_create_a_dev_container#34---a-phppythonrust-sub-container">Setup Guide</a>
    </span>
</span>
<span class="nje-table-row">
    <span class="nje-column1-value">X11 Slint C++ Python</span>
    <span class="nje-column2-desc">Slint UI with C++ and Python development</span>
    <span class="nje-column3-button">
        <a href="./Howtos/howto_create_a_dev_container#35---sub-container-slintsdl2-c-and-python">Setup Guide</a>
    </span>
</span>
</div>

<span class="nje-br2"> </span>
<hr>

### Latest major changes

<small>🌟</small> *New documentation style has been applied.*{: style="color: Darkgray;font-size:13px; "} <br>
<small>🌟</small> *The documentation has been simplified.*{: style="color: Darkgray;font-size:13px; "} <br>
<small>🌟</small> *Irrelevant actions have been removed.*{: style="color: Darkgray;font-size:13px; "}<br>
<small>🌟</small> *Names have been shortened.*{: style="color: Darkgray;font-size:13px; "} <br>
<small>🌟</small> *Instructions have been improved for clarity and usability.*{: style="color: Darkgray;font-size:13px; "} <br>

<span class="nje-br3"> </span>

<sub><i> This file is part of:  X11 GUI Development Template Stack
Copyright (c) 2025 Nico Jan Eelhart. This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree.
</i></sub>


<p align="center">─── ✦ ───</p>
