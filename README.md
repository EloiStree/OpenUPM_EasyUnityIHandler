# OpenUPM: Easy Unity IHandler

This tool is designed for quickly creating XR interactions without relying on complex toolkits. If all you need is a simple sphere that interacts with Unity’s IHandler, this is the solution.  

The concept is straightforward:  

- Currently, the tool provides a basic sphere for interaction.  
- A "gaze" laser feature will be added when time permits.  

While there are plenty of tools that achieve similar results, I wanted something simple and fully "owned" to keep my code open source and flexible.  

This tool wraps Unity3D's default interfaces used for mouse and input interactions and extends them to support touch, gaze, and watch-based interactions—focusing on the most common XR use cases that don’t involve grabbing.  

For grabbing interactions, tools like MRTK, VRTK, and XRTK are already available and well-suited for that purpose.
