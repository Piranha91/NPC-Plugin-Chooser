<h1>NPC Appearance Plugin Filterer</h1>

<h2>Overview</h2>
This patcher forwards the appearance of NPCs specified by the user. It copies the chosen NPCs' records into the plugin it builds, forwards their FaceGen to a chosen output directory, and (optionally) forwards the other meshes and textures required for that NPC to the output directory. It is currently built for MO2. Vortex support may come in the future.

<h2>Installation</h2>
At this time, the .exe does not have a user interface built in. Instead, you will need to add the patcher to Synthesis as a Git Repository and run Synthesis. This will be updated as soon as Synthesis supports exporting patchers with UI. In the meantime, add the patcher to Synthesis by clicking "Git Repository" in the top left corner -> Input tab -> Repository Path = https://github.com/Piranha91/NPC-Appearance-Plugin-Filterer. Choose the only available project from the Project dropdown menu and click the blue circle in the bottom right corner. Configure the settings from the patcher's settings menu, and then either copy the settings to where the .exe is expecting them, or run the patcher directly through Synthesis. Note: it is expected that you run the patcher through MO2, either via Synthesis or directly as the .exe.

<h3>Pros and Cons of each approach:</h3>

<h4>Running .exe</h4>
<h5>Pros:</h5>

 * Output plugin will be named NPC Appearance.esp (you probably won't want to run this patcher every time you run Synthesis, so having the results not in Synthesis.esp will be convenient). 
<h5>Cons:</h5>

* Every time you want to tweak a setting you will need to go to Synthesis, perform the tweak, and then re-copy the setting .json to where it's expected by the .exe

<h4>Running as patcher
<h5>Pros:</h5>

 * You can edit the settings and run the patcher right away
<h5>Cons:</h5>
  
* Output will be named Synthesis.esp
 
 * You will probably want to disable all other patchers when running this one, and disable this one when running your normal Synthesis patchers.
<h2>Usage</h2>
The settings of this patcher appear as follows:

![Screenshot](https://raw.github.com/Piranha91/NPC-Appearance-Plugin-Filterer/main/Docs/Images/Main_Menu.PNG)

<h3> Main Menu </h3>
<h4>Mode</h4>
Mode dictates how the patcher will run. There are three modes in the dropdown menu:

* Simple: intended for trimming either a single NPC mod, or a group of non-overlapping NPC mods, down to the NPCs you select. FaceGen will be grabbed from the conflict winner in your mod order. Setting the **Mod Organizer 2\mods Path** is *not* required for this mode.
* Deep: intended for compiling a NPCs from a set of overlapping NPCs mods. Setting the **Mod Organizer 2\mods Path** *is* required for this mode. The patcher will scan your mod folders and find where the FaceGen lives for each of the plugins that you choose.
* SettingsGen: Intended for users who already have their own curated NPC appearance list. The patcher will scan your current winning FaceGen for each NPC, find the source of the winning FaceGen, and write that conflict winner to a settings.json file in the output folder. You can then replace the current settings file with the one generated and it will have your current selected NPCs. You can keep a copy as a backup and then start modifying your list as desired. Setting the **Mod Organizer 2\mods Path** *is* required for this mode.

<h4>Mod Organizer 2\mods Path</h4>
This setting tells the patcher where your mods folder is. Required for **Deep** and **SettingsGen** modes but not for **Simple** mode.

<h4>Game Directory</h4>
This setting tells the patcher where your base game lives. It is only required to fill this out if you are planning to forward the vanilla appearance of an NPC. Otherwise it can be left blank.

<h4>Asset Output Directory</h4>
The folder to which the patcher will output FaceGen (and optionally additional NPC-related textures and meshes). Recommended to be a folder within MO2\mods so that you can simply activate it in the left panel at the bottom of your mod list and overwrite all other FaceGen with the result.

<h4>Plugins To Forward</h4>
The list of plugins, and the NPCs which they contain, that you would like to forward. Discussed in detail below.

<h4>Clear Asset Output Directory</h4>
If checked, the meshes and textures within the **Asset Output Directory** will be deleted each time you run the patcher. Settings generated in **SettingsGen** mode will not be deleted.

<h4>Forward Conflict Winner Data</h4>
If checked, the non-appearance-related data for each NPC's conflict winner will be forwarded into the output plugin. This is intended for use if you have a consistency patch that you would like to apply to NPCs for perks/stats/etc. and you don't want to update it by hand each time you make a pick a new appearance for an NPC - just make this consistency patch last in your load order and it will be forwarded to the resulting output. Note that this can add additional masters to the generated plugin if the conflict winner is mastered to other plugins.

<h4>Forward Conflict Winner Outfits</h4>
If checked, *and* if **Forward Conflict Winner Data** is checked, outfits will be forwarded from the conflict winner. Otherwise they will be forwarded from the chosen Appearance plugin.

<h4>Handle BSA files during patching</h4>
If checked, the patcher will extract FaceGen (and optionally additional meshes and textures for the chosen NPCs) from BSA archives that reference the chosen mod. Make sure to check this if you are forwarding NPC appearance from mods that contain BSA archives. Note that this can slow down the patcher by ~2x.

<h4>Handle BSA files during settings generation</h4>
If checked, the patcher will compare FaceGen from BSA archives that reference the chosen mod when in **SettingsGen** mode. Make sure to check this if you are forwarding NPC appearance from mods that contain BSA archives. 

<h4>Copy Extra Assets</h4>
If checked, the patcher will copy non-FaceGen-related meshes and textures for the NPCs being forwarded, so that the resulting output folder can be used as a standalone mod. If unchecked, only FaceGen will be forwarded to the output folder.

<h4>Abort If Missing FaceGen</h4>
If checked, the patcher will error out if it cannot find the FaceGen files that it's expecting. Recommended to leave enabled to avoid FaceGen bugs in-game. 

<h4>Abort If Missing Extra Assets</h4>
If checked, the patcher will error out if it cannot find non-FaceGen meshes or textures referenced by the the NPC's plugin (or optionally its meshes as well; see below). Note that the patcher will only look in an NPC's mod folder, so if you have an NPC that requires KS Hairs (for example) to be installed as a separate mod, it won't find those assets and will error out. Therefore, recommended to uncheck unless you're *sure* all of your NPC appearance mods are self-contained.

<h4>Suppress Known Missing File Warnings</h4>
Some NPC appearance plugins reference meshes and textures that aren't distributed with the mod itself, and are not required for the mod to look correct. For example, the Bijin series references .tri files that aren't distributed with the mod. If checked, the patcher will compare all files that it expects to find and can't against a list located in Warnings To Suppress.json and skip warning the user if the file is in that list. That file was made based on my own load order; you may want to edit it as required by yours. Please contact me with additional submissions for this list.

<h4>Plugins Excluded from Merge</h4>
After forwarding an NPC's record to the output patch, the patcher will also forward additional immediate dependency records (head parts, worn armor, face textures, etc.) - forwarding such records from the base game is both unnecessary and causes the patcher to stall. It is recommended not to edit this list, but it has been made accessible in case you encounter issues merging dependencies from a particular plugin.

 <h3> Plugins to Forward </h3>
To select which NPCs from which plugin you wish to forward to the output patch, click **Plugins To Forward** in the main menu. You will be taken to the NPC selection menu, which appears as follows:

![Screenshot](https://raw.github.com/Piranha91/NPC-Appearance-Plugin-Filterer/main/Docs/Images/Plugin_Menu.PNG)

To add NPCs from a new plugin, click the **+** sign to the right of **Plugins To Forward**

For each plugin, you may set the following:

<h4>Plugin</h4>
Select the plugin which contains the NPC appearance that you wish to forward. It needs to be in your load order, and will be accessible via the dropdown menu.

<h4>NPCs</h4>
The list of NPCs whose appearance you wish to forward from the chosen plugin. To add an NPC, type its EditorID into the **EditorID** box, or its Synthesis FormKey into the **FormKey** box. A Synthesis FormKey consists of Last6CharactersOfFormID:PluginName.esp/esm. NPCs are searchable by EditorID but if the NPC doesn't have an EditorID or if there is more than one NPC with that EditorID, you will need to specify its FormKey. The list of chosen NPCs will appear in the **Added Records** box. Chosen **NPCs** that are not in the chosen **Plugin** will be ignored.

<h4>Invert Selection</h4>
If checked, all **NPCs** from the chosen **Plugin** *except for* the specified **NPCs** will be forwarded. To quickly select all NPCs from a plugin, choose that **Plugin**, do not select any **NPCs**, and check the **Invert Selection** box.

<h4>Forced Asset Directory</h4>
If not blank, the patcher will look in this directory *instead of* the plugin's directory in MO2\mods for FaceGen and Extra Assets. Useful if the source plugin and meshes/textures live in separate folders.

<h4>Extra Data Directories</h4>
Extra directories that the patcher will look in trying to copy Extra Assets. To add a directory, click the **+** sign to the right of this setting and type in the desired path.

<h4>Find Extra Textures In Nifs</h4>
Only relevant if **Copy Extra Assets** is checked. In some mods, the plugin does not reference all of the textures used by the NPC - instead, some textures can only be found by looking into the .nif files. If checked, the patcher will look into the NPC's referenced .nif files (including the FaceGen Nif) and and forward the additional textures that it finds if they exist within the NPC's mod folder. Some appearance mods work this way (for example Bijin and Pandorable) while others such as the Northbourne series do not. It is recommended to leave this setting enabled, but due to the extra patching time required it has been made accessible per-plugin so that it can be skipped for mods known not to require it.

<h2>FAQ</h2>
Q: Why doesn't the .exe file have a UI like the Synthesis patcher? Why do I need to copy settings between them?<br/>
A: Currently, when exporting Synthesis patchers to .exe, the UI does not get exported with it. As soon as this changes I will update the patcher to break the Synthesis dependence (but you will still be able to run it via Synthesis if that's what you prefer, and you don't mind the output being named Synthesis.esp).<br/><br/>

Q: I'm using Nordic Faces, which doesn't have a plugin! How do I forward the appearance of Nordic Faces NPCs?<br/>
A: This is what the **Forced Asset Directory** setting is for. For **Plugin** choose Skyrim.esm (or whichever plugin the NPC is first referenced in), and for **Forced Asset Directory** type in the MO2 folder of Nordic Faces (e.g. C:\Games\MO2\mods\Nordic Faces - Immersive Characters Overhaul).<br/>

Q: I've added a lot of plugins and the UI is laggy.<br/>
A: That's not a Q. Furthermore, coding the UI is beyond my current capabilities; I'm depending purely and exclusively on the Synthesis UI and that is how it performs.<br/>

Q: Can you make it so that once you choose a plugin, only NPCs from that plugin are listed when searching by EditorID?<br/>
A: That would be great, but again I am reliant on the Synthesis UI and that is not currently a feature.<br/>

<h2>Acknowledgements</h2>

 * Noggog - creator of Synthesis who very patiently helped me learn how to make patchers, and did not rip my head off when I incessantly requested new features for both the UI and under the hood
 * SteveTownsend - for helping me learn how to use Synthesis to handle BSA archives, and for creating the package I needed to get additional textures from Nifs (and indeed for writing the function to do it).
 * Janquel - for suggesting the name for this patcher
 * DarkladyLexy - for being my first beta tester
 * All of the great NPC appearance mod authors that made this patcher necessary - rxkx22 (Bijin series), Pandorable for her eponymous mods, Southpawe (Northbourne series), PoeticAnt44 (Pride of Skyrim), deletepch (Nordic Faces... but please don't delete the PCH; it's beautiful!) and many others! Thank you for making it so hard to choose!

