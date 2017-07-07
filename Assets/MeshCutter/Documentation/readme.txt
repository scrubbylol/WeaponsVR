Mesh Cutter - Cut, Copy and Paste Parts of Meshes
v1.4 - Released 29/04/2016 by Alan Baylis
----------------------------------------


Foreword
----------------------------------------
Thank you for your purchase of Mesh Cutter. This program allows you to now quickly select different parts of the mesh and cut, copy, paste or remove them. The program will preserve the materials and submeshes of the large mesh and create a new mesh and prefab whenever you cut or copy a part, transferring the materials and submeshes to the new object. You can also re-center and adjust the pivot point of the new object to use it elsewhere in your projects.

I hope you find many uses for Mesh Cutter and if you have a problem, question or a suggestion please contact me at support@meshmaker.com


Notes
----------------------------------------
This version allows you to select all triangles that have the same material. You can select the triangles of more than one material by holding down the hotkey when clicking on the Select button.
  
The default hotkey is currently set to the Left-Ctrl key but you can change this in the settings options and the program will remember your preferences when it closes. On the Mac the command key is set to be a permanent hotkey.

If you are working on a mesh with negative scale values and you have selected Parent With Original Mesh then you will run into an issue where the child object will act like it is snapping to the grid. This is standard behavior in Unity and it is discussed here:
http://answers.unity3d.com/questions/835786/negative-scale-toggles-grid-snapping-on-children.html
and here:
http://forum.unity3d.com/threads/collider-issues-with-negative-scale.129975/

In Unity 5 it is recommended to turn of continuous baking of lightmaps by going to the menu and navigating to Window/Lighting/Lightmaps and unchecking the checkbox. This will prevent lag and stuttering in the editor.


Todo List.
----------------------------------------
Done - Skinned meshes.
Done - Selection of triangles based on materials used and their UV coordinates.
Cut triangles with user defined shapes.


Attributions
----------------------------------------
The city model used in the screenshots and videos is called Japanese Otaku City and was created by ZENRIN CO. LTD. I would like to thank the creators of this great asset which is available for free on the asset store at  https://www.assetstore.unity3d.com/en/#!/content/20359


Common Issues / FAQ
----------------------------------------
Please visit the home page at http://www.meshmaker.com for the latest news and help forum.


Contact
----------------------------------------
Alan Baylis
www.meshmaker.com
support@meshmaker.com


Update Log
-----------------------------------------
v0.9 released 18/05/15
First release of Mesh Cutter.

v1.0 released 31/07/15
Changed the mesh copy dialog to select both the filename and location
Added select all triangles by material
Holding the Alt key down now blocks triangle selection
Added Mac friendly hotkey selection
Added Save Mesh button to save to asset

v1.1 released 14/08/15
Added skinned mesh support

v1.3 released 04/10/15
Fix for inline shaders

v1.4 released 29/04/16
Improved support for Unity 5.x and hotkey on Mac