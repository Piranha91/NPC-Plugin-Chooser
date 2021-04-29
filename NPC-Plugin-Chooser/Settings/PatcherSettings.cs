using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noggog;
using Mutagen.Bethesda.Synthesis.Settings;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NPCPluginChooser.Settings
{
    public class PatcherSettings
    {
        [SynthesisOrder]
        [SynthesisTooltip("Simple: assumes that the NPC records and associated meshes and textures are conflict winners.\nDeep: Searches through conflict losers as well as winners to forward the correct records (requires MO2 path to be set).\nSettingsGen: Instead of forwarding NPC records, the program will scan your current winning NPC overrides and genrate a settings.json to use based on your current setup (requires MO2 path to be set).")]
        public Mode Mode { get; set; } = Mode.Simple;

        [SynthesisOrder]
        [SynthesisTooltip("All: a record will be generated for every NPC regardless of conflict status.\nRecordConflictsOnly: Records will only be generated for NPCs with non-vanilla appearance-related plugin data.\nFaceGenConflictsOnly: Records will only be generated for NPCs with non-vanilla FaceGen files.")]
        public SettingsGenMode SettingsGenMode { get; set; } = SettingsGenMode.All;

        [SynthesisOrder]
        [SynthesisSettingName("Mod Organizer 2\\mods Path")]
        [SynthesisTooltip("Path of your MO2\\mods folder. Can be left blank if using Simple Mode.")]
        public string MO2DataPath { get; set; } = "";

        [SynthesisOrder]
        [SynthesisSettingName("Game Directory")]
        [SynthesisTooltip("Path of your Game Directory (the folder that contains Skyrim(SE/VR).exe. Only required if forwarding NPC appearance from one a vanilla record, otherwise can be left blank.")]
        public string GameDirPath { get; set; } = "";

        [SynthesisOrder]
        [SynthesisTooltip("Directory to which meshes and textures should be copied.")]
        public string AssetOutputDirectory { get; set; } = "";

        [SynthesisOrder]
        [SynthesisTooltip("Plugins from which NPC appearance should be forwarded.")]
        public HashSet<PerPluginSettings> PluginsToForward { get; set; } = new HashSet<PerPluginSettings>();

        [SynthesisOrder]
        [SynthesisTooltip("If checked, the output directory's contents will be cleared between each run (so that if an NPC is removed from your transfer list, their associated meshes and textures won't remain).")]
        public bool ClearAssetOutputDirectory { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, the non-appearance-related data from the winning override in your load order will be merged into the output plugin.")]
        public bool ForwardConflictWinnerData { get; set; } = true;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, outfits are considered as \"non-appearance-related data\" in the above setting and will be forwarded from the winning override.")]
        public bool ForwardConflictWinnerOutifts { get; set; } = true;

        [SynthesisOrder]
        [SynthesisSettingName("Handle BSA files during patching")]
        [SynthesisTooltip("If checked, the patcher will look inside BSA files for FaceGen and Extra Assets to forward.")]
        public bool HandleBSAFiles_Patching { get; set; } = true;

        [SynthesisOrder]
        [SynthesisSettingName("Handle BSA files during settings generation")]
        [SynthesisTooltip("If checked, the patcher will look inside BSA files for FaceGen during settings generation. May ~double the time it takes to generate settings depending on your mod list.")]
        public bool HandleBSAFiles_SettingsGen { get; set; } = true;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, all detected resources required by each NPC will be copied (provided they reside in the same mod folder the the plugin. If unchecked, only facegen will be copied.")]
        public bool CopyExtraAssets { get; set; } = false;

        [SynthesisOrder]
        [SynthesisSettingName("Abort If Missing FaceGen")]
        [SynthesisTooltip("If checked, the patcher will error out if an expected FaceGen file is not found in a mod's directory (recommended to leave on).")]
        public bool AbortIfMissingFaceGen { get; set; } = true;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, the patcher will error out if an expected Extra Asset (non-FaceGen textures and meshes) is not found in a mod's directory (recommended to leave off unless you're sure your mods have absolutely no external dependencies).")]
        public bool AbortIfMissingExtraAssets { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("If an expected Extra Asset doesn't exist in the mod's MO2 folder, the patcher will search all mods and choose the conflict-winning asset at the same path, if it exists.")]
        public bool GetMissingExtraAssetsFromAvailableWinners { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("Suppresses all log warnings about missing files.")]
        public bool SuppressAllMissingFileWarnings { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("Some plugins reference files that don't exist in their own download - for example the Bijin series references a bunch of .tri files that don't ship with the mod. While I can't account for every mod that exists, this settings suppresses \"file could not be found\" warnings from mods with known missing files so that any warnings you do see are more likely to be real.")]
        public bool SuppressKnownMissingFileWarnings { get; set; } = true;

        [SynthesisOrder]
        [SynthesisTooltip("The following plugins will never have their assets merged into the generated .esp or copied to the output directory. Don't touch unless you know what you're doing.")]
        public HashSet<ModKey> PluginsExcludedFromMerge = new HashSet<ModKey>()
        {
            ModKey.FromNameAndExtension("Skyrim.esm"),
            ModKey.FromNameAndExtension("Update.esm"),
            ModKey.FromNameAndExtension("Dawnguard.esm"),
            ModKey.FromNameAndExtension("HearthFires.esm"),
            ModKey.FromNameAndExtension("Dragonborn.esm")
        };

        [SynthesisIgnoreSetting]
        public HashSet<ModKey> BaseGamePlugins = new HashSet<ModKey>()
        {
            ModKey.FromNameAndExtension("Skyrim.esm"),
            ModKey.FromNameAndExtension("Update.esm"),
            ModKey.FromNameAndExtension("Dawnguard.esm"),
            ModKey.FromNameAndExtension("HearthFires.esm"),
            ModKey.FromNameAndExtension("Dragonborn.esm")
        };

        [SynthesisIgnoreSetting]
        public HashSet<string> pathsToIgnore { get; set; } = new HashSet<string>();

        [SynthesisIgnoreSetting]
        public HashSet<suppressedWarnings> warningsToSuppress { get; set; } = new HashSet<suppressedWarnings>();
        [SynthesisIgnoreSetting]
        public suppressedWarnings warningsToSuppress_Global { get; set; } = new suppressedWarnings();
    }

    public enum Mode
    {
        Simple,
        Deep,
        SettingsGen
    }

    public enum SettingsGenMode
    {
        All,
        RecordConflictsOnly,
        FaceGenConflictsOnly
    }

    public class PerPluginSettings
    {
        [SynthesisOrder]
        [SynthesisTooltip("Plugin")]
        public ModKey Plugin { get; set; } = new ModKey();

        [SynthesisOrder]
        [SynthesisSettingName("NPCs")]
        [SynthesisTooltip("Click here to select which NPCs should have their appearance forwarded. If the NPC appears in multiple plugins, the load order conflict winner will be forwarded.")]
        public HashSet<IFormLinkGetter<INpcGetter>> NPCs { get; set; } = new HashSet<IFormLinkGetter<INpcGetter>>();

        [SynthesisOrder]
        [SynthesisTooltip("If checked, all NPCs in the chosen plugin EXCEPT the ones specified above will be forwarded.")]
        public bool InvertSelection { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("If not blank, patcher will look in this directory INSTEAD OF the plugin's MO2 directory for FaceGen and Extra Assets.")]
        public string ForcedAssetDirectory { get; set; } = "";

        [SynthesisOrder]
        [SynthesisTooltip("If FaceGen or extra assets are not found in the plugin's MO2 directory or its Forced Asset Directory, NAPF will search through these additional directories to try to find them.")]
        public HashSet<string> ExtraDataDirectories { get; set; } = new HashSet<string>();
        
        [SynthesisOrder]
        [SynthesisTooltip("If checked, patcher will look in .nif files for additional textures not references in the NPC's plugin. Safer to leave on to avoid missing texture, but slows down patching ~4-5x.")]
        public bool FindExtraTexturesInNifs { get; set; } = true;
    }

    public class suppressedWarnings
    {
        public string Plugin { get; set; } = "";
        public HashSet<string> Paths { get; set; } = new HashSet<string>();
    }
}
