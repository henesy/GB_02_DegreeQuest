# How to Add Sprites

Say for example we want to add a given 64px by 64px .png file to add...

We need to load and build sprites through the MonoGame Content Pipeline:

1. Open Pipeline.exe (Link in base project file)
- If you see an empty Content dropdown: File->Open and open Content/Content.mgcb
- Click and drag the desired .png file to the top left Content tree, you can drag it to a sub-folder further from that point
- To finalize, File->Save and Build->Rebuild
- The given textures should be availabe in DegreeQuest.Textures and accessible as per: filename.png would be accessible as Textures["filename"] which would return the respective Texture2D

Note: The process for adding fonts is similarly through the Pipeline, but you can perform Edit->Add->New Item, then selecting SpriteFont. The .spritefont file would then be created in the Content/ folder and could be edited in any standard text editor.

