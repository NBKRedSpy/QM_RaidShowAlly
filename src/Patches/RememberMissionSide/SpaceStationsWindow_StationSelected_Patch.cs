using HarmonyLib;
using MGSC;

namespace QM_RaidShowAlly.Patches.RememberMissionSide
{
    /// <summary>
    /// Handles the "reverse the mission side" when the PrepareRaidScreen has been opened.
    /// The execution is based on the ExecuteMissionSideReversal flag.
    /// </summary>
    [HarmonyPatch(typeof(SpaceStationsWindow), nameof(SpaceStationsWindow.StationSelected))]
    public static class SpaceStationsWindow_StationSelected_Patch
    {
        public static void Prefix()
        {

            //I haven't found an easy way to detect if the PrepareMissionScreen is being opened for the first time or if it is being returned to from a child dialog.
            //  This particular screen's Configure, OnEnable, etc. are all called in those cases.

            //This is required for compatibility with Squad_Leader since any time Configure() is called, it resets its internal state.

            RememberMissionSide.ExecuteMissionSideCheck = true;
        }
    }
}

