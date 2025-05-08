

**Announcement:** <br>
*This container stack replaces the stack **App X11 Forward GUI**, which is now deprecated and will be removed on **June 1, 2025.***   <br><br>
**What is new:** <br>
<small>🌟</small> *This new stack includes all the sub-containers previously available in App X11 Forward GUI.* <br>
<small>🌟</small> *The documentation has been simplified.* <br>
<small>🌟</small> *Irrelevant actions have been removed.*<br>
<small>🌟</small> *Names have been shortened.* <br>
<small>🌟</small> *Instructions have been improved for clarity and usability.* <br>

<hr>

# What
This is a Docker Linux (Ubuntu 24.04) general purpose template container with GUI output to an X11 server on a Windows host. The container is **designed** for use on a Windows Docker Desktop host, enabling the **development** of **GUI applications** within a **Docker container**. Additionally, the container can be used to run other Linux GUI applications.

This container consists of a **Base Container** and several **Sub Containers**. **The Base Container** is ***required*** for any **Sub Container** and provides the infrastructure for outputting GUI data from the Linux application to the X11 server on Windows. 

The **Sub Containers** contain development environments for GUI application running on Linux. 

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

 <br> <br>

<details closed>  
  <summary class="clickable-summary">
  <span  class="summary-icon"></span> 
  Side note: Preview Markdown Files(.md)
  </summary> 	<!-- On same line is failure, Don't indent the following Markdown lines!  -->

> <br>
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


<br><br>
<small>Version: 0.1 </small>