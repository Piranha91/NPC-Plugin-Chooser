# NPC: NPC Plugin Chooser

## Overview
This patcher forwards the appearance of NPCs specified by the user. It copies the chosen NPCs' records into the plugin it builds, forwards their FaceGen to a chosen output directory, and (optionally) forwards the other meshes and textures required for that NPC to the output directory. It is currently built for MO2. Vortex support may come in the future.

## Installation
Extract the downloaded folder to your drive. I prefer `Mod Organizer 2\tools`. Add `NPC-Plugin-Chooser.exe` as an executable in MO2. At this time, the .exe does not have a user interface built in. Instead, you will need to add the patcher to Synthesis as a Git Repository and run Synthesis (**through MO2**). This will be updated as soon as Synthesis supports exporting patchers with UI. In the meantime, add the patcher to Synthesis by clicking "Git Repository" in the top left corner -> Input tab -> Repository Path = https://github.com/Piranha91/NPC-Plugin-Chooser. Choose the only available project from the Project dropdown menu and click the blue circle in the bottom right corner. Configure the settings from the patcher's settings menu and then either copy the settings to where the .exe is expecting them, or run the patcher directly through Synthesis. **Note: it is expected that you run the patcher through MO2, either via Synthesis or directly as the .exe.**

### Transferring settings from Synthesis to the standalone patcher
Configure the settings in Synthesis to reflect your preferences. Then, you will need to actually run the patcher to generate or update the settings.json file. However, if you like, you can immediately stop the patcher by clicking the faded Abort button:

![Screenshot](https://raw.github.com/Piranha91/NPC-Plugin-Chooser/main/Docs/Images/Abort_Button.png)

Now, you should have a settings.json file in your `Synthesis\Data\NPC-Plugin-Chooser` folder. Copy that file to `NPC 1.x\Data`, replacing the settings.json file that already exists there. Then you can add the patcher as an executable in MO2 and run it. 

### Pros and cons of each approach

#### Running standalone executable

##### Pros
 * Output plugin will be named `NPC Appearance.esp` (you probably won't want to run this patcher every time you run Synthesis, so having the results not in Synthesis.esp will be convenient).

##### Cons
* Every time you want to tweak a setting you will need to go to Synthesis, perform the tweak, and then re-copy the settings json to the location the executable expects it in.

#### Running Synthesis Git patcher

##### Pros
 * You can edit the settings and run the patcher right away

##### Cons
* Output will be named Synthesis.esp
* You will probably want to disable all other patchers when running this one, and disable it when running your normal Synthesis patchers.
<h2>Usage</h2>
The settings of this patcher appear as follows:

![Screenshot](https://raw.github.com/Piranha91/NPC-Plugin-Chooser/main/Docs/Images/Main_Menu.PNG)

### Main Menu (Synthesis)
#### Mode
Mode dictates how the patcher will run. There are three modes in the dropdown menu:

* Simple: intended for trimming either a single NPC mod, or a group of non-overlapping NPC mods, down to the NPCs you select. FaceGen will be grabbed from the conflict winner in your mod order. Setting the **Mod Organizer 2\mods Path** is *not* required for this mode.
* Deep: intended for compiling NPCs from a set of overlapping NPC appearance mods. Setting the **Mod Organizer 2\mods Path** *is* required for this mode. The patcher will scan your mod folders and find where the FaceGen lives for each of the plugins that you choose.
* SettingsGen: Intended for users who already have their own curated NPC appearance list. The patcher will scan your current NPC appearance mods, find the winning mods, and write that conflict winner to a settings.json file in the output folder. You can then replace the current settings file with the one generated and it will have your current selected NPCs. You can keep a copy as a backup and then start modifying your list as desired. Setting the **Mod Organizer 2\mods Path** *is* required for this mode.

#### SettingsGen Mode
If **Mode** is set to `SettingsGen`, this setting dictates which NPCs the patcher will import into the settings file that it creates.

* All: Every NPC in your load order will be imported into the generated settings (unless blocked by **SettingsGen Ignored Plugins**).
* RecordConflictsOnly: Only NPCs that have modded faces will be imported into the generated settings.

Note that RecordConflictsOnly will still import NPCs from mods that aren't typically thought of as "appearance-related". Such NPCs include those from Skyrim.esm with new FaceGen provided by Update.esm, Dawnguard.esm, etc., as well as those with "fixed" FaceGen provided by the Unofficial Skyrim Special Edition Patch. If you don't want your generated settings to include NPCs from such mods, add these mods to **SettingsGen Ignored Plugins**.

#### SettingsGen Choose Winner By
If **Mode** is set to `SettingsGen`, this setting dictates how conflict-winning NPCs are chosen for import.

* LoadOrder: The NPC mod from the winning load order plugin (MO2 right panel) will be forwarded.
* FaceGenOrder: The NPC mod containing the winning FaceGen files (MO2 left panel) will be forwarded.

Note that LoadOrder mode is considerably faster than FaceGenOrder mode.

#### SettingsGen Ignored Plugins
If **Mode** is set to `SettingsGen`, NPC appearance edits by the mods in this list will be disregarded when generating your settings. By default it contains the base game + DLC. You may remove any and all plugins from this list if you wish for your generated settings file to include non-modded NPCs.

#### Mod Organizer 2\mods Path
This setting tells the patcher where your mods folder is. Required for **Deep** and **SettingsGen** modes but not for **Simple** mode.

#### Asset Output Directory
The folder to which the patcher will output FaceGen (and optionally additional NPC-related textures and meshes). Recommended to be a folder within MO2\mods so that you can simply activate it in the left panel at the bottom of your mod list and overwrite all other FaceGen with the result.

#### Plugins To Forward
The list of plugins, and the NPCs which they contain, that you would like to forward. Discussed in detail below.

#### Clear Asset Output Directory
If checked, the meshes and textures within the **Asset Output Directory** will be deleted each time you run the patcher. Settings generated in **SettingsGen** mode will not be deleted.

#### Forward Conflict Winner Data
If checked, the non-appearance-related data for each NPC's conflict winner will be forwarded into the output plugin. This is intended for use if you have a consistency patch that you would like to apply to NPCs for perks/stats/etc. and you don't want to update it by hand each time you make a pick a new appearance for an NPC - just let this consistency patch load as the last plugin in your load order and it will be forwarded to the resulting output. Note that this can add additional masters to the generated plugin if the conflict winner is mastered to other plugins.

#### Forward Conflict Winner Outfits
If checked, *and* if **Forward Conflict Winner Data** is checked, outfits will be forwarded from the conflict winner. Otherwise they will be forwarded from the chosen Appearance plugin.

#### Handle BSA files during patching
If checked, the patcher will extract FaceGen (and optionally additional meshes and textures for the chosen NPCs) from BSA archives that reference the chosen mod. Make sure to check this if you are forwarding NPC appearance from mods that contain BSA archives. Note: this is a heavy operation and it can double the time needed to run the patcher.

#### Copy Extra Assets
If checked, the patcher will copy non-FaceGen-related meshes and textures for the NPCs being forwarded, so that the resulting output folder can be used as a standalone mod. If unchecked, only FaceGen will be forwarded to the output folder.

#### Abort If Missing FaceGen
If checked, the patcher will error out if it cannot find the FaceGen files that it's expecting. Recommended to leave enabled to avoid FaceGen bugs (dark faces) in-game. 

#### Abort If Missing Extra Assets
If checked, the patcher will error out if it cannot find non-FaceGen meshes or textures referenced by the the NPC's plugin (or optionally its meshes as well; see below). Note that the patcher will only look in an NPC's mod folder, so if you have an NPC that requires KS Hairs (for example) to be installed as a separate mod, it won't find those assets and will error out. Therefore, recommended to uncheck unless you're *sure* all of your NPC appearance mods are self-contained.

#### Get Missing Extra Assets From Available Winners
If checked, the patcher will look in the full available data folder for **Extra Assets** that are not within the **Plugin**'s home folder or any specified **Extra Data Folders**. Note that this will grab the conflict winner for that particular mesh or texture.

#### Suppress All Missing File Warnings
If checked, the patcher will not warn of files that it expected to find in the **Plugin**'s home folder or its specified **Extra Data Directories**. Note that not all warnings are "real", in the sense that some NPC mods are distributed with missing texture or mesh paths that don't affect the final appearance.

#### Suppress Known Missing File Warnings
Some NPC appearance plugins reference meshes and textures that aren't distributed with the mod itself, and are not required for the mod to look correct. For example, the Bijin series references `.tri` files that aren't distributed with the mod. If checked, the patcher will compare all files that it expects to find and can't against a list located in Warnings To Suppress.json and skip warning the user if the file is in that list. That file was made based on my own load order; you may want to edit it as required by yours. Please contact me with additional submissions for this list.

#### Plugins Excluded from Merge
After forwarding an NPC's record to the output patch, the patcher will also forward additional immediate dependency records (head parts, worn armor, face textures, etc.) - forwarding such records from the base game is both unnecessary and causes the patcher to stall. It is recommended not to edit this list, but it has been made accessible in case you encounter issues merging dependencies from a particular plugin. Such issues would appear as the following error during patching:

```
Remapping Dependencies from Mod.esp
System.Collections.Generic.KeyNotFoundException: Could not locate record to make self contained: (Mutagen.Bethesda.Skyrim.ISkyrimMajorRecordGetter) => xxxxxx:Mod.esp
```

### Plugins to Forward
To select which NPCs from which plugin you wish to forward to the output patch, click **Plugins To Forward** in the main menu. You will be taken to the NPC selection menu, which appears as follows:

![Screenshot](https://raw.github.com/Piranha91/NPC-Plugin-Chooser/main/Docs/Images/Plugin_Menu.PNG)

To add NPCs from a new plugin, click the **+** sign to the right of **Plugins To Forward**

For each plugin, you may set the following:

#### Plugin
Select the plugin which contains the NPC appearance that you wish to forward. Before a plugin is recognized it needs to be activated in the load order, after it will be accessible via the dropdown menu.

#### NPCs
The list of NPCs whose appearance you wish to forward from the chosen plugin. To add an NPC, type its EditorID into the **EditorID** box, or its Synthesis FormKey into the **FormKey** box. A Synthesis FormKey consists of `Last6CharactersOfFormID:PluginName.esp` - note that the file extension might also end in `.esl` or `.esm`. NPCs are searchable by EditorID but if the NPC doesn't have an EditorID or if there is more than one NPC with that EditorID, you will need to specify its FormKey. The list of chosen NPCs will appear in the **Added Records** box. Chosen **NPCs** that are not in the chosen **Plugin** will be ignored.

#### Select All
If checked, every NPC from the chosen **Plugin** will be forwarded. The **NPCs** list will be ignored if this setting is checked.

#### Invert Selection
If checked, all **NPCs** from the chosen **Plugin** *except for* the specified **NPCs** will be forwarded.

#### Forced Asset Directory
If not blank, the patcher will look in this directory *instead of* the plugin's directory in MO2\mods for FaceGen and Extra Assets. Useful if the source plugin and meshes/textures live in separate folders.

#### Extra Data Directories
Extra directories that the patcher will look in trying to copy Extra Assets. To add a directory, click the **+** sign to the right of this setting and type in the desired path.

#### Find Extra Textures In Nifs
Only relevant if **Copy Extra Assets** is checked. In some mods, the plugin does not reference all of the textures used by the NPC - instead, some textures can only be found by looking into the .nif files. If checked, the patcher will look into the NPC's referenced .nif files (including the FaceGen Nif) and and forward the additional textures that it finds if they exist within the NPC's mod folder. Some appearance mods work this way (for example Bijin and Pandorable) while others such as the Northbourne series do not. It is recommended to leave this setting enabled, but due to the extra patching time required it has been made accessible per-plugin so that it can be skipped for mods known not to require it.

#### Add to merge.json
If checked, the patcher will generate a `merge\merge.json` file containing this **Plugin**. This file can be read by [Merge Plugins Hide](https://github.com/deorder/mo2-plugins/releases) to quickly hide or unhide all **Plugin**s forwarded into the generated appearance patch. This setting is ignored for base game and official DLC plugins.

## FAQ
**Why doesn't the .exe file have a UI like the Synthesis patcher? Why do I need to copy settings between them?**

*Currently, when exporting Synthesis patchers to .exe, the UI does not get exported with it. As soon as this changes I will update the patcher to break the Synthesis dependence (but you will still be able to run it via Synthesis if that's what you prefer, and you don't mind the output being named Synthesis.esp).*


**I'm using Nordic Faces, which doesn't have a plugin! How do I forward the appearance of Nordic Faces NPCs?**

*This is what the **Forced Asset Directory** setting is for. For **Plugin** choose Skyrim.esm (or whichever plugin the NPC is first referenced in), and for **Forced Asset Directory** type in the MO2 folder of Nordic Faces (e.g. `C:\Games\MO2\mods\Nordic Faces - Immersive Characters Overhaul`).*

**I've added a lot of plugins and the UI is laggy.**

*That's not a Q. Furthermore, coding the UI is beyond my current capabilities; I'm depending purely and exclusively on the Synthesis UI and that is how it performs.*

**Can you make it so that once you choose a plugin, only NPCs from that plugin are listed when searching by EditorID?**

*That would be great, but again I am reliant on the Synthesis UI and that is not currently a feature.*

## Acknowledgements

 * Noggog - creator of Synthesis who very patiently helped me learn how to make patchers, and did not rip my head off when I incessantly requested new features for both the UI and under the hood
 * SteveTownsend - for helping me learn how to use Synthesis to handle BSA archives, and for creating the package I needed to get additional textures from Nifs (and indeed for writing the function to do it).
 * Janquel - for suggesting the name for this patcher
 * DarkladyLexy - for being my first beta tester & making several good suggestions for SettingsGen mode.
 * trawzified - for fixing up the Markdown on this README file.
 * All of the great NPC appearance mod authors that made this patcher necessary - rxkx22 (Bijin series), Pandorable for her eponymous mods, Southpawe (Northbourne series), PoeticAnt44 (Pride of Skyrim), deletepch (Nordic Faces... but please don't delete the PCH; it's beautiful!) and many others! Thank you for making it so hard to choose!

