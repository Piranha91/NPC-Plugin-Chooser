using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noggog;
using Mutagen.Bethesda.Synthesis.Settings;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;

namespace NPCAppearancePluginFilterer.Settings
{
    class NAPFsettings
    {
        [SynthesisOrder]
        [SynthesisTooltip("Plugins from which NPC appearance should be forwarded.")]
        public HashSet<PerPluginSettings> PluginsToForward { get; set; } = new HashSet<PerPluginSettings>();

        [SynthesisOrder]
        [SynthesisTooltip("Directory to which meshes and textures should be copied.")]
        public string AssetOutputDirectory { get; set; } = "";
    }

    class PerPluginSettings
    {
        [SynthesisOrder]
        [SynthesisTooltip("Plugin")]
        public ModKey Plugin { get; set; } = new ModKey();

        [SynthesisOrder]
        [SynthesisTooltip("Click here to select which NPCs should have their appearance forwarded. If the NPC appears in multiple plugins, the load order conflict winner will be forwarded.")]
        public HashSet<IFormLinkGetter<INpcGetter>> NPCs { get; set; } = new HashSet<IFormLinkGetter<INpcGetter>>();

        [SynthesisOrder]
        [SynthesisTooltip("If checked, all NPCs in the chosen plugin EXCEPT the ones specified above will be forwarded.")]
        public bool RemoveTheseNPCs { get; set; }
    }
}
