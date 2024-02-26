# VR Accessibility SDK
## Overview
The VR Accessibility SDK is a toolkit designed to make virtual reality experiences more accessible to users with visual impairments. It provides a set of tools and features that developers can integrate into their VR applications to improve accessibility and ensure that all users can enjoy their experiences.

## Features
* **Partial Vision Tool**: Allows users to target objects to bring up descriptive text menus.
* **Alt-Text Generation**: Allows developers to add alt-text descriptions to GameObjects via editor script; features both manual and automated editor tools.
* **Text-to-Speech (TTS)**: Provide spoken feedback for text-based content to assist users with visual impairments.
* **Alt-Text Linting**: Checks for presence, completeness, and clarity among alt-text fields among GameObjects.

## Installation
1. Download the Accessibility in VR Unity SDK from GitHub.
2. Import the SDK package into your Unity project.
    a. Add the **'AccessibilityTags.cs'** script to project's **/Scripts** folder.
    b. Add the editor scripts in the **/Editor** folder to project's **/Editor** folder.
    c. Add the **'PartialVis.cs'** script to project's **/Assets** folder.
    
## Usage
* Use **'Tools'/'Add Accessible Field(s) to entire scene'** command in upper Unity menu bar to add appropriate fields to all GameObjects in scene
* Use **[RMB] -> 'Add Accessible Field(s)'** command to add fields to selected GameObject * *Will create separate buttons for each field*
* Add an instance of the Partial Vision tool to scene*?*
* Set up input command for Partial Vision tool*?*

## License
?

## Contact
For questions, feedback, or support, contact us at [ju691930@ucf.edu](ju691930@ucf.edu).