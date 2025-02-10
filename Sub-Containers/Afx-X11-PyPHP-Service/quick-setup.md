---
layout: default_c
RefPages:
 - howto_create_a_dev_container
--- 

<small>
<br>
_ This file is part of: APP-Forward-X11-Development-Template-Stack_
_Copyright (c) 2025 Nico Jan Eelhart_
_This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree._
</small>
<br><br>


### Quick setup PyPHP sub container
When you have previously installed this,or any other sub-container, you can use the quick setup steps below. Otherwise please first read the [how to create a development container](https://nicojane.github.io/APP-X11-Forward-Development-Template-Stack/Howtos/howto_create_a_dev_container) document. <br>

1) **Create the WSLs**{: style="color:green; "} &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <small>*(if not yet done*) </small>      
In case you don't have the **WSL** container, open CMD in the folder: *'APP-X11-Forward-Development-Template-Stack\Base-Container\Afx-Base-Service\'* and execute:
<pre class="nje-cmd-one-line"> wsl --import Ubuntu-docker-App-X11-Win32Dev ./wsl2-distro  "install.tar.gz"  </pre>

> *Remark:*{: style="color: black;font-size:13px; "} <br>
> <small>By default the WSL image is created in the sub folder of the current directory (./wsl2-distro) you may choose to store this image more **central**, for example like in 'd:\wsl-data\afx-stacks', this way you can **reuse** this WSL distribution for different **AFX stacks**  <br></small>


2) **Create docker base container (Afx-X11-PyPHP-Service\)**{: style="color:green; "} &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <small>*(if not yet done*) </small> <br>
Create docker base container (in folder: '.\Base-Container\Afx-Base-Service\')
<pre class="nje-cmd-one-line">docker-compose -f  compose_app_forward_x11_base.yml up -d --build --force-recreate  </pre><br>

3) ***Create the sub container (Afx-X11-PyPHP-Service\)***{: style="color:green; "} <br>
Create the Sub container (in folder: '.\Sub-Containers\Afx-X11-PyPHP-Service\'): 
<pre class="nje-cmd-one-line">docker  compose -f compose_Python-Apache-php.yml up -d  --remove-orphans --build --force-recreate  </pre><br>
 
4) **Start the Docker sub container via the WSL**{: style="color:green; "} <small>*(optional*) </small> <br>
Execute the following commands: 
<pre class="nje-cmd-multi-line">wsl -d Ubuntu-docker-App-X11-Win32Dev 
docker exec -it afx-x11-pyphp-service-afx-python-apache-php-1 /bin/bash 
</pre>
> *Warning:*{: style="color: red;font-size:13px; "} <br>
> <small>When  the container cannot be found( with `Docker ps`), restart the Docker app and ensure WSL integration is enabled in Docker settings! <br></small>

5) **Start the sub-container in Visual Studio Code**{: style="color:green; "}<br>
After this you should be able to open the container in VSC and start developing, 
- F1 Dev containers: Attach to running container
- select afx-x11-pyphp-service-afx-python-apache-php-1
- Open the folder: /projects/pyphp/[your project name]

6) **Required extensions** 
Also be sure to run the following commands(in the container) first to make sure the required extensions are installed: 
<pre class="nje-cmd-multi-line">
code --install-extension ms-python.vscode-pylance
code --install-extension ms-python.python
code --install-extension ms-vscode.makefile-tools
code --install-extension rust-lang.rust-analyzer
code --install-extension ms-vscode.cpptools-extension-pack
code --install-extension ms-python.debugpy
code --install-extension xyz.local-history
code --install-extension DEVSENSE.composer-php-vscode
code --install-extension DEVSENSE.intelli-php-vscode
code --install-extension DEVSENSE.phptools-vscode
code --install-extension xdebug.php-debug
code --install-extension DEVSENSE.profiler-php-vscode
</pre>

7) **Test the Docker container and check the config files**
The following commands should working after the container is created and started.
In the Docker CLI:
- `phpunit --version          # Should return version of Phpunit`
- `composer -V		        # Should return version of Composer`

<br>

In the web browser at the host system
- ` http://localhost:8071/web-app/phpinfo.php`
- ` http://localhost:8071/web-app/home/index.php`
- ` http://localhost:8071/web-rest/wr-api.php?name=John`
- ` http://localhost:8071/web-rest/test-client/client.php`

<br>

Config files & Log files
- Log files in: /opt/www/log
- httpd.conf:    ->  /opt/apache2/conf
-  php.ini       ->  /etc/php/8.2/fpm/

<br>

8) **Create a extra Docker CLI**
- `docker ps    # get the docker "container name" or ID`
- `docker exec -it [container name] /bin/bash`
<br><br>
