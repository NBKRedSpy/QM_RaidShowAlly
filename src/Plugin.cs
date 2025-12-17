using HarmonyLib;
using MGSC;
using QM_RaidShowAlly_Bootstrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_RaidShowAlly
{
    public class Plugin : BootstrapMod
    {
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public static string ConfigPath => Path.Combine(Application.persistentDataPath, ModAssemblyName, "config.json");
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, ModAssemblyName);

        public Plugin(HookEvents hookEvents, bool isBeta) : base(hookEvents, isBeta)
        {
            HookEvents.AfterConfigsLoaded += AfterConfig;
        }

        public void AfterConfig(IModContext context)
        {
            Harmony harmony = new Harmony("NBKRedSpy_" + ModAssemblyName);

            HarmonyMethod patchMethod = new HarmonyMethod(AccessTools.Method(typeof(Plugin), nameof(Plugin.SetStartButtonText)));

            harmony.Patch(AccessTools.Method(typeof(PrepareRaidScreen), nameof(PrepareRaidScreen.Configure),
                new Type[] { typeof(Mission), typeof(bool) }),  
                postfix: patchMethod);

            harmony.Patch(AccessTools.Method(typeof(PrepareRaidScreen), nameof(PrepareRaidScreen.Configure),
                new Type[] { typeof(Station) }),
                postfix: patchMethod);
        }

        public static void SetStartButtonText(PrepareRaidScreen __instance)
        {
            if (__instance?._mission?.IsStoryMission ?? true) return;

            string ally = Localization.Get("faction." + __instance._mission.BeneficiaryFactionId + ".name");
            __instance._startOperationButton.ChangeLabel($"Start ({ally})");
        }


    }
}
