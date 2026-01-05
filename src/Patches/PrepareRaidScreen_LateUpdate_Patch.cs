using HarmonyLib;
using MGSC;
using System;

namespace QM_RaidShowAlly.Patches
{
    /// <summary>
    /// Handles the "reverse the mission side" when the PrepareRaidScreen has been opened.
    /// The execution is based on the ExecuteMissionSideReversal flag.
    /// </summary>
    [HarmonyPatch(typeof(PrepareRaidScreen), nameof(PrepareRaidScreen.LateUpdate))]
    public static class PrepareRaidScreen_LateUpdate_Patch
    {

        

        /// <summary>
        /// Reverse the mission if the ReverseMission flag is set.
        /// Late update is used since The Configure method is used for the screen init as well as when the user clicks
        /// a side change button.
        /// </summary>
        /// <param name="__instance"></param>
        public static void Postfix(PrepareRaidScreen __instance)
        {
            try
            {
                if (!RememberMissionSide.ExecuteMissionSideReversal) return;

                RememberMissionSide.ExecuteMissionSideReversal = false;


                //Get the reverse side of the mission.  The attack and defense missions are different missions.
                Mission reversedMission = __instance._missions.Get(__instance._mission.StationId, true);

                //Verify mission hasn't changed.
                if (!RememberMissionSide.CurrentMission.TryGetTarget(out Mission mission) || mission != reversedMission) return;

                //"Defense" actually means reverse.
                if (!RememberMissionSide.ReverseMission) return;

                __instance.SwapSideToDefenseButtonOnClick(default, default);

            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
                
            }
        }
    }
}
