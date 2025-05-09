
# What
Sub container for the ***'X11-GUI-Dev-Template-Stack'*** development stack. Containing a project template for **.NET Core 8.x** in combination with **GTK 3.24.24**

> For more information about setting up this stack and/or this sub-container , visit the [main documentation page](https://nicojane.github.io/X11-GUI-Dev-Template-Stack/). You can find other Docker Template Stack (DTS) containers  [here.](https://nicojane.github.io/Docker-Template-Stacks-Home/)
>
> <sub> &nbsp;&nbsp;&nbsp;&nbsp; *Is this a local repository project? If so, use this local link to access the [main page](./index) file. <sub>
> <br>

### Quick setup .NET Gtk sub container
If you have previously installed this (or a similar) AFX foreward sub container, you can use these 'quick setup steps'. Otherwise please first read the **main documentation page**.


1. **Create the base container** &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <small>*(if not yet done*) </small> <br>
Create docker base container (in folder: 'Afx-Base-Service')
<pre class="nje-cmd-one-line">docker-compose -f  compose_app_forward_x11_base.yml up -d --build --force-recreate  --remove-orphans </pre><br>

2. ***Create the sub container*** <br>
Create the .NET GTK Sub container (in folder: 'Afx-X11-Forward-NET-Service-GTK#'): 
<pre class="nje-cmd-one-line">docker-compose -f compose_net_x11_gtksharp_project.yml up -d --build --force-recreate --remove-orphans  </pre><br>
 

3. **Start the sub-container in Visual Studio Code**<br>
Install the following extension(s) in the container
<pre class="nje-cmd-multi-line">code --install-extension ms-dotnettools.csharp
code --install-extension ms-dotnettools.csdevkit
~~code --install-extensionms-dotnettools.vscode-dotnet-runtime~~
</pre>
<br>

### The project Template and specific tasks in VSC
**Project structure** <br>
After opening your project you find a **'src'** folder with the following structure: <br>
- **src** -->> *root source folder*
  - **backend** -->> *.NET class libraries*. <small>**See VSC task: '2.1 AFX ...' below**</small>
    - **cl_example** -->> *simple sample class library*
    - **cl_example** -->> *other sample class library, with simple Rest example*
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
 <br><span class="nje-ident" style="--nje-number-of-spaces: 30px;"></span> * That the class libraries are created in a **folder** with that name on **src\backend\cl_yourname***
 <br><span class="nje-ident" style="--nje-number-of-spaces: 30px;"></span> * During the build process the **cl_** is replaced with with **'lib_'**  resulting into library: ***lib_yourname***
<br><span class="nje-ident" style="--nje-number-of-spaces: 30px;"></span> * The **clean tasks 4.x** wil work properly, because they act on these names!
 </small> 

<br>

<details closed>  
  <summary class="clickable-summary">
  <span  class="summary-icon"></span> 
  Side note: Preview Markdown Files(.md)
  </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->

> <br>
> 
> ### Preview Markdown Files(.md)
>
>To preview the Markdown (.md) files in this project, one of the best solutions is to open these files in Visual Studio Code (VSC) and install the plugin: **Markdown Preview GitHub Styling** (Tested with version 2.04). Other plugins, or plugins for other programs, may not always work correctly with the file links in the documentation. I use the file link syntax supported by GitHub (Jekyll), which is also compatible with the above-mentioned plugin.
>
> To display the Preview screen in VSC: 
>- Ensure that you are **not** working in ***Restricted mode***.
>- Click on the "file.md" tab and choose: "Open preview." 
>- Alternatively, you can click the 'Open Preview to the Side' button at the top right. 
>
><br>
<a href="https://github.com/mjbvz/vscode-github-markdown-preview-style" target="_blank">Click here for more information on the Markdown Preview GitHub Styling plugin</a>
</details>


<br><br>
*Version: 1.1