using HarmonyLib;
using MGSC;

namespace QM_RaidShowAlly.Patches.RememberMissionSide
{
    [HarmonyPatch(typeof(PrepareRaidScreen), nameof(PrepareRaidScreen.OnEnable))]
    public static class PrepareRaidScreen_OnEnable_Patch
    {
        public static void Postfix(PrepareRaidScreen __instance)
        {
            //The dialog has been opened, set the mission side as needed.
            RememberMissionSide.ExecuteMissionSideCheck = true;

        }
    }
}
