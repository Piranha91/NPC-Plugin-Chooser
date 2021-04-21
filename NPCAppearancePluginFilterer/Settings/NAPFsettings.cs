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

namespace NPCAppearancePluginFilterer.Settings
{
    public class NAPFsettings
    {
        [SynthesisOrder]
        [SynthesisTooltip("Simple: assumes that the NPC records and associated meshes and textures are conflict winners.\nDeep: Searches through conflict losers as well as winners to forward the correct records (requires MO2 path to be set).\nSettingsGen: Instead of forwarding NPC records, the program will scan your current winning NPC overrides and genrate a settings.json to use based on your current setup (requires MO2 path to be set).")]
        public Mode Mode { get; set; } = Mode.Simple;

        [SynthesisOrder]
        [SynthesisSettingName("Mod Organizer 2\\mods Path")]
        [SynthesisTooltip("Path of your MO2\\mods folder. Can be left blank if using Simple Mode.")]
        public string MO2DataPath { get; set; } = "";

        [SynthesisOrder]
        [SynthesisTooltip("Plugins from which NPC appearance should be forwarded.")]
        public HashSet<PerPluginSettings> PluginsToForward { get; set; } = new HashSet<PerPluginSettings>();

        [SynthesisOrder]
        [SynthesisTooltip("Directory to which meshes and textures should be copied.")]
        public string AssetOutputDirectory { get; set; } = "";

        [SynthesisOrder]
        [SynthesisTooltip("If checked, the output directory's contents will be cleared between each run (so that if an NPC is removed from your transfer list, their associated meshes and textures won't remain).")]
        public bool ClearAssetOutputDirectory { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, the non-appearance-related data from the winning override in your load order will be merged into the output plugin.")]
        public bool ForwardConflictWinnerData { get; set; } = true;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, outfits are considered as \"non-appearance-related data\" in the above setting.")]
        public bool ForwardConflictWinnerOutifts { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, all detected resources required by each NPC will be copied (provided they reside in the same mod folder the the plugin. If unchecked, only facegen will be copied.")]
        public bool CopyExtraAssets { get; set; } = false;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, the patcher will error out if an expected FaceGen file is not found in a mod's directory (recommended to leave on).")]
        public bool AbortIfMissingFaceGen { get; set; } = true;

        [SynthesisOrder]
        [SynthesisTooltip("If checked, the patcher will error out if an expected Extra Asset (non-FaceGen textures and meshes) is not found in a mod's directory (recommended to leave off unless you're sure your mods have absolutely no external dependencies).")]
        public bool AbortIfMissingExtraAssets { get; set; } = false;

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
        public HashSet<string> pathsToIgnore { get; set; } = new HashSet<string>();

        [SynthesisIgnoreSetting]
        public HashSet<string> warningsToSuppress { get; set; } = new HashSet<string>();
    }

    public enum Mode
    {
        Simple,
        Deep,
        SettingsGen
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
        [SynthesisTooltip("If FaceGen or extra assets are not found in the plugin's MO2 directory, NAPF will search through these additional directories to try to find them.")]
        public HashSet<string> ExtraDataDirectories { get; set; } = new HashSet<string>();
    }
}
