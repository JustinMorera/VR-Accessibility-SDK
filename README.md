# VR Accessibility SDK
## Overview
The VR Accessibility SDK is a toolkit designed to make virtual reality experiences more accessible to users with visual impairments. It provides a set of tools and features that developers can integrate into their VR applications to improve accessibility and ensure that all users can enjoy their experiences.

## Features
* **Partial Vision Tool**: Allows users to target objects to bring up descriptive text menus.
* **Alt-Text Generation**: Allows developers to add alt-text descriptions to GameObjects via editor script; features both manual and automated editor tools.
* **Text-to-Speech (TTS)**: Provide spoken feedback for text-based content to assist users with visual impairments.
* **Alt-Text Linting**: Checks for presence, completeness, and clarity among alt-text fields among GameObjects.

## Installation
### Manual Installation:
1. Download the Accessibility in VR Unity SDK from GitHub.
2. Insert the SDK package files into your Unity project:
    a. Open project in Unity.
    b. Right-click **/Packages** folder in lower menu's "Project" tab.
    c. Select "Show in Explorer"
    d. Add the **VR-Accessibility-SDK** folder to project's **/Packages** directory.
    e. Allow Unity to reload the project.
    
### Install via Unity Package Manager import feature:
1. Navigate to **'Window -> Package Manager'** using the tabs on the top of the Unity platform.
2. In the Package Manager window, press the **'+'** icon on the top-left and press *'Add package from git URL...'* and enter this project's URL, *'https://github.com/JustinMorera/VR-Accessbility-SDK.git'*.
3. The package will automatically install.
    
## Usage
* Use **'Tools'/'Add Accessible Field(s) to entire scene'** command in upper Unity menu bar to add appropriate fields to all GameObjects in scene.
* Use **'Tools'/'Check Alt-Text'** command in upper Unity menu bar to Alt-Text of all GameObjects in scene for completeness and redundancy.
* Use **'Tools'/'Remove Accessible Field(s) from entire scene'** command in upper Unity menu bar to remove all fields from all GameObjects in scene.
* Use **[RMB] -> 'Add Accessible Field(s)'** command to add fields to selected GameObject
* To use Partial Vision Tool in scene:
    1. Add an instance of the *Partial Vision Assistance* prefab tool to scene.
    2. Set input command for Partial Vision tool using Unity's Input System.
    3. Under *Partial Vision Assistance*'s *Partial Vis* script:
        a. Select a Raycast Origin corresponding to the player's right-controller (recommended), or any other object.
        b. Set an Input System button reference corresponding to the previously set input command.
    4. Under *Partial Vision Assistance*'s *Menu Manager* script:
        a. Select a Head corresponding to the player's camera viewpoint (recommended), or any other object.
        b. Set Input System Display and Hide references corresponding to the previously set input command, or any other desired Input System references.

## Requirements
* Unity's **'InputSystem'** and **'XR.Interaction'** modules must be installed to the scene in order for the Partial Vision Tool to function.
    * The modules can be installed by following these steps:
        1. Open the Unity Editor.
        2. Go to **Window > Package Manager**.
        3. In the Package Manager window, navigate to the **"Packages: ..."** drop down menu and select **"Unity Registry"**.
        4. Navigate to **"Input System"** or **"XR Interaction Toolkit"** package. (Or select the **'+'** in the top-left and select **"Add package by name"** and enter "com.unity.xr.interaction.toolkit" for XR Interaction Toolkit)
        5. If the package is not installed, click on the "Install" button next to it to install it into the project.
* Oculus Voice SDK must be installed to the scene in order for the Text-to-Speech functionality to take effect.
    * 

## License
?

## Contact
For questions, feedback, or support, contact us at [ju691930@ucf.edu](ju691930@ucf.edu).