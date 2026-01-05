using HarmonyLib;
using MGSC;
using System.CodeDom;
using System.Collections;

namespace QM_RaidShowAlly.Patches.RememberMissionSide
{

    /// <summary>
    /// Handles setting the mission side when the user clicks the side change buttons.
    /// NOTE: This does not change the UI since the UI uses Configure for both the switch and the initial open.
    /// </summary>
    [HarmonyPatch]
    public static class PrepareRaidScreen_SideChanged_MultiPatch
    {


        [HarmonyPostfix]
        [HarmonyPatch(typeof(PrepareRaidScreen), nameof(PrepareRaidScreen.SwapSideToDefenseButtonOnClick))]
        public static void SwapSideToDefenseButtonOnClickPostfix(PrepareRaidScreen __instance)
        {
            //"Defense" actually means reverse.
            RememberMissionSide.SetAsReverseMission(__instance._mission);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PrepareRaidScreen), nameof(PrepareRaidScreen.SwapSideToAttackButtonOnClick))]
        public static void SwapSideToAttackButtonOnClickPostfix(PrepareRaidScreen __instance)
        {
            //"Attack" actually means don't reverse.
            RememberMissionSide.SetAsNormalMission(__instance._missions, __instance._station.Id);
        }
    }
}
