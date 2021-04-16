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
        [SynthesisTooltip("Click here to select which NPCs should have their appearance transferred.")]
        //public HashSet<ModKey> PluginsToForward { get; set; } = new HashSet<ModKey>();

        public HashSet<(ModKey, bool)> PluginsToForward2 { get; set; } = new HashSet<(ModKey, bool)>();
    }

    class PerPluginSettings
    {
        [SynthesisOrder]
        [SynthesisTooltip("Click here to select which NPCs should have their appearance forwarded. If the NPC appears in multiple plugins, the conflict winner will be forwarded.")]
        public HashSet<IFormLinkGetter<INpcGetter>> NPCs { get; set; } = new HashSet<IFormLinkGetter<INpcGetter>>();

        public bool RemoveThisNPCs { get; set; }
    }
}
