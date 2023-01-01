# Game Texture Pack Manager
Manage and apply texture packs for games that have an exposed texture & audio folder in a fast and easy to use way!
> **PLEASE make sure your default texture packs are up-to-date. You can update a default texture pack by pressing the 'Reset Default Texture Pack' button.**

This program uses WinForms for its UI, this means that this program can only be run on Windows.

## How To Use
After opening up the program, select the game you want to change the textures of in the dropdown next to the 'Selected Game' label. If you do not see your game there, you will have to select the 'Add Game' option.

This will open up the Add Game window.

### Adding Games
Open up the Add Game window by going to the selected game dropdown and pressing 'Add Game'.

Here you can give a name to the game you want to add, or select one of the preset options by clicking on the down arrow near the textbox and selecting one of the options. However, those games will only show up, if the program could find their resource/content folders.

You cannot type in the content folder path textbox, but by pressing the button next to it, you can select a content/resource folder to manage. 
> Please note that this folder should only contain files and folders for files like images, sounds, ... . It should not contain the .exe file for the game itself or other very important files.
> For example: the Geometry Dash Preset sets the content folder path not to C:\Program Files (x86)\Steam\steamapps\common\Geometry Dash, but to C:\Program Files (x86)\Steam\steamapps\common\Geometry Dash\Resources.

In the 'Allowed File Extensions' checklist, you can select which file types (like images, audio, ...) you want to manage. 
> Note: You cannot add custom file types within the program. This is done for safety reasons. However, you can edit the FileExtensions.txt file in the GTPMAssets folder if you really need to add a custom file type or extension.

To add the game to the list, just press the 'Add Game' button. GTPM will ask you if you want to let it create a default texture pack, this is required if you want to manage texture packs.

![Add Game Window](https://raw.githubusercontent.com/YaEnergy/GDTS/master/GTPMAssets/Assets/Github/AddGame.gif)

### Applying Texture Packs

To add a texture pack to the available list, you must open the texture pack folder and insert a texture pack folder.
To update the available list, press the 'Refresh texture packs' button.

You can add texture packs to the selected list by dragging a texture pack from the available list, to the selected list.
> The lowest selected texture pack in the list, has the highest priority.

To remove a texture pack from the selected list, just double click on the texture pack you want to remove.
> The Default Texture Pack cannot be removed from the list.

To apply the selected texture packs, just press the 'Apply Texture Packs' button.
> Make sure your default texture pack is up-to-date. You can update your default texture pack by pressing the 'Reset Default Texture Pack' button.

![Applying Texture Packs](https://raw.githubusercontent.com/YaEnergy/GDTS/master/GTPMAssets/Assets/Github/ApplyRemoveTexturePacksAndSelectGame.gif)

### Default Texture Packs

To apply texture packs to a selected game, it must have a default texture pack. This texture pack gets created when adding a game.
These texture packs should contain the base files (textures, audio, ...) of the game, if it contains files from other texture packs, you should change your game files back to the original ones and then reset the default texture pack using the 'Reset Default Texture Pack' button.

If a selected game does not have a default texture pack, it will ask to create one, but if you decline, you won't be able to apply texture packs.

##Notes

> PLEASE make sure your default texture packs are up-to-date. You can update a default texture pack by pressing the 'Reset Default Texture Pack' button.

> The selected content/resource folder for a game should only contain files and folders for files like images, sounds, ... . It should not contain the .exe file for the game itself or other very important files.
> For example: the Geometry Dash Preset sets the content folder path not to C:\Program Files (x86)\Steam\steamapps\common\Geometry Dash, but to C:\Program Files (x86)\Steam\steamapps\common\Geometry Dash\Resources.

> You cannot add custom file types within the program. This is done for safety reasons. However, you can edit the FileExtensions.txt file in the GTPMAssets folder if you really need to add a custom file type or extension.

> The lowest selected texture pack in the list, has the highest priority.

> The Default Texture Pack cannot be removed from the selected list.

> This program uses WinForms for its UI, this means that this program can only be run on Windows.
