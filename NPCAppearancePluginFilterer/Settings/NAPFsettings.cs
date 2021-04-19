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
        [SynthesisTooltip("If checked, all detected resources required by each NPC will be copied (provided they reside in the same mod folder the the plugin. If unchecked, only facegen will be copied.")]
        public bool CopyExtraAssets { get; set; } = false; 

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
        public HashSet<string> pathsToIgnore = new HashSet<string>()
        {
            // Meshes folder
            "Actors\\Character\\Character Assets\\MaleBody_0.NIF",
            "Actors\\Character\\Character Assets\\FemaleBody_0.NIF",
            "Actors\\Character\\Character Assets\\MaleHands_0.nif",
            "Actors\\Character\\Character Assets\\FemaleHands_0.nif",
            "Actors\\Character\\Character Assets\\MaleFeet_0.nif",
            "Actors\\Character\\Character Assets\\FemaleFeet_0.nif",
            "Actors\\Character\\Character Assets\\MaleBody_1.NIF",
            "Actors\\Character\\Character Assets\\FemaleBody_1.NIF",
            "Actors\\Character\\Character Assets\\MaleHands_1.nif",
            "Actors\\Character\\Character Assets\\FemaleHands_1.nif",
            "Actors\\Character\\Character Assets\\MaleFeet_1.nif",
            "Actors\\Character\\Character Assets\\FemaleFeet_1.nif",

            //tri
            "Actors\\Character\\Character Assets\\FemaleHeadRaces.tri",
            "Actors\\Character\\Character Assets\\FemaleHead.tri",
            "Actors\\Character\\Character Assets\\FemaleHeadCharGen.tri",

            //misc
            "Actors\\Character\\Character Assets\\FaceParts\\FemaleBrows.nif",

            //Textures Folder


            //human body
            "Actors\\Character\\Male\\MaleBody_1.dds",
            "Actors\\Character\\Male\\MaleBody_1_msn.dds",
            "Actors\\Character\\Male\\MaleBody_1_sk.dds",
            "Actors\\Character\\Male\\MaleBody_1_S.dds",
            "Actors\\Character\\Female\\FemaleBody_1.dds",
            "Actors\\Character\\Female\\FemMaleBody_1_msn.dds",
            "Actors\\Character\\Female\\FemaleBody_1_sk.dds",
            "Actors\\Character\\Female\\FemaleBody_1_S.dds",

            "Actors\\Character\\Male\\MaleBodyAfflicted.dds",
            "Actors\\Character\\Male\\MaleBodySnowElf.dds",
            //human hands
            
            "Actors\\Character\\Male\\MaleHands_1.dds",
            "Actors\\Character\\Male\\MaleHands_1_msn.dds",
            "Actors\\Character\\Male\\MaleHands_1_sk.dds",
            "Actors\\Character\\Male\\MaleHands_1_S.dds",
            "Actors\\Character\\Female\\FemaleHands_1.dds",
            "Actors\\Character\\Female\\FemMaleHands_1_msn.dds",
            "Actors\\Character\\Female\\FemaleHands_1_sk.dds",
            "Actors\\Character\\Female\\FemaleHands_1_S.dds",

            "Actors\\Character\\Male\\MaleHandsAfflicted.dds",
            "Actors\\Character\\Male\\MaleHandsSnowElf.dds",
            
            //human head
            "Actors\\Character\\Male\\blankdetailmap.dds",

            "Actors\\Character\\Male\\MaleHead.dds",
            "Actors\\Character\\Male\\MaleHead_msn.dds",
            "Actors\\Character\\Male\\MaleHead_sk.dds",
            "Actors\\Character\\Male\\MaleHead_S.dds",

            "Actors\\Character\\Male\\MaleHeadAfflicted.dds",
            "Actors\\Character\\Male\\MaleHeadSnowElf.dds",
            "Actors\\Character\\Male\\MaleHeadVampire.dds",
            "Actors\\Character\\Male\\MaleHeadVampire_msn.dds",

            "Actors\\Character\\Male\\MaleHeadDetail_Age40.dds",
            "Actors\\Character\\Male\\MaleHeadDetail_Age40Rough.dds",
            "Actors\\Character\\Male\\MaleHeadDetail_Age50.dds",
            "Actors\\Character\\Male\\MaleHeadDetail_Rough01.dds",
            "Actors\\Character\\Male\\MaleHeadDetail_Rough02.dds",

            "Actors\\Character\\Female\\FemaleHead.dds",
            "Actors\\Character\\Female\\FemaleHead_msn.dds",
            "Actors\\Character\\Female\\FemaleHead_sk.dds",
            "Actors\\Character\\Female\\FemaleHead_S.dds",

            "Actors\\Character\\Female\\FemaleHeadAfflicted.dds",
            "Actors\\Character\\Female\\FemaleHeadVampire.dds",
            "Actors\\Character\\Female\\FemaleHeadVampire_msn.dds",

            "Actors\\Character\\Female\\FemaleHeadDetail_Age40.dds",
            "Actors\\Character\\Female\\FemaleHeadDetail_Age40Rough.dds",
            "Actors\\Character\\Female\\FemaleHeadDetail_Age50.dds",
            "Actors\\Character\\Female\\FemaleHeadDetail_Rough.dds",
            "Actors\\Character\\Female\\FemaleHeadDetail_Frekles.dds",

            "Actors\\Character\\BretonFemale\\FemaleHead_msn.dds"

            

            //argonian body

        };

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
        public bool InvertSelection { get; set; }
    }
}
