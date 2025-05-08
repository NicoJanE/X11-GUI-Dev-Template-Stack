---
layout: default_c
RefPages:
 - howto_create_a_dev_container   
--- 

<small>
<br><br>
_Copyright (c) 2025 Nico Jan Eelhart_
_This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree._
</small><br><br>

# 1. X11 support in Docker Containers

<h3 style="color: #409EFF; font-size: 18px; font-style: italic; margin-top: 0;">Graphical output in the host via WSL</h3>

`X11-GUI-[name]` Docker containers forward their GUI output to the Windows host. This text  provides a short description describing what is required and how it works.

>📍**Note:**{: style="color: black;font-size:13px; "} <br>
> <small>In earlier versions of my `APP X11 Forward` GUI containers, I included instructions to install and configure a dedicated WSL distribution. **This is completely unnecessary**. The container works fine without it, as this guide will show.<br>
</small>

## Requirements

To enable X11 forwarding to Windows, the following is required:

- **Docker:** Docker Desktop must run using the WSL 2 backend <br>(*Enable via: Settings → General → Use the WSL 2 based engine*).
- **Host:** XLaunch must be running on the Windows host to receive and display X11 output.
- **Host:** Make sure Windows Firewall allows connections to port 6000 (default for X server).


## How It Works

When Docker starts (with the WSL2 backend enabled), it automatically creates and manages an internal WSL environment called `docker-desktop`.

Your Linux Docker container runs inside this environment, and because WSL2 supports interop with Windows, graphical output from the container can be forwarded to XLaunch (an X11 server for Windows).

To make this work, you need to set the DISPLAY environment variable in ***docker compose*** to:
<pre class="nje-cmd-multi-line">DISPLAY=host.docker.internal:0</pre>
This is a special DNS name that allows containers to reach the Windows host.This is critical because XLaunch runs on the Windows side and listens on display :0 (port 6000).

<br>
✅ Example: Docker Compose Snippet
<pre class="nje-cmd-multi-line">
services:
    x11-gui-basic-service:                        #  Our service
        build:
            context: .  
            dockerfile: Dockerfile_X11-Gui-Base
        image: eelhart/x11-gui-base:latest # This name can be used to extend or chain this image.
        networks:
            network_common_X11_gui:
              ipv4_address: ${FIXED_IP1}
        environment:
            - PORT=${PORT}
            - DISPLAY=host.docker.internal:0    # This relays Linux GUI output through WSL to XLaunch on the Windows host.
</pre>

End document.
