using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.Threading.Tasks;
using NPCAppearancePluginFilterer.Settings;

namespace NPCAppearancePluginFilterer
{
    public class Program
    {
        static Lazy<NAPFsettings> Settings = null!;
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "YourPatcher.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            NAPFsettings settings = Settings.Value;

            HashSet<IFormLinkGetter<INpcGetter>> FinishedNPCs = new HashSet<IFormLinkGetter<INpcGetter>>();

            //foreach (var npc in settings.PluginsToForward)
            //{
            //    mk.
            //}
        }
    }
}
